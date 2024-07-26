namespace RoadTrafficManagement.Repositories
{
    public class UserModel
    {
        public uint Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public sbyte InActive { get; set; }
    }
}
