using Microsoft.AspNetCore.Mvc;
using MyApiConsume.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyApiConsume.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _Client;
        Uri baseaddress = new Uri("https://localhost:7044/api");

        public StudentsController()
        {
            _Client = new HttpClient();
            _Client.BaseAddress = baseaddress;
        }

        public async Task<IActionResult> Index()
        {
            List<StudentViewModel> customerList = new List<StudentViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7044/api"); // Replace with your API base URL
                var response = await client.GetAsync("/api/Students");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<StudentViewModel>>(data);
                }
            }
            return View(customerList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7044/api");
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/Students", jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle API request failure
                        TempData["ErrorMessage"] = "Failed to create a new customer.";
                    }
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7044/api"); // Replace with your API base URL
                var response = await client.GetAsync($"/api/Students/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<StudentViewModel>(data);
                    return View(customer);
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7044/api"); // Replace with your API base URL

                // Send a GET request to fetch the customer by ID
                var response = await client.DeleteAsync($"/api/Students/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<StudentViewModel>(data);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7044/api"); // Replace with your API base URL

                // Send a GET request to fetch the customer by ID
                var response = await client.GetAsync($"/api/Students/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<StudentViewModel>(data);

                    if (customer != null)
                    {
                        return View(customer);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7044/api"); // Replace with your API base URL

                    // Convert the model to JSON
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    // Send a POST request to create a new customer
                    var response = await client.PutAsync($"/api/Students/{id}", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle API request failure
                        TempData["ErrorMessage"] = "Failed to create a new customer.";
                    }
                }
            }
            return View(model);
        }
    }
}

