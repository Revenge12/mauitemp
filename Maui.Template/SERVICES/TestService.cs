using Shared.Template;
using Shared.Template.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Services
{
    public class TestService
    {
        private readonly HttpClient _http;

        public TestService(HttpClient http)
        {
            _http = http;
        }

        string uri = "/api/testing/";

        //Get logic
        S


        public async Task<ServiceResponse<Test>> Upsert(Test test)
        {
            var result = await _http.PostAsJsonAsync(uri + "upsert", test);

            if(result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ServiceResponse<Test>>();
            }

            return new ServiceResponse<Test>()
            {
                Message = "API was not reached",
                Success = false
            };
        }

        public async Task<ServiceResponse<Test>> Delete(Test test)
        {
            var result = await _http.PostAsJsonAsync(uri + "remove", test);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ServiceResponse<Test>>();
            }

            return new ServiceResponse<Test>()
            {
                Message = "API was not reached",
                Success = false
            };
        }

    }
}
