namespace HelloApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public required string Email { get; set; }
        public int? DeviceId { get; set; }
        public Device? Device { get; set; }
    }
}
