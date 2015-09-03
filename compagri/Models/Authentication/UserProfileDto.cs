namespace CompAgri.Models.Authentication
{
    public class UserProfileDto
    {
        public UserProfileDto(UserProfile userProfile)
        {
            UserProfile_Id = userProfile.UserProfile_Id;
            ProfileName = userProfile.ProfileName;
            CanLikeContributorConnection = userProfile.CanLikeContributorConnection.HasValue && userProfile.CanLikeContributorConnection.Value;
            CanDeleteOtherPeopleConnection = userProfile.CanDeleteOtherPeopleConnection;
            CanDownloadFullLog = userProfile.CanDownloadFullLog;
        }
        public int UserProfile_Id { get; set; }
        public string ProfileName { get; set; }
        public bool CanLikeContributorConnection { get; set; }
        public bool CanDeleteOtherPeopleConnection { get; private set; }
        public bool CanDownloadFullLog { get; private set; }
    }
}