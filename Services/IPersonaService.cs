using GestionDeUsuarios.Models;
using System.Collections.Generic;

namespace GestionDeUsuarios.Services
{
  public interface IPersonaService
  {
    Persona GetFirst();
    List<Persona> GetAllPersons();
    Persona GetPersonByDni(string dni);
    List<Persona> AddPerson(Persona persona);
  }
}