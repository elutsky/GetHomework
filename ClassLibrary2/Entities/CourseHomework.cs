namespace HW.Infrastructure.Entities
{
    using System.Collections.Generic;

    public class CourseHomework
    {
        public CourseHomework()
        {
            Lessons = new List<Lesson>();
        }
        public string CourseName { get; set; }

        public List<Lesson> Lessons{ get; set; }

    }
}