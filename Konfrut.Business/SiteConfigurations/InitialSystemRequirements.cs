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
        //public static async Task InitializeAsync(IUnitOfWork unitOfWork)
        //{
        //    var provinces = await unitOfWork.Province.GetAllAsync();
        //    // Örnek veri ekleme
        //    if (!(provinces.Count > 0))
        //    {
        //        string projectRoot = Directory.GetCurrentDirectory();
        //        string wwwrootpath = Path.Combine(projectRoot, "wwwroot");
        //        string provincejson = "province.json";
        //        // SeedData klasöründeki province.json dosyasını oku
        //        var jsonFilePath = Path.Combine(wwwrootpath, "SeedData", provincejson);

        //        // UTF-8 encoding ile dosyayı okuyun
        //        string jsonData;
        //        using (var reader = new StreamReader(jsonFilePath, Encoding.UTF8))
        //        {
        //            jsonData = await reader.ReadToEndAsync();
        //        }

        //        var provinceData = JObject.Parse(jsonData);  // JObject.Parse kullanarak ham veri olarak oku


        //        // Okunan verileri veritabanına ekle
        //        foreach (var item in provinceData["features"])
        //        {
        //            var province = await unitOfWork.Province.AddAsync(new Province()
        //            {
        //                CreatedByName = "SYSTEM",
        //                ModifiedByName = "SYSTEM",
        //                CreatedDate = DateTime.Now,
        //                ModifiedDate = DateTime.Now,
        //                IsDeleted = false,
        //                IsActive = true,
        //                Name = item["properties"]["text"].ToString(),
        //                PropertyId = Convert.ToInt32(item["properties"]["id"].ToString()),
        //                Note = "HGM Api bilgisi"
        //            });
        //            await unitOfWork.SaveAsync();

        //            var geometry = item["geometry"];
        //            if (geometry != null)
        //            {
        //                foreach (var provinceCoordinates in geometry["coordinates"])
        //                {
        //                    foreach (var coordinate in provinceCoordinates)
        //                    {
        //                        foreach (var value in coordinate)
        //                        {
        //                            if (value is JArray array && array.Count == 2 &&
        //                                double.TryParse(array[0].ToString(), out double latitude) &&
        //                                double.TryParse(array[1].ToString(), out double longitude))
        //                            {
        //                                var newCoordinate = await unitOfWork.Coordinates.AddAsync(new Coordinates()
        //                                {
        //                                    EntityName = "Province",
        //                                    EntityId = province.Id.ToString(),
        //                                    CreatedByName = "SYSTEM",
        //                                    ModifiedByName = "SYSTEM",
        //                                    CreatedDate = DateTime.Now,
        //                                    ModifiedDate = DateTime.Now,
        //                                    IsDeleted = false,
        //                                    IsActive = true,
        //                                    Latitude = latitude,
        //                                    Longitude = longitude,
        //                                    Note = "HGM İl Kordinat Bilgisi"
        //                                });
        //                                await unitOfWork.SaveAsync();
        //                            }
        //                            else if (value is not JArray)
        //                            {
        //                                // Log.Error($"Invalid coordinate data: {JsonConvert.SerializeObject(value)}");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

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
                        foreach (var provinceCoordinates in item.geometry.coordinates)
                        {
                            if (provinceCoordinates.Count == 1) // Eğer sadece bir koordinat varsa
                            {
                                var coordinate = provinceCoordinates[0]; // Koordinatı al
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
                                    Latitude = coordinate[1].ToString(), // Koordinatın enlem değeri
                                    Longitude = coordinate[0].ToString(), // Koordinatın boylam değeri
                                    Note = "HGM İl Kordinat Bilgisi"
                                });
                                await unitOfWork.SaveAsync();
                            }
                            else // Eğer birden fazla koordinat varsa
                            {
                                foreach (var coordinate in provinceCoordinates)
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
                                        Latitude = coordinate[1].ToString(), // Koordinatın enlem değeri
                                        Longitude = coordinate[0].ToString(), // Koordinatın boylam değeri
                                        Note = "HGM İl Kordinat Bilgisi"
                                    });
                                    await unitOfWork.SaveAsync();
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
