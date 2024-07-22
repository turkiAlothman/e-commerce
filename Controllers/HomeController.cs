using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Models;
using e_commerce.Domain.Repositories.Interfaces;
using e_commerce.Domain.Models;

namespace e_commerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;

    public HomeController(ILogger<HomeController> logger,IProductRepository _productRepository)
    {
        this._logger = logger;
        this._productRepository = _productRepository;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> details(string id)
    {
        Product product =  await _productRepository.getById(id);
        if (product == null) return NotFound();
        return View(product);
    }
    public IActionResult cart()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
