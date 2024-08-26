using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleMakes.Authentcations;


namespace VehicleMakes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController (JwtOptions jwtOptions,VehicleMakesDbContext DbContext): ControllerBase
    {

       
      
        [HttpPost]
        [Route("auth")]
        public ActionResult<String> AuthenticationUser(AuthenticationRequest request)
        {
            var user = DbContext.Set<User>().FirstOrDefault(x => x.Name==request.UserName&&
            x.Password==request.Password);
            if (user==null)
                return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials =new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                SecurityAlgorithms.HmacSha256),
                Subject =new ClaimsIdentity(new Claim[]
                {
                       new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                       new(ClaimTypes.Name,user.Name)

                })
            };
            var SecurityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(SecurityToken);
            return Ok(accessToken);
        }
    }
}
