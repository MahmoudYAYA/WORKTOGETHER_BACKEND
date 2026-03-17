using WORKTOGETHER.DATA.Entities;
using WORKTOGETHER.DATA.Repositories;

namespace WORKTOGETHER.DATA.Repositories
{
    public class UserRepository : Repository<User>
    {
        public User FindByEmail(string email )
        {
            return table.FirstOrDefault(u => u.Email == email);
        }


        public List<User> FindClientAdtifs()
        {
            return table
                .Where(u => u.Actif == 1 && u.Roles.Contains("ROLE_CLIENTE"))
                .ToList();
        }
    }

}