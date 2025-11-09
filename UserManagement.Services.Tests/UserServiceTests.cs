using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IDataContext> _dataContext = new();
        private UserService CreateService() => new(_dataContext.Object);

        [Fact]
        public async Task GetAllUsersAsync_WhenContextReturnsEntities_ShouldReturnSameEntities()
        {

            var users = new List<User>
            {
                new User
                {
                    Forename = "Johnny",
                    Surname = "User",
                    Email = "juser@example.com",
                    IsActive = true
                }
            };

            _dataContext
                .Setup(s => s.GetAllAsync<User>())
                .ReturnsAsync(users);

            var service = CreateService();

            List<User> result = await service.GetAllUsersAsync();

            result.Should().BeSameAs(users);
        }
    }
}
