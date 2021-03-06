
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2012 17:19:11
-- Generated from EDMX file: D:\Documents\GitHub\SocialProgramming\WebApplication\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db2d18f7c2b5554fe49690a0bb00803280];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_UserEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryEntry_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryEntry] DROP CONSTRAINT [FK_CategoryEntry_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryEntry_Entry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryEntry] DROP CONSTRAINT [FK_CategoryEntry_Entry];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_UserPage];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryKeyword_Entry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntryKeyword] DROP CONSTRAINT [FK_EntryKeyword_Entry];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryKeyword_Keyword]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntryKeyword] DROP CONSTRAINT [FK_EntryKeyword_Keyword];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_EntryFile];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Entries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries];
GO
IF OBJECT_ID(N'[dbo].[Pages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pages];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[FeedBacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedBacks];
GO
IF OBJECT_ID(N'[dbo].[Keywords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Keywords];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[VisitorDataSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VisitorDataSet];
GO
IF OBJECT_ID(N'[dbo].[CategoryEntry]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryEntry];
GO
IF OBJECT_ID(N'[dbo].[EntryKeyword]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntryKeyword];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [huTitle] nvarchar(max)  NOT NULL,
    [enTitle] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [ActivationCode] nvarchar(max)  NOT NULL,
    [IsActivated] bit  NOT NULL,
    [RegistrationDate] datetime  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [CookieHash] nvarchar(max)  NULL
);
GO

-- Creating table 'Entries'
CREATE TABLE [dbo].[Entries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [huTitle] nvarchar(max)  NOT NULL,
    [enTitle] nvarchar(max)  NOT NULL,
    [Published] bit  NOT NULL,
    [PublishedDate] datetime  NOT NULL,
    [huContent] nvarchar(max)  NOT NULL,
    [enContent] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [huIntroduction] nvarchar(max)  NOT NULL,
    [enIntroduction] nvarchar(max)  NOT NULL,
    [IsFeatured] bit  NOT NULL,
    [FeaturedImage] nvarchar(max)  NOT NULL,
    [FeaturedEnText] nvarchar(max)  NULL,
    [FeaturedHuText] nvarchar(max)  NULL
);
GO

-- Creating table 'Pages'
CREATE TABLE [dbo].[Pages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [huTitle] nvarchar(max)  NOT NULL,
    [enTitle] nvarchar(max)  NOT NULL,
    [Published] bit  NOT NULL,
    [huContent] nvarchar(max)  NOT NULL,
    [enContent] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [PublishedDate] datetime  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FeedBacks'
CREATE TABLE [dbo].[FeedBacks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [SendDate] datetime  NOT NULL,
    [Checked] bit  NOT NULL
);
GO

-- Creating table 'Keywords'
CREATE TABLE [dbo].[Keywords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Type] bit  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [EntryId] int  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Entry_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'VisitorDataSet'
CREATE TABLE [dbo].[VisitorDataSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IpAddress] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'CategoryEntry'
CREATE TABLE [dbo].[CategoryEntry] (
    [Categories_Id] int  NOT NULL,
    [Entries_Id] int  NOT NULL
);
GO

-- Creating table 'EntryKeyword'
CREATE TABLE [dbo].[EntryKeyword] (
    [Entries_Id] int  NOT NULL,
    [Keywords_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [PK_Entries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pages'
ALTER TABLE [dbo].[Pages]
ADD CONSTRAINT [PK_Pages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FeedBacks'
ALTER TABLE [dbo].[FeedBacks]
ADD CONSTRAINT [PK_FeedBacks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Keywords'
ALTER TABLE [dbo].[Keywords]
ADD CONSTRAINT [PK_Keywords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VisitorDataSet'
ALTER TABLE [dbo].[VisitorDataSet]
ADD CONSTRAINT [PK_VisitorDataSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Categories_Id], [Entries_Id] in table 'CategoryEntry'
ALTER TABLE [dbo].[CategoryEntry]
ADD CONSTRAINT [PK_CategoryEntry]
    PRIMARY KEY NONCLUSTERED ([Categories_Id], [Entries_Id] ASC);
GO

-- Creating primary key on [Entries_Id], [Keywords_Id] in table 'EntryKeyword'
ALTER TABLE [dbo].[EntryKeyword]
ADD CONSTRAINT [PK_EntryKeyword]
    PRIMARY KEY NONCLUSTERED ([Entries_Id], [Keywords_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [FK_UserEntry]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEntry'
CREATE INDEX [IX_FK_UserEntry]
ON [dbo].[Entries]
    ([UserId]);
GO

-- Creating foreign key on [Categories_Id] in table 'CategoryEntry'
ALTER TABLE [dbo].[CategoryEntry]
ADD CONSTRAINT [FK_CategoryEntry_Category]
    FOREIGN KEY ([Categories_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Entries_Id] in table 'CategoryEntry'
ALTER TABLE [dbo].[CategoryEntry]
ADD CONSTRAINT [FK_CategoryEntry_Entry]
    FOREIGN KEY ([Entries_Id])
    REFERENCES [dbo].[Entries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryEntry_Entry'
CREATE INDEX [IX_FK_CategoryEntry_Entry]
ON [dbo].[CategoryEntry]
    ([Entries_Id]);
GO

-- Creating foreign key on [UserId] in table 'Pages'
ALTER TABLE [dbo].[Pages]
ADD CONSTRAINT [FK_UserPage]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPage'
CREATE INDEX [IX_FK_UserPage]
ON [dbo].[Pages]
    ([UserId]);
GO

-- Creating foreign key on [Entries_Id] in table 'EntryKeyword'
ALTER TABLE [dbo].[EntryKeyword]
ADD CONSTRAINT [FK_EntryKeyword_Entry]
    FOREIGN KEY ([Entries_Id])
    REFERENCES [dbo].[Entries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Keywords_Id] in table 'EntryKeyword'
ALTER TABLE [dbo].[EntryKeyword]
ADD CONSTRAINT [FK_EntryKeyword_Keyword]
    FOREIGN KEY ([Keywords_Id])
    REFERENCES [dbo].[Keywords]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryKeyword_Keyword'
CREATE INDEX [IX_FK_EntryKeyword_Keyword]
ON [dbo].[EntryKeyword]
    ([Keywords_Id]);
GO

-- Creating foreign key on [EntryId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_EntryFile]
    FOREIGN KEY ([EntryId])
    REFERENCES [dbo].[Entries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryFile'
CREATE INDEX [IX_FK_EntryFile]
ON [dbo].[Files]
    ([EntryId]);
GO

-- Creating foreign key on [Entry_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentEntry]
    FOREIGN KEY ([Entry_Id])
    REFERENCES [dbo].[Entries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentEntry'
CREATE INDEX [IX_FK_CommentEntry]
ON [dbo].[Comments]
    ([Entry_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentUser'
CREATE INDEX [IX_FK_CommentUser]
ON [dbo].[Comments]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------