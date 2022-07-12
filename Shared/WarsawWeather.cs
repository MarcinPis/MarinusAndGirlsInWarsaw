using System;
using System.Collections.Generic;

namespace BlazorApp.Shared
{
    public class WarsawWeather
    {
        public Timelines data { get; set; }
    }

    public class Timelines
    {
        public List<Timeline> timelines { get; set; }
    }

    public class Timeline
    {
        public string timestep { get; set; }
        public string endTime { get; set; }
        public string startTime { get; set; }
        public List<Interval> intervals { get; set; }
    }

    public class Interval
    {
        public DateTime startTime { get; set; }
        public Values values { get; set; }
    }

    public class Values
    {
        public double temperatureMax { get; set; }
        public double temperatureMin { get; set; }
        public double temperatureApparent { get; set; }
        public double cloudCover { get; set; }
        public double precipitationIntensity { get; set; }
        public double precipitationProbability { get; set; }
        public int precipitationType { get; set; }
        public double windSpeed { get; set; }
        public int weatherCode { get; set; }

    }
}
