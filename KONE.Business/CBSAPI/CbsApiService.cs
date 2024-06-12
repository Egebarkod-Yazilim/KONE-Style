using KONE.Business.CBSAPI.Configuration;
using KONE.Business.CBSAPI.Models;
using KONE.DataAccess.KONE.Abstract;
using KONE.Entities.Concrete;
using Newtonsoft.Json;
using static KONE.Business.CBSAPI.Models.ProvinceReturnModel;

namespace KONE.Business.CBSAPI
{
    public class CbsApiService : ICbsApiService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CbsApiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public async Task<DistrictReturnModel.Root> GetDistrictList(int provinceId)
        {
            using (HttpClient client = new HttpClient())
            {
                // Create Url
                string baseUrl = EndPoints.BaseUrl;
                string endPoint = EndPoints.DistrictList;
                string addData = endPoint + "/" + provinceId;
                string url = baseUrl + addData;

                try
                {
                    // Get province and district from db
                    var province = await _unitOfWork.Province.GetAsync(c => c.PropertyId == provinceId);
                    var districts = await _unitOfWork.District.GetAllAsync(c => c.ProvinceId == province.Id);

                    var returnModel = new DistrictReturnModel.Root();
                    //if exists fetch from db
                    if (districts.Count > 0)
                    {
                        returnModel.type = "Features";

                        foreach (var district in districts)
                        {
                            returnModel.features.Add(new DistrictReturnModel.Feature
                            {
                                properties = new DistrictReturnModel.Properties()
                                {
                                    id = district.PropertyId,
                                    name = district.Name,
                                    text = district.Name
                                },
                                type = "Feature",
                                geometry = new DistrictReturnModel.Geometry()
                            });
                            var districtCoordinates = await _unitOfWork.Coordinates.GetAllAsync(c => c.EntitiesName == "District" && c.EntitiesId == district.Id.ToString());
                            if (districtCoordinates.Any(c => c.IsMultiPolygon))
                            {
                                var example = districtCoordinates.FirstOrDefault();
                                List<List<List<List<double>>>> coordinatesList = new List<List<List<List<double>>>>();

                                var partCounts = districtCoordinates.Select(c => c.Part).Distinct();
                                foreach (var part in partCounts)
                                {
                                    var partCoordinates = districtCoordinates.Where(c => c.Part == part).ToList();

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

                                    // İlk koordinatı sona ekleyerek alanı kapat
                                    singleCoordinateList.Add(singleCoordinateList.First());

                                    partCoordinateList.Add(singleCoordinateList);
                                    coordinatesList.Add(partCoordinateList);
                                }

                                var jsonModel = new
                                {
                                    geometry = new
                                    {
                                        type = "MultiPolygon",
                                        coordinates = coordinatesList
                                    }
                                };


                                try
                                {

                                    var objectJson = JsonConvert.SerializeObject(jsonModel);
                                    var serializedObject = JsonConvert.DeserializeObject<DistrictReturnModel.Geometry2>(objectJson);

                                    var deserialize = JsonConvert.SerializeObject(serializedObject, Formatting.Indented);

                                    var objectSerialize = JsonConvert.DeserializeObject<DistrictReturnModel.Geometry>(deserialize);

                                    foreach (var item in returnModel.features)
                                    {
                                        item.geometry.coordinates = objectSerialize.coordinates;
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }

                            }
                            else
                            {
                                try
                                {
                                    List<List<List<double>>> coordinates = new List<List<List<double>>>();
                                    List<List<double>> partCoordinateList = new List<List<double>>();

                                    if (districtCoordinates.Count > 0)
                                    {
                                        foreach (var coordinate in districtCoordinates)
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
                            Convert.ToDouble(districtCoordinates.First().Latitude),
                                 Convert.ToDouble(districtCoordinates.First().Longitude)
                        });

                                        coordinates.Add(partCoordinateList);
                                        var jsonModel =
                                             new
                                             {
                                                 type = "Polygon",
                                                 coordinates = coordinates
                                             };

                                        var objectJson = JsonConvert.SerializeObject(jsonModel);
                                        var serializedObject = JsonConvert.DeserializeObject<DistrictReturnModel.Geometry2>(objectJson);
                                        var deserialize = JsonConvert.SerializeObject(serializedObject, Formatting.Indented);
                                        var objectSerialize = JsonConvert.DeserializeObject<DistrictReturnModel.Geometry>(deserialize);

                                        foreach (var item in returnModel.features)
                                        {
                                            item.geometry.coordinates = objectSerialize.coordinates;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    throw;
                                }



                            }
                        }

                        return returnModel;


                    }
                    // if not exists fetch from api and insert db
                    else
                    {
                        // Fetch api
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var serializedObject = JsonConvert.DeserializeObject<DistrictReturnModel.Root>(responseBody);

                        // insert to db
                        foreach (var district in serializedObject.features)
                        {
                            if (!districts.Any(c => c.Name == district.properties.text && c.PropertyId == district.properties.id))
                            {
                                var addDistrict = await _unitOfWork.District.AddAsync(new Entities.Concrete.District()
                                {
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now,
                                    IsDeleted = false,
                                    IsActive = true,
                                    CreatedByName = "SYSTEM",
                                    ModifiedByName = "SYSTEM",
                                    Name = district.properties.text,
                                    Note = "HGM İlçe Bilgisi",
                                    PropertyId = district.properties.id,
                                    ProvinceId = province.Id
                                });
                                await _unitOfWork.SaveAsync();


                                if (district.geometry != null)
                                {
                                    if (district.geometry.type == "Polygon")
                                    {
                                        foreach (var provinceCoordinates in district.geometry.coordinates)
                                        {

                                            foreach (var coordi in provinceCoordinates)
                                            {
                                                var newCoordinate = await _unitOfWork.Coordinates.AddAsync(new Coordinates()
                                                {
                                                    EntitiesName = "District",
                                                    EntitiesId = addDistrict.Id.ToString(),
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
                                                await _unitOfWork.SaveAsync();
                                            }


                                        }
                                    }
                                    else if (district.geometry.type == "MultiPolygon")
                                    {
                                        var coordinateDeserialize = JsonConvert.SerializeObject(district.geometry);

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
                                                        var newCoordinate = await _unitOfWork.Coordinates.AddAsync(new Coordinates()
                                                        {
                                                            EntitiesName = "District",
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
                                                        await _unitOfWork.SaveAsync();
                                                    }
                                                    part++;
                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                        if (serializedObject != null)
                            return serializedObject;
                    }


                    return new DistrictReturnModel.Root();

                }
                catch (HttpRequestException e)
                {
                    // Handle exception
                    Console.WriteLine($"Request error: {e.Message}");
                    return new DistrictReturnModel.Root();

                }
            }
        }
        public async Task<VillageReturnModel.Root> GetVillageList(int districtId)
        {

            using (HttpClient client = new HttpClient())
            {
                // Create Url
                string baseUrl = EndPoints.BaseUrl;
                string endPoint = EndPoints.NeighboorHoodList;
                string addData = endPoint + "/" + districtId;
                string url = baseUrl + addData;

                try
                {
                    // Get province and district from db
                    var district = await _unitOfWork.District.GetAsync(c => c.PropertyId == districtId);

                    if (district != null)
                    {
                        var villages = await _unitOfWork.Village.GetAllAsync(c => c.DistrictId == district.Id);


                        try
                        {
                            //if exists fetch from db
                            if (villages.Count > 0)
                            {
                                var returnModel = new VillageReturnModel.Root();
                                foreach (var village in villages)
                                {
                                    var villageCoordinates = await _unitOfWork.Coordinates.GetAllAsync(c => c.EntitiesName == "Village" && c.EntitiesId == village.Id.ToString());


                                    List<List<double>> singleCoordinate = new List<List<double>>();
                                    string coordinateType = "Polygon";
                                    foreach (var coordinate in villageCoordinates)
                                    {
                                        coordinateType = coordinate.IsMultiPolygon ? "MultiPolygon" : "Polygon";
                                        singleCoordinate.Add(new List<double>
                        {
                            Convert.ToDouble(coordinate.Latitude),
                            Convert.ToDouble(coordinate.Longitude)
                        });
                                    }

                                    if (singleCoordinate.Count > 0)
                                    {
                                        var doubleSingleCoordinate = JsonConvert.SerializeObject(singleCoordinate);
                                        var listModel = new List<List<object>>();
                                        var objectSingleCoordinate = JsonConvert.DeserializeObject<List<List<object>>>(doubleSingleCoordinate);
                                        returnModel.features.Add(new VillageReturnModel.Feature()
                                        {
                                            properties = new VillageReturnModel.Properties()
                                            {
                                                name = village.Name,
                                                id = village.PropertyId,
                                                text = village.Name
                                            },
                                            type = "Feature",
                                            geometry = new VillageReturnModel.Geometry()
                                            {
                                                type = coordinateType,
                                                coordinates = new List<List<List<object>>>() { objectSingleCoordinate }
                                            }
                                        });
                                    }
                                }

                                return returnModel;

                            }
                            // if not exists fetch from api and insert db
                            else
                            {
                                // Fetch api
                                HttpResponseMessage response = await client.GetAsync(url);
                                response.EnsureSuccessStatusCode();
                                string responseBody = await response.Content.ReadAsStringAsync();

                                var serializedObject = JsonConvert.DeserializeObject<VillageReturnModel.Root>(responseBody);

                                // insert to db
                                foreach (var village in serializedObject.features)
                                {
                                    if (!villages.Any(c => c.Name == village.properties.text && c.PropertyId == village.properties.id))
                                    {
                                        var addVillage = await _unitOfWork.Village.AddAsync(new Entities.Concrete.Village()
                                        {
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            IsDeleted = false,
                                            IsActive = true,
                                            CreatedByName = "SYSTEM",
                                            ModifiedByName = "SYSTEM",
                                            Name = village.properties.text,
                                            Note = "HGM İlçe Bilgisi",
                                            PropertyId = village.properties.id,
                                            DistrictId = district.Id
                                        });
                                        await _unitOfWork.SaveAsync();


                                        if (village.geometry != null)
                                        {
                                            if (village.geometry.type == "Polygon")
                                            {
                                                foreach (var villageCoordinates in village.geometry.coordinates)
                                                {

                                                    foreach (var coordi in villageCoordinates)
                                                    {
                                                        var newCoordinate = await _unitOfWork.Coordinates.AddAsync(new Coordinates()
                                                        {
                                                            EntitiesName = "Village",
                                                            EntitiesId = addVillage.Id.ToString(),
                                                            CreatedByName = "SYSTEM",
                                                            ModifiedByName = "SYSTEM",
                                                            CreatedDate = DateTime.Now,
                                                            ModifiedDate = DateTime.Now,
                                                            IsDeleted = false,
                                                            IsActive = true,
                                                            Latitude = coordi[0].ToString(), // Koordinatın enlem değeri
                                                            Longitude = coordi[1].ToString(), // Koordinatın boylam değeri
                                                            Note = "HGM Mahalle/Köy Kordinat Bilgisi",
                                                            Part = 1
                                                        });
                                                        await _unitOfWork.SaveAsync();
                                                    }


                                                }
                                            }
                                            else if (village.geometry.type == "MultiPolygon")
                                            {
                                                var coordinateDeserialize = JsonConvert.SerializeObject(village.geometry);

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
                                                                var newCoordinate = await _unitOfWork.Coordinates.AddAsync(new Coordinates()
                                                                {
                                                                    EntitiesName = "Village",
                                                                    EntitiesId = addVillage.Id.ToString(),
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
                                                                await _unitOfWork.SaveAsync();
                                                            }
                                                            part++;
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                    }
                                }

                                if (serializedObject != null)
                                    return serializedObject;
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }




                    return new VillageReturnModel.Root();

                }
                catch (HttpRequestException e)
                {
                    // Handle exception
                    Console.WriteLine($"Request error: {e.Message}");
                    return new VillageReturnModel.Root();

                }
            }
        }
        public async Task<ProvinceReturnModel.Root> GetProvinceList()
        {

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = EndPoints.BaseUrl;
                string endPoint = EndPoints.ProvinceList;
                string url = baseUrl + endPoint;

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var serializedObject = JsonConvert.DeserializeObject<ProvinceReturnModel.Root>(responseBody);

                    if (serializedObject != null)
                        return serializedObject;

                    return new ProvinceReturnModel.Root();

                }
                catch (HttpRequestException e)
                {
                    // Handle exception
                    Console.WriteLine($"Request error: {e.Message}");
                    return new ProvinceReturnModel.Root();

                }
            }
        }
        public async Task<ParcelReturnModel.Root> GetParcelList(int villageId, int land = 0, int parcel = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                // Create Url
                string baseUrl = EndPoints.BaseUrl2;
                string endPoint = EndPoints.ParcelList;
                string addData = endPoint + "/" + villageId + "/" + land + "/" + parcel;
                string url = baseUrl + addData;

                try
                {
                    // Fetch api
                    HttpResponseMessage response = await client.GetAsync(url);
                    //response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var serializedObject = JsonConvert.DeserializeObject<ParcelReturnModel.Root>(responseBody);

                    if (serializedObject != null)
                        return serializedObject;


                    return new ParcelReturnModel.Root();

                }
                catch (HttpRequestException e)
                {
                    // Handle exception
                    Console.WriteLine($"Request error: {e.Message}");
                    return new ParcelReturnModel.Root();

                }
            }
        }
        #endregion
    }
}
