using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using swdestinydb;
using CollectionTracker.Models;
using swdestinydb.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CollectionTracker.Controllers
{
    public class CollectionController : Controller
    {
	    private readonly IMapper _mapper;

	    public CollectionController(IMapper mapper)
	    {
		    _mapper = mapper;
	    }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
			var client = new CollectionClient();
			var collectionTask = client.GetCollectionAsync();
			var cardsTask = client.GetCardsAsync();

			await Task.WhenAll(collectionTask, cardsTask);

			var collection = await collectionTask;
			var cards = await cardsTask;
	        var cardModels = cards.Select(c => _mapper.Map<Card, CardViewModel>(c)).ToList();
	        foreach (var cardViewModel in cardModels)
	        {
		        int quantity;
		        try
		        {
					quantity = collection.First(ci => ci.Key == cardViewModel.Code).Quantity;
				}
		        catch (InvalidOperationException)
		        {
			        quantity = 0;
		        }
		        cardViewModel.Quantity = quantity;
	        }

			var model = new CollectionViewModel
			{
				Collection = cardModels
			};

			return View(model);
        }

    }
}
