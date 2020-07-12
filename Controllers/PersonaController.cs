using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models.Enums;
using GestionDeUsuarios.Models.Response;
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

    #region CRUD
    [HttpGet]
    public async Task<IActionResult> GetAllPersons() => Ok(await _personaService.GetAllPersons());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(int id) => Ok(await _personaService.GetPerson(id));

    [HttpGet("tipoDocumento/{tipoDocumento}/documento/{documento}/pais/{pais}/sexo/{sexo}")]
    public async Task<IActionResult> GetPerson(TipoDocumento tipoDocumento, string documento, Pais pais, Sexo sexo)
    {
      return Ok(await _personaService.GetPerson(ConvertToGetPersonaDto(tipoDocumento, documento, pais, sexo)));
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson(PersonaDto personaDto) => Ok(await _personaService.AddPerson(personaDto));
    

    [HttpPut]
    public async Task<IActionResult> UpdatePerson(PersonaDto personaDto)
    {
      ServiceResponse<PersonaDto> response = await _personaService.UpdatePerson(personaDto);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }

    [HttpDelete("tipoDocumento/{tipoDocumento}/documento/{documento}/pais/{pais}/sexo/{sexo}")]
    public async Task<IActionResult> DeletePerson(TipoDocumento tipoDocumento, string documento, Pais pais, Sexo sexo)
    {
      ServiceResponse<List<PersonaDto>> response = await _personaService.DeletePerson(ConvertToGetPersonaDto(tipoDocumento, documento, pais, sexo));
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }

    private GetPersonaDto ConvertToGetPersonaDto(TipoDocumento tipoDocumento, string documento, Pais pais, Sexo sexo)
    {
      return new GetPersonaDto { TipoDocumento = tipoDocumento, Documento = documento, Pais = pais, Sexo = sexo };
    }
    #endregion

    [HttpGet("estadisticas")]
    public async Task<IActionResult> GetStatistics() => Ok(await _personaService.GetStatistics());

    #region Relationships
    [HttpPost("{id1}/padre/{id2}")]
    public async Task<IActionResult> AddFather(int id1, int id2)
    {
      return Ok(await _personaService.AddFather(id1, id2));
    }

    [HttpGet("relaciones/{id1}/{id2}")]
    public async Task<IActionResult> GetRelationship(int id1, int id2)
    {
      return Ok(await _personaService.GetRelationship(id1, id2));
    }
    #endregion
  }
}