using DistriHelp.API.Data.Entities;
using DistriHelp.API.Helpers;
using DistriHelp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Data
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
            await CheckRequestTypesAsync();
            await CheckAreasAsync();
            await CheckSolutionsAsync();
            await CheckStatusesAsync();
            await CheckRolesAsync();
            await CheckUserAsync(1, "Soporte", "Tecnico", "soporte@distrimedical.com.co", "TI", UserType.Admin);
            await CheckUserAsync(2, "Fredy", "Asprilla", "prueba@distrimedical.com.co", "SAC", UserType.User);
            //await CheckUserAsync(2, "Alexa", "Torres", "alexa@yopmail.com", "BI", UserType.User);
       



        }

        private async Task CheckUserAsync(int id, string firstName, string lastName, string email, string area, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Area = _context.Areas.FirstOrDefault(x => x.Description == "TI"),
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    UserType = userType
                };
                await _userHelper.AddUserAsync(user, "1234567");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckStatusesAsync()
        {
            if (!_context.Statuses.Any())
            {
                _context.Statuses.Add(new Status { Description = "ABIERTO" });
                _context.Statuses.Add(new Status { Description = "PENDIENTE" });
                _context.Statuses.Add(new Status { Description = "CERRADO" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckSolutionsAsync()
        {
            if (!_context.Solutions.Any())
            {
                _context.Solutions.Add(new Solution { Tittle = "A", Description = "PRIMERA" });
                _context.Solutions.Add(new Solution { Tittle = "B", Description = "SEGUNDA" });
                _context.Solutions.Add(new Solution { Tittle = "C", Description = "TERCERA" });
                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckAreasAsync()
        {
            if (!_context.Areas.Any())
            {
                _context.Areas.Add(new Area { Description = "GERENCIA" });
                _context.Areas.Add(new Area { Description = "GESTIÓN HUMANA" });
                _context.Areas.Add(new Area { Description = "TI" });
                _context.Areas.Add(new Area { Description = "BI" });
                _context.Areas.Add(new Area { Description = "LOGISTICA" });
                _context.Areas.Add(new Area { Description = "SAC" });
                _context.Areas.Add(new Area { Description = "COTIZACIONES" });
                _context.Areas.Add(new Area { Description = "COMERCIAL" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRequestTypesAsync()
        {
            if (!_context.RequestTypes.Any())
            {
                _context.RequestTypes.Add(new RequestType { Description = "ALTA" });
                _context.RequestTypes.Add(new RequestType { Description = "MEDIA" });
                _context.RequestTypes.Add(new RequestType { Description = "BAJA" });
                await _context.SaveChangesAsync();
            }
        }

    }

}
