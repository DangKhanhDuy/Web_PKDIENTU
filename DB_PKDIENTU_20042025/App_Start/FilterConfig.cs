using System.Web;
using System.Web.Mvc;

namespace DB_PKDIENTU_20042025
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
