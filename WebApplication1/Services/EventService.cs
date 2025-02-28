using WebApplication1.DBcontext;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class EventService
    {
        private readonly FASTSocietyManagementContext _context;

        public EventService(FASTSocietyManagementContext context)
        {
            _context = context;
        }
        // add event
        public void AddEvent(Event EVENT)
        {
            _context.Events.Add(EVENT);
            _context.SaveChanges();
        }
        public List<Event> GetSocieties()
        {
            return _context.Events.ToList();
        }
    }
}
