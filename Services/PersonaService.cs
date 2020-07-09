using GestionDeUsuarios.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestionDeUsuarios.Services
{
  public class PersonaService : IPersonaService
  {
    private static List<Persona> personas = new List<Persona> { new Persona(), new Persona { Nombre = "Juan", Documento = "123" } };

    public Persona GetFirst()
    {
      return personas[0];
    }

    public List<Persona> GetAllPersons()
    {
      return personas;
    }

    public Persona GetPersonByDni(string documento)
    {
      return personas.FirstOrDefault(p => p.Documento == documento);
    }
    public List<Persona> AddPerson(Persona persona)
    {
      personas.Add(persona);
      return personas;
    }
  }
}