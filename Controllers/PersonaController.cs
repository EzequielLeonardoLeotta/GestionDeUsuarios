using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using GestionDeUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    public async Task<IActionResult> AddPerson(PersonaDto persona)
    {
      return Ok(await _personaService.AddPerson(persona));
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson(UpdatePersonDto persona)
    {
      ServiceResponse<PersonaDto> response = await _personaService.UpdatePerson(persona);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }

    [HttpDelete("{documento}")]
    public async Task<IActionResult> DeletePerson(string documento)
    {
      ServiceResponse<List<PersonaDto>> response = await _personaService.DeletePerson(documento);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
  }
}