using KONE.Business.CBSAPI;
using KONE.Business.CBSAPI.Models;
using KONE.Business.Services.Abstract;
using KONE.Business.Services.Concrete;
using KONE.Business.Utilities;
using KONE.DataAccess.KONE.Abstract;
using KONE.DataAccess.KONE.Concrete;
using KONE.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net;
using System.Text;
using static KONE.Business.CBSAPI.Models.ProvinceReturnModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace KONE.Business.SiteConfigurations
{
    public static class InitialSystemRequirements
    {
        public static IServiceCollection CookieSettings(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication().AddCookie(options =>
            {
                options.LoginPath = "/IdEntities/Account/Login";
                options.LogoutPath = "/IdEntities/Account/Logout";
                options.AccessDeniedPath = "/Error/403";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(3);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = false,
                    Name = ".KONE.Security.Cookie",
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
            serviceCollection.AddDbContext<KONEContext>(options =>
            {
                options.UseSqlServer(ConfigurationHelper.config.GetSection("ConnectionStrings:ApplicationMsSql").Value);
            });

            return serviceCollection;
        }
        public static IServiceCollection IdEntities(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<KONEContext>(options =>
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
                                        EntitiesName = "Province",
                                        EntitiesId = province.Id.ToString(),
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
                                                EntitiesName = "Province",
                                                EntitiesId = province.Id.ToString(),
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

        public static async Task SeedCountries(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            string projectRoot = Directory.GetCurrentDirectory();
            string wwwrootpath = Path.Combine(projectRoot, "wwwroot");
            try
            {
                var countries = await unitOfWork.Countries.GetAllAsync();

                if (!(countries.Count > 0))
                {
                    // SeedData klasörünün altındaki Excel dosyasının adını belirtin
                    string excelFileName = "CountryList.xlsx";

                    // Excel dosyasının fiziksel yolu
                    string excelFilePath = Path.Combine(wwwrootpath, "SeedData", excelFileName);

                    Console.WriteLine($"Excel dosyasının yolu: {excelFilePath}");

                    // Excel dosyasını oku
                    List<Country> countryList = ReadLanguageCodesExcelFile(excelFilePath);

                    // NACE kodlarını veritabanına ekleyin
                    foreach (var code in countryList)
                    {
                        await unitOfWork.Countries.AddAsync(code);
                        await unitOfWork.SaveAsync();
                    }

                    Console.WriteLine("Form Dilleri başarıyla eklenmiştir.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }

        }

        private static List<Country> ReadLanguageCodesExcelFile(string filePath)
        {
            List<Country> countries = new List<Country>();

            FileInfo existingFile = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // İlk sayfa

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Başlıkları atladık, 2. satırdan başla
                {
                    try
                    {
                        Country country = new Country
                        {
                            Name = worksheet.Cells[row, 1].Value?.ToString(),
                            SpecialCode = worksheet.Cells[row, 2].Value?.ToString(),
                            CreatedDate = DateTime.Now,
                            IsDeleted = false,
                            CreatedByName = "",
                            IsActive = true,
                            ModifiedByName = "",
                            Note = "",
                            ModifiedDate = DateTime.Now
                        };

                        countries.Add(country);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            return countries;
        }
    }
}
