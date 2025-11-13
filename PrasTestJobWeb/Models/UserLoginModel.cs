namespace PrasTestJobWeb.Models
{
    public class UserLoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool? IsLoginSucces { get; set; }
    }
}
