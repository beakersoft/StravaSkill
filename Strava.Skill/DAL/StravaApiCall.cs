using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Strava.Skill.DAL
{
    public class StravaApiCall
    {
        public T GetStravaData<T>(string url)
        {
            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var serviceData = response.Content.ReadAsStringAsync().Result;
                    T serviceObject = JsonConvert.DeserializeObject<T>(serviceData);
                    return serviceObject;
                }
                throw new Exception("Exception getting strava data, please try again later");
            }
            catch (WebException ex)
            {
                using (WebResponse errorResponse = ex.Response)
                {
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                        {
                            string errorText = reader.ReadToEnd();
                        }
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
