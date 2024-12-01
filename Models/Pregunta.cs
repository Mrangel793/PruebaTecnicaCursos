using System;
using System.Collections.Generic;

namespace PruebaCursos.Models;

public partial class Pregunta
{
    public int PreguntaId { get; set; }

    public int EvaluacionId { get; set; }

    public string Texto { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual Evaluacione Evaluacion { get; set; } = null!;

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();

    public virtual ICollection<RespuestasUsuario> RespuestasUsuarios { get; set; } = new List<RespuestasUsuario>();
}
