using GestionDeUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionDeUsuarios.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Persona> Personas { get; set; }
  }
}