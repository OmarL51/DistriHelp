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
                _context.Areas.Add(new Area{ Description = "GERENCIA" });
                _context.Areas.Add(new Area { Description = "GESTIÓN HUMANA" });
                _context.Areas.Add(new Area { Description = "TI" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRequestTypesAsync()
        {
            if (!_context.RequestTypes.Any())
            {
                _context.RequestTypes.Add(new RequestType { Description = "TRES" });
                _context.RequestTypes.Add(new RequestType { Description = "CUATRO" });
                await _context.SaveChangesAsync();
            }
        }

    }
    
}
