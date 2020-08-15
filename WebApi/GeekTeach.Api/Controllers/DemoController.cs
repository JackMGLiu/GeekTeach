using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geek.Framework.Db;
using Geek.Framework.Jwt;
using GeekTeach.Application.Services;
using GeekTeach.Application.Services.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekTeach.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService demoService;
        private readonly ISysUserService sysUserService;

        public DemoController(IDemoService demoService, ISysUserService sysUserService)
        {
            this.demoService = demoService;
            this.sysUserService = sysUserService;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Demo>> Get()
        //{
        //    await demoServive.AddDemo();
        //    return await demoServive.DemoList();
        //    //return new string[] { "value1", "value2" };
        //}
        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token()
        {
            JwtSessionModel model = new JwtSessionModel
            {
                UserId = "99999",
                UserName = "SystemTest",
                RoleId = "88888",
                RoleName = "测试管理"
            };
            var token = TokenHelper.GenerateToken(model);
            return Ok(token);
        }

        /// <summary>
        /// 分页测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
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

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="key">主键标识</param>
        /// <returns></returns>
        [HttpGet("model")]
        public async Task<IActionResult> GetModel(long key)
        {
            var res = await demoService.GetModel(key);
            return Ok(res);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListBy(int obj)
        {
            var res = await demoService.DemoListByWhere(obj);
            return Ok(res);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUserList()
        {
            var res = await sysUserService.GetUsers();
            return Ok(res);
        }
    }
}
