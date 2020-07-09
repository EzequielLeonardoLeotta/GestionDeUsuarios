using GestionDeUsuarios.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GestionDeUsuarios.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class PersonaController : ControllerBase
  {
    private static List<Persona> personas = new List<Persona> {new Persona(), new Persona { Nombre = "Juan", Documento = "123" } };

    [HttpGet("GetFirst")]
    public IActionResult GetFirst()
    {
      return Ok(personas[0]);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
      return Ok(personas);
    }

    [HttpGet("{documento}")]
    public IActionResult GetSingle(string documento)
    {
      return Ok(personas.FirstOrDefault(p => p.Documento == documento));
    }

    [HttpPost]
    public IActionResult AddCharacter(Persona persona) 
    {
      personas.Add(persona);
      return Ok(personas);
    }
  }
}