namespace SmartBusAPI.Common.Consts
{
    public static class MailServerConsts
    {
        public const string Host = "smtp.gmail.com";
        public const int Port = 587;
        public static string Username = Encoding.Default.GetString(Convert.FromBase64String("c21hcnRidXNhZUBnbWFpbC5jb20="));
        public static string Password = Encoding.Default.GetString(Convert.FromBase64String("dHJsbnd6aW10bmllbXd3bA=="));
    }
}