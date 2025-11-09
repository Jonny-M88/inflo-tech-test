using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data.Enum;
using UserManagement.Data.Interfaces;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Whomever came up with this user list deserves a raise.
        _configureEntity<User>(modelBuilder, new[]
        {
        new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", DateOfBirth = new DateOnly(1989, 06, 02),  IsActive = true, Quote = "I'M A VAMPIRE!" },
        new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", DateOfBirth = new DateOnly(2004, 12, 26), IsActive = true, Quote = "I'm gonna steal the Declaration of Independence"},
        new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", DateOfBirth = new DateOnly(1997, 06, 27), IsActive = false, Quote = "I could eat a peach for hours" },
        new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", DateOfBirth = new DateOnly(2000, 06, 09), IsActive = true, Quote = "Hey man, I thought you were from Long Beach" },
        new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", DateOfBirth = new DateOnly(1996, 06, 07), IsActive = true, Quote = "Do you know how this **** works?"},
        new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", DateOfBirth = new DateOnly(1987, 07, 10), IsActive = true, Quote = "I'll be taking these Huggies and whatever cash ya got" },
        new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", DateOfBirth = new DateOnly(1997, 06, 06), IsActive = false, Quote = "Put the bunny back in the box" },
        new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", DateOfBirth = new DateOnly(2006, 09, 06), IsActive = false, Quote = "NOT THE BEEEES!" },
        new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", DateOfBirth = new DateOnly(2010, 03, 10), IsActive = false, Quote = "You're gonna be fine babydoll!" },
        new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", DateOfBirth = new DateOnly(2007, 02, 16), IsActive = true, Quote = "My daddy once said, 'If you don't make a choice, the choice makes you" },
        new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", DateOfBirth = new DateOnly(2021, 08, 20), IsActive = true, Quote = "The critics the customers, none of them are real because you aren't real" },
        });

        _configureEntity<LogRecord>(modelBuilder, new[]
        {
            new LogRecord { Id = 1, Action = LogAction.Create, ActionDate = new DateTime(1989, 06, 02), EntityId = 1, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 2, Action = LogAction.Create, ActionDate = new DateTime(2004, 12, 26), EntityId = 2, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 3, Action = LogAction.Create, ActionDate = new DateTime(1997, 06, 27), EntityId = 3, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 4, Action = LogAction.Create, ActionDate = new DateTime(2000, 06, 09), EntityId = 4, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 5, Action = LogAction.Create, ActionDate = new DateTime(1996, 06, 07), EntityId = 5, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 6, Action = LogAction.Create, ActionDate = new DateTime(1987, 07, 10), EntityId = 6, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 7, Action = LogAction.Create, ActionDate = new DateTime(1997, 06, 06), EntityId = 7, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 8, Action = LogAction.Create, ActionDate = new DateTime(2006, 09, 06), EntityId = 8, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 9, Action = LogAction.Create, ActionDate = new DateTime(2010, 03, 10), EntityId = 9, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 10, Action = LogAction.Create, ActionDate = new DateTime(2007, 02, 16), EntityId = 10, PerformedBy = "SYSTEM" },
            new LogRecord { Id = 11, Action = LogAction.Create, ActionDate = new DateTime(2021, 08, 20), EntityId = 11, PerformedBy = "SYSTEM" }
        });
    }

    public DbSet<User>? Users { get; set; }

    public async Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IEntity
    {
        return await Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class, IEntity
    {
        return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<List<TEntity>> GetActiveAsync<TEntity>() where TEntity : class, IEntity
    {
        return await Set<TEntity>().Where(e => e.IsActive).ToListAsync();
    }
    public async Task<List<TEntity>> GetInactiveAsync<TEntity>() where TEntity : class, IEntity
    {
        return await Set<TEntity>().Where(e => e.IsActive == false).ToListAsync();
    }
    public async Task<List<TEntity>> GetByEntityIdAsync<TEntity>(long id) where TEntity : class, IEntity
    {
        return await Set<TEntity>().Where(e => e.EntityId == id).ToListAsync();
    }
    public async Task<long> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        base.Add(entity);
        await base.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<long> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        base.Update(entity);
        await base.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        base.Remove(entity);
        await base.SaveChangesAsync();
    }


    //PRIVATE METHODS
    private void _configureEntity<TEntity>(ModelBuilder modelBuilder, IEnumerable<TEntity> seedData)
    where TEntity : class
    {
        //Auto increment Id property value on row entry
        modelBuilder.Entity<TEntity>()
            .Property("Id")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TEntity>().HasData(seedData);
    }
}
