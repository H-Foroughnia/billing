using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using billing.Models;

namespace billing.Controllers;

public class HomeController : SiteBaseController
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult success()
    {
        return View();
    }
    
}