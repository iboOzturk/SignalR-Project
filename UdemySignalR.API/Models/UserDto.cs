namespace UdemySignalR.API.Models
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public List<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
