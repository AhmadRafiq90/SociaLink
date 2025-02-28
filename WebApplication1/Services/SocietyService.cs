using WebApplication1.DBcontext;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SocietyService
    {
        private readonly FASTSocietyManagementContext _context;

        public SocietyService(FASTSocietyManagementContext context)
        {
            _context = context;
        }
        // Add society
        public void AddSociety(Society society)
        {
            _context.Societies.Add(society);
            _context.SaveChanges();
        }
        public List<Society> GetSocieties()
        {
            return _context.Societies.ToList();
        }
    }
}
