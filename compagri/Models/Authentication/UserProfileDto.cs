namespace CompAgri.Models.Authentication
{
    public class UserProfileDto
    {
        public UserProfileDto(UserProfile userProfile)
        {
            UserProfile_Id = userProfile.UserProfile_Id;
            ProfileName = userProfile.ProfileName;
            CanLikeContributorConnection = userProfile.CanLikeContributorConnection.HasValue && userProfile.CanLikeContributorConnection.Value;
        }
        public int UserProfile_Id { get; set; }
        public string ProfileName { get; set; }
        public bool CanLikeContributorConnection { get; set; }
    }
}