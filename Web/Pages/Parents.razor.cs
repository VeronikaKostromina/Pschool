using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Pschool.Shared.ViewModels.ParentViewModels;
using Web.Services.Contracts;

namespace Web.Pages
{
    public class ParentBase : ComponentBase
    {
        [Inject]
        public IParentService ParentService { get; set; }

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
                Parents.Add(await ParentService.Create(ParentViewModel));
                ActionType = ActionType.None;
            }
        }

        public async Task Delete()
        {
            await ParentService.Delete(ParentViewModel.Id);
            Parents.Remove(ParentViewModel);
            ActionType = ActionType.None;
        }

        public async Task Update()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                var parent = await ParentService.Update(ParentViewModel);
                int index = Parents.FindIndex(x => x.Id == parent.Id);
                Parents[index] = parent;
                ActionType = ActionType.None;
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
