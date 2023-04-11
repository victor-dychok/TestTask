using testTask.Data.Models;

namespace testTask.Data.Interfaces
{
    public interface IAllUsers
    {
        IEnumerable<User> Users { get; }
        void SetRoleUser(ref User user);
        void SetRoleAdmin(ref User user);
        void SetRoleUnregistred(ref User user);
        User Authenticate(string login, string password);

        IEnumerable<UserRole> GetRoles();

        UserRole GetRole(int id);

        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
