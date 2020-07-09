using GestionDeUsuarios.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public class PersonaService : IPersonaService
  {
    private static List<Persona> personas = new List<Persona> { new Persona(), new Persona { Nombre = "Juan", Documento = "123" } };

    public async Task<ServiceResponse<Persona>> GetFirst()
    {
      ServiceResponse<Persona> serviceResponse = new ServiceResponse<Persona>();
      serviceResponse.Data = personas[0];
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Persona>>> GetAllPersons()
    {
      ServiceResponse<List<Persona>> serviceResponse = new ServiceResponse<List<Persona>>();
      serviceResponse.Data = personas;
      return serviceResponse;
    }

    public async Task<ServiceResponse<Persona>> GetPersonByDni(string documento)
    {
      ServiceResponse<Persona> serviceResponse = new ServiceResponse<Persona>();
      serviceResponse.Data = personas.FirstOrDefault(p => p.Documento == documento);
      return serviceResponse;
    }
    public async Task<ServiceResponse<List<Persona>>> AddPerson(Persona persona)
    {
      ServiceResponse<List<Persona>> serviceResponse = new ServiceResponse<List<Persona>>();
      personas.Add(persona);
      serviceResponse.Data = personas;
      return serviceResponse;
    }
  }
}