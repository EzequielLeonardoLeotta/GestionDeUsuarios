using AutoMapper;
using GestionDeUsuarios.Data;
using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeUsuarios.Services
{
  public class PersonaService : IPersonaService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PersonaService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<ServiceResponse<List<PersonaDto>>> GetAllPersons()
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();
      List<Persona> dbPersonas = await _context.Personas.ToListAsync();
      serviceResponse.Data = dbPersonas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> GetPersonByDni(string documento)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();
      Persona dbPersona = await _context.Personas.FirstOrDefaultAsync(p => p.Documento == documento);
      serviceResponse.Data = _mapper.Map<PersonaDto>(dbPersona);
      return serviceResponse;
    }
    public async Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto persona)
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();
      await _context.Personas.AddAsync(_mapper.Map<Persona>(persona));
      await _context.SaveChangesAsync();
      serviceResponse.Data = _context.Personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> UpdatePerson(UpdatePersonDto persona)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();

      try
      {
        Persona personaAux = await _context.Personas.FirstOrDefaultAsync(p => p.Documento == persona.Documento);
        personaAux.Nombre = persona.Nombre;
        personaAux.Apellido = persona.Apellido;
        personaAux.Contacto = persona.Contacto;
        personaAux.Edad = persona.Edad;
        personaAux.Sexo = persona.Sexo;

        _context.Personas.Update(personaAux);
        await _context.SaveChangesAsync();
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
        Persona personaAux = await _context.Personas.FirstAsync(p => p.Documento == documento);
        _context.Personas.Remove(personaAux);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
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