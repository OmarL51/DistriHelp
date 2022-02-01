using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using DistriHelp.API.Models;
using DistriHelp.Common.Enums;
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

        public async Task<Request> ToRequestAsync(RequestViewModel model, bool isNew)
        {
            return new Request
            {
                Category = await _context.Categories.FindAsync(model.CategoryId),
                Description = model.Description,
                DateI = model.DateI,
                DateF = model.DateF,
                Id = isNew ? 0 : model.Id,
                RequesType = await _context.RequestTypes.FindAsync(model.RequestTypeId),
                Resolution = model.Resolution,
                Status = await _context.Statuses.FindAsync(model.StatusId),
                Tittle = model.Tittle,
                User = await _context.Users.FindAsync(model.UserId),
                Userr = model.Userr
            };
        }

        public RequestViewModel ToRequestViewModel(Request request)
        {
           
            if (request.Userr.ToString() != "omar@yopmail.com")
            {
                return new RequestViewModel
                {
                    CategoryId = request.Category.Id,
                    Categories = _combosHelper.GetComboCategories(),
                    Description = request.Description,
                    DateI = request.DateI,
                    DateF = request.DateF,
                    Id = request.Id,
                    RequestTypeId = request.RequesType.Id,
                    RequestTypes = _combosHelper.GetComboRequestTypes(),
                    Resolution = request.Resolution,
                    StatusId = request.Status.Id,
                    Statuses = _combosHelper.GetComboStatuses(),
                    Tittle = request.Tittle,
                    Userr = request.Userr
                };

            }
            else
            {
                return new RequestViewModel
                {
                    CategoryId = request.Category.Id,
                    Categories = _combosHelper.GetComboCategories(),
                    Description = request.Description,
                    DateI = request.DateI,
                    DateF = request.DateF,
                    Id = request.Id,
                    RequestTypeId = request.RequesType.Id,
                    RequestTypes = _combosHelper.GetComboRequestTypes(),
                    Resolution = request.Resolution,
                    StatusId = request.Status.Id,
                    Statuses = _combosHelper.GetComboStatuses(),
                    Tittle = request.Tittle,
                    UserId = request.User.Id,
                    Users = _combosHelper.GetComboUsersN(),
                    Userr = request.Userr
                };
            }
            
        }

        public async Task<User> ToUserAsync(UserViewModel model, bool isNew)
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
                Areas = _combosHelper.GetComboAreas(),
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserType = user.UserType,



            };
        }


    }
}
