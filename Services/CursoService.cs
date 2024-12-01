// Services/CursoService.cs
using Microsoft.EntityFrameworkCore;
using PruebaCursos.Models;

namespace PruebaCursos.Services
{
    public class CursoService
    {
        private readonly PlataformaEvaluacionCursosDev4Context _context;

        public CursoService(PlataformaEvaluacionCursosDev4Context context)
        {
            _context = context;
        }

        // Obtener todos los cursos
        public async Task<List<Curso>> GetAllCursosAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        // Obtener un curso por ID
        public async Task<Curso> GetCursoByIdAsync(int id)
        {
            return await _context.Cursos.FirstOrDefaultAsync(c => c.CursoId == id);
        }


    }
}
