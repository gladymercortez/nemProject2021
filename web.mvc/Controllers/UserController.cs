using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using web.mvc.DTO;

namespace web.mvc.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection formFields)
        {
            try
            {
                var createUserInfoDTO = new CreateUserInfoDTO
                {
                    EmailAddress = formFields["EmailAddress"],
                    Password = formFields["Password"],
                    PhoneNumber = formFields["PhoneNumber"]
                };

                using (var httpClient = new HttpClient())
                {

                    string createUserInfoData = JsonConvert.SerializeObject(createUserInfoDTO);
                    var contentData = new StringContent(createUserInfoData, System.Text.Encoding.UTF8, "application/json");
                    using var response = await httpClient.PostAsync("https://localhost:44363/api/Accounts/Create", contentData);
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                        //UserTokenDTO userTokenDTO = JsonConvert.DeserializeObject<UserTokenDTO>(apiResponse);
                        //var token = userTokenDTO.Token;
                        //var expiration = userTokenDTO.Expiration.ToString("MM/dd/yyyy HH:mm:ss");
                        ////put session variable here
                        //HttpContext.Session.SetString("EmailAddress", createUserInfoDTO.EmailAddress);
                        //HttpContext.Session.SetString("Token", token);
                        //HttpContext.Session.SetString("Expiration", expiration);
                         
                        return RedirectToAction("Success", "User");
                    }

                }
                return RedirectToAction("Login", "Account");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Success()
        {
            return View();
        }


        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}