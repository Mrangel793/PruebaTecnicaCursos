using Microsoft.EntityFrameworkCore;
using PruebaCursos.Models;

public class EvaluacionService
{
    private readonly PlataformaEvaluacionCursosDev4Context _context;

    public EvaluacionService(PlataformaEvaluacionCursosDev4Context context)
    {
        _context = context;
    }

    // Obtener evaluaciones por curso
    public async Task<List<Evaluacione>> GetEvaluacionesPorCursoAsync(int cursoId)
    {
        return await _context.Evaluaciones
            .Where(e => e.CursoId == cursoId)
            .Include(e => e.Curso)
            .ToListAsync();
    }

    // Crear una nueva evaluación
    public async Task<bool> CrearEvaluacionAsync(Evaluacione evaluacion)
    {
        try
        {
            _context.Evaluaciones.Add(evaluacion);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Obtener historial de evaluaciones de un usuario
    //public async Task<List<Evaluacione>> GetHistorialEvaluacionesPorUsuarioAsync(int usuarioId)
    //{
    //    return await _context.Intentos
    //        .Where(i => i.UsuarioId == usuarioId)
    //        .Select(i => i.Evaluacion)
    //        .Include(e => e.Curso)
    //        .ToListAsync();
    //}

    public async Task<List<Evaluacione>> GetHistorialEvaluacionesPorUsuarioAsync(int usuarioId)
    {
        // Obtener los IDs de los cursos en los que está inscrito el usuario
        var cursosIds = await _context.Inscripciones
            .Where(i => i.UsuarioId == usuarioId)
            .Select(i => i.CursoId)
            .ToListAsync();

        // Obtener las evaluaciones de esos cursos
        var evaluaciones = await _context.Evaluaciones
            .Where(e => cursosIds.Contains(e.CursoId))
            .Include(e => e.Curso) // Incluir información del curso
            .ToListAsync();

        Console.WriteLine($"Se encontraron {evaluaciones.Count} evaluaciones para el usuario {usuarioId}");

        return evaluaciones;
    }

}
