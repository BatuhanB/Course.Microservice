namespace Course.Web.Models
{
    public class SignIn
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool IsRemember { get; set; }
    }
}
