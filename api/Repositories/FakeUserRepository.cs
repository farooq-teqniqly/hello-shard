using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Models;

namespace api.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> userStore = new Dictionary<string, User>();

        public Task<IEnumerable<User>> GetTopUsers(int count)
        {
            var topUsers = this.userStore
                .OrderByDescending(kvp => kvp.Value.Clicks)
                .Take(count)
                .Select(kvp => kvp.Value);

            return Task.FromResult(topUsers);
        }

        public Task<User> RecordClick(string userId)
        {
            if (!this.userStore.ContainsKey(userId))
            {
                throw new InvalidOperationException($"User with id {userId} does not exist.!");
            }

            var user = this.userStore[userId];
            user.Clicks++;

            return Task.FromResult(user);
        }

        public Task<User> GetUserDetails(string userId)
        {
            if (!this.userStore.ContainsKey(userId))
            {
                return null;
            }

            return Task.FromResult(this.userStore[userId]);
        }

        public Task<User> AddUser(AddUserModel model)
        {
            if (this.userStore.ContainsKey(model.Id))
            {
                throw new InvalidOperationException($"User with id {model.Id} already exists.!");
            }

            var user = new User
            {
                Id = model.Id,
                Clicks = 0,
                ImageUrl = model.ImageUrl,
                Name = model.Name
            };
            
            this.userStore.Add(user.Id, user);

            return Task.FromResult(user);
        }
    }
}