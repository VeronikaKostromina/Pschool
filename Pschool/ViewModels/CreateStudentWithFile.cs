using Microsoft.AspNetCore.Mvc;
using Pschool.Shared.DataModelBinders;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Pschool.ViewModels
{
    [ModelBinder(BinderType = typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class CreateStudentWithFile : CreateStudentViewModel
    {
        public IFormFile? Document { get; set; }
    }
}
