using eCommerce.Api.DTOs;
using eCommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ApiDbContext dbContext, IConfiguration config) : ControllerBase
{
    [HttpPost("[action]")]
    public IActionResult Register([FromBody] User user)
    {
        var userExists = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
        if (userExists != null)
        {
            return BadRequest("Aynı E-Postaya sahip kullanıcı mevcut");
        }
        dbContext.Users.Add(user);

        dbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]")]
    public IActionResult Login([FromBody] UserLoginDTO user)
    {
        var currentUser = dbContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        if (currentUser == null)
        {
            return NotFound();
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Email , user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: config["JWT:Issuer"],
            audience: config["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(60),
            signingCredentials: credentials);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new ObjectResult(new
        {
            access_token = jwt,
            token_type = "bearer",
            user_id = currentUser.Id,
            user_name = currentUser.Name
        });

    }

    [Authorize]
    [HttpPost("uploadphoto")]
    public IActionResult UploadUserPhoto(IFormFile image)
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = dbContext.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null)
        {
            return NotFound("Kullanıcı Bulunumadı !");
        }

        if (image != null)
        {
            // Generate a unique filename for the uploaded image (e.g., using a GUID)
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine("wwwroot/userimages", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            // Update the user's ImageUrl property with the URL of the uploaded image
            user.ImageUrl = "/userimages/" + uniqueFileName; 

                
            dbContext.SaveChanges();

            return Ok("Görsel Yükleme Başarılı");
        }

        return BadRequest("Görüntü Yüklenmedi");
    }



    [Authorize]
    [HttpGet("profileimage")]
    public IActionResult UserProfileImage()
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
        if (user == null) return NotFound();
        var responseResult = dbContext.Users
            .Where(x => x.Email == userEmail)
            .Select(x => new
            {
                x.ImageUrl,
            })
            .SingleOrDefault();
        return Ok(responseResult);
    }
}