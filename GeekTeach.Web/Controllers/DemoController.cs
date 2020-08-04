using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekTeach.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekTeach.Web.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        private readonly IDemoServive demoServive;

        public DemoController(IDemoServive demoServive)
        {
            this.demoServive = demoServive;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Demo>> Get()
        {
            return await demoServive.DemoList();
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
