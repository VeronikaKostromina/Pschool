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

        public async Task<List<ParentViewModel>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<ParentViewModel>>("api/parents");
        }

        public async Task Delete(long id)
        {
            await httpClient.DeleteAsync($"api/Parents/{id}");
        }

        public async Task<ParentViewModel> Create(ParentViewModel parentViewModel)
        {
            var json = JsonConvert.SerializeObject(parentViewModel);
            var result = await httpClient.PostAsync("api/Parents", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            var stringResult = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ParentViewModel>(stringResult);
        }

        public async Task<ParentViewModel> Update(ParentViewModel parentViewModel)
        {
            var json = JsonConvert.SerializeObject(parentViewModel);
            var result = await httpClient.PutAsync("api/Parents", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            var stringResult = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ParentViewModel>(stringResult);
        }
    }
}
