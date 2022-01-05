using ArieotechLive.Model;
using ArieotechLive.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArieotechLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepositiory loginRepository;
        private readonly IUserRepositiory userRepositiory;

        public LoginController(ILoginRepositiory loginRepository, IUserRepositiory userRepositiory)
        {
            this.loginRepository = loginRepository;
            this.userRepositiory = userRepositiory;
        }
        [Route("/Login")]
        [HttpPost]
        public ActionResult Authenticate(Login login)
        {
            ActionResult result;

            try
            {

                User user = this.userRepositiory.GetUserByEmail(login.Email);

                if (user == null)
                {
                    result = new StatusCodeResult(401);

                }
                else
                {
                    bool verify = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);

                    if (verify)
                    {
                        string usertoken = this.getAutToken(user);
                        var mydata = new
                        {
                            Email = user.Email,
                            Token = usertoken,
                            RoleId = user.RoleId,
                            IsAuthenticated = true,
                            // RoleId = user.RoleId,
                            // IsAuthenticated = true,
                        };
                        result = Ok(mydata);
                    }
                    else
                    {
                        result = new StatusCodeResult(500);
                    }
                }
            }
            catch (Exception ex)
            {

                result = new StatusCodeResult(500);
            }
            return result;
        }
        private string getAutToken(User user)
        {
            string secret = "kshdoihernjvonfew;jewporfm;fk'fmew;lke;e";
            var claims = new[]
            {
              new Claim(ClaimTypes.Email,user.Email),
             new Claim(ClaimTypes.Role,user.RoleId.ToString()),
             new Claim(ClaimTypes.UserData,user.Id.ToString()),

             };
            var jwtToken = new JwtSecurityToken(
                "ASPL",
                "ASPLUI",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }
    }
}
