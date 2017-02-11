using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionTracker.Models
{
    public class CollectionViewModel
    {
		public JObject Collection { get; set; }
		public JArray Cards { get; set; }
	}
}
