namespace ResidenceStoreDemo
{
    using System;
    using System.Net.Mail;
    using ResidenceStore.Mailer;

    public class ResidenceStoreMailer : IResidenceStoreMailer
    {
        public void SendVerificationMail(string email, string verificationToken, Func<string, string> linkGenerator)
        {
            var sender = new MailAddress("noreply@residencestoredemo.blergh.be", "Residence Store Demo");
            var message = new MailMessage() {
                Sender = sender,
                From = sender,
                Subject = "[ResidenceStoreDemo] Residence Verification needed",
                Body = "Hi,\n\nYou just registered a new residence at http://residencestoredemo.blergh.be.\nTo continue, please verify your residence by clicking the link below:\n\n\t"+ linkGenerator(verificationToken) + "\n\nThanks,\n\tThe Residence Store Demo Team"
            };
            message.To.Add(new MailAddress(email));
            message.ReplyToList.Add(sender);
            new SmtpClient().Send(message);
        }
    }
}