using GestionDeUsuarios.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public class PersonaService : IPersonaService
  {
    private static List<Persona> personas = new List<Persona> { new Persona(), new Persona { Nombre = "Juan", Documento = "123" } };

    public async Task<Persona> GetFirst()
    {
      return personas[0];
    }

    public async Task<List<Persona>> GetAllPersons()
    {
      return personas;
    }

    public async Task<Persona> GetPersonByDni(string documento)
    {
      return personas.FirstOrDefault(p => p.Documento == documento);
    }
    public async Task<List<Persona>> AddPerson(Persona persona)
    {
      personas.Add(persona);
      return personas;
    }
  }
}