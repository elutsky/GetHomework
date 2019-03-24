namespace HW.Tests
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    using FluentAssertions;

    using HW.Infrastructure;
    using HW.Infrastructure.DTO;

    using Xunit;

    public class XmlParserShould
    {
        private string _document;
        XmlSerializer _serializer = new XmlSerializer(typeof(Rss));

        public XmlParserShould()
        {
            _document = @"<?xml version=""1.0"" encoding=""utf-8""?><rss xmlns:a10=""http://www.w3.org/2005/Atom"" version=""2.0""><channel><title>פירוט מהלך שיעורים לא' - 2</title><link>http://edu.gov.il/</link><description>פירוט מהלך שיעורים לתאריכים י' אדר ב תשע""ט, 17/03/2019 עד י""ז אדר ב תשע""ט, 24/03/2019 עבור א' - 2 (עדכני ל-24/03/2019 14:20)</description><language>he-il</language><item><guid isPermaLink=""false"">0</guid><title>חשבון - 24/03/2019</title><description>&lt;span style='font-size: 0.7em;  color:Gray;'&gt;(עודכן ב-24/03/2019 13:31)&lt;/span&gt;&lt;br /&gt; נושא השיעור: תרגול וחזרה על החומר הנלמד &lt;br /&gt;  ש""ב לשיעור: תלמידים שנמצאים בחוברת 2 מתבקשים להתקדם גם בבית מספר עמודים </description><pubDate>Sun, 24 Mar 2019 10:15:00 +0200</pubDate></item><item><guid isPermaLink=""false"">1</guid><title>עברית - 24/03/2019</title><description>&lt;span style='font-size: 0.7em;  color:Gray;'&gt;(עודכן ב-24/03/2019 13:04)&lt;/span&gt;&lt;br /&gt; נושא השיעור: חזרה ותרגול בקריאה &lt;br /&gt; פירוט נושא השיעור: תרגול במשחקים דידיקטיים, לוח מחיק... &lt;br /&gt; ש""ב לשיעור: אין שב.
לתרגל קריאה מהחוברת </description><pubDate>Sun, 24 Mar 2019 08:00:00 +0200</pubDate></item><item><guid isPermaLink=""false"">2</guid><title>גיאומטריה - 18/03/2019</title><description>&lt;span style='font-size: 0.7em;  color:Gray;'&gt;(עודכן ב-18/03/2019 09:31)&lt;/span&gt;&lt;br /&gt; נושא השיעור: מלבן וריבוע &lt;br /&gt; פירוט נושא השיעור: עבודה בחוברת מלבן וריבוע- ההבדלים ביניהם &lt;br /&gt; ש""ב לשיעור: חוברת עמוד 87  בלבד!! נא לא להתקדם לבד!! </description><pubDate>Mon, 18 Mar 2019 08:00:00 +0200</pubDate></item></channel></rss>
";
        }

        [Fact]
        public void ParseRssDocument()
        {
            // When 
            var result = (Rss)_serializer.Deserialize(new StringReader(_document));

            // Then 
            result.Channel.Item.Count.Should().Be(3);
        }

        [Fact]
        public void ParseCourseName()
        {
            // Given 
            var parsed = (Rss)_serializer.Deserialize(new StringReader(_document));

            // When 
            var result = new RssParserHelper().ParseCourseName(parsed.Channel.Item[0]);

            // Then 
            result.courseName.Should().Be("חשבון");
        }

        [Fact]
        public void ParseCourseDescription()
        {
            // Given 
            var parsed = (Rss)_serializer.Deserialize(new StringReader(_document));

            // When 
            var result = new RssParserHelper().ParseDescription(parsed.Channel.Item[0]);

            // Then 
            result.topic.Should().Be("תרגול וחזרה על החומר הנלמד");
            result.homework.Should().Be("תלמידים שנמצאים בחוברת 2 מתבקשים להתקדם גם בבית מספר עמודים");
        }

        [Fact]
        public void ParseHomeWork()
        {
            // Given 
            var parsed = (Rss)_serializer.Deserialize(new StringReader(_document));

            // When 
            var result = new RssParserHelper().ParseHomework(parsed.Channel.Item);

            // Then 
            result.Count.Should().Be(3);
        }
    }
}
