using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Models
{
    public class CustomIdentityValidator: IdentityErrorDescriber //parola doğrulayıcı
    {
        public override IdentityError PasswordTooShort(int length) // Parola çok kısa hatası
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola Minimum {length} karakter olmalıdır"
            };
        }
        public override IdentityError PasswordRequiresUpper() // Büyük harf 
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = "Parola en az 1 büyük harf içermelidir"
            };
        }
        public override IdentityError PasswordRequiresLower() // Küçük harf
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = "Parola en az 1 küçük harf içermelidir"
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric() // AlfaNumeric olmayan karakter gereksinimi hatası
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Parola en az 1 sembol içermelidir"
            };
        }
    }
}
