namespace booking_api.Models
{
    public class LoginResponse : Response
    {
        public User User { get; set; }
        public string JWT { get; set; }
    }
}