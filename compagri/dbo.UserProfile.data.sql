SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT INTO [dbo].[UserProfile] ([UserProfile_Id], [ProfileName], [CanLikeContributorConnection]) VALUES (1, 'Admin', 1)
INSERT INTO [dbo].[UserProfile] ([UserProfile_Id], [ProfileName], [CanLikeContributorConnection]) VALUES (2, 'Contributor', 1)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
