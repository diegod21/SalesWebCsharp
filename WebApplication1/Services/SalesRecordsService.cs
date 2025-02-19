using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SalesRecordsService
    {
        private readonly WebApplication1Context _context;
        public SalesRecordsService(WebApplication1Context context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result.Where(x => x.Date <= maxDate.Value);
            }

            return await result.Include(x => x.Seller)
                    .Include(x => x.Seller.Departament)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();
        }

        public async Task<List<IGrouping<Departament, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Departament)
                .ToListAsync();
        }
    }
}
