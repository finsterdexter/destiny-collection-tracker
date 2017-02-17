using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using swdestinydb.Models;

namespace swdestinydb
{
	/// <summary>
	/// This converter handles converting the content of the collection json, which is one giant object with a ton of properties,
	/// instead of being an array of objects.
	/// </summary>
    public class CollectionItemJsonConverter : JsonConverter
    {
	    public override bool CanConvert(Type objectType)
	    {
		    return objectType == typeof(IEnumerable<CollectionItem>);
	    }

	    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	    {
		    var jObj = JObject.Load(reader);
		    var results = jObj.Properties().Select(jp => new CollectionItem {Key = jp.Name, Dice = (int)jp.Value["dice"], Quantity = (int)jp.Value["quantity"]});
		    return results;
	    }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	    {
		    throw new NotImplementedException();
	    }

    }
}
