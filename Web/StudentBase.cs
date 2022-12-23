using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Pschool.Shared.ViewModels.ParentViewModels;
using Pschool.Shared.ViewModels.StudentViewModels;
using Web.Services.Contracts;

namespace Web
{
    public class StudentBase : ComponentBase
    {
        [Inject]
        public IStudentService StudentService { get; set; }
        [Inject]
        public IParentService ParentService { get; set; }

        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
        public List<ParentViewModel> Parents { get; set; } = new List<ParentViewModel>();
        public StudentViewModel StudentViewModel { get; set; } = new StudentViewModel();
        public ParentViewModel ParentViewModel { get; set; } = new ParentViewModel();
        public FluentValidationValidator? FluentValidationValidator { get; set; } = new FluentValidationValidator();
        public long ParentId { get; set; }
        public bool ShowModal { get; set; } = false;
        public bool DeleteAction { get; set; } = false;

        public bool CreateAction { get; set; } = false;
        public bool UpdateAction { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Students = await StudentService.GetAll();
            Parents = await ParentService.GetAll();
        }

        public async Task Add()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                Students.Add(await StudentService.Create(StudentViewModel));
                ShowModal = false;
            }
        }

        public async Task Delete()
        {
            await StudentService.Delete(StudentViewModel.Id);
            Students.Remove(StudentViewModel);
            ShowModal = false;
        }

        public async Task Update()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                var student = await StudentService.Update(StudentViewModel);
                int index = Students.FindIndex(x => x.Id == student.Id);
                Students[index] = student;
                ShowModal = false;
            }
        }

        public void ModalCancel()
        {
            ShowModal = false;
            DeleteAction = false;
            CreateAction = false;
            UpdateAction = false;
        }

        public void ShowDialogModal(bool update, bool create, bool delete, StudentViewModel student)
        {
            ShowModal = true;
            DeleteAction = delete;
            CreateAction = create;
            UpdateAction = update;

            StudentViewModel = student;
        }

        public async void OnParentChange(long parentId)
        {
            ParentId = parentId;

            Students = parentId >= 0
                ? await StudentService.GetByParentId(parentId)
                : await StudentService.GetAll();

            this.StateHasChanged();
        }
    }
}
