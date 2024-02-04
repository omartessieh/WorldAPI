using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using Models.Dtos;

namespace WorldWeb.Pages
{
    public class CountryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<CountryDto> Countries { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("WorldWebAPI");
                var response = await httpClient.GetAsync("api/Country");

                if (response.IsSuccessStatusCode)
                {
                    Countries = await response.Content.ReadFromJsonAsync<List<CountryDto>>();
                    return Page();
                }
                else
                {
                    // Log or handle the error appropriately
                    return BadRequest("Failed to retrieve data from the API.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest("An error occurred while fetching data.");
            }
        }

    }
}