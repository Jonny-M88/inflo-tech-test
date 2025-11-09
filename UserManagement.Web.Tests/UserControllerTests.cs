using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService = new();
        private UsersController CreateController() => new(_userService.Object);

        [Fact]
        public async Task List_WhenServiceReturnsUsers_ModelMustContainUsers()
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

            _userService
                .Setup(s => s.GetAllUsersAsync())
                .ReturnsAsync(users);

            var controller = CreateController();

            var result = await controller.List();

            result.Model
                .Should().BeOfType<UserListViewModel>()
                .Which.Items.Should().BeEquivalentTo(users);
        }
    }
}
