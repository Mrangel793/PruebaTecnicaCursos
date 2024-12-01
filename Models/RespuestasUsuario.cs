using System;
using System.Collections.Generic;

namespace PruebaCursos.Models;

public partial class RespuestasUsuario
{
    public int RespuestaUsuarioId { get; set; }

    public int IntentoId { get; set; }

    public int PreguntaId { get; set; }

    public int? RespuestaId { get; set; }

    public string? RespuestaAbierta { get; set; }

    public virtual Intento Intento { get; set; } = null!;

    public virtual Pregunta Pregunta { get; set; } = null!;

    public virtual Respuesta? Respuesta { get; set; }
}
