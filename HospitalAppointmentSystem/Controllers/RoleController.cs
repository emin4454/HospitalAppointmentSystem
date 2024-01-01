﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;
        public RoleController(RoleManager<IdentityRole> manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var roles = _manager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (!_manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();

            }
            return RedirectToAction("Index");
        }
    }
}
