namespace Pschool.Web.Services.Contracts
{
    public interface IParentService
    {
        Task<List<ParentDetailsViewModel>> GetAll();
    }
}
