SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT INTO [dbo].[UserProfile] ([UserProfile_Id], [ProfileName], [CanLikeContributorConnection], [CanDeleteOtherPeopleConnection], [CanDownloadFullLog]) VALUES (1, 'Admin', 1, 1, 1)
INSERT INTO [dbo].[UserProfile] ([UserProfile_Id], [ProfileName], [CanLikeContributorConnection], [CanDeleteOtherPeopleConnection], [CanDownloadFullLog]) VALUES (2, 'Contributor', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
