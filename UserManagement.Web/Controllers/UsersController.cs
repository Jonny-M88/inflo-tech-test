using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("filter")]
    public ViewResult FilterByActive(bool isActive)
    {
        var items = _userService.FilterByActive(isActive)
         .Select(u => new UserListItemViewModel
         {
             Id = u.Id,
             Forename = u.Forename,
             Surname = u.Surname,
             Email = u.Email,
             DateOfBirth = u.DateOfBirth,
             IsActive = u.IsActive
         })
         .ToList();

        UserListViewModel model = new UserListViewModel
        {
            Items = items
        };

        return View("List", model);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new UserListItemViewModel();
        return View(model);
    }
}
