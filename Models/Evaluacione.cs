using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaCursos.Models;

[Table("Evaluaciones")]
public partial class Evaluacione
{
    public int EvaluacionId { get; set; }

    public int CursoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual ICollection<Intento> Intentos { get; set; } = new List<Intento>();

    public virtual ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
}
