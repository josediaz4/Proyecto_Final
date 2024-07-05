﻿using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Enum;
using Proyecto_Final.Helpers;

namespace Proyecto_Final.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Manu", "Diaz", "manu@yopmail.com", "3815556397", "Av Calchaqui 159", UserType.Admin);
            await CheckUserAsync("2020", "Juan", "Perez", "juan@yopmail.com", "3815556300", "Av Siempre Viva 123", UserType.User);
        }

        private async Task<User> CheckUserAsync(
             string document,
             string firstName,
             string lastName,
             string email,
             string phone,
             string address,
             UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
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
                    Document = document,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

    }
}
