﻿using AutoMapper;
using GestionDeUsuarios.Dtos;
using GestionDeUsuarios.Models;

namespace GestionDeUsuarios.Maps
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Persona, PersonaDto>();
      CreateMap<PersonaDto, Persona>();
      CreateMap<UpdatePersonDto, Persona>();
    }
  }
}