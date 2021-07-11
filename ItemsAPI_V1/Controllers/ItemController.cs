using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ItemsAPI_V1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {

        private string CollectionName { get; set; } = "Items";
        public IItemService _ItemService { get; set; }
        public ItemController(IItemService iItemService)
        {
            _ItemService = iItemService;
             InsertTestItems();

        }
        

        public void InsertTestItems()
        {
            for (int i = 0; i < 20; i++)
            {
                Item item = new Item();
                item.Type = "Iphone" + i;
                item.HasToChange = ((i % 2) == 0) ? true : false;
                item.NewURL = (item.HasToChange) ? item.Type + i : "";
                _ItemService.InsertItem(CollectionName, item);
            }
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _ItemService.GetAllItems(CollectionName).ToArray();
        }
        [HttpGet("{type}")]
        public IActionResult GetTByype(string type)
        {
           
            Item item = _ItemService.GetItemByType(CollectionName, type);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(item);
            }
        }
        [HttpPost]
        public IActionResult Post([FromForm] string currentURL)
        {
            string type = ParseURLAndGetTypeVale(currentURL);
            if (type == null)
            {
                return NotFound();
            }
            Item item = _ItemService.GetItemByType(CollectionName, type);
            
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(item);

            }
        }
        
        private string ParseURLAndGetTypeVale(string URL)
        {
            string typeValue = null;

            const string pattern = @"\b&type=\S*\b";

            MatchCollection myMatches = Regex.Matches(URL, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            
            foreach (Match nextMatch in myMatches)
            {
                string result = nextMatch.ToString();
                typeValue = result.Substring(6);
            }

            return typeValue;
        }

    }
}
