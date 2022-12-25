using Blazored.FluentValidation;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Pschool.Shared.ViewModels.ParentViewModels;
using Web.Services.Contracts;

namespace Web.Pages
{
    public class ParentBase : ComponentBase
    {
        private string ErrorMessage => "There is an error, please refresh the page or try again later.";

        [Inject]
        public IParentService ParentService { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }

        public List<ParentDetailsViewModel> Parents { get; set; } = new List<ParentDetailsViewModel>();
        public ParentDetailsViewModel ParentViewModel { get; set; } = new ParentDetailsViewModel();
        public FluentValidationValidator? FluentValidationValidator { get; set; } = new FluentValidationValidator();

        public ActionType ActionType { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parents = await ParentService.GetAll();
        }

        public async Task Add()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                var parent = await ParentService.Create(ParentViewModel);
                if (parent != null)
                {
                    Parents.Add(parent);
                    ActionType = ActionType.None;

                    ToastService.ShowSuccess("Parent created.");
                }
                else
                {
                    ToastService.ShowError(ErrorMessage);
                }
            }
        }

        public async Task Delete()
        {
            var result = await ParentService.Delete(ParentViewModel.Id);
            if (result)
            {
                Parents.Remove(ParentViewModel);
                ActionType = ActionType.None;

                ToastService.ShowSuccess("Parent deleted.");
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
                var parent = await ParentService.Update(ParentViewModel);
                if (parent != null)
                {
                    int index = Parents.FindIndex(x => x.Id == parent.Id);
                    Parents[index] = parent;
                    ActionType = ActionType.None;

                    ToastService.ShowSuccess("Parent updated.");
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

        public void ShowDialogModal(ActionType actionType, ParentDetailsViewModel parent)
        {
            ActionType = actionType;
            ParentViewModel = parent;
        }
    }
}
