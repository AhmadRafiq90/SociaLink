using WebApplication1.Models;
using WebApplication1.DBcontext;

namespace WebApplication1.Services
{
    public class SocietyMembershipService
    {
        private readonly FASTSocietyManagementContext _context;

        public SocietyMembershipService(FASTSocietyManagementContext context)
        {
            _context = context;
        }
        // Add membership.
        public void AddMembership(SocietyMembership membership)
        {
            _context.SocietyMemberships.Add(membership);
            _context.SaveChanges();
        }
        public List<SocietyMembership> GetNotifications()
        {
            return _context.SocietyMemberships.ToList();
        }
    }
}
