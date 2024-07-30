namespace Web.Aplication.Modal
{
    public class LoginModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class UserModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string FistName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public string[] Roles { get; set; }

    }

}
