using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaCursos.Models;

[Table("Intentos")]
public partial class Intento
{
    public int IntentoId { get; set; }

    public int UsuarioId { get; set; }

    public int EvaluacionId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public double? Calificacion { get; set; }

    public virtual Evaluacione Evaluacion { get; set; } = null!;

    public virtual ICollection<RespuestasUsuario> RespuestasUsuarios { get; set; } = new List<RespuestasUsuario>();

    public virtual Usuario Usuario { get; set; } = null!;
}
