using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Entities;
using api.Models;

namespace api.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetTopUsers(int count);

        Task<User> RecordClick(string userId);
        Task<User> GetUserDetails(string userId);
        Task<User> AddUser(AddUserModel model);
    }
}