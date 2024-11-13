using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ToDo.Data;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AccountController(AppDbContext context, UserService userService, TokenService tokenService)
        {
            _context = context;
            _userService = userService;
            _tokenService = tokenService;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users_lw9_02.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_lw5_02 = await _context.Users_lw9_02
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user_lw5_02 == null)
            {
                return NotFound();
            }

            return View(user_lw5_02);
        }

        // GET: Account/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Email,Password")] User_lw9_02 user)
        {
            if (ModelState.IsValid)
            {
                var CreateUser = new User_lw9_02
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                };

                _context.Add(CreateUser);
                await _context.SaveChangesAsync();
                return Redirect("~/");
            }
            return Redirect("~/");
        }

        private bool User_lw5_02Exists(int id)
        {
            return _context.Users_lw9_02.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] User_lw9_02 user)
        {

            var userExist = _userService.UserVerify(user);

            if (userExist == null)
            {
                TempData["ErrorMessage"] = "Неверный логин или пароль.";
                return Redirect("~/");
            }

            Response.Cookies.Append("A", _tokenService.CreateToken(userExist));

            return Redirect("~/Note");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("A");

            return RedirectToAction("Index", "Home");
        }
    }
}
