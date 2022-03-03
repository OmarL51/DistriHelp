using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using DistriHelp.Common.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace DistriHelp.API.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public MailHelper(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public Response SendMail(string to, string subject, string body, string from, string password, IFormFileCollection attachments)
        {
          
            if (subject.Contains("CREACIÓN"))
            {
                try
                {


                
                    string smtp = _configuration["Mail:Smtp"];
                    string port = _configuration["Mail:Port"];
                    

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress(from));
                    message.To.Add(new MailboxAddress(to));
                    message.Subject = subject;
                    //message.Attachments = attachments;
                    



                    BodyBuilder bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = body
                    };

                    
                  
                    message.Body = bodyBuilder.ToMessageBody();
                    //if (attachments !=  null)
                    //{
                    //    foreach (var attachment in attachments)
                    //    {
                    //        bodyBuilder.Attachments.Add(attachment);
                    //    }
                    //}
                    using (SmtpClient client = new SmtpClient())
                    {
                        
                        client.Connect(smtp, int.Parse(port), false);
                        client.Authenticate(from, password);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    return new Response { IsSuccess = true };

                }
                catch (Exception ex)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                        Result = ex
                    };
                }


            }
            else
            {
                try
                {


                     from = _configuration["Mail:From"];
                     password = _configuration["Mail:Password"];
                    string smtp = _configuration["Mail:Smtp"];
                    string port = _configuration["Mail:Port"];
                   

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress(from));
                    message.To.Add(new MailboxAddress(to));
                    message.Subject = subject;


                    BodyBuilder bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = body
                    };
                    message.Body = bodyBuilder.ToMessageBody();

                    using (SmtpClient client = new SmtpClient())
                    {
                        client.Connect(smtp, int.Parse(port), false);
                        client.Authenticate(from, password);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    return new Response { IsSuccess = true };

                }
                catch (Exception ex)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                        Result = ex
                    };
                }


            }
        }
        }
               
        }
           
    

