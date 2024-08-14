using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Models.DTO;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly Context _context;
        //private readonly IConfiguration _configuration;
        //public AuthController(Context context, IConfiguration configuration)
        //{
        //    _context = context;
        //    _configuration = configuration;
        //}

        //[HttpPost("Login")]
        //public Result Login([FromBody] UserLogin userLogin)
        //{
        //    if (userLogin.Username == "test" && userLogin.Password == "test")
        //    {
        //        var token = GenerateJwtToken();
        //        return new Result()
        //        {
        //            Data = token,
        //            Notification = new Notification()
        //            {
        //                Message = "you successfully logged in",
        //                Status = NotificationStatus.Success,
        //                Title = "successfully login"
        //            }
        //        };
        //    }

        //    throw new UnauthorizedException("userrname or password is incorrect");
        //}

        //[HttpPost("Register")]
        //public async  Task<Result> Register([FromBody] UserDto user)
        //{
        //    var userExist = _context.Users.Any(u => u.Email == user.Email);
        //    if (userExist)
        //    {
        //        return new Result()
        //        {
        //            Notification = new Notification()
        //            {
        //                Message = $"user with username {user.Email} already exists",
        //                Status = NotificationStatus.Info,
        //                Title = "already exists"
        //            }
        //        };
        //    }
        //    else
        //    {
        //        if(user.Password != user.ConfirmPassword)
        //        {
        //            return new Result()
        //            {
        //                Notification = new Notification()
        //                {
        //                    Message = $"password not mach",
        //                    Status = NotificationStatus.Warning,
        //                    Title = "not mach"
        //                }
        //            };
        //        }
        //        var newUser = new User()
        //        {
        //            Email = user.Email,
        //            Password = user.Password,
        //            Firstname = user.Firstname,
        //            Lastname = user.Lastname,
        //            Phone = user.Phone,
        //            Image = user.Image,
        //            Role = user.Role,
        //        };
        //        try
        //        {
        //            await _context.Users.AddAsync(newUser);
        //            await _context.SaveChangesAsync();
        //            return new Result() 
        //            { 
        //                Data = newUser,
        //                Notification = new Notification()
        //                {
        //                    Message = "registration complated successfully",
        //                    Status = NotificationStatus.Success,
        //                    Title = "succesfully registred"
        //                }
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("an error occured!");
        //        }
        //    }
        //}

        //private string GenerateJwtToken()
        //{
        //    var jwtSettings = _configuration.GetSection("Jwt");
        //    var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", "1") }),
        //        Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        Issuer = jwtSettings["Issuer"],
        //        Audience = jwtSettings["Audience"]
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
