namespace GestionDeUsuarios.Models
{
  public class Persona
  {
    public int Id { get; set; }
    public string TipoDocumento { get; set; } 
    public string Documento { get; set; } 
    public string Pais { get; set; } 
    public string Sexo { get; set; } 
    public int Edad { get; set; } 
    public string Contacto { get; set; }
    public string Nombre { get; set; } 
    public string Apellido { get; set; } 
  }
}