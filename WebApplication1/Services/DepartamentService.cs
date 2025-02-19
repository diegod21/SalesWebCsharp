using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{ 
    public class DepartamentService
    {
        private readonly WebApplication1Context _context;
        public DepartamentService(WebApplication1Context context)
        {
            _context = context;
        }

        public async Task<List<Departament>> FindAllAsync()
        {
            return await _context.Departament.OrderBy(d => d.Name).ToListAsync();
        }
    }
}
