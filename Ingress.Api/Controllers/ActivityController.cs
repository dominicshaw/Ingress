using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Ingress.Api.Factories;
using Ingress.Data.Models;
using Ingress.Data.Repositories;
using Ingress.DTOs;
using log4net;

namespace Ingress.Api.Controllers
{
    [RoutePrefix("Activities")]
    public class ActivityController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ActivityController));

        [HttpGet]
        [Route("Get")]
        public async Task<List<ActivityDTO>> Get()
        {
            using (var context = new ActivityRepository(new IngressContext()))
            {
                var results = await context.GetAll();

                return results.Select(ActivityToDTO.Get).ToList();
            }
        }

        [HttpGet]
        [Route("ByUser/{username}")]
        public async Task<List<ActivityDTO>> ByUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                return new List<ActivityDTO>();

            using (var context = new ActivityRepository(new IngressContext()))
            {
                var results = await context.GetByUsername(username);

                return results.Select(ActivityToDTO.Get).ToList();
            }
        }

        [HttpGet]
        [Route("ById/{id}")]
        public async Task<ActivityDTO> ById(int id)
        {
            using (var context = new ActivityRepository(new IngressContext()))
            {
                return ActivityToDTO.Get(await context.GetById(id));
            }
        }

        [HttpPost]
        [Route("Save")]
        public async Task<string> Save(ActivityDTO dto)
        {
            _log.Info($"Save for activity \'{dto?.Subject}\'; type is \'{dto?.GetType()}\'; ID is {dto?.ActivityID} (for user \'{dto?.Username}')");

            try
            {
                using (var context = new ActivityRepository(new IngressContext()))
                {
                    var activity = DTOToActivity.Get(dto);

                    if (activity.ActivityID == 0)
                        context.Create(activity);
                    else
                        context.Update(activity);

                    await context.SaveChanges();

                    return activity.ActivityID.ToString();
                }
            }
            catch (Exception ex) { _log.Error(ex); throw; }
        }
    }
}