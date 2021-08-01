using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            return  _ItemService.GetAllItems(CollectionName).ToArray();
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
        [HttpPost("{hastochange}")]
        public IActionResult Post(bool hastochange, [FromForm] string currentURL)
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
                //Todo replace currentURl with new
               item.NewURL= currentURL.Replace(type, item.NewURL+100);
                return new ObjectResult(item);

            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Collection<Item> items)
        {
            if (items==null)
            {
                return NotFound();
            }
            else
                _ItemService.InsertManyItems(CollectionName, items);
           
                return new ObjectResult(items);

        }
        // TODO insert list of Items and delete or update item with spesific type
        //[HttpPut("{id}")]
        //public  ActionResult<Item> Put(string id, [FromBody] Item item)
        //{
        //    Guid _id = Guid.Parse(id);
        //    var _item = _ItemService.GetItemById(CollectionName, _id);

        //    if (_item == null)
        //        return new NotFoundResult();

        //    _ItemService.UpdateItem(CollectionName, _id, item);

        //    return new OkObjectResult(item);
        //}
        [HttpPut("{type}")]
        public ActionResult<Item> PutByType(string type, [FromBody] Item item)
        {
            
            var _item = _ItemService.GetItemByType(CollectionName, type);

            if (_item == null)
                return new NotFoundResult();

            _ItemService.UpdateItem(CollectionName, item.Id, item);

            return new OkObjectResult(item);
        }
        [HttpDelete("{type}")]
        public IActionResult Delete(string type)
        {
            var _item = _ItemService.GetItemByType(CollectionName, type);

            if (_item == null)
                return new NotFoundResult();

            _ItemService.DeleteItemByType(CollectionName, type);

            return new OkResult();
        }
        private string ParseURLAndGetTypeVale(string URL)
        {
            string typeValue = null;

            //const string pattern = @"\b&type=\S*\b";
            const string pattern = @"\b&type=([^&]*)\b";
            //const string pattern = @"^&type=(.*)";

            MatchCollection myMatches = Regex.Matches(URL, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            
            foreach (Match nextMatch in myMatches)
            {
                string result = nextMatch.Groups[1].Value.ToString();
               
            }

            return typeValue;
        }
        //private string ParseURLAndGetTypeVale(string URL)
        //{
        //    string typeValue = null;

        //    const string pattern = @"\b&type=\S*\b";
        //    //const string pattern = @"\b&type=[^&]*\b";
        //    //const string pattern = @"^&type=(.*)";

        //    MatchCollection myMatches = Regex.Matches(URL, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

        //    foreach (Match nextMatch in myMatches)
        //    {
        //        string result = nextMatch.ToString();
        //        typeValue = result.Substring(6);
        //    }

        //    return typeValue;
        //}

    }
}
