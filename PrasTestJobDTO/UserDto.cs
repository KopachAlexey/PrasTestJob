namespace PrasTestJobDTO
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public string Login { get; set; }
        public string PasswordHas { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
