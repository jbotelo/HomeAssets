namespace HomeAssets.ViewModels
{
    public class AuthorizedEmails_vmodel
    {
        public AuthorizedEmails_vmodel()
        {
            AlreadyAuthorizedEmails = new List<string>();
        }
        public List<string> AlreadyAuthorizedEmails { get; set; }
    }
}