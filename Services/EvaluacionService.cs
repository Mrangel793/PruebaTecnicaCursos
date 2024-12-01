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
    public async Task<List<Evaluacione>> GetHistorialEvaluacionesPorUsuarioAsync(int usuarioId)
    {
        return await _context.Intentos
            .Where(i => i.UsuarioId == usuarioId)
            .Select(i => i.Evaluacion)
            .Include(e => e.Curso)
            .ToListAsync();
    }
}
