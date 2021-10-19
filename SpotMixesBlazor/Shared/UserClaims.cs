using System;

namespace SpotMixesBlazor.Shared
{
    public class UserClaims
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }
        public string UrlProfilePicture { get; set; }
    }
}