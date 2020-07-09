using GestionDeUsuarios.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public interface IPersonaService
  {
    Task<Persona> GetFirst();
    Task<List<Persona>> GetAllPersons();
    Task<Persona> GetPersonByDni(string dni);
    Task<List<Persona>> AddPerson(Persona persona);
  }
}