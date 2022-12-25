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
        public bool ShowModal { get; set; } = false;
        public bool DeleteAction { get; set; } = false;

        public bool CreateAction { get; set; } = false;
        public bool UpdateAction { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Parents = await ParentService.GetAll();
        }

        public async Task Add()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                Parents.Add(await ParentService.Create(ParentViewModel));
                ShowModal = false;
            }
        }

        public async Task Delete()
        {
            await ParentService.Delete(ParentViewModel.Id);
            Parents.Remove(ParentViewModel);
            ShowModal = false;
        }

        public async Task Update()
        {
            if (await FluentValidationValidator!.ValidateAsync())
            {
                var parent = await ParentService.Update(ParentViewModel);
                int index = Parents.FindIndex(x => x.Id == parent.Id);
                Parents[index] = parent;
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

        public void ShowDialogModal(bool update, bool create, bool delete, ParentDetailsViewModel parent)
        {
            ShowModal = true;
            DeleteAction = delete;
            CreateAction = create;
            UpdateAction = update;

            ParentViewModel = parent;
        }
    }
}
