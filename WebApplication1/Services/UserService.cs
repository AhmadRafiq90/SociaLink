using WebApplication1.Models;
using WebApplication1.DBcontext;
namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly FASTSocietyManagementContext _context;
        public UserService(FASTSocietyManagementContext context)
        {
            _context = context;
        }
        // Add a user
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
