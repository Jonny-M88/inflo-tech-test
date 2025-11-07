using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Enum;
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
        var items = _userService.GetAll().Select(u => new UserListItemViewModel
        {
            Id = u.Id,
            Forename = u.Forename,
            Surname = u.Surname,
            Email = u.Email,
            DateOfBirth = u.DateOfBirth,
            IsActive = u.IsActive
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
        return View("UserView", model);
    }

    [HttpGet("/details/{id}/{mode?}")]
    public IActionResult Details(long id, string? mode)
    {
        {
            var entity = _userService.GetById(id).FirstOrDefault();
            if (entity == null)
                return NotFound();

            UserListItemViewModel model = new()
            {
                Id = entity.Id,
                Forename = entity.Forename,
                Surname = entity.Surname,
                Email = entity.Email,
                DateOfBirth = entity.DateOfBirth,
                IsActive = entity.IsActive,
                Mode = mode == "view" ? FormMode.View : FormMode.Edit
            };

            return View("UserView", model);
        }
    }

    [HttpPost]
    public IActionResult Save(UserListItemViewModel model)
    {

        if (!ModelState.IsValid)
            return View("UserView", model);

        //Create new
        if (model.Mode == FormMode.Create)
        {
            var entity = new User
            {
                Forename = model.Forename!,
                Surname = model.Surname!,
                Email = model.Email!,
                DateOfBirth = model.DateOfBirth!.Value,
                IsActive = model.IsActive
            };
            _userService.Create(entity);
        }
        else //Update existing
        {
            var entity = _userService.GetById(model.Id).FirstOrDefault();
            if (entity == null) return NotFound();

            entity.Forename = model.Forename!;
            entity.Surname = model.Surname!;
            entity.Email = model.Email!;
            entity.DateOfBirth = model.DateOfBirth!.Value;
            entity.IsActive = model.IsActive;

            _userService.Update(entity);
        }

        return RedirectToAction("List");
    }

    [HttpGet("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var entity = _userService.GetById(id).FirstOrDefault();
        if (entity == null) return NotFound();

        _userService.Delete(entity);
        return RedirectToAction("List");
    }
}
