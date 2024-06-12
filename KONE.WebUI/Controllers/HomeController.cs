using KONE.Business.CBSAPI;
using KONE.Business.Services.Abstract;
using KONE.DataAccess.KONE.Abstract;
using KONE.Shared.Utilities.Results.ComplexTypes;
using KONE.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static KONE.Business.CBSAPI.Models.ProvinceReturnModel;

namespace KONE.KOne.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICbsApiService _bsApiService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICbsApiService bsApiService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _productService = productService;
            _bsApiService = bsApiService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetPagedListAsync(1, 25);
            return View(products);
        }
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetPagedListAsync(1, int.MaxValue);
            var countOfProducts = await _productService.GetCountAsync();
            var productList = products.ToList();
            return Json(new { data = productList, draw = 1, recordsTotal = countOfProducts, recordsFiltered = products.Count });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task<IActionResult> Defines()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ProvinceList(int provinceId)
        {
            var provinces = await _bsApiService.GetProvinceList();

            var province = provinces.features.FirstOrDefault(c => c.properties.id == provinceId);

            return Json(province);
        }

        [HttpGet]
        public async Task<IActionResult> VillageList(int districtId)
        {
            var provinces = await _bsApiService.GetVillageList(districtId);

            return Json(provinces);
        }

        [HttpGet]
        public async Task<IActionResult> DistrictList(int provinceId)
        {
            var provinces = await _bsApiService.GetDistrictList(provinceId);

            return Json(provinces);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrictCoordinates(int districtId)
        {
            var district = await _unitOfWork.District.GetAsync(c => c.PropertyId == districtId);
            var returnModel = new Feature();

            if (district != null)
            {
                var provinceCoordinates = await _unitOfWork.Coordinates.GetAllAsync(c => c.EntitiesName == "District" && c.EntitiesId == district.Id.ToString());

                if (provinceCoordinates != null && provinceCoordinates.Any())
                {
                    if (provinceCoordinates.Any(c => c.IsMultiPolygon))
                    {
                        var example = provinceCoordinates.FirstOrDefault();
                        List<List<List<List<double>>>> coordinatesList = new List<List<List<List<double>>>>();

                        var partCounts = provinceCoordinates.Select(c => c.Part).Distinct();
                        foreach (var part in partCounts)
                        {
                            var partCoordinates = provinceCoordinates.Where(c => c.Part == part).ToList();

                            List<List<List<double>>> partCoordinateList = new List<List<List<double>>>();

                            List<List<double>> singleCoordinateList = new List<List<double>>();
                            foreach (var coordinate in partCoordinates)
                            {
                                List<double> singleCoordinate = new List<double>
                        {
                            Convert.ToDouble(coordinate.Latitude),
                            Convert.ToDouble(coordinate.Longitude)
                        };
                                singleCoordinateList.Add(singleCoordinate);
                            }

                            // Ýlk koordinatý sona ekleyerek alaný kapat
                            singleCoordinateList.Add(singleCoordinateList.First());

                            partCoordinateList.Add(singleCoordinateList);
                            coordinatesList.Add(partCoordinateList);
                        }
                        return Json(new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "MultiPolygon",
                                coordinates = coordinatesList
                            }
                        });
                    }
                    else
                    {
                        var example = provinceCoordinates.FirstOrDefault();
                        List<List<List<double>>> coordinates = new List<List<List<double>>>();
                        List<List<double>> partCoordinateList = new List<List<double>>();

                        foreach (var coordinate in provinceCoordinates)
                        {
                            List<double> singleCoordinate = new List<double>
                            {
                                Convert.ToDouble(coordinate.Latitude),
                                 Convert.ToDouble(coordinate.Longitude)
                            };


                            partCoordinateList.Add(singleCoordinate);
                        }
                        partCoordinateList.Add(new List<double>
                        {
                            Convert.ToDouble(provinceCoordinates.First().Latitude),
                                 Convert.ToDouble(provinceCoordinates.First().Longitude)
                        });

                        coordinates.Add(partCoordinateList);
                        return Json(new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "Polygon",
                                coordinates = coordinates
                            }
                        });
                    }
                }
            }

            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetVillageCoordinates(int villageId)
        {
            var village = await _unitOfWork.Village.GetAsync(c => c.PropertyId == villageId);
            var returnModel = new Feature();

            if (village != null)
            {
                var villageCoordinates = await _unitOfWork.Coordinates.GetAllAsync(c => c.EntitiesName == "Village" && c.EntitiesId == village.Id.ToString());

                if (villageCoordinates != null && villageCoordinates.Any())
                {
                    if (villageCoordinates.Any(c => c.IsMultiPolygon))
                    {
                        var example = villageCoordinates.FirstOrDefault();
                        List<List<List<List<double>>>> coordinatesList = new List<List<List<List<double>>>>();

                        var partCounts = villageCoordinates.Select(c => c.Part).Distinct();
                        foreach (var part in partCounts)
                        {
                            var partCoordinates = villageCoordinates.Where(c => c.Part == part).ToList();

                            List<List<List<double>>> partCoordinateList = new List<List<List<double>>>();

                            List<List<double>> singleCoordinateList = new List<List<double>>();
                            foreach (var coordinate in partCoordinates)
                            {
                                List<double> singleCoordinate = new List<double>
                        {
                            Convert.ToDouble(coordinate.Latitude),
                            Convert.ToDouble(coordinate.Longitude)
                        };
                                singleCoordinateList.Add(singleCoordinate);
                            }

                            // Ýlk koordinatý sona ekleyerek alaný kapat
                            singleCoordinateList.Add(singleCoordinateList.First());

                            partCoordinateList.Add(singleCoordinateList);
                            coordinatesList.Add(partCoordinateList);
                        }
                        return Json(new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "MultiPolygon",
                                coordinates = coordinatesList
                            }
                        });
                    }
                    else
                    {
                        var example = villageCoordinates.FirstOrDefault();
                        List<List<List<double>>> coordinates = new List<List<List<double>>>();
                        List<List<double>> partCoordinateList = new List<List<double>>();

                        foreach (var coordinate in villageCoordinates)
                        {
                            List<double> singleCoordinate = new List<double>
                            {
                                Convert.ToDouble(coordinate.Latitude),
                                 Convert.ToDouble(coordinate.Longitude)
                            };


                            partCoordinateList.Add(singleCoordinate);
                        }
                        partCoordinateList.Add(new List<double>
                        {
                            Convert.ToDouble(villageCoordinates.First().Latitude),
                                 Convert.ToDouble(villageCoordinates.First().Longitude)
                        });

                        coordinates.Add(partCoordinateList);
                        return Json(new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "Polygon",
                                coordinates = coordinates
                            }
                        });
                    }
                }
            }

            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetProvinceCoordinates(int provinceId)
        {
            var province = await _unitOfWork.Province.GetAsync(c => c.PropertyId == provinceId);
            var returnModel = new Feature();

            if (province != null)
            {
                var provinceCoordinates = await _unitOfWork.Coordinates.GetAllAsync(c => c.EntitiesName == "Province" && c.EntitiesId == province.Id.ToString());

                if (provinceCoordinates != null && provinceCoordinates.Any())
                {
                    if (provinceCoordinates.Any(c => c.IsMultiPolygon))
                    {
                        var example = provinceCoordinates.FirstOrDefault();
                        List<List<List<List<double>>>> coordinatesList = new List<List<List<List<double>>>>();

                        var partCounts = provinceCoordinates.Select(c => c.Part).Distinct();
                        foreach (var part in partCounts)
                        {
                            var partCoordinates = provinceCoordinates.Where(c => c.Part == part).ToList();

                            List<List<List<double>>> partCoordinateList = new List<List<List<double>>>();

                            List<List<double>> singleCoordinateList = new List<List<double>>();
                            foreach (var coordinate in partCoordinates)
                            {
                                List<double> singleCoordinate = new List<double>
                        {
                            Convert.ToDouble(coordinate.Latitude),
                            Convert.ToDouble(coordinate.Longitude)
                        };
                                singleCoordinateList.Add(singleCoordinate);
                            }

                            // Ýlk koordinatý sona ekleyerek alaný kapat
                            singleCoordinateList.Add(singleCoordinateList.First());

                            partCoordinateList.Add(singleCoordinateList);
                            coordinatesList.Add(partCoordinateList);
                        }
                        return Json(new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "MultiPolygon",
                                coordinates = coordinatesList
                            }
                        });
                    }
                    else
                    {
                        var example = provinceCoordinates.FirstOrDefault();
                        List<List<List<double>>> coordinates = new List<List<List<double>>>();
                        List<List<double>> partCoordinateList = new List<List<double>>();

                        foreach (var coordinate in provinceCoordinates)
                        {
                            List<double> singleCoordinate = new List<double>
                            {
                                Convert.ToDouble(coordinate.Latitude),
                                 Convert.ToDouble(coordinate.Longitude)
                            };


                            partCoordinateList.Add(singleCoordinate);
                        }
                        partCoordinateList.Add(new List<double>
                        {
                            Convert.ToDouble(provinceCoordinates.First().Latitude),
                                 Convert.ToDouble(provinceCoordinates.First().Longitude)
                        });

                        coordinates.Add(partCoordinateList);
                        return Json(new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "Polygon",
                                coordinates = coordinates
                            }
                        });
                    }
                }
            }

            return Json(null);
        }


        [HttpGet]
        public async Task<IActionResult> GetParcelCoordinates(int villageId, int land = 0, int parcel = 0)
        {
            try
            {
                if (villageId == 0 || parcel == 0)
                {
                    return Json(new
                    {
                        resultStatus = ResultStatus.Error,
                        messages = new List<string> { "Lütfen Mahalle/Köy ve Parsel alanlarýný doldurunuz." },
                        data = string.Empty
                    });
                }

                var parcelCoordinates = await _bsApiService.GetParcelList(villageId, land, parcel);

                if (parcelCoordinates != null)
                {
                    List<List<List<double>>> coordinates = new List<List<List<double>>>();
                    List<List<double>> partCoordinateList = new List<List<double>>();

                    if (parcelCoordinates.geometry != null)
                    {
                        foreach (var coordinate in parcelCoordinates.geometry.coordinates)
                        {
                            foreach (var item in coordinate)
                            {
                                List<double> singleCoordinate = new List<double>
                            {
                                Convert.ToDouble(item[0]),
                                 Convert.ToDouble(item[1])
                            };
                                partCoordinateList.Add(singleCoordinate);
                            }


                        }
                        partCoordinateList.Add(new List<double>
                        {
                            Convert.ToDouble(parcelCoordinates.geometry.coordinates.First()[0][0]),
                                 Convert.ToDouble(parcelCoordinates.geometry.coordinates.First()[0][1])
                        });

                        coordinates.Add(partCoordinateList);

                        return Json(new
                        {
                            resultStatus = ResultStatus.Success,
                            messages = new List<string> { "Lütfen Mahalle/Köy ve Parsel alanlarýný doldurunuz." },
                            data = new
                            {
                                type = "Feature",
                                geometry = new
                                {
                                    type = "Polygon",
                                    coordinates = coordinates
                                },
                                properties = parcelCoordinates.properties
                            }

                        });
                    }

                    return Json(new
                    {
                        resultStatus = ResultStatus.Error,
                        messages = new List<string> { "Belirtilen Ada/Parsel bulunamadý." },
                        data = string.Empty
                    });
                }
                return Json(new
                {
                    resultStatus = ResultStatus.Error,
                    messages = new List<string> { "Belirtilen Ada/Parsel bulunamadý." },
                    data = string.Empty
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    resultStatus = ResultStatus.Error,
                    messages = new List<string> { "Sorgu sýrasýnda hata ile karþýlaþýldý. Lütfen geliþtiricinize bildiriniz." },
                    data = string.Empty
                });
            }

        }



    }
}