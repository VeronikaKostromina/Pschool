using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Pschool.Shared.ViewModels.StudentViewModels;
using Web.Services.Contracts;

namespace Web.Services.Managers
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient httpClient;

        public StudentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<StudentDetailsViewModel>?> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<StudentDetailsViewModel>>("api/Students");
        }

        public async Task<bool> Delete(long id)
        {
            var result = await httpClient.DeleteAsync($"api/Students/{id}");
            return result.StatusCode == HttpStatusCode.OK;
        }

        public async Task<StudentDetailsViewModel?> Create(StudentDetailsViewModel studentViewModel)
        {
            var json = JsonConvert.SerializeObject(studentViewModel);
            var result = await httpClient.PostAsync("api/Students", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            return result.StatusCode != HttpStatusCode.OK
                ? null
                : JsonConvert.DeserializeObject<StudentDetailsViewModel>(await result.Content.ReadAsStringAsync());
        }

        public async Task<StudentDetailsViewModel?> Update(StudentDetailsViewModel studentViewModel)
        {
            var json = JsonConvert.SerializeObject(studentViewModel);
            var result = await httpClient.PutAsync($"api/Students/{studentViewModel.Id}", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            return result.StatusCode != HttpStatusCode.OK
                ? null
                : JsonConvert.DeserializeObject<StudentDetailsViewModel>(await result.Content.ReadAsStringAsync());
        }

        public async Task<List<StudentDetailsViewModel>?> GetByParentId(long id)
        {
            return await httpClient.GetFromJsonAsync<List<StudentDetailsViewModel>>($"api/parents/{id}/students");
        }
    }
}
