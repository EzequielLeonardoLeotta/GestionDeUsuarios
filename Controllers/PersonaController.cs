using GestionDeUsuarios.Models;
using GestionDeUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class PersonaController : ControllerBase
  {
    private readonly IPersonaService _personaService;

    public PersonaController(IPersonaService personaService)
    {
      _personaService = personaService;
    }

    [HttpGet("GetFirst")]
    public async Task<IActionResult> GetFirst()
    {
      return Ok(await _personaService.GetFirst());
    }

    [HttpGet("GetAllPersons")]
    public async Task<IActionResult> GetAllPersons()
    {
      return Ok(await _personaService.GetAllPersons());
    }

    [HttpGet("{documento}")]
    public async Task<IActionResult> GetPersonByDni(string documento)
    {
      return Ok(await _personaService.GetPersonByDni(documento));
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson(Persona persona) 
    {
      return Ok(await _personaService.AddPerson(persona));
    }
  }
}