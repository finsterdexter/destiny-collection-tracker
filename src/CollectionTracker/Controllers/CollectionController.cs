using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using swdestinydb;
using CollectionTracker.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CollectionTracker.Controllers
{
    public class CollectionController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
			var client = new CollectionClient();
			var collectionTask = client.GetCollectionAsync();
			var cardsTask = client.GetCardsAsync();

			await Task.WhenAll(collectionTask, cardsTask);

			var collection = await collectionTask;
			var cards = await cardsTask;

			var model = new CollectionViewModel
			{
				Collection = collection,
				Cards = cards
			};

			return View(model);
        }

    }
}
