using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Pschool.Shared.ViewModels.ParentViewModels;
using Web.Services.Contracts;

namespace Web.Services.Managers
{
    public class ParentService : IParentService
    {
        private readonly HttpClient httpClient;

        public ParentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ParentDetailsViewModel>?> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<ParentDetailsViewModel>>("api/parents");
        }

        public async Task<bool> Delete(long id)
        {
            var result = await httpClient.DeleteAsync($"api/Parents/{id}");
            return result.StatusCode == HttpStatusCode.OK;
        }

        public async Task<ParentDetailsViewModel?> Create(ParentDetailsViewModel parentViewModel)
        {
            var json = JsonConvert.SerializeObject(parentViewModel);
            var result = await httpClient.PostAsync("api/Parents", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            return result.StatusCode != HttpStatusCode.OK
                ? null
                : JsonConvert.DeserializeObject<ParentDetailsViewModel>(await result.Content.ReadAsStringAsync());
        }

        public async Task<ParentDetailsViewModel?> Update(ParentDetailsViewModel parentViewModel)
        {
            var json = JsonConvert.SerializeObject(parentViewModel);
            var result = await httpClient.PutAsync($"api/Parents/{parentViewModel.Id}", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            return result.StatusCode != HttpStatusCode.OK
                ? null
                : JsonConvert.DeserializeObject<ParentDetailsViewModel>(await result.Content.ReadAsStringAsync());

        }
    }
}
