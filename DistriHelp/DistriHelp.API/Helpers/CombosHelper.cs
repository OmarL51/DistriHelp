using DistriHelp.API.Data;
using DistriHelp.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboAreas()
        {
            List<SelectListItem> list = _context.Areas.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"

            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir área]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            List<SelectListItem> list = _context.Categories.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"

            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir categoria]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRequestTypes()
        {
            List<SelectListItem> list = _context.RequestTypes.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"

            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir prioridad]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStatuses()
        { 
          
            List<SelectListItem> list = _context.Statuses.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"

            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir estado]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStatusesU()
        {
            List<SelectListItem> list = _context.Statuses.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"

            }).Where(x => x.Text == "ABIERTO").OrderBy(x => x.Text).ToList();

           

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStatusesA()
        {
            List<SelectListItem> list = _context.Statuses.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"

            }).Where(x => x.Text == "ABIERTO" || x.Text == "CERRADO").OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir estado]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboUsers()
        {
            List<SelectListItem> list = _context.Users.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = $"{x.Id}"

            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir usuario]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboUsersN()
        {
            List<SelectListItem> list = _context.Users.Select(x => new SelectListItem
            {
                Text = x.Email,
                Value = $"{x.Id}"

            }).Where(x => x.Text == "soporte@distrimedical.com.co").OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elegir usuario]",
                Value = "0"
            });

            return list;
        }
    }
}
