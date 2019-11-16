﻿using MyVet.Common.Helpers;
using MyVet.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyVet.Prism.ViewModels
{
    public class PetPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PetResponse _pet;
        private DelegateCommand _editPetCommand;

        public PetPageViewModel(
            INavigationService navigationService
            ) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Details";
        }

        public DelegateCommand EditPetCommand => _editPetCommand ?? (_editPetCommand = new DelegateCommand(EditPetAsync));

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            //if (parameters.ContainsKey("pet"))
            //{
            //    Pet = parameters.GetValue<PetResponse>("pet");
            //    Title = _pet.Name;
            //}

            Pet = JsonConvert.DeserializeObject<PetResponse>(Settings.Pet);
        }

        private async void EditPetAsync()
        {
            var parameters = new NavigationParameters
            {
                { "pet", Pet }
            };

            await _navigationService.NavigateAsync("AddEditPetPage", parameters);
        }
    }
}
