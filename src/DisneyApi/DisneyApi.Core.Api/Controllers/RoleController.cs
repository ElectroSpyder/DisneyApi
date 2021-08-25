using DisneyApi.Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public List<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();

        }

        [HttpPost]
        public async Task<ActionResult<IdentityRole>> CreateRole(IdentityRole role)
        {
            return Ok(await _roleManager.CreateAsync(role));
        }
    }
}
