using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerReviews_Server
{
    public class AuthOptions
    {
        public const string ISSUER = "BeerReviews";
        public static string KEY = "asd1231asd123213dasd1sdas";
        public const int LIFETIME = 60;
        public const string AUDIENCE = "DUXAI"; // потребитель токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
