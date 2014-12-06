
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/18/2014 16:35:57
-- Generated from EDMX file: F:\LaCeibaNetv4Copy2\LaCeibaNetv4\Models\LaCeibaDbv4Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LaCeibaDbv4];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientsTbl_CenterTbl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientsTbls] DROP CONSTRAINT [FK_ClientsTbl_CenterTbl];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramClientTbl_ClientsTbl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramClientTbls] DROP CONSTRAINT [FK_ProgramClientTbl_ClientsTbl];
GO
IF OBJECT_ID(N'[dbo].[FK_LoansTbl_RoundTbl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LoansTbls] DROP CONSTRAINT [FK_LoansTbl_RoundTbl];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTbl_LoansTbl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentTbls] DROP CONSTRAINT [FK_PaymentTbl_LoansTbl];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramClientTbl_ProgramTbl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramClientTbls] DROP CONSTRAINT [FK_ProgramClientTbl_ProgramTbl];
GO
IF OBJECT_ID(N'[dbo].[FK_RoundTbl_ProgramClientTbl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoundTbls] DROP CONSTRAINT [FK_RoundTbl_ProgramClientTbl];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CenterTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CenterTbls];
GO
IF OBJECT_ID(N'[dbo].[ClientsTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientsTbls];
GO
IF OBJECT_ID(N'[dbo].[LoansTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoansTbls];
GO
IF OBJECT_ID(N'[dbo].[PaymentTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentTbls];
GO
IF OBJECT_ID(N'[dbo].[ProgramClientTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramClientTbls];
GO
IF OBJECT_ID(N'[dbo].[ProgramTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramTbls];
GO
IF OBJECT_ID(N'[dbo].[RoundTbls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoundTbls];
GO
IF OBJECT_ID(N'[dbo].[RepFreq]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepFreq];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CenterTbls'
CREATE TABLE [dbo].[CenterTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Center] nchar(3)  NOT NULL
);
GO

-- Creating table 'ClientsTbls'
CREATE TABLE [dbo].[ClientsTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nchar(10)  NOT NULL,
    [LastName] nchar(10)  NOT NULL,
    [CenterId] int  NOT NULL,
    [MiddleName1] nchar(10)  NULL,
    [MiddleName2] nchar(10)  NULL,
    [DateAdded] datetime  NOT NULL,
    [BirthDay] datetime  NULL
);
GO

-- Creating table 'LoansTbls'
CREATE TABLE [dbo].[LoansTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AmtLoan] decimal(19,4)  NOT NULL,
    [TransferDate] datetime  NOT NULL,
    [Active] bit  NOT NULL,
    [RoundId] int  NOT NULL,
    [RepFreqId] int  NULL
);
GO

-- Creating table 'PaymentTbls'
CREATE TABLE [dbo].[PaymentTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DatePmtDue] datetime  NOT NULL,
    [DatePmtPaid] datetime  NULL,
    [AmtPaid] decimal(19,4)  NULL,
    [LoanId] int  NOT NULL
);
GO

-- Creating table 'ProgramClientTbls'
CREATE TABLE [dbo].[ProgramClientTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClientId] int  NOT NULL,
    [ProgramId] int  NOT NULL
);
GO

-- Creating table 'ProgramTbls'
CREATE TABLE [dbo].[ProgramTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Program] nchar(3)  NOT NULL,
    [InterestRate] float  NOT NULL
);
GO

-- Creating table 'RoundTbls'
CREATE TABLE [dbo].[RoundTbls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PClientNameId] int  NOT NULL,
    [RoundNum] int  NOT NULL
);
GO

-- Creating table 'RepFreq'
CREATE TABLE [dbo].[RepFreq] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CenterTbls'
ALTER TABLE [dbo].[CenterTbls]
ADD CONSTRAINT [PK_CenterTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientsTbls'
ALTER TABLE [dbo].[ClientsTbls]
ADD CONSTRAINT [PK_ClientsTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoansTbls'
ALTER TABLE [dbo].[LoansTbls]
ADD CONSTRAINT [PK_LoansTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentTbls'
ALTER TABLE [dbo].[PaymentTbls]
ADD CONSTRAINT [PK_PaymentTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramClientTbls'
ALTER TABLE [dbo].[ProgramClientTbls]
ADD CONSTRAINT [PK_ProgramClientTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramTbls'
ALTER TABLE [dbo].[ProgramTbls]
ADD CONSTRAINT [PK_ProgramTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoundTbls'
ALTER TABLE [dbo].[RoundTbls]
ADD CONSTRAINT [PK_RoundTbls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RepFreq'
ALTER TABLE [dbo].[RepFreq]
ADD CONSTRAINT [PK_RepFreq]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CenterId] in table 'ClientsTbls'
ALTER TABLE [dbo].[ClientsTbls]
ADD CONSTRAINT [FK_ClientsTbl_CenterTbl]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[CenterTbls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientsTbl_CenterTbl'
CREATE INDEX [IX_FK_ClientsTbl_CenterTbl]
ON [dbo].[ClientsTbls]
    ([CenterId]);
GO

-- Creating foreign key on [ClientId] in table 'ProgramClientTbls'
ALTER TABLE [dbo].[ProgramClientTbls]
ADD CONSTRAINT [FK_ProgramClientTbl_ClientsTbl]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[ClientsTbls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramClientTbl_ClientsTbl'
CREATE INDEX [IX_FK_ProgramClientTbl_ClientsTbl]
ON [dbo].[ProgramClientTbls]
    ([ClientId]);
GO

-- Creating foreign key on [RoundId] in table 'LoansTbls'
ALTER TABLE [dbo].[LoansTbls]
ADD CONSTRAINT [FK_LoansTbl_RoundTbl]
    FOREIGN KEY ([RoundId])
    REFERENCES [dbo].[RoundTbls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoansTbl_RoundTbl'
CREATE INDEX [IX_FK_LoansTbl_RoundTbl]
ON [dbo].[LoansTbls]
    ([RoundId]);
GO

-- Creating foreign key on [LoanId] in table 'PaymentTbls'
ALTER TABLE [dbo].[PaymentTbls]
ADD CONSTRAINT [FK_PaymentTbl_LoansTbl]
    FOREIGN KEY ([LoanId])
    REFERENCES [dbo].[LoansTbls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentTbl_LoansTbl'
CREATE INDEX [IX_FK_PaymentTbl_LoansTbl]
ON [dbo].[PaymentTbls]
    ([LoanId]);
GO

-- Creating foreign key on [ProgramId] in table 'ProgramClientTbls'
ALTER TABLE [dbo].[ProgramClientTbls]
ADD CONSTRAINT [FK_ProgramClientTbl_ProgramTbl]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[ProgramTbls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramClientTbl_ProgramTbl'
CREATE INDEX [IX_FK_ProgramClientTbl_ProgramTbl]
ON [dbo].[ProgramClientTbls]
    ([ProgramId]);
GO

-- Creating foreign key on [PClientNameId] in table 'RoundTbls'
ALTER TABLE [dbo].[RoundTbls]
ADD CONSTRAINT [FK_RoundTbl_ProgramClientTbl]
    FOREIGN KEY ([PClientNameId])
    REFERENCES [dbo].[ProgramClientTbls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoundTbl_ProgramClientTbl'
CREATE INDEX [IX_FK_RoundTbl_ProgramClientTbl]
ON [dbo].[RoundTbls]
    ([PClientNameId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------