using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using System.Web.Http;
using Ingress.Data.Models;
using Ingress.Data.Repositories;
using Ingress.DTOs;
using log4net;

namespace Ingress.Api.Controllers
{
    [RoutePrefix("Users")]
    public class UserController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(UserController));

        [HttpGet]
        [Route("Get")]
        public async Task<List<string>> Get()
        {
            using (var context = new ActivityRepository(new IngressContext()))
            {
                return await context.GetUsers();
            }
        }

        [HttpPost]
        [Route("Login")]
        public string Login(LoginDTO dto)
        {
            try
            {
                _log.Info($"Login: {dto.Username}");

                using (var context = new PrincipalContext(ContextType.Domain, "TTINT", null, ContextOptions.Negotiate, null, null))
                {
                    using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, dto.Username))
                    {
                        if (user != null)
                        {
                            var password = LoginDTO.Decrypt(dto.PasswordBytes);

                            if (context.ValidateCredentials(dto.Username, password))
                            {
                                _log.Info($"...{user} successful login :-)");
                                return "SUCCESS";
                            }
                        }

                        _log.Info($"...'{user}' unsuccessful login :-(");
                        return "FAILURE";
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }
    }
}
