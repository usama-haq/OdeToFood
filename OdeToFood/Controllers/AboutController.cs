using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // Using Tokenized route
    // You can also use literals like [Route("company/[controller]/[action]")]
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "1+555-555-5555";
        }

        public string Address()
        {
            return "USA";
        }
    }
}