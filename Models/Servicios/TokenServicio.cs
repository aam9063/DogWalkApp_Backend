using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Interfaces;
using Models.Models;

namespace Models.Servicios
{
    public class TokenServicio : ITokenServicio //Implementamos la interfaz ITokenServicio
    {
        private readonly SymmetricSecurityKey _key; //Creamos una variable de tipo SymmetricSecurityKey

        public TokenServicio(IConfiguration config) 
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //Inicializamos la variable _key con la clave de seguridad
        }

        public string CrearToken(Usuario usuario) //Método para crear un token
        {
            var claims = new List<Claim> //Creamos una lista de claims. Los claims son los datos que se van a guardar en el token
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Email)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); //Creamos las credenciales para firmar el token
            var tokenDescriptor = new SecurityTokenDescriptor //Creamos un descriptor de token
            {
                Subject = new ClaimsIdentity(claims), 
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler(); //Creamos un manejador de token
            var token = tokenHandler.CreateToken(tokenDescriptor); //Creamos el token
            return tokenHandler.WriteToken(token); //Retornamos el token
        }
    }
}
