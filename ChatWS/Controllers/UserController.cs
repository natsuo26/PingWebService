using ChatWS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatWS.Models;
using ChatWS.Helper;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext DbContext;
        private readonly HashAndVerifyPassword hashAndVerify;


        public UserController(AppDbContext dbContext, HashAndVerifyPassword hashAndVerify)
        {
            this.DbContext = dbContext;
            this.hashAndVerify = hashAndVerify;


        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var AllUsers = DbContext.users.ToList();
            return Ok(AllUsers);

        }
        [HttpGet("{id}")]
        public IActionResult SearchForUser(int id)
        {
            var user = DbContext.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateUser(int id, UpdateUsersDTO updateUsersDTO)
        {
            var user = DbContext.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.userName = updateUsersDTO.userName;
            user.Password = updateUsersDTO.Password;
            DbContext.SaveChanges();
            return Ok(new UpdateUsersDTO { userName = user.userName, Password = user.Password });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id, DeleteUserDTO deleteUserDTO)
        {
            var deletedUser = DbContext.users.Find(id);
            if (deletedUser == null)
            {
                return NotFound();
            }
            DbContext.users.Remove(deletedUser);
            DbContext.SaveChanges();
            return NoContent();

        }
        [HttpPost("hash")]
        public IActionResult HashPassword([FromBody] HashAndVerifyDTO hashAndVerifyDTO)
        {
            var hash = hashAndVerify.HashPassword(hashAndVerifyDTO.PlainPassword);
            return Ok(new { hashed = hash });
        }
        [HttpPost("verify")]
        public IActionResult verifyPassword([FromBody] HashAndVerifyDTO hashAndVerifyDTO)
        {

            bool isVerified = hashAndVerify.VerifyPassword(hashAndVerifyDTO.PlainPassword, hashAndVerifyDTO.HashPassword);
            return Ok(new { verify = isVerified });
        }

        [HttpPost("{Registration}")]



        public IActionResult RegisterUser([FromBody] RegisterUsersDTO registerUsers)
        {
            var service = new HashAndVerifyPassword();
            string hashed = service.HashPassword(registerUsers.PlainPassword);

            User user = new User(registerUsers.userName, hashed);
            string special = "!@#$%^&*";
            char[] arr = special.ToCharArray();
            bool flag = false;

            if (registerUsers.userName == null || registerUsers.PlainPassword.Length < 6)
            {

                return StatusCode(400, "Make sure user name is not null, and that password is greater than 6 characters");

            }

            foreach (var character in registerUsers.PlainPassword)
            {
                if (arr.Contains(character))
                {
                    flag = true;
                    break;

                }

            }
            if (!flag)
            {
                return StatusCode(400, " Password must contain a special value");
            }

            foreach (var newUser in DbContext.users)
            {
                if (newUser.userName == registerUsers.userName)
                {
                    return StatusCode(409, "Username has been taken");
                }

            }
            DbContext.Add(user);




            DbContext.SaveChanges();

            return Ok("Registration has been successfull");

        }

        public IActionResult Login([FromBody] RegisterUsersDTO registerUsers)
        {
            User loginUser = new User(registerUsers.PlainPassword, registerUsers.userName);

            foreach(var user in DbContext.users)
            {

                if(loginUser.userName == user.userName)
                {
                    return Ok();
                }
                hashAndVerify.VerifyPassword(loginUser.Password, user.Password);
            }
            
            return Ok($"User: {loginUser} is logged on");

        }
    }
}


