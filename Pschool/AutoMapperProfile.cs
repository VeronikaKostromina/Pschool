using AutoMapper;
using Pschool.Shared.Models;
using Pschool.Shared.ViewModels.ParentViewModels;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Pschool
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDetailsViewModel>().ForMember(model => model.ParentFullName,
                options => options
                    .MapFrom(x => string.Join(' ', x.Parent!.FirstName, x.Parent!.LastName)));
            CreateMap<CreateStudentViewModel, Student>();
            CreateMap<UpdateStudentViewModel, Student>();

            CreateMap<Parent, ParentDetailsViewModel>();
            CreateMap<CreateParentViewModel, Parent>();
            CreateMap<UpdateParentViewModel, Parent>();
        }
    }
}
