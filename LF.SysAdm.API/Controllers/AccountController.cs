using LF.SysAdm.API.Controllers.Base;
using LF.SysAdm.API.Security;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.User;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LF.SysAdm.API.Controllers
{
    [Route("api")]
    public class AccountController : BaseController
    {

        private readonly IBusinessUser _service;
        private Users _UserAuthenticated;
        private readonly TokenOptions _TokenOptions;
        private readonly JsonSerializerSettings _serializeSettings;

        public AccountController(IOptions<TokenOptions> jwtOptions, IBusinessUser service)
        {
            _service = service;
            _TokenOptions = jwtOptions.Value;    // recebendo o value do token
            ThrowIfInvalidOptions(_TokenOptions);  // usando o metodo criado para tratamento de excessões 

            // vericar se é preciso para versões mais recentes se nao precisar pode apagar essa configuração
            _serializeSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("v1/authenticate")]
        public async Task<IActionResult> PostUserAuthenticate([FromForm] AuthenticateUserCommand cmd)
        {
            if (cmd == null)
                return
                    await CreateResponse(new ObjectRequest().CreateObjectRequest("Usuario ou Senha Invalidos",false));

            ClaimsIdentity identity = await GetClaims(cmd);

            if (identity == null)
                return
                   await CreateResponse(new ObjectRequest().CreateObjectRequest("Usuario ou Senha Invalidos",false));

            // criando um array de claims para podermos acessar no  controller atraves do User.Identity.Name
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, cmd.Email),
                new Claim(JwtRegisteredClaimNames.NameId, cmd.Email),
                new Claim(JwtRegisteredClaimNames.Email,cmd.Email),
                new Claim(JwtRegisteredClaimNames.Sub, cmd.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _TokenOptions.JtiGenerator()),                
                new Claim(JwtRegisteredClaimNames.Iat, ToUnityEpochDate(_TokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst("Lafan")   
            };

            // Gerar o Json Web Token 

            var jwt = new JwtSecurityToken(
                issuer: _TokenOptions.Issuer,
                audience: _TokenOptions.Audience,
                claims: Claims.AsEnumerable(),
                notBefore: _TokenOptions.NotBefore,
                expires: _TokenOptions.Expiration,
                signingCredentials: _TokenOptions.SigningCredentials);

            // codificar o token 
            var encondedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Gerar a Resposta

            var response = new
            {
                access_token = encondedJwt,
                expires_in = (int)_TokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _UserAuthenticated.ID,
                    name = _UserAuthenticated.Name,
                    email = _UserAuthenticated.Email,
                    role = _UserAuthenticated.Profile.ToString()
                }
            };

            // converter para Json
            var json = JsonConvert.SerializeObject(response, _serializeSettings);

            return Ok(json);
             
        }


        // metodo para tratar as excptions, quando verifica o tokenOptions
        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentNullException("O Periodo deve ser maior que zero ", nameof(TokenOptions.ValidFor));
            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));
            if (options.JtiGenerator == null) throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }


        // metodo para converter data para formato TimeStamp/ Unix
        private static long ToUnityEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand cmd)
        {
            var user = _service.GetUserOAuth(cmd.Email, cmd.Password);
            if (user == null) return Task.FromResult<ClaimsIdentity>(null);

            _UserAuthenticated = user;
            

            return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.Email, "Token"),
                new[]
                {
                    new Claim("Lafan", user.Profile.ToString())
                }
                ));
        }
    }
}
