using GestionDeUsuarios.Models;
using GestionDeUsuarios.Services;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetFirst()
    {
      return Ok(_personaService.GetFirst());
    }

    [HttpGet("GetAll")]
    public IActionResult GetAllPersons()
    {
      return Ok(_personaService.GetAllPersons());
    }

    [HttpGet("{documento}")]
    public IActionResult GetPersonByDni(string documento)
    {
      return Ok(_personaService.GetPersonByDni(documento));
    }

    [HttpPost]
    public IActionResult AddPerson(Persona persona) 
    {
      return Ok(_personaService.AddPerson(persona));
    }
  }
}