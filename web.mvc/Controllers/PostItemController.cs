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
using web.mvc.Filters;
using web.mvc.Models;

namespace web.mvc.Controllers
{
    [SessionTimeout]
    public class PostItemController : Controller
    {
        // GET: PostItem
        public async Task<ActionResult> Index()
        {

            using (var httpClient = new HttpClient())
            {

                //var accessToken = HttpContext.Session.GetString("Token");
                //httpClient.DefaultRequestHeaders.Clear();
                //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using var responseItem = await httpClient.GetAsync("https://localhost:44363/api/postitems/");
                var apiResponseItem = await responseItem.Content.ReadAsStringAsync();

                if (responseItem.IsSuccessStatusCode)
                {
                    List<PostItemViewModel> postItemListViewModel = JsonConvert.DeserializeObject<List<PostItemViewModel>>(apiResponseItem);
                    return View(postItemListViewModel);
                }
            }

            return View();
        }

        public async Task<ActionResult> PostItemByUser()
        {

            var userId = "";

            using (var httpClient = new HttpClient())
            {

                var accessToken = HttpContext.Session.GetString("Token");
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var sessionEmailAddress = HttpContext.Session.GetString("EmailAddress");
                var userDTO = new UserDTO
                {
                    EmailAddress = sessionEmailAddress,
                };
                string loginData = JsonConvert.SerializeObject(userDTO);
                var contentData = new StringContent(loginData, System.Text.Encoding.UTF8, "application/json");
                using var response = await httpClient.PostAsync("https://localhost:44363/api/Accounts/GetUserInfoByEmail/", contentData);
                var apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(apiResponse);
                    userId = user.UserId;
                }

                if (!String.IsNullOrEmpty(userId))
                {
                    using var responseItem = await httpClient.GetAsync("https://localhost:44363/api/postitem/GetItemByUserID/" + userId);
                    var apiResponseItem = await responseItem.Content.ReadAsStringAsync();

                    if (responseItem.IsSuccessStatusCode)
                    {
                        List<PostItemViewModel> postItemListViewModel = JsonConvert.DeserializeObject<List<PostItemViewModel>>(apiResponseItem);
                        return View(postItemListViewModel);
                    }
                }             
            }

            return View();
        }

        // GET: PostItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostItem/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PostItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePostItemDTO createPostItemDTO)
        {
            try
            {
                var userId = "";

                //var createPostItemDTO = new CreatePostItemDTO
                //{
                //    //Picture = formFields["Picture"],
                //    Condition = Convert.ToInt32(formFields["Condition"]),
                //    Title = formFields["Title"],
                //    ItemDescription = formFields["ItemDescription"],
                //    MeetingLocation = formFields["MeetingLocation"],
                //};
                using (var httpClient = new HttpClient())
                {

                    var accessToken = HttpContext.Session.GetString("Token");
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var sessionEmailAddress = HttpContext.Session.GetString("EmailAddress");
                    var userDTO = new UserDTO
                    {
                        EmailAddress = sessionEmailAddress,
                    };
                    string loginData = JsonConvert.SerializeObject(userDTO);
                    var contentData = new StringContent(loginData, System.Text.Encoding.UTF8, "application/json");
                    using var response = await httpClient.PostAsync("https://localhost:44363/api/Accounts/GetUserInfoByEmail/", contentData);
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(apiResponse);
                        userId = user.UserId;
                    }

                    if (!String.IsNullOrEmpty(userId))
                    {
                        createPostItemDTO.UserId = userId;

                        string createPostItemData = JsonConvert.SerializeObject(createPostItemDTO);
                        //var postItemData = new FormUrlEncodedContent(createPostItemData, );
                        var postItemData = new StringContent(createPostItemData, System.Text.Encoding.UTF8, "application/json");
                        using var postItemResponse = await httpClient.PostAsync("https://localhost:44363/api/PostItems/Create", postItemData);
                        var apiPostItemResponse = await postItemResponse.Content.ReadAsStringAsync();

                        if (postItemResponse.IsSuccessStatusCode)
                        {

                            //UserTokenDTO userTokenDTO = JsonConvert.DeserializeObject<UserTokenDTO>(apiResponse);
                            //var token = userTokenDTO.Token;
                            //var expiration = userTokenDTO.Expiration.ToString("MM/dd/yyyy HH:mm:ss");
                            ////put session variable here
                            //HttpContext.Session.SetString("EmailAddress", createUserInfoDTO.EmailAddress);
                            //HttpContext.Session.SetString("Token", token);
                            //HttpContext.Session.SetString("Expiration", expiration);

                            return RedirectToAction("Index", "PostItem");
                        }
                    }
                }
                return RedirectToAction("Index", "PostItem");
            }
            catch
            {
                return View();
            }
        }

        // GET: PostItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostItem/Edit/5
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

        // GET: PostItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostItem/Delete/5
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