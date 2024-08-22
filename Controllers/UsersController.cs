using System;
using Microsoft.EntityFrameworkCore;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;


// a public class that returns a view called Create for creating a user to a database tht has id, username and password
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        return View(_context.Users.ToList());
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
    //action for editing a user
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    // action for updating a user
    [HttpPost]

    public async Task<IActionResult> Edit(int id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
    //deletign a user

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users
           .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _context.Users.FindAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
}