using System;

namespace MarsWebSite
{
    public class AppSettings
    {
        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }

        public static string IndexPage {
            get { return GetEnvironmentVariable("indexPage"); }
        }

        public static string CoureseContentEndPoint {
            get { return GetEnvironmentVariable("CoureseContentEndPoint"); }
        }

        public static string CourseSASToken
        {
            get { return GetEnvironmentVariable("CourseSASToken"); }
        }
    }
}
