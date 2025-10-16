using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agent_portal.Models;
using agent_portal.Data;

namespace agent_portal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AgentContext _context;

    public HomeController(ILogger<HomeController> logger, AgentContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.ClientCount = _context.Clients.Count();
        ViewBag.PolicyCount = _context.Policies.Count();
        ViewBag.ClaimCount = _context.Claims.Count();
        ViewBag.AgentCount = _context.Agents.Count();
        ViewBag.ProductCount = _context.InsuranceProducts.Count();
        ViewBag.RecentClaims = _context.Claims.Include(c => c.Policy).OrderByDescending(c => c.Id).Take(5).ToList();
        ViewBag.RecentPolicies = _context.Policies.Include(p => p.Client).Include(p => p.Product).OrderByDescending(p => p.Id).Take(5).ToList();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
