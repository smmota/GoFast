namespace GoFast.API.Models.ViewModels
{
    public class LoginViewModel
    {
        public string? LoginUser { get; set; }
        public string? Senha { get; set; }
    }

    public class LoginErroViewModel
    {
        public string? Error { get; set; }
    }
}
