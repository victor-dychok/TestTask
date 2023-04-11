using Microsoft.EntityFrameworkCore;
using testTask.Data.Interfaces;
using testTask.Data.Models;

namespace testTask.Data.Repository
{
    public class UserRepository : IAllUsers
    {
        private readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Users => _context.Users.Include(r => r.Role);

        public void AddUser(User user)
        {
            if(user != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
        public void DeleteUser(User user)
        {
            if (user != null)
            {
                if(user.Role.Name == "Admin")
                {
                    var admins = _context.Users.Where(Users => Users.Role.Name == "Admin").ToList();
                    if(admins.Count > 1)
                    {
                        _context.Users.Remove(user);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
        }

        public void UpdateUser(User user)
        {
            if (user != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public void SetRoleUser(ref User user)
        {
            user.Role = _context.Roles.First(r => r.Name == "User");
        }

        public void SetRoleAdmin(ref User user)
        {
            user.Role = _context.Roles.First(r => r.Name == "Admin");
        }

        public void SetRoleUnregistred(ref User user)
        {
            user.Role = _context.Roles.First(r => r.Name == "Unregistered");
        }

        public IEnumerable<UserRole> GetRoles() => _context.Roles;

        public User Authenticate(string login, string password)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login);

            return user;
        }

        public UserRole GetRole(int id) => _context.Roles.FirstOrDefault(u => u.Id == id);
    }
}
