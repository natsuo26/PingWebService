using Azure.Identity;
using Chat_App_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        // DB connection string
        public UsersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUSers(AppDbContext applicationDbContext)
        {
            var allUsers = this.dbContext.Users.ToList();
            return Ok(allUsers);
        }

        [HttpPost]
        public IActionResult AddUsers(UsersClassDto usersClassDto)
        {
            var newUser = new UsersTable()
            {
                userName = usersClassDto.userName,
                password = usersClassDto.password
            };

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            return Ok(newUser);

        }
        [HttpPut]
        public IActionResult UpdateUser(int userID, UpdateUsersDto updateUsersDto)
        {
            var user = dbContext.Users.Find(userID);
            if (user == null)
            {
                return NoContent();
            }

            user.userName = updateUsersDto.userName;
            user.password = updateUsersDto.password;

            dbContext.SaveChanges();
            return Ok(user);

        }


    }
}
