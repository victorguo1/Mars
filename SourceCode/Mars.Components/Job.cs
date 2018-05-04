using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.Components
{
    public class Job
    {
        public string Title { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Summary { get; set; }

        public string Url { get; set; }

        public Job(string title, string company, string location, string summary, string url)
        {
            this.Title = title;
            this.Company = company;
            this.Location = location;
            this.Summary = summary;
            this.Url = url;
        }
    }
}
