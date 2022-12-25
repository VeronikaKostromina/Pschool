using Blazored.FluentValidation;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Pschool.Shared.ViewModels.ParentViewModels;
using Pschool.Shared.ViewModels.StudentViewModels;
using Web.Services.Contracts;

namespace Web.Pages
{
    public class StudentBase : ComponentBase
    {
        private string ErrorMessage => "There is an error, please refresh the page or try again later.";

        [Inject]
        public IStudentService StudentService { get; set; }

        [Inject]
        public IParentService ParentService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        public List<StudentDetailsViewModel>? Students { get; set; } = new List<StudentDetailsViewModel>();
        public List<ParentDetailsViewModel> Parents { get; set; } = new List<ParentDetailsViewModel>();
        public StudentDetailsViewModel StudentViewModel { get; set; } = new StudentDetailsViewModel();
        public FluentValidationValidator? FluentValidationValidator { get; set; } = new FluentValidationValidator();

        public long ParentId { get; set; }

        public ActionType ActionType { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Students = await StudentService.GetAll();
            Parents = await ParentService.GetAll();
        }

        public async Task Add()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                var student = await StudentService.Create(StudentViewModel);
                if (student != null)
                {
                    var parent = Parents[Parents.FindIndex(x => x.Id == StudentViewModel.ParentId)];
                    student.ParentFullName = string.Join(' ', parent.FirstName, parent.LastName);
                    Students?.Add(student);
                    ActionType = ActionType.None;

                    ToastService.ShowSuccess("Student created.");
                }
                else
                {
                    ToastService.ShowError(ErrorMessage);
                }
            }
        }

        public async Task Delete()
        {
            var result = await StudentService.Delete(StudentViewModel.Id);
            if (result)
            {
                Students?.Remove(StudentViewModel);
                ActionType = ActionType.None;

                ToastService.ShowSuccess("Student deleted.");
            }
            else
            {
                ToastService.ShowError(ErrorMessage);
            }
        }

        public async Task Update()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                var student = await StudentService.Update(StudentViewModel);
                if (student != null)
                {
                    var parent = Parents[Parents.FindIndex(x => x.Id == StudentViewModel.ParentId)];
                    student.ParentFullName = string.Join(' ', parent.FirstName, parent.LastName);
                    if (Students != null)
                        Students[Students.FindIndex(x => x.Id == student.Id)] = student;
                    ActionType = ActionType.None;
                    ToastService.ShowSuccess("Student updated.");
                }
                else
                {
                    ToastService.ShowError(ErrorMessage);
                }
            }
        }

        public void ModalCancel()
        {
            ActionType = ActionType.None;
        }

        public void ShowDialogModal(ActionType actionType, StudentDetailsViewModel student)
        {
            ActionType = actionType;
            StudentViewModel = student;
        }

        public async void OnParentChange(long parentId)
        {
            ParentId = parentId;

            Students = parentId >= 0
                ? await StudentService.GetByParentId(parentId)
                : await StudentService.GetAll();

            StateHasChanged();
        }
    }
}