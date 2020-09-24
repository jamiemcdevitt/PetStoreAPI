using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PetStoreAPITests.HelperClasses
{
    class ApiHelper
    {
        public static string TargetUrl = "https://petstore.swagger.io/v2";
        public static Uri CreatePetUri()
        {
            var urlPrefix = TargetUrl;
            var urlSuffix = "pet";
            var url = $"{urlPrefix}/{urlSuffix}";

            return new Uri(url);
        }
        public static Uri SearchPetUri(int petId)
        {
            var urlPrefix = TargetUrl;
            var urlSuffix = "pet";
            var url = $"{urlPrefix}/{urlSuffix}/{petId}";

            return new Uri(url);
        }
        public static HttpClient CreateHttpClient()
        {
            var result = new HttpClient();
            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            result.Timeout = TimeSpan.FromMinutes(5);

            return result;
        }
        public static HttpContent BuildHttpContent(object obj)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, obj);
                }
            }

            return new StringContent(stringBuilder.ToString(), Encoding.UTF8, "application/json");
        }

        public static async Task<int> GeneratePetId()
        {
            var flag = true;
            var petId = 0;

            do
            {
                Random random = new Random();
                petId = random.Next(0, 30000);
                flag = await CreateHelper.DoesPetExistAlready(petId);

            } while (flag);

            return petId;
        }
    }
}
