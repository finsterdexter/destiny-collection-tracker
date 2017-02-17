using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace swdestinydb.Models
{
    public class Card
    {
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("name")]
	    public string Name { get; set; }

		[JsonProperty("rarity_code")]
	    public string RarityCode { get; set; }

		[JsonProperty("set_name")]
	    public string SetName { get; set; }

		[JsonProperty("position")]
		public int Position { get; set; }

    }
}
