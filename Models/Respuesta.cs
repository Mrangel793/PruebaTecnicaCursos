using System;
using System.Collections.Generic;

namespace PruebaCursos.Models;

public partial class Respuesta
{
    public int RespuestaId { get; set; }

    public int PreguntaId { get; set; }

    public string Texto { get; set; } = null!;

    public bool EsCorrecta { get; set; }

    public virtual Pregunta Pregunta { get; set; } = null!;

    public virtual ICollection<RespuestasUsuario> RespuestasUsuarios { get; set; } = new List<RespuestasUsuario>();
}
