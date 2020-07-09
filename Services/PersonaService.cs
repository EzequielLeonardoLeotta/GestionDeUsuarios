using AutoMapper;
using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public class PersonaService : IPersonaService
  {
    private readonly IMapper _mapper;

    public PersonaService(IMapper mapper)
    {
      _mapper = mapper;
    }

    private static List<Persona> personas = new List<Persona> { new Persona(), new Persona { Nombre = "Juan", Documento = "123" } };

    public async Task<ServiceResponse<List<PersonaDto>>> GetAllPersons()
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();
      serviceResponse.Data = personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> GetPersonByDni(string documento)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();
      serviceResponse.Data = _mapper.Map<PersonaDto>(personas.FirstOrDefault(p => p.Documento == documento));
      return serviceResponse;
    }
    public async Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto persona)
    {
        ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();
        personas.Add(_mapper.Map<Persona>(persona));
        serviceResponse.Data = personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> UpdatePerson(UpdatePersonDto persona)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();

      try
      {
        Persona personaAux = personas.FirstOrDefault(p => p.Documento == persona.Documento);
        personaAux.Nombre = persona.Nombre;
        personaAux.Apellido = persona.Apellido;
        personaAux.Contacto = persona.Contacto;
        personaAux.Edad = persona.Edad;
        personaAux.Sexo = persona.Sexo;

        serviceResponse.Data = _mapper.Map<PersonaDto>(personaAux);
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<PersonaDto>>> DeletePerson(string documento)
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();

      try
      {
        Persona personaAux = personas.First(p => p.Documento == documento);
        personas.Remove(personaAux);
        serviceResponse.Data = personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }
  }
}