using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;
using GestionDeUsuarios.Models.Response;
using System;
using System.Collections.Generic;

namespace GestionDeUsuarios.Services
{
  interface IPersonaError
  {
    public void AddError(ServiceResponse<Dictionary<string, int>> serviceResponse, Exception e, string message);
    public void AddError(ServiceResponse<PersonaDto> serviceResponse, Exception e, string message);
    public void AddError(ServiceResponse<List<PersonaDto>> serviceResponse, Exception e, string message);
    public void AddError(ServiceResponse<Persona> serviceResponse, Exception e, string message);
    public void AddError(ServiceResponse<string> serviceResponse, Exception e, string message);
  }
}