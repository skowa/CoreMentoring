using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    public class RegisterUserResult
    {
        public bool EmailConfirmationRequired { get; set; }

        public IEnumerable<string> RegistrationErrors { get; set; }
    }
}
