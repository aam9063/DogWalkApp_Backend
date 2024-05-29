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

namespace Models.Interfaces
{
    public interface ITokenServicioPaseador 
    {
        string CrearTokens(Paseador paseador);

    }
}
