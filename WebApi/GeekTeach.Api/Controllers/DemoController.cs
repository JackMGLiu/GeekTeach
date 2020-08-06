using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekTeach.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService demoService;

        public DemoController(IDemoService demoService)
        {
            this.demoService = demoService;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Demo>> Get()
        //{
        //    await demoServive.AddDemo();
        //    return await demoServive.DemoList();
        //    //return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public async Task<IActionResult> GetPage()
        {
            var page = new PageInfo
            {
                tableName = "Demo",
                page = 1,
                size = 3,
                orderFiled = "Id",
                order = "desc"
            };
            var res = await demoService.DemoPageList(page);
            return Ok(res.Items);
        }
    }
}
