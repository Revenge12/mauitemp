using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using $safeprojectname$.Data;
using $safeprojectname$.Services;
using Shared.Template.Database;

namespace $safeprojectname$.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestingController : ControllerBase
    {
        private readonly MainDbContext _context;

        public TestingController(MainDbContext context)
        {
            _context = context;
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> Upsert(Test test)
        {
            var generic = new GenericDataService<Test>(_context);

            var resposne = await generic.GenericUpdateInsert(test);

            return Ok(resposne);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove(Test test)
        {
            var generic = new GenericDataService<Test>(_context);

            var resposne = await generic.GenericRemove(test);

            return Ok(resposne);
        }
    }
}
