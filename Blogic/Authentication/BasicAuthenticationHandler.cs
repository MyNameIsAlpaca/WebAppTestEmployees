﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace WebAppTestEmployees.Blogic.Authentication
{
    public class BasicAuthenticationHandler :AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione mancante: impossibile accedere al servizio"));
            }
            var authorizationHeader = Request.Headers["Authorization"].ToString();

            var authorizationRegEx = new Regex(@"Basic (.*)");

            if(!authorizationRegEx.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione non valida: Impossibile accedere al servizio"));
            }

            var authorizationBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationRegEx.Replace(authorizationHeader, "$1")));

            var authorizationSplit = authorizationBase64.Split(':');

            if(authorizationSplit.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione non valida: Impossibile accedere al servizio"));
            }

            var username = authorizationSplit[0];

            //if((username != "tony") || (authorizationSplit[1] != "controllo"))
            //{
            //    return Task.FromResult(AuthenticateResult.Fail("Nome utente o password errata"));
            //}

            var authenticationUser = new AuthenticationUser(username, "BasicAuthentication", true);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(authenticationUser));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claims, "BasicAuthentication")));
        }
    }
}
