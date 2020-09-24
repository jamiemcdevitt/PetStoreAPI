using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PetStoreAPITests.HelperClasses;
using PetStoreAPITests.PropertyClasses;

namespace PetStoreAPITests
{
    public class TestCases
    {
        [Test]
        //Creates
        [Parallelizable(ParallelScope.Self)]
        public async Task TC_1_CreatePet()
        {
            var petId = ApiHelper.GeneratePetId().Result;
            await CreateHelper.CreatePet(petId);
        }

        [Test]
        //Creates>Searches>ConfirmsFound
        [Parallelizable(ParallelScope.Self)]
        public async Task TC_2_SearchPet()
        {
            var petId = ApiHelper.GeneratePetId().Result;

            await CreateHelper.CreatePet(petId);

            var searchPetId = await CreateHelper.SearchPet(petId);
            Assert.AreEqual(searchPetId, petId.ToString());
        }

        [Test]
        //Creates>Updates>Confirms Updates
        [Parallelizable(ParallelScope.Self)]
        public async Task TC_3_UpdatePet()
        {
            var petId = ApiHelper.GeneratePetId().Result;
            var updatedName = "UpdatedPetName";

            await CreateHelper.CreatePet(petId);

            await CreateHelper.UpdatePet(petId, updatedName);

            var name = await CreateHelper.SearchPet(petId, "name");

            Assert.AreEqual(name,updatedName);
        }

        [Test]
        //Creates>Deletes>Confirms deleted
        [Parallelizable(ParallelScope.Self)]
        public async Task TC_4_DeletePet()
        {
            var petId = ApiHelper.GeneratePetId().Result;

            await CreateHelper.CreatePet(petId);

            await CreateHelper.DeletePet(petId);

            var message = await CreateHelper.SearchPet(petId, shouldFind: false);
            Assert.AreEqual(message, "Pet not found");
        }
    }
}
