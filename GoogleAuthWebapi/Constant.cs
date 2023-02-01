
using System.Configuration;


namespace GoogleAuthWebapi
{
    public static class Constant
    {
        public static string dbcon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
    }
}