using GestionDeUsuarios.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public interface IPersonaService
  {
    Task<ServiceResponse<Persona>> GetFirst();
    Task<ServiceResponse<List<Persona>>> GetAllPersons();
    Task<ServiceResponse<Persona>> GetPersonByDni(string dni);
    Task<ServiceResponse<List<Persona>>> AddPerson(Persona persona);
  }
}