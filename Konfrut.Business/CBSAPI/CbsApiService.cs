using KONE.Business.CBSAPI.Configuration;
using KONE.Business.CBSAPI.Models;
using Newtonsoft.Json;

namespace KONE.Business.CBSAPI
{
    public class CbsApiService : ICbsApiService
    {
        #region Fields
        #endregion

        #region Ctor
        public CbsApiService()
        {

        }
        #endregion

        #region Methods

        public async Task<DistrictReturnModel.Root> GetDistrictList(int provinceId)
        {

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = EndPoints.BaseUrl;
                string endPoint = EndPoints.DistrictList;
                string addData = endPoint + "/" + provinceId;
                string url = baseUrl + addData;

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var serializedObject = JsonConvert.DeserializeObject<DistrictReturnModel.Root>(responseBody);

                    if (serializedObject != null)
                        return serializedObject;

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

        #endregion
    }
}
