using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaboratorioClinico.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ResultadoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ResultadoController/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resultado>>> Get()
        {
            return await _context.Resultados.ToListAsync();
        }

        // GET api/<ResultadoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ResultadoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ResultadoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResultadoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
