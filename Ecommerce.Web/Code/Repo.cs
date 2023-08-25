namespace Ecommerce.Web.Code
{
    public class Repo
    {
        public static class Session
        {
            public static string? Username
            {
                get {
                    string username = new HttpContextAccessor().HttpContext.Session.GetString("Username");
                    return username;
                }
                set {
                    new HttpContextAccessor().HttpContext.Session.SetString("Username", value ?? "");
                }
            }

            public static string? Token
            {
                get
                {
                    string token = new HttpContextAccessor().HttpContext.Session.GetString("Token");
                    return token;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Token", value ?? "");
                }
            }

            public static string? Role
            {
                get
                {
                    string rol = new HttpContextAccessor().HttpContext.Session.GetString("Rol");
                    return rol;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Rol", value ?? "");
                }
            }
        }
    }
}
