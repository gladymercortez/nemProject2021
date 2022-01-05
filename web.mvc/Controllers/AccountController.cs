using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using web.mvc.DTO;
using web.mvc.Models;

namespace web.mvc.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var loginDTO = new LoginDTO
                {
                    EmailAddress = model.EmailAddress,
                    Password = model.Password
                };

                using (var httpClient = new HttpClient())
                {
                    string loginData = JsonConvert.SerializeObject(loginDTO);
                    var contentData = new StringContent(loginData, System.Text.Encoding.UTF8, "application/json");
                    using var response = await httpClient.PostAsync("https://localhost:44363/api/Accounts/Login/", contentData);
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                        UserTokenDTO userTokenDTO = JsonConvert.DeserializeObject<UserTokenDTO>(apiResponse);
                        var token = userTokenDTO.Token;
                        var expiration = userTokenDTO.Expiration.ToString("MM/dd/yyyy HH:mm:ss");
                        //put session variable here
                        HttpContext.Session.SetString("EmailAddress", model.EmailAddress);
                        HttpContext.Session.SetString("Token", token);
                        HttpContext.Session.SetString("Expiration", expiration);

                        return RedirectToAction("Index", "Home");

                    }
                }

                //return RedirectToAction("Index");


                //if (userList.Contains((model.Username.ToUpper())))
                //{
                //    var result = AuthemticateUser(model.Username, model.Password);

                //    if (result.Authenticated)
                //    {
                //        //put session variable here
                //        HttpContext.Session.SetString("UserName", model.Username);
                //        HttpContext.Session.SetString("ClinicCode", model.ClinicCode);
                //        var clinicDTO = await _clinicProxyService.GetAllCliniByClinicCode(model.ClinicCode);
                //        HttpContext.Session.SetString("EntityCode", clinicDTO.EntityCode);

                //        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                //        {
                //            return Redirect(model.ReturnUrl);
                //        }
                //        else
                //        {
                //            return RedirectToAction("Index", "Home");
                //        }
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "You are not authorised to access this page");
                //    return View(model);
                //}
                //return RedirectToAction("Index", "Home");

                //if (model.EmailAddress != null && model.Password != null)
                //{
                //    HttpContext.Session.SetString("Email", model.EmailAddress);
                //    return View("Success");
                //}
                //else
                //{
                //    ModelState.AddModelError("", "You are not authorised to access this page");
                //    return View(model);
                //}

            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public async Task<ActionResult> DisplayUsers()
        {
            using (var httpClient = new HttpClient())
            {

                var accessToken = HttpContext.Session.GetString("Token");
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using var response = await httpClient.GetAsync("https://localhost:44363/api/Accounts/Users");
                var apiResponse = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {

                    List<UserViewModel> userListViewModel = JsonConvert.DeserializeObject<List<UserViewModel>>(apiResponse);

                    return View(userListViewModel);

                }
            }

            return View();
        }


        // GET: Account/Details/5
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

                        return RedirectToAction("DisplayUsers","Account");

                    }

                }
                    return RedirectToAction(nameof(DisplayUsers));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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