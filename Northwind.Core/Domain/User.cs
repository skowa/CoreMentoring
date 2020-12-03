namespace Northwind.Core.Domain
{
    public class User
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string Code { get; set; }
    }
}
