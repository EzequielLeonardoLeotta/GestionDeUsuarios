using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public interface IPersonaService
  {
    #region CRUD
    Task<ServiceResponse<List<PersonaDto>>> GetAllPersons();
    Task<ServiceResponse<PersonaDto>> GetPerson(GetPersonaDto getPersonaDto);
    Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto personaDto);
    Task<ServiceResponse<PersonaDto>> UpdatePerson(PersonaDto personaDto);
    Task<ServiceResponse<List<PersonaDto>>> DeletePerson(GetPersonaDto getPersonaDto);
    #endregion
    Task<ServiceResponse<Dictionary<string, int>>> GetStatistics();
  }
}