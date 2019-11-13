﻿using MyVet.Web.Data.Entities;
using MyVet.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private User _customer1;
        private User _customer2;
        private User _customer3;
        private User _customer4;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1010", "Sevann", "Radhak", "sevann.radhak@gmail.com", "54 9 111 73627795", "Santiago del Estero 690", "Admin");
            _customer1 = await CheckUserAsync("2020", "Homero", "Simpson", "homero@correo.com", string.Empty, "Av. Siempreviva 742", "Customer");
            _customer2 = await CheckUserAsync("3030", "Bart", "Simpson", "bart@correo.com", string.Empty, "Av. Siempreviva 742", "Customer");
            _customer3 = await CheckUserAsync("4040", "Lisa", "Simpson", "lisa@correo.com", string.Empty, "Av. Siempreviva 742", "Customer");
            _customer4 = await CheckUserAsync("5050", "Maggie", "Simpson", "maggie@correo.com", string.Empty, "Av. Siempreviva 742", "Customer");
            await CheckPetTypesAsync();
            await CheckServiceTypesAsync();
            await CheckOwnerAsync();
            await CheckManagerAsync(manager);
            await CheckPetsAsync();
            //await CheckAgendasAsync();
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckPetsAsync()
        {
            if (!_dataContext.Pets.Any())
            {
                var owner = _dataContext.Owners.FirstOrDefault();
                var petType = _dataContext.PetTypes.FirstOrDefault();
                AddPet("Otto", owner, petType, "Shih tzu");
                AddPet("Killer", owner, petType, "Dobermann");
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckServiceTypesAsync()
        {
            if (!_dataContext.ServiceTypes.Any())
            {
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Consulta" });
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Urgencia" });
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Vacunación" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPetTypesAsync()
        {
            if (!_dataContext.PetTypes.Any())
            {
                _dataContext.PetTypes.Add(new PetType { Name = "Perro" });
                _dataContext.PetTypes.Add(new PetType { Name = "Gato" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckOwnerAsync()
        {
            if (!_dataContext.Owners.Any())
            {
                _dataContext.Owners.Add(new Owner { User = _customer1});
                _dataContext.Owners.Add(new Owner { User = _customer2 });
                _dataContext.Owners.Add(new Owner { User = _customer3 });
                _dataContext.Owners.Add(new Owner { User = _customer4 });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new Manager { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddPet(string name, Owner owner, PetType petType, string race)
        {
            _dataContext.Pets.Add(new Pet
            {
                Born = DateTime.Now.AddYears(-2),
                Name = name,
                Owner = owner,
                PetType = petType,
                Race = race,
                Remarks = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                "when an unknown printer took a galley of type and scrambled it to make a type specimen book."
            });
        }

        private async Task CheckAgendasAsync()
        {
            if (!_dataContext.Agendas.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var finalDate = initialDate.AddYears(1);
                while (initialDate < finalDate)
                {
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while (initialDate < finalDate2)
                        {
                            _dataContext.Agendas.Add(new Agenda
                            {
                                Date = initialDate,
                                IsAvailable = true
                            });

                            initialDate = initialDate.AddMinutes(30);
                        }

                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }
                }
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}
