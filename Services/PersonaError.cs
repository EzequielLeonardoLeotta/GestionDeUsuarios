using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using GestionDeUsuarios.Models.Response;
using System;
using System.Collections.Generic;

namespace GestionDeUsuarios.Services
{
  public class PersonaError : IPersonaError
  {
    public void AddError(ServiceResponse<Dictionary<string, int>> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    public void AddError(ServiceResponse<PersonaDto> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    public void AddError(ServiceResponse<List<PersonaDto>> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    public void AddError(ServiceResponse<Persona> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }

    public void AddError(ServiceResponse<string> serviceResponse, Exception e, string message)
    {
      serviceResponse.Success = false;
      if (e is null)
        serviceResponse.Message = message;
      else if (e.Message.Contains("Enumerator failed to MoveNextAsync."))
        serviceResponse.Message = message;
      else
        serviceResponse.Message = e.Message;
    }
  }
}