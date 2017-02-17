using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Conventions;
using swdestinydb.Models;

namespace CollectionTracker.Models
{
    public class CollectionViewModel
    {
		public List<CardViewModel> Collection { get; set; }
	}

	public class CardViewModel
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		[MapTo("RarityCode")]
		public string Rarity { get; set; }
		public string SetName { get; set; }
		[MapTo("Position")]
		public int SetPosition { get; set; }
	}
}
