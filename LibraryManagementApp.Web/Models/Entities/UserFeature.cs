namespace LibraryManagementApp.Web.Models.Entities
{
    public class UserFeature
    {
        public Guid UserId { get; set; }
        public string Gender { get; set; } = default!;

        public AppUser AppUser { get; set; } = default!;
    }
}
