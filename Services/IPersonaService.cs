using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public interface IPersonaService
  {
    Task<ServiceResponse<List<PersonaDto>>> GetAllPersons();
    Task<ServiceResponse<PersonaDto>> GetPersonByDni(string documento);
    Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto persona);
    Task<ServiceResponse<PersonaDto>> UpdatePerson(UpdatePersonDto persona);
    Task<ServiceResponse<List<PersonaDto>>> DeletePerson(string documento);
  }
}