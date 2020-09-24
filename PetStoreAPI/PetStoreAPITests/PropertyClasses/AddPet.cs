using System;
using System.Collections.Generic;
using System.Text;

namespace PetStoreAPITests.PropertyClasses
{
    public class AddPet
    {
        public AddPet()
        {
            Category = new Dictionary<string, object>();
            Tags = new List<Dictionary<string, object>>();
        }
        /// <summary>
        /// Id of pet
        /// </summary>
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Category of pet
        /// </summary>
        [Newtonsoft.Json.JsonProperty("category")]
        public Dictionary<string, object> Category { get; set; }

        /// <summary>
        /// Name of pet
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Photo URL of pet
        /// </summary>
        [Newtonsoft.Json.JsonProperty("photoUrls")]
        public List<string> PhotoUrls { get; set; }

        /// <summary>
        /// Tags of pet
        /// </summary>
        [Newtonsoft.Json.JsonProperty("tags")]
        public List<Dictionary<string, object>> Tags { get; set; }

        /// <summary>
        /// Status of pet
        /// </summary>
        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }
    }
}
