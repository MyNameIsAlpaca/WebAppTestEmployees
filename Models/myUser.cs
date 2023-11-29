namespace WebAppTestEmployees.Models
{
    public class myUser
    {
        public int Id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string? salt { get; set; } = null;
    }
}
