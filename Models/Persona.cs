namespace GestionDeUsuarios.Models
{
  public class Persona
  {
    public string TipoDocumento { get; set; } = "DNI";
    public string Documento { get; set; } = "40423033";
    public string Pais { get; set; } = "Argentina";
    public string Sexo { get; set; } = "Masculino";
    public int Edad { get; set; } = 18;
    public string Contacto { get; set; } = "leotta010@hotmail.com";
    public string Nombre { get; set; } = "Ezequiel";
    public string Apellido { get; set; } = "Leotta";
  }
}