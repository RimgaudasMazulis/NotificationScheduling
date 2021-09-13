using Microsoft.AspNetCore.Mvc;
using NotificationScheduling.Domain.Exceptions;
using NotificationScheduling.Domain.Interfaces.Services;
using NotificationScheduling.Domain.Models;
using System.Threading.Tasks;

namespace NotificationScheduling.Web.Controllers
{
    [Route("api/notification-scheduling")]
    [ApiController]
    public class NotificationSchedulingController : ControllerBase
    {
        private readonly INotificationSchedulingService _service;

        public NotificationSchedulingController(INotificationSchedulingService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllCompanySchedules();
            return Ok(result);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CompanyModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException();
            }

            return Ok(await _service.CreateScheduleWithCompanyData(model));
        }
    }
}
