using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaCursos.Models;

[Table("Usuarios")]
public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<Intento> Intentos { get; set; } = new List<Intento>();

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
