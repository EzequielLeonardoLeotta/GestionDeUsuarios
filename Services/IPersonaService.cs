using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using GestionDeUsuarios.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public interface IPersonaService
  {
    #region CRUD
    Task<ServiceResponse<List<PersonaDto>>> GetAllPersons();
    Task<ServiceResponse<Persona>> GetPerson(int id);
    Task<ServiceResponse<PersonaDto>> GetPerson(GetPersonaDto getPersonaDto);
    Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto personaDto);
    Task<ServiceResponse<PersonaDto>> UpdatePerson(PersonaDto personaDto);
    Task<ServiceResponse<List<PersonaDto>>> DeletePerson(GetPersonaDto getPersonaDto);
    #endregion
    Task<ServiceResponse<Dictionary<string, int>>> GetStatistics();
    #region Relationaships
    Task<ServiceResponse<string>> AddFather(int id1, int id2);
    Task<ServiceResponse<string>> GetRelationship(int id1, int id2);
    #endregion
  }
}