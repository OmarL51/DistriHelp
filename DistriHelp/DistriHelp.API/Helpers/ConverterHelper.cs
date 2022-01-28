using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using DistriHelp.API.Models;
using System;
using System.Threading.Tasks;

namespace DistriHelp.API.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<User> ToUserAsync(UserViewModel model, Area area, bool isNew)
        {
            return new User
            {
                Area = await _context.Areas.FindAsync(model.AreaId),
                Email = model.Email,
                FirstName = model.FirstName,
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                LastName = model.LastName,
                UserName = model.Email,
                UserType = model.UserType,
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                AreaId = user.Area.Id,
                Area = user.Area,
                Areas = _combosHelper.GetComboAreas(),
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.Email,
                UserType = user.UserType,


            };
        }
    }
}
