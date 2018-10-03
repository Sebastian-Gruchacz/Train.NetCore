namespace Actio.Common.Events 
{
    public class UserAuthenticated : IEvent 
    {
        // for serialization
        protected UserAuthenticated () 
        {
        }

        public UserAuthenticated (string email) 
        {
            this.Email = email;
        }

        public string Email { get; }

    }
}