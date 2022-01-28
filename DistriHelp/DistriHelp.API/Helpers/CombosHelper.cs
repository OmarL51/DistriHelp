using DistriHelp.API.Data;
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
    }
}
