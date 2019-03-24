using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.App.Controllers
{
    using System.Net.Http;
    using System.Text;

    using HW.Infrastructure;
    using HW.Infrastructure.Entities;

    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private HomeworkProvider _homeworkProvider = new HomeworkProvider();
        // GET api/values
        [Route("api/[controller]")]
        [HttpGet]
        public ActionResult<IEnumerable<CourseHomework>> Get([FromQuery] bool json = false)
        {
            var homeworks = _homeworkProvider.GetHomeWork();
            if (json)
            {

                return homeworks;
            }

            var responseText = new StringBuilder();
            foreach (var homework in homeworks)
            {
                responseText.AppendLine(homework.CourseName);
                foreach (var lesson in homework.Lessons)
                {
                    responseText.AppendLine("------------------");
                    responseText.AppendLine($"{lesson.Date}");
                    responseText.AppendLine($"Topic: {lesson.Topic}");
                    responseText.AppendLine($"HW: {lesson.Homework}");
                }
                responseText.AppendLine("==========================");
            }


            return Content(responseText.ToString());

        }

    }
}
