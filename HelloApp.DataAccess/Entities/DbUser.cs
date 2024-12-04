namespace HelloApp.DataAccess

{
    public class DbUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public required string Email { get; set; }
        public int? DeviceId { get; set; }
        public DbDevice? DbDevice { get; set; }
    }

}