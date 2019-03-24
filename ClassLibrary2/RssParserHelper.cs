using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Infrastructure
{
    using System.Linq;

    using HW.Infrastructure.DTO;
    using HW.Infrastructure.Entities;

    public class RssParserHelper
    {
        public (string courseName, string date) ParseCourseName(Item item)
        {
            var parsedTitle = item.Title.Split('-');
            return (courseName: parsedTitle[0]?.Trim(), date: parsedTitle[1]?.Trim());
        }

        public (string topic, string homework) ParseDescription(Item item)
        {
            var topicPrefix = "נושא השיעור:";
            var homeWorkPreffix = "ש\"ב לשיעור:";

            var parsedDescription = item.Description.Split("<br>".ToCharArray());

            return (topic: parsedDescription.FirstOrDefault(split => split.Trim().StartsWith(topicPrefix))?.Replace(topicPrefix,String.Empty).Trim(),
                homework: parsedDescription.FirstOrDefault(split => split.Trim().StartsWith(homeWorkPreffix))?.Replace(homeWorkPreffix, String.Empty).Trim());
        }

        public Lesson ParseLesson(Item item)
        {
            var topicPrefix = "נושא השיעור:";
            var homeWorkPrefix = "ש\"ב לשיעור:";

            var parsedDescription = item.Description.Split("<br>".ToCharArray());

            var desc = (topic: parsedDescription.FirstOrDefault(split => split.Trim().StartsWith(topicPrefix))?.Replace(topicPrefix, String.Empty).Trim(),
                homework: parsedDescription.FirstOrDefault(split => split.Trim().StartsWith(homeWorkPrefix))?.Replace(homeWorkPrefix, String.Empty).Trim());

            var lesson = new Lesson { Homework = desc.homework ?? "---", Topic = desc.topic ?? "---", Date = DateTime.Parse(item.PubDate).ToString("MMMM dd") };

            return lesson;
        }

        public List<CourseHomework> ParseHomework(List<Item> rawLessons)
        {
            var result = new List<CourseHomework>();

            var courses = new Dictionary<string,CourseHomework>();

            foreach (var rawLesson in rawLessons)
            {
                var course = ParseCourseName(rawLesson);
                var homework = new CourseHomework { CourseName = ParseCourseName(rawLesson).courseName, };
                var lesson = ParseLesson(rawLesson);
                homework.Lessons.Add(lesson);

                var courseName = homework.CourseName.ToLowerInvariant();
                if(!courses.ContainsKey(courseName))
                {
                    courses.Add(courseName, homework);
                }
                else
                {
                    courses[courseName].Lessons.Add(lesson);
                }

            }

            return courses.Values.ToList();
        }
    }
}
