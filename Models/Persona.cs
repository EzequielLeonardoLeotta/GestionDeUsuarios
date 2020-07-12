using GestionDeUsuarios.Models.Enums;
using System;

namespace GestionDeUsuarios.Models
{
  public class Persona
  {
    public int Id { get; set; }
    public TipoDocumento TipoDocumento { get; set; } 
    public string Documento { get; set; } 
    public Pais Pais { get; set; } 
    public Sexo Sexo { get; set; } 
    public int Edad { get; set; } 
    public string Contacto { get; set; }
    public string Nombre { get; set; } 
    public string Apellido { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public Persona Padre { get; set; }
  }
}