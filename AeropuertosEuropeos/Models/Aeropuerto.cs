using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Airports.Models
{
    [DataContract]
    public class Aeropuerto
    {
        // describe los datos que se van a intercambiar
        [DataMember(Name = "iata")]
        public  string Iata { get; set; }
        [DataMember(Name = "lon")]
        public  string Lon { get; set; }
        [DataMember(Name = "iso")]
        public  string Iso { get; set; }
        [DataMember(Name = "status")]
        public  string Status { get; set; } //0=closed, 1=open
        [DataMember(Name = "name")]
        public  string Name { get; set; }
        [DataMember(Name = "continent")]
        public  string Continent { get; set; }
        [DataMember(Name = "type")]
        public  string Type { get; set; }
        [DataMember(Name = "lat")]
        public  string Lat { get; set; }
        [DataMember(Name = "size")]
        public  string Size { get; set; }
    }
}