using WebApplication1.DBcontext;
using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Services
{
    public class NotificationService
    {
        private readonly FASTSocietyManagementContext _context;

        public NotificationService(FASTSocietyManagementContext context)
        {
            _context = context;
        }
        // Add notification service.
        public void AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
        public List<Notification> GetNotifications()
        {
            return _context.Notifications.ToList();
        } 
    }
}
