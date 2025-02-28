using WebApplication1.Models;
using WebApplication1.DBcontext;
using System.Collections.Generic;
namespace WebApplication1.Services
{
    public class AnnouncementService
    {
    private readonly FASTSocietyManagementContext _context;

        public AnnouncementService(FASTSocietyManagementContext context)
        {
            _context = context;
        }
        // add announcement.
        public void AddAnnouncement(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            _context.SaveChanges();
        }
        public List<Announcement> GetAnnouncements()
        {
            return _context.Announcements.ToList();
        }
    }
}
