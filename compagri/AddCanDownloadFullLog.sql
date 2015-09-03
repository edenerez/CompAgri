ALTER TABLE [UserProfile]
ADD  [CanDownloadFullLog]             BIT            NOT NULL DEFAULT 0
GO
ALTER TABLE [UserProfile]
ADD DEFAULT 0 FOR [CanDeleteOtherPeopleConnection]
GO