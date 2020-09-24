using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PetStoreAPITests.PropertyClasses;

namespace PetStoreAPITests.HelperClasses
{
    class CreateHelper
    {
        public static async Task<bool> DoesPetExistAlready(int petId)
        {
            using (var httpClient = ApiHelper.CreateHttpClient())
            {
                var requestUri = ApiHelper.SearchPetUri(petId);

                var apiResponse = await httpClient.GetAsync(requestUri);
                if (apiResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    var jsonString = await apiResponse.Content.ReadAsStringAsync();
                    string message = JObject.Parse(jsonString)["message"].ToString();
                    Assert.AreEqual(message, "Pet not found");
                    return false;
                }
            }

            return true;
        }

        public static AddPet CreatePetRequest(int petId, string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "doggie";
            }



            var categoryData = new Dictionary<string, object>();
            categoryData.Add("id", 0);
            categoryData.Add("name", "string");

            var tagsDataList = new List<Dictionary<string, object>>();
            var tagsData = new Dictionary<string, object>();
            tagsData.Add("id", 0);
            tagsData.Add("name", "string");
            tagsDataList.Add(tagsData);

            var photoUrl = new List<string>();
            photoUrl.Add("www.freephotoupload.com/photoid123");

            var AddPet = new AddPet
            {
                Id = petId,
                Category = categoryData,
                Name = name,
                PhotoUrls = photoUrl,
                Tags = tagsDataList,
                Status = "available"
            };
            return AddPet;
        }
        public static async Task CreatePet(int petId)
        {
            using (var httpClient = ApiHelper.CreateHttpClient())
            {
                var requestUri = ApiHelper.CreatePetUri();
                var petData = CreatePetRequest(petId);
                var httpContent = ApiHelper.BuildHttpContent(petData);

                var apiResponse = await httpClient.PostAsync(requestUri, httpContent);
                apiResponse.EnsureSuccessStatusCode();
            }
        }
        public static async Task<string> SearchPet(int petId, string searchInfo = "id", bool shouldFind = true)
        {
            using (var httpClient = ApiHelper.CreateHttpClient())
            {
                var data = string.Empty;
                var requestUri = ApiHelper.SearchPetUri(petId);

                var apiResponse = await httpClient.GetAsync(requestUri);
                var message = await apiResponse.Content.ReadAsStringAsync();
                var jsonString = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    data = JObject.Parse(jsonString)[searchInfo].ToString();
                }
                else
                {
                    if (apiResponse.StatusCode == HttpStatusCode.NotFound && !shouldFind)
                    {
                        data = JObject.Parse(jsonString)["message"].ToString();
                    }
                    else
                    {
                        throw new Exception($"Failed to find Pet id: {petId}");
                    }
                }

                return data;
            }
        }
        public static async Task UpdatePet(int petId, string name)
        {
            using (var httpClient = ApiHelper.CreateHttpClient())
            {
                var requestUri = ApiHelper.CreatePetUri();
                var petData = CreatePetRequest(petId, name);
                var httpContent = ApiHelper.BuildHttpContent(petData);

                var apiResponse = await httpClient.PutAsync(requestUri, httpContent);
                var message = await apiResponse.Content.ReadAsStringAsync();
                apiResponse.EnsureSuccessStatusCode();
            }
        }
        public static async Task DeletePet(int petId)
        {
            using (var httpClient = ApiHelper.CreateHttpClient())
            {
                var data = string.Empty;
                var requestUri = ApiHelper.SearchPetUri(petId);

                var apiResponse = await httpClient.DeleteAsync(requestUri);
                var message = await apiResponse.Content.ReadAsStringAsync();
                apiResponse.EnsureSuccessStatusCode();
            }
        }
    }
}
