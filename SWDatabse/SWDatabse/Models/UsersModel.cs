using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWDatabse.Models
{
    public class UsersModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id {get; set;}


        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }


        [JsonProperty(PropertyName = "Name")]
        public string Name {get; set;}


        [JsonProperty(PropertyName = "Surname")]
        public string Surname {get; set;}


        [JsonProperty(PropertyName = "Email")]
        public string Email {get; set;}


        [JsonProperty(PropertyName = "Password")]
        public string Password {get; set;}


        [JsonProperty(PropertyName = "Area")]
        public string Area {get; set;}


        [JsonProperty(PropertyName = "Comments")]
        public string Comments {get; set;}


        [JsonProperty(PropertyName = "IncidentDescription")]
        public string IncidentDescription { get; set; }


        [JsonProperty(PropertyName = "Incident")]
        public string Incident { get; set; }


        [JsonProperty(PropertyName = "Vote")]
        public string Vote { get; set; }


        [JsonProperty(PropertyName = "Report")]
        public string Report { get; set; }


        [JsonProperty(PropertyName = "Points")]
        public int Points { get; set; }
    }
}
