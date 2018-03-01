using System;
using System.Security.Claims;
using System.Security.Principal;
using System.ComponentModel;

namespace Core.Domain.Extensions
{
    public static class IdentityExtensions 
    {
        public static T GetClaim<T>(this IIdentity identity, string claimsKey)
        {
            if (identity == null) throw new ArgumentNullException("Identity");

            var claimsIdenity = identity as ClaimsIdentity;
            var claimsValue = claimsIdenity.FindFirst(claimsKey).Value;

            T t = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claimsValue);

            return t;

        }
    }
}
