using NabzeArz.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NabzeArz.Service
{
    public class TgjuAPIService : AbstractAPI
    {
        public static async Task<USDAPIModel> GetUSDPrice()
        {
            string result = await FileService.OpenJsonFile(AppSetting.tgju_file); //get string form json file
            if (!string.IsNullOrEmpty(result))
            {
                var USDprice = JsonConvert.DeserializeObject<USDAPIModel>(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                return USDprice;
            }
            return null;
        }

        public async static Task<USDAPIModel> RequestAndSaveToFile()
        {
            var errorList = new List<Exception>(); //error list add exception error in each attemp
            int maxTimeOut = 3; //attemp max number

            do
            {
                --maxTimeOut;
                try
                {
                   return await FileService.RequestTgjuAndSaveTojson(AppSetting.tgju_api + GetTimeStamp(), AppSetting.tgju_file); 
                }
                catch (Exception e)
                {
                    errorList.Add(e);
                }

            } while (maxTimeOut > 0);

            return null; 
        }
        //public async static Task<SanaModel> GetCurrencies()
        //{
        //    var response = await GetResponse("http://www.tgju.org/?act=sanarateservice&client=tgju&noview&type=json");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<SanaModel>(responseString);
        //        return result;
        //    }
        //    return null;
        //}
        //public async static Task<bool> RequestSingle()
        //{
        //    HttpResponseMessage response = await GetResponse(url: AppSetting.tgju_api + GetTimeStamp());

        //    if (response.IsSuccessStatusCode) //is successfull 
        //    {
        //        string result = await response.Content.ReadAsStringAsync(); //read to end of file                       
        //        return FileService.SaveJsonFile(JsonConvert.DeserializeObject(result), AppSetting.tgju_file); //save to json file        
        //    }
        //    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        //    {
        //        throw new Exception("there is problem with remote server that has a internal server error");
        //    }
        //    else
        //    {
        //        throw new Exception("there problem with connect to remote server");
        //    }
        //}
    }
}