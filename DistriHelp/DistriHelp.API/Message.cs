using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace DistriHelp.API
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public string From { get; set; }
        public string Password { get; set; }
        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments, string from, string password)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
            From = from;
            Password = password;
        }
    }
}
