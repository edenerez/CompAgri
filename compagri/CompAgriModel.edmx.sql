
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/20/2015 21:23:17
-- Generated from EDMX file: C:\Users\Fausto\Proyectos\CompAgri\CompAgri\CompAgriModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_9BA48E_xmldb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------
IF OBJECT_ID(N'[dbo].[FK_ConnectionUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Connection] DROP CONSTRAINT [FK_ConnectionUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfileUserProfilePermission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfilePermission] DROP CONSTRAINT [FK_UserProfileUserProfilePermission];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_UserUserProfile];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfile];
GO
IF OBJECT_ID(N'[dbo].[UserProfilePermission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfilePermission];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------
-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [User_Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL,
    [Token] nvarchar(max)  NULL,
    [UserProfile_Id] int  NOT NULL,
    [UserName] nvarchar(max)  NULL
);
GO

-- Creating table 'UserProfile'
CREATE TABLE [dbo].[UserProfile] (
    [UserProfile_Id] int IDENTITY(1,1) NOT NULL,
    [ProfileName] nvarchar(max)  NOT NULL,
    [CanLikeContributorConnection] bit  NULL,
    [CanDeleteOtherPeopleConnection] bit  NOT NULL
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

-- Creating foreign key on [Connection_Id_User] in table 'Connection'
ALTER TABLE [dbo].[Connection]
ADD CONSTRAINT [FK_ConnectionUser]
    FOREIGN KEY ([Connection_Id_User])
    REFERENCES [dbo].[User]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------