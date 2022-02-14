using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboAreas();
        IEnumerable<SelectListItem> GetComboStatuses();

        IEnumerable<SelectListItem> GetComboStatusesU();

        IEnumerable<SelectListItem> GetComboStatusesA();
        IEnumerable<SelectListItem> GetComboUsers();

        IEnumerable<SelectListItem> GetComboUsersN();
        IEnumerable<SelectListItem> GetComboCategories();

        IEnumerable<SelectListItem> GetComboRequestTypes();
    }
}
