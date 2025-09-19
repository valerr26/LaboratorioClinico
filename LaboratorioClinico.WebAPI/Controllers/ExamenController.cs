using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ExamenController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Examen/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examen>>> Get()
        {
            return await _context.Examenes.ToListAsync();
        }

        // GET api/<ExamenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExamenController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ExamenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExamenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
