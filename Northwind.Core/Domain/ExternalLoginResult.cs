using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    public class ExternalLoginResult
    {
        public bool UserExists { get; set; }

        public bool SignInSucceeded { get; set; }

        public bool IsLockedOut { get; set; }

        public bool CreateUserSucceeded { get; set; }

        public IEnumerable<string> CreateUserErrors { get; set; }
    }
}
