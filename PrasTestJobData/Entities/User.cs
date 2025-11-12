namespace PrasTestJobData.Entities
{
    public class User
    {
        private Guid _id;

        public Guid Id => _id;
        public string Login { get; set; }
        public string PasswordHas { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
