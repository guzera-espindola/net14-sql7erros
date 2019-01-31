﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeuTrabalho.Models;
using System.Data.SqlClient;

namespace MeuTrabalho.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Server=localhost;Database=TRAB01;Integrated Security=true;Max Pool Size=10");
                SqlCommand cmd = new SqlCommand($"SELECT username FROM tbLogin WHERE email='{model.Email}' AND pwd='{model.Password}'", connection);

                connection.Open();
                string username = (string)cmd.ExecuteScalar();

                connection.Close();

                return RedirectToAction("Dashboard", "Home", new { name = username});
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }
    }
}
