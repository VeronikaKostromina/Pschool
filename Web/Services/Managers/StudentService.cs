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

        public async Task<List<StudentViewModel>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<StudentViewModel>>("api/Students");
        }

        public async Task Delete(long id)
        {
            await httpClient.DeleteAsync($"api/Students/{id}");
        }

        public async Task<StudentViewModel> Create(StudentViewModel studentViewModel)
        {
            var json = JsonConvert.SerializeObject(studentViewModel);
            var result = await httpClient.PostAsync("api/Students", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            var stringResult = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StudentViewModel>(stringResult);
        }

        public async Task<StudentViewModel> Update(StudentViewModel studentViewModel)
        {
            var json = JsonConvert.SerializeObject(studentViewModel);
            var result = await httpClient.PutAsync("api/Students", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            var stringResult = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StudentViewModel>(stringResult);
        }

        public async Task<List<StudentViewModel>> GetByParentId(long id)
        {
            return await httpClient.GetFromJsonAsync<List<StudentViewModel>>($"api/parents/{id}/students");
        }
    }
}
