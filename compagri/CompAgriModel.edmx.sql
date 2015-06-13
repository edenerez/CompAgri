
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/09/2015 18:29:49
-- Generated from EDMX file: D:\Projects\GIT\CompAgri\CompAgri\CompAgriModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_9BA48E_xmldb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO


-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [User_Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [UserProfile_Id] int  NOT NULL
);
GO

-- Creating table 'UserProfile'
CREATE TABLE [dbo].[UserProfile] (
    [UserProfile_Id] int IDENTITY(1,1) NOT NULL,
    [ProfileName] nvarchar(max)  NOT NULL,
    [CanLikeContributorConnection] bit  NULL
);
GO

-- Creating table 'UserProfilePermission'
CREATE TABLE [dbo].[UserProfilePermission] (
    [UserProfilePermission_Id] int IDENTITY(1,1) NOT NULL,
    [ControllerName] nvarchar(max)  NOT NULL,
    [ActionName] nvarchar(max)  NOT NULL,
    [Allow] bit  NULL,
    [Deny] bit  NULL,
    [UserProfile_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [User_Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([User_Id] ASC);
GO

-- Creating primary key on [UserProfile_Id] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [PK_UserProfile]
    PRIMARY KEY CLUSTERED ([UserProfile_Id] ASC);
GO

-- Creating primary key on [UserProfilePermission_Id] in table 'UserProfilePermission'
ALTER TABLE [dbo].[UserProfilePermission]
ADD CONSTRAINT [PK_UserProfilePermission]
    PRIMARY KEY CLUSTERED ([UserProfilePermission_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserProfile_Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_UserUserProfile]
    FOREIGN KEY ([UserProfile_Id])
    REFERENCES [dbo].[UserProfile]
        ([UserProfile_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserProfile'
CREATE INDEX [IX_FK_UserUserProfile]
ON [dbo].[User]
    ([UserProfile_Id]);
GO

-- Creating foreign key on [UserProfile_Id] in table 'UserProfilePermission'
ALTER TABLE [dbo].[UserProfilePermission]
ADD CONSTRAINT [FK_UserProfileUserProfilePermission]
    FOREIGN KEY ([UserProfile_Id])
    REFERENCES [dbo].[UserProfile]
        ([UserProfile_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfileUserProfilePermission'
CREATE INDEX [IX_FK_UserProfileUserProfilePermission]
ON [dbo].[UserProfilePermission]
    ([UserProfile_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------