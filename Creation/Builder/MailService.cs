using System;
using System.Collections.Generic;
using System.Text;

namespace designs.Creation.Builder
{
    public class Email
    {
        public string From, To, Subject, Body;
    }

    public class MailService
    {
        public class EmailBuilder
        {
            private Email email { get; }

            public EmailBuilder(Email email)
            {
                this.email = email;
            }
            public EmailBuilder From(string from)
            {
                email.From = from;
                return this;
            }
            public EmailBuilder To(string to)
            {
                email.To = to;
                return this;
            }

            public static implicit operator Email(EmailBuilder builder) => builder.email;
        }

        private void SendEmailInternal(Email email)
        {
            Console.WriteLine($"{email.From} - {email.To}");
        }
        public void SendEmail(Action<EmailBuilder> builder)
        {
            var email = new Email();
            builder(new EmailBuilder(email));
            SendEmailInternal(email);
        }
    }

    [TestCase]
    public class MailServiceTest : ITest
    {
        public void Run()
        {
            var ms = new MailService();
            ms.SendEmail(email => email.From("foo@bar.com")
            .To("bar@baz.com"));
        }
    }
}
