using SeniorLearnDataSeed.Models.Session;

namespace SeniorLearnDataSeed.Helpers
{
    public class JSONListHelper
    {
        public static string GetEventListJsonString(List<Models.Session.SessionDetails> events)
        {
            var eventlist = new List<Event>();

            foreach (var e in events)
            {
                var myevent = new Event
                {
                    id = e.SessionId,
                    start = e.StartTime,
                    end = e.EndTime,
                };

                eventlist.Add(myevent);
            }

            return System.Text.Json.JsonSerializer.Serialize(eventlist);

        }

    }
    public class Event
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }


    }

 
}
