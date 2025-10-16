using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agent_portal.Data;
using agent_portal.Models;

namespace agent_portal.Controllers;

public class PoliciesController : Controller
{
    private readonly AgentContext _context;

    public PoliciesController(AgentContext context)
    {
        _context = context;
    }

    // GET: Policies
    public async Task<IActionResult> Index()
    {
        var policies = _context.Policies.Include(p => p.Product).Include(p => p.Client).Include(p => p.Agent);
        return View(await policies.ToListAsync());
    }

    // GET: Policies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var policy = await _context.Policies
            .Include(p => p.Product)
            .Include(p => p.Client)
            .Include(p => p.Agent)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (policy == null)
        {
            return NotFound();
        }

        return View(policy);
    }

    // GET: Policies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Policies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ProductId,ClientId,AgentId")] Policy policy)
    {
        if (ModelState.IsValid)
        {
            _context.Add(policy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(policy);
    }

    // GET: Policies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
        {
            return NotFound();
        }
        return View(policy);
    }

    // POST: Policies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ClientId,AgentId")] Policy policy)
    {
        if (id != policy.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(policy);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(policy.Id))
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
        return View(policy);
    }

    // GET: Policies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var policy = await _context.Policies
            .Include(p => p.Product)
            .Include(p => p.Client)
            .Include(p => p.Agent)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (policy == null)
        {
            return NotFound();
        }

        return View(policy);
    }

    // POST: Policies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy != null)
        {
            _context.Policies.Remove(policy);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PolicyExists(int id)
    {
        return _context.Policies.Any(e => e.Id == id);
    }
}