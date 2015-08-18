
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/17/2015 21:07:32
-- Generated from EDMX file: C:\Users\Fausto\Proyectos\CompAgri\CompAgri\CompAgriModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [XMLDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserOpinions'
CREATE TABLE [dbo].[UserOpinions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Connection_Id] int  NOT NULL,
    [Opinion] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserOpinions'
ALTER TABLE [dbo].[UserOpinions]
ADD CONSTRAINT [PK_UserOpinions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'UserOpinions'
ALTER TABLE [dbo].[UserOpinions]
ADD CONSTRAINT [FK_UserUserConnectionOpinion]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[User]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserConnectionOpinion'
CREATE INDEX [IX_FK_UserUserConnectionOpinion]
ON [dbo].[UserOpinions]
    ([User_Id]);
GO

-- Creating foreign key on [Connection_Id] in table 'UserOpinions'
ALTER TABLE [dbo].[UserOpinions]
ADD CONSTRAINT [FK_ConnectionUserConnection_Opinion]
    FOREIGN KEY ([Connection_Id])
    REFERENCES [dbo].[Connection]
        ([Connection_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConnectionUserConnection_Opinion'
CREATE INDEX [IX_FK_ConnectionUserConnection_Opinion]
ON [dbo].[UserOpinions]
    ([Connection_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------