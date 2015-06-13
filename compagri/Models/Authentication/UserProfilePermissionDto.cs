namespace CompAgri.Models.Authentication
{
    public class UserProfilePermissionDto
    {
        public int UserProfilePermission_Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool? Allow { get; set; }
        public bool? Deny { get; set; }
        public int UserProfile_Id { get; set; }
    }
}