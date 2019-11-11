using MyVet.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyVet.Prism.ViewModels
{
    public class PetsPageViewModel : ViewModelBase
    {
        private OwnerResponse _owner;
        private ObservableCollection<PetResponse> _pets;

        public PetsPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Pets";
        }

        public ObservableCollection<PetResponse> Pets
        {
            get => _pets;
            set => SetProperty(ref _pets, value);
        }     

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("owner"))
            {
                _owner = parameters.GetValue<OwnerResponse>("owner");
                Title = $"Hi {_owner.FirstName}";

                Pets = new ObservableCollection<PetResponse>(_owner.Pets);
            }
        }
    }
}
