using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Enum;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Constants;
using UserManagement.Web.Enum;
using UserManagement.Web.Models.Logs;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route(Constants.BaseUsersRoute)]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets all users via the user service
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ViewResult> List()
    {

        var items = (await _userService.GetAllUsersAsync())
            .Select(u => new UserListItemViewModel
            {
                Id = u.Id,
                Forename = u.Forename,
                Surname = u.Surname,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth,
                IsActive = u.IsActive,
                Quote = u.Quote
            })
            .ToList();

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    /// <summary>
    /// Gets a list of active or inactive users via the user service
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    [HttpGet(Constants.FilterUsersRoute)]
    public async Task<ViewResult> FilterByActive(bool isActive)
    {
        var items = (await _userService.FilterUsersByActiveAsync(isActive))
         .Select(u => new UserListItemViewModel
         {
             Id = u.Id,
             Forename = u.Forename,
             Surname = u.Surname,
             Email = u.Email,
             DateOfBirth = u.DateOfBirth,
             IsActive = u.IsActive,
             Quote = u.Quote
         })
         .ToList();

        UserListViewModel model = new UserListViewModel
        {
            Items = items
        };

        return View("List", model);
    }

    /// <summary>
    /// Displays the UserView view in create mode
    /// </summary>
    /// <returns></returns>
    [HttpGet(Constants.CreateUsersRoute)]
    public IActionResult Create()
    {
        var model = new UserDetailsListViewModel();
        return View("UserView", model);
    }

    /// <summary>
    /// Displays the UserView view in either View or Edit mode
    /// </summary>
    /// <param name="id">The Id of the selected User</param>
    /// <param name="mode">The mode in which to open the UserView view</param>
    /// <returns></returns>
    [HttpGet(Constants.DetailsUserRoute)]
    public async Task<IActionResult> Details(long id, string? mode)
    {
        User? user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        UserDetailsListViewModel model = new()
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive,
            Mode = mode == "view" ? FormMode.View : FormMode.Edit,
            Quote = user.Quote
        };

        //Only get logs for View mode, we don't show them any other time.
        if (mode == "view")
        {
            model.Logs = (await _userService.GetLogRecordsByEntityIdAsync(id))
                .Select(log => new LogRecordListItemViewModel
                {
                    Id = log.Id,
                    EntityId = log.EntityId,
                    Action = log.Action,
                    ActionDate = log.ActionDate,
                    IsActive = log.IsActive,
                    PerformedBy = log.PerformedBy
                })
                .ToList();
        }

        return View("UserView", model);
    }

    /// <summary>
    /// Commits changes to a new or existing user
    /// </summary>
    /// <param name="model">The model to be committed to the database</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SaveAsync(UserDetailsListViewModel model)
    {

        if (!ModelState.IsValid)
            return View("UserView", model);

        long affectedEntityId = -1;

        //Create new
        if (model.Mode == FormMode.Create)
        {
            User entity = new()
            {
                Forename = model.Forename!,
                Surname = model.Surname!,
                Email = model.Email!,
                DateOfBirth = model.DateOfBirth!.Value,
                IsActive = model.IsActive
            };
            affectedEntityId = await _userService.CreateAsync(entity);

        }
        else //Update existing
        {
            var userEntity = await _userService.GetUserByIdAsync(model.Id);
            if (userEntity == null) return NotFound();

            userEntity.Forename = model.Forename!;
            userEntity.Surname = model.Surname!;
            userEntity.Email = model.Email!;
            userEntity.DateOfBirth = model.DateOfBirth!.Value;
            userEntity.IsActive = model.IsActive;

            affectedEntityId = await _userService.UpdateAsync(userEntity);
        }

        if (affectedEntityId != -1)
        {
            string username = Environment.UserName;

            LogRecord logRecord = new()
            {
                Action = model.Mode == FormMode.Create ? CommitAction.Create : CommitAction.Update,
                ActionDate = DateTime.UtcNow,
                EntityId = affectedEntityId,
                PerformedBy = !string.IsNullOrWhiteSpace(username) ? username : Constants.DefaultUsername
            };
            await _userService.CreateAsync(logRecord);
        }

        return RedirectToAction("List");
    }

    /// <summary>
    /// Deletes an existing user
    /// </summary>
    /// <param name="id">The Id of the user to be deleted</param>
    /// <returns></returns>
    [HttpGet(Constants.DeleteUserRoute)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var entity = await _userService.GetUserByIdAsync(id);
        if (entity == null) return NotFound();

        await _userService.DeleteAsync(entity);
        return RedirectToAction("List");
    }
}
