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
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();

            CreateMap<Parent, ParentViewModel>();
            CreateMap<ParentViewModel, Parent>();
        }
    }
}
