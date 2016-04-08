using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Protocols.WSTrust;

namespace SecuredWebApi.Client
{
    internal static class Program
    {
        private static void Main()
        {
            var jwt = GetJwtFromTokenIssuer();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var result = client.GetStringAsync("http://localhost:5000/api/employee/123").Result;

            Console.WriteLine(result);
            Console.ReadLine();
        }

        // the token is usually aqquired by a broker
        // creating the token manually is just for demonstration 
        private static string GetJwtFromTokenIssuer()
        {
            var key = Convert.FromBase64String("ZzXLrqfHwpYY0snL2+0FagmGX+4FrnwO5CrP51YCFxc=");
            var symmetricKey = new InMemorySymmetricSecurityKey(key);
            
            var descriptor = new SecurityTokenDescriptor
            {
                TokenIssuerName = "http://authzserver.demo", 
                AppliesToAddress = "http://localhost:5000/api", 
                Lifetime = new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(1)), 
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest), 
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, "johny"), 
                })
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
