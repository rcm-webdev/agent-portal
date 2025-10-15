using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agent_portal.Data;
using agent_portal.Models;

namespace agent_portal.Controllers;

public class InsuranceProductsController : Controller
{
    private readonly AgentContext _context;

    public InsuranceProductsController(AgentContext context)
    {
        _context = context;
    }

    // GET: InsuranceProducts
    public async Task<IActionResult> Index()
    {
        return View(await _context.InsuranceProducts.ToListAsync());
    }

    // GET: InsuranceProducts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var insuranceProduct = await _context.InsuranceProducts
            .FirstOrDefaultAsync(m => m.Id == id);
        if (insuranceProduct == null)
        {
            return NotFound();
        }

        return View(insuranceProduct);
    }

    // GET: InsuranceProducts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: InsuranceProducts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Type,CoverageSummary,EligibilityNotes,QuickLinks")] InsuranceProduct insuranceProduct)
    {
        if (ModelState.IsValid)
        {
            _context.Add(insuranceProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(insuranceProduct);
    }

    // GET: InsuranceProducts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var insuranceProduct = await _context.InsuranceProducts.FindAsync(id);
        if (insuranceProduct == null)
        {
            return NotFound();
        }
        return View(insuranceProduct);
    }

    // POST: InsuranceProducts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,CoverageSummary,EligibilityNotes,QuickLinks")] InsuranceProduct insuranceProduct)
    {
        if (id != insuranceProduct.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(insuranceProduct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceProductExists(insuranceProduct.Id))
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
        return View(insuranceProduct);
    }

    // GET: InsuranceProducts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var insuranceProduct = await _context.InsuranceProducts
            .FirstOrDefaultAsync(m => m.Id == id);
        if (insuranceProduct == null)
        {
            return NotFound();
        }

        return View(insuranceProduct);
    }

    // POST: InsuranceProducts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var insuranceProduct = await _context.InsuranceProducts.FindAsync(id);
        if (insuranceProduct != null)
        {
            _context.InsuranceProducts.Remove(insuranceProduct);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InsuranceProductExists(int id)
    {
        return _context.InsuranceProducts.Any(e => e.Id == id);
    }
}