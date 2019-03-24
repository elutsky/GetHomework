using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.App.Controllers
{
    using HW.Infrastructure;
    using HW.Infrastructure.Entities;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private HomeworkProvider _homeworkProvider = new HomeworkProvider();
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CourseHomework>> Get()
        {
            var homeworks = _homeworkProvider.GetHomeWork();

            return homeworks;
        }

    }
}
