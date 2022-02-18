using DistriHelp.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body, string from, string password);
    }
}
