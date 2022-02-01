using DistriHelp.API.Data.Entities;
using DistriHelp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model, bool isNew);
        Task<Request> ToRequestAsync(RequestViewModel model, bool isNew);

        UserViewModel ToUserViewModel(User user);

        RequestViewModel ToRequestViewModel(Request request);


    }
}
