using AutoMapper;
using GestionDeUsuarios.Data;
using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using GestionDeUsuarios.Models.Enums;
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

    #region CRUD
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
        AddError(serviceResponse, e, e.Message);
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<Persona>> GetPerson(int id)
    {
      ServiceResponse<Persona> serviceResponse = new ServiceResponse<Persona>();

      try
      {
        Persona persona = await _context.Personas.Include(p => p.Padre).FirstOrDefaultAsync(p => p.Id == id);
        if(persona is null)
          AddError(serviceResponse, null, "Persona no encontrada");
        else
          serviceResponse.Data = persona;

        return serviceResponse;
      }
      catch (Exception e)
      {
        AddError(serviceResponse, e, "Persona no encontrada");
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
      }
      catch (Exception e)
      {
        AddError(serviceResponse, e, "Persona no encontrada");
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<PersonaDto>>> AddPerson(PersonaDto personaDto)
    {
      ServiceResponse<List<PersonaDto>> serviceResponse = new ServiceResponse<List<PersonaDto>>();

      try
      {
        if (personaDto.Edad < 18)
          AddError(serviceResponse, null, "No se permite ingresar una persona que no sea mayor de edad");
        else
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
            AddError(serviceResponse, null, "No se permite duplicar personas");
          }
        }
      }
      catch (Exception e)
      {
        AddError(serviceResponse, e, "Persona no encontrada");
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<PersonaDto>> UpdatePerson(PersonaDto personaDto)
    {
      ServiceResponse<PersonaDto> serviceResponse = new ServiceResponse<PersonaDto>();

      try
      {
        if (personaDto.Edad < 18)
          AddError(serviceResponse, null, "No se permite ingresar una edad menor a 18");
        else
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
      }
      catch (Exception e)
      {
        AddError(serviceResponse, e, "Persona no encontrada");
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
        AddError(serviceResponse, e, "Persona no encontrada");
      }

      return serviceResponse;
    }

    private async Task<Persona> GetPersonModel(GetPersonaDto getPersonaDto)
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

    private void AddError(ServiceResponse<PersonaDto> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    private void AddError(ServiceResponse<List<PersonaDto>> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    private void AddError(ServiceResponse<Persona> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    private void AddError(ServiceResponse<string> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }
    #endregion

    public async Task<ServiceResponse<Dictionary<string, int>>> GetStatistics()
    {
      ServiceResponse<Dictionary<string, int>> serviceResponse = new ServiceResponse<Dictionary<string, int>>();

      Dictionary<string, int> statistics = new Dictionary<string, int>(3);

      var getAllPersons = await GetAllPersons();
      var allPersons = getAllPersons.Data;

      int quantityWomens = allPersons.Where(p => p.Sexo.Equals(Sexo.Femenino.ToString())).Count();
      int quantityMens = allPersons.Where(p => p.Sexo.Equals(Sexo.Masculino.ToString())).Count();

      int quantityArgentines = allPersons.Where(p => p.Pais.Equals(Pais.Argentina.ToString())).Count();
      int percentageArgentines = (quantityArgentines * 100) / allPersons.Count();

      statistics.Add("cantidad_mujeres", quantityWomens);
      statistics.Add("cantidad_hombres", quantityMens);
      statistics.Add("porcentaje_argentinos", percentageArgentines);

      serviceResponse.Data = statistics;

      return serviceResponse;
    }

    #region Relationships
    public async Task<ServiceResponse<string>> AddFather(int id1, int id2)
    {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

      try
      {
        if (id1 != id2)
        {
          var getPerson = await GetPerson(id2);
          if (getPerson.Success) //existe el hijo
          {
            var hijo = getPerson.Data;
            if (hijo.Padre is null) //no tiene padre
            {
              var getFather = await GetPerson(id1);
              if (getFather.Success) //existe el padre
              {
                var padre = getFather.Data;
                if (padre.Edad > hijo.Edad)
                {
                  hijo.Padre = padre;
                  _context.Personas.Update(hijo);
                  await _context.SaveChangesAsync();
                  serviceResponse.Data = "El id: " + padre.Id.ToString() + " es padre del id: " + hijo.Id.ToString();
                }
                else
                {
                  AddError(serviceResponse, null, "El padre no puede ser padre de un antepasado");
                }
                //if (padre.Padre != null) //el padre tiene padre
                //{
                //  if (padre.Padre.Id != hijo.Id) //el padre del padre es distinto que el hijo?
                //  {
                //    hijo.Padre = padre;
                //    _context.Personas.Update(hijo);
                //    await _context.SaveChangesAsync();
                //    serviceResponse.Data = "El id: " + padre.Id.ToString() + " es padre del id: " + hijo.Id.ToString();
                //  }
                //  else
                //  {
                //    AddError(serviceResponse, null, "El padre no puede ser hijo del hijo");
                //  }
                //}
                //else
                //{
                //  hijo.Padre = padre;
                //  _context.Personas.Update(hijo);
                //  await _context.SaveChangesAsync();
                //  serviceResponse.Data = "El id: " + padre.Id.ToString() + " es padre del id: " + hijo.Id.ToString();
                //}
              }
              else
              {
                AddError(serviceResponse, null, "Padre no encontrado");
              }
            }
            else
            {
              AddError(serviceResponse, null, "El hijo ya tiene un padre");
            }
          }
          else
          {
            AddError(serviceResponse, null, "Hijo no encontrado");
          }
        }
        else
        {
          AddError(serviceResponse, null, "No puede ser padre de si mismo");
        }
      }
      catch(Exception e)
      {
        AddError(serviceResponse, e, e.Message);
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<string>> GetRelationship(int id1, int id2)
    {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

      try
      {
        if (id1 != id2)
        {
          var getPerson1 = await GetPerson(id1);
          if (getPerson1.Success) //existe la persona 1
          {
            var getPerson2 = await GetPerson(id2);
            if (getPerson2.Success) //existe la persona 2
            {
              serviceResponse.Data = await VerifyRelationship(getPerson1.Data, getPerson2.Data);
            }
            else
            {
              AddError(serviceResponse, null, "No existe la persona 2");
            }
          }
          else
          {
            AddError(serviceResponse, null, "No existe la persona 1");
          }
        }
        else
        {
          AddError(serviceResponse, null, "No existen relaciones entre la misma persona");
        }
      }
      catch (Exception e)
      {
        AddError(serviceResponse, e, e.Message);
      }
      return serviceResponse;
    }

    private async Task<string> VerifyRelationship(Persona persona1, Persona persona2)
    {
      string response = "";
      //si tienen el mismo padre son hermanos
      //si tienen dos padres distintos pero entre ellos son hermanos entonces son primos (para que sean hermanos los dos padres tienen que ser hijos del mismo padre)
      //si uno de los dos es hijo del abuelo del otro uno es tio
      Persona padre1 = persona1.Padre;
      Persona padre2 = persona2.Padre;

      if (padre1 != null && padre2 != null)
      {
        if (padre1.Equals(padre2))
        {
          response = "HERMAN@";
        }
        else
        {
          ServiceResponse<Persona> getAbuelo1 = new ServiceResponse<Persona>();
          ServiceResponse<Persona> getAbuelo2 = new ServiceResponse<Persona>();
          if (persona1.Padre != null)
            getAbuelo1 = await GetPerson(persona1.Padre.Id);
          if (persona2.Padre != null)
            getAbuelo2 = await GetPerson(persona2.Padre.Id);

          Persona abuelo1 = new Persona();
          Persona abuelo2 = new Persona();
          if (getAbuelo1.Data != null)
            abuelo1 = getAbuelo1.Data.Padre;
          if (getAbuelo2.Data != null)
            abuelo2 = getAbuelo2.Data.Padre;

          if (abuelo1.Equals(abuelo2))
            response = "PRIM@";
          else if (abuelo1.Equals(padre2) || abuelo2.Equals(padre1))
            response = "TI@";
          else
            response = "NO TIENEN RELACION";
        }
      }
      else
      {
        response = "NO TIENEN RELACION";
      }

      return response;
    }
    #endregion
  }
}