using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agent_portal.Data;
using agent_portal.Models;

namespace agent_portal.Controllers;

public class ClaimsController : Controller
{
    private readonly AgentContext _context;

    public ClaimsController(AgentContext context)
    {
        _context = context;
    }

    // GET: Claims
    public async Task<IActionResult> Index()
    {
        var claims = _context.Claims.Include(c => c.Policy);
        return View(await claims.ToListAsync());
    }

    // GET: Claims/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var claim = await _context.Claims
            .Include(c => c.Policy)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (claim == null)
        {
            return NotFound();
        }

        return View(claim);
    }

    // GET: Claims/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Claims/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PolicyId")] Claim claim)
    {
        if (ModelState.IsValid)
        {
            _context.Add(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(claim);
    }

    // GET: Claims/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var claim = await _context.Claims.FindAsync(id);
        if (claim == null)
        {
            return NotFound();
        }
        return View(claim);
    }

    // POST: Claims/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,PolicyId")] Claim claim)
    {
        if (id != claim.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(claim);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimExists(claim.Id))
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
        return View(claim);
    }

    // GET: Claims/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var claim = await _context.Claims
            .Include(c => c.Policy)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (claim == null)
        {
            return NotFound();
        }

        return View(claim);
    }

    // POST: Claims/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim != null)
        {
            _context.Claims.Remove(claim);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClaimExists(int id)
    {
        return _context.Claims.Any(e => e.Id == id);
    }
}