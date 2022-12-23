using Pschool.Web.Services.Contracts;

namespace Pschool.Web.Services
{
    public class ParentService : IParentService
    {
        private readonly HttpClient httpClient;
        public ParentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ParentDetailsViewModel>> GetAll()
        {
            try
            {
                var parents = await httpClient.GetFromJsonAsync<List<ParentDetailsViewModel>>("api/Parents/GetAll");
                return parents;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
