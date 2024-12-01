using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaCursos.Models;

[Table("Cursos")]
public partial class Curso
{
    public int CursoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}
