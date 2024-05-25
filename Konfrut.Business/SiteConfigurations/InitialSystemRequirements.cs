using KONE.Business.CBSAPI;
using KONE.Business.CBSAPI.Models;
using KONE.Entity.Concrete;
using Konfrut.Business.Services.Abstract;
using Konfrut.Business.Services.Concrete;
using Konfrut.Business.Utilities;
using Konfrut.DataAccess.Konfrut.Abstract;
using Konfrut.DataAccess.Konfrut.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static KONE.Business.CBSAPI.Models.ProvinceReturnModel;

namespace Konfrut.Business.SiteConfigurations
{
    public static class InitialSystemRequirements
    {
        public static IServiceCollection CookieSettings(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication().AddCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Error/403";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(3);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = false,
                    Name = ".Konfrut.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                };
            });

            return serviceCollection;
        }
        public static IServiceCollection LoadContexts(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<KonfrutContext>(options =>
            {
                options.UseSqlServer(ConfigurationHelper.config.GetSection("ConnectionStrings:ApplicationMsSql").Value);
            });

            return serviceCollection;
        }
        public static IServiceCollection Identity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<KonfrutContext>(options =>
            {
                options.UseSqlServer(ConfigurationHelper.config.GetSection("ConnectionStrings:ApplicationMsSql").Value);
            });

            return serviceCollection;
        }
        public static IServiceCollection IdentityOptions(this IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<IdentityOptions>(options =>
            {
                //Password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;

                //Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                //EmailConfirmed
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
            });

            return serviceCollection;
        }
        public static IServiceCollection LoadServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<ICbsApiService, CbsApiService>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            return serviceCollection;
        }
        public static async Task InitializeAsync(IUnitOfWork unitOfWork)
        {
            var provinces = await unitOfWork.Province.GetAllAsync();
            // Örnek veri ekleme
            if (!(provinces.Count > 0))
            {
                string projectRoot = Directory.GetCurrentDirectory();
                string wwwrootpath = Path.Combine(projectRoot, "wwwroot");
                string provincejson = "province.json";
                // SeedData klasöründeki province.json dosyasını oku
                var jsonFilePath = Path.Combine(wwwrootpath, "SeedData", provincejson);


                var jsonData = await File.ReadAllTextAsync(jsonFilePath, Encoding.UTF8);

                var provinceData = JsonConvert.DeserializeObject<ProvinceReturnModel.Root>(jsonData);

                // Okunan verileri veritabanına ekle
                foreach (var item in provinceData.features)
                {
                    var province = await unitOfWork.Province.AddAsync(new Province()
                    {
                        CreatedByName = "SYSTEM",
                        ModifiedByName = "SYSTEM",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Name = item.properties.text,
                        PropertyId = item.properties.id,
                        Note = "HGM Api bilgisi"
                    });
                    await unitOfWork.SaveAsync();


                    if (item.geometry != null)
                    {
                        if (item.geometry.type == "Polygon")
                        {
                            foreach (var provinceCoordinates in item.geometry.coordinates)
                            {

                                foreach (var coordi in provinceCoordinates)
                                {
                                    var newCoordinate = await unitOfWork.Coordinates.AddAsync(new Coordinates()
                                    {
                                        EntityName = "Province",
                                        EntityId = province.Id.ToString(),
                                        CreatedByName = "SYSTEM",
                                        ModifiedByName = "SYSTEM",
                                        CreatedDate = DateTime.Now,
                                        ModifiedDate = DateTime.Now,
                                        IsDeleted = false,
                                        IsActive = true,
                                        Latitude = coordi[0].ToString(), // Koordinatın enlem değeri
                                        Longitude = coordi[1].ToString(), // Koordinatın boylam değeri
                                        Note = "HGM İl Kordinat Bilgisi",
                                        Part = 1
                                    });
                                    await unitOfWork.SaveAsync();
                                }


                            }
                        }
                        else if (item.geometry.type == "MultiPolygon")
                        {
                            var coordinateDeserialize = JsonConvert.SerializeObject(item.geometry);

                            var coordinateSerialize = JsonConvert.DeserializeObject<MultiPolygonModel>(coordinateDeserialize.ToString());

                            int part = 1;

                            foreach (var provinceCoordinates in coordinateSerialize.Coordinates)
                            {
                                foreach (var coordinates in provinceCoordinates)
                                {
                                    foreach (var coordinate in provinceCoordinates)
                                    {
                                        foreach (var coor in coordinate)
                                        {
                                            var newCoordinate = await unitOfWork.Coordinates.AddAsync(new Coordinates()
                                            {
                                                EntityName = "Province",
                                                EntityId = province.Id.ToString(),
                                                CreatedByName = "SYSTEM",
                                                ModifiedByName = "SYSTEM",
                                                CreatedDate = DateTime.Now,
                                                ModifiedDate = DateTime.Now,
                                                IsMultiPolygon = true,
                                                Part = part,
                                                IsDeleted = false,
                                                IsActive = true,
                                                Latitude = coor[0].ToString(), // Koordinatın enlem değeri
                                                Longitude = coor[1].ToString(), // Koordinatın boylam değeri
                                                Note = "HGM İl Kordinat Bilgisi"
                                            });
                                            await unitOfWork.SaveAsync();
                                        }
                                        part++;
                                    }
                                }

                            }
                        }
                    }

                }
            }
        }
    }
}
