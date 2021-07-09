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

        //public List<Item> Items { get; set; }
        public IItemService _ItemService { get; set; }
        public ItemController()
        {
            _ItemService = new ItemService("", "ItemsDB");
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
                _ItemService.InsertItem("Items", item);
            }
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _ItemService.GetAllItems("Items").ToArray();
        }
        [HttpGet("{type}")]
        public IActionResult GetTByype(string type)
        {
           
            Item item = _ItemService.GetItemByType("Items", type);
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
            Item item = _ItemService.GetItemByType("Items", type);
            return new ObjectResult(item);

            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(item);

            }
        }
        //[HttpPost]
        //public IActionResult Post([FromBody] Item item)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _ItemService.InsertItem("Items", item);

        //    return CreatedAtAction("Get", new { id = item.Id }, item);

        //}

        //[HttpPost("{currentURL}")]
        // [HttpPost]
        //public IActionResult Post([FromBody] string currentURL)
        //{
        //    Debug.WriteLine("--------------------------------------------------------------------------------------"+currentURL);
        //    Console.WriteLine("--------------------------------------------------------------------------------------" + currentURL);

        //    return Created(currentURL,new Item());
        //    //return Ok(new ObjectResult(new Item()));

        //    //Debug.WriteLine("--------------------------------------------------------------------------------------"+currentURL);
        //    //string type = ParseURLAndGetTypeVale(currentURL);
        //    //Item item = _ItemService.GetItemByType("Items", type);
        //    //if (item == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //else
        //    //{
        //    //    return new ObjectResult(item);
        //    //}
        //    //Created()
        //    //Item item = _ItemService.GetItemByType("Items", type);
        //    //if (item == null)
        //    //{
        //    //    return BadRequest();
        //    //}
        //    //else
        //    //{
        //    //    return CreatedAtAction(
        //    //        "",
        //    //        new
        //    //        {
        //    //            HasToChange = item.HasToChange
        //    //        },
        //    //        item);
        //    //    //return new ObjectResult(item);
        //    //}
        //}

        private string ParseURLAndGetTypeVale(string URL)
        {
            string typeValue = "";

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
