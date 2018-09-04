using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trialApps.API.Data;

namespace trialApps.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext ctx;
        public ValuesController(DataContext ctx)
        {
            this.ctx = ctx;

        }
        // GET api/values
        
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var nilai = await ctx.Values.ToListAsync();
            return Ok(nilai);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValues(int id)
        {
            var nilai = await ctx.Values.FirstOrDefaultAsync(ent => ent.Id == id);
            return Ok(nilai);
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
