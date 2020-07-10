using AutoMapper;
using GestionDeUsuarios.Data;
using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using GestionDeUsuarios.Models.Response;
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

      try
      {
        List<Persona> personas = await _context.Personas.ToListAsync();
        serviceResponse.Data = personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> GetPerson(GetPersonaDto getPersonaDto)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();

      try
      {
        Persona persona = await _context.Personas.FirstAsync(p => p.Documento == getPersonaDto.Documento && p.Pais.Equals(getPersonaDto.Pais) && p.Sexo.Equals(getPersonaDto.Sexo) && p.TipoDocumento.Equals(getPersonaDto.TipoDocumento));
        serviceResponse.Data = _mapper.Map<PersonaDto>(persona);
        return serviceResponse;
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
          serviceResponse.Message = "Persona no encontrada";
        else
          serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }

    public async Task<Persona> GetPersonModel(GetPersonaDto getPersonaDto)
    {
      Persona persona = new Persona();

      try
      {
        persona = await _context.Personas.FirstAsync(p => p.Documento == getPersonaDto.Documento && p.Pais.Equals(getPersonaDto.Pais) && p.Sexo.Equals(getPersonaDto.Sexo) && p.TipoDocumento.Equals(getPersonaDto.TipoDocumento));
      }
      catch (Exception e)
      {
        throw e;
      }

      return persona;
    }

    public async Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto personaDto)
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();

      try
      {
        var getPersonaDto = await GetPerson(_mapper.Map<GetPersonaDto>(personaDto));
        if (!getPersonaDto.Success)
        {
          await _context.Personas.AddAsync(_mapper.Map<Persona>(personaDto));
          await _context.SaveChangesAsync();
          serviceResponse.Data = _context.Personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
        }
        else
        {
          serviceResponse.Success = false;
          serviceResponse.Message = "No se permite duplicar personas";
        }
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
          serviceResponse.Message = "Persona no encontrada";
        else
          serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> UpdatePerson(PersonaDto personaDto)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();

      try
      {
        var persona = await GetPersonModel(_mapper.Map<GetPersonaDto>(personaDto));
        
        persona.Nombre = personaDto.Nombre;
        persona.Apellido = personaDto.Apellido;
        persona.Contacto = personaDto.Contacto;
        persona.Edad = personaDto.Edad;

        _context.Personas.Update(persona);
        await _context.SaveChangesAsync();
        serviceResponse.Data = personaDto;
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
          serviceResponse.Message = "Persona no encontrada";
        else
          serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<PersonaDto>>> DeletePerson(GetPersonaDto getPersonaDto)
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();

      try
      {
        var persona = await GetPersonModel(getPersonaDto);
        _context.Personas.Remove(persona);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Personas.Select(p => _mapper.Map<PersonaDto>(p)).ToList();
      }
      catch (Exception e)
      {
        serviceResponse.Success = false;
        if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
          serviceResponse.Message = "Persona no encontrada";
        else
          serviceResponse.Message = e.Message;
      }

      return serviceResponse;
    }
  }
}