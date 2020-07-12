using GestionDeUsuarios.Models.Enums;

namespace GestionDeUsuarios.Dtos
{
  public class GetPersonaDto
  {
    public TipoDocumento TipoDocumento { get; set; }
    public string Documento { get; set; }
    public Pais Pais { get; set; }
    public Sexo Sexo { get; set; }
  }
}