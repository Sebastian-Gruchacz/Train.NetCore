namespace Actio.Common.Events 
{
    public class UserCreated : IEvent 
    {
        // For serializers
        protected UserCreated () 
        {

        }

        public UserCreated (string email, string name) 
        {
            this.Email = email;
            this.Name = name;
        }

        public string Email { get; }

        public string Name { get; }
    }
}