﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IntelliTect.Utilities.Security
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user ID from a <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> to find a user ID for.</param>
        /// <returns>A <see cref="string"/>, or null if the <see cref="ClaimsPrincipal"/> doesn't contain a <see cref="ClaimTypes.NameIdentifier"/>.</returns>
        /// <exception cref="ArgumentNullException">principal is null.</exception>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));

            Claim claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null) return claim.Value;
            claim = principal.FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }


        /// <summary>
        /// Gets all the roles a <see cref="ClaimsPrincipal"/> belongs to.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> to find roles for.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="string"/> if found, or an empty set.</returns>
        /// <exception cref="ArgumentNullException">principal is null.</exception>
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));

            IEnumerable<Claim> claims = principal.FindAll(ClaimTypes.Role);
            return claims?.Select(claim => claim.Value) ?? Enumerable.Empty<string>();
        }
    }
}