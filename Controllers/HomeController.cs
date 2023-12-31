﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CAPATHON.Models;

namespace CAPATHON.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult Pricing()
    {
        return View();
    }

     public IActionResult Calendar()
    {
        return View();
    }

        public IActionResult CheckIn()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Locations()
    {
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
