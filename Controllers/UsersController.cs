using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly WebApiContext _context;
    private readonly IRabbitMQService _rabbitMQService;

    public UsersController(WebApiContext context, IRabbitMQService rabbitMQService)
    {
        _rabbitMQService = rabbitMQService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] User user)
    {
        user.RegistrationDate = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _rabbitMQService.SendMessage($"User with ID: {user.Id}, Name: {user.Name} has been registered ({user.RegistrationDate})");

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return Ok(users);
    }
}