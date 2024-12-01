using System;
using System.Collections.Generic;

namespace PruebaCursos.Models;

public partial class Inscripcione
{
    public int InscripcionId { get; set; }

    public int UsuarioId { get; set; }

    public int CursoId { get; set; }

    public DateTime FechaInscripcion { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
