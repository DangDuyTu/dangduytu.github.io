namespace RoadTrafficManagement.Models
{
    public class UserModel
    {
        public uint Id { get; set; }
        public string UserName { get; set; }
        public List<RoleDetail> RoleDetails { get; set; }
    }
}
