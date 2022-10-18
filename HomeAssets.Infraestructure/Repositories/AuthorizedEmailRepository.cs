using HomeAssets.Domain.Interfaces;
using HomeAssets.Domain.Models;
using HomeAssets.Infraestructure.DataBaseContext;

namespace HomeAssets.Infraestructure.Repositories
{
    public class AuthorizedEmailRepository : IAuthorizedEmailRepository
    {
        private readonly AppDbContext context;

        public AuthorizedEmailRepository(AppDbContext context)
        {
            this.context = context;
        }

        public AuthorizedEmail AddAuthorizedEmail(string newAuthorizedEmail)
        {
            AuthorizedEmail newAuthEmail = new AuthorizedEmail()
            {
                EmailAddress = newAuthorizedEmail,
                DateOfCreation = DateTime.UtcNow.Date
            };
            context.AuthorizedEmails.Add(newAuthEmail);
            context.SaveChanges();
            return newAuthEmail;
        }

        public IEnumerable<AuthorizedEmail> GetAllAuthorizedEmails()
        {
            return context.AuthorizedEmails;
        }

        public AuthorizedEmail GetByEmail(string email)
        {
            return GetAllAuthorizedEmails().FirstOrDefault(e => e.EmailAddress == email);
        }

        public AuthorizedEmail RemoveAuthorizedEmail(string emailToRemove)
        {
            AuthorizedEmail authEmailToDelete = GetByEmail(emailToRemove);
            if (authEmailToDelete != null)
            {
                context.AuthorizedEmails.Remove(authEmailToDelete);
                context.SaveChanges();
            }
            return authEmailToDelete;
        }
    }
}