DROP TABLE [dbo].[Secret];

CREATE TABLE [dbo].[Secret]
(
	[SecretId] INT IDENTITY NOT NULL PRIMARY KEY, 
    [AccessPhrase] VARCHAR(8) NOT NULL, 
    [Value] VARCHAR(512) NOT NULL, 
    [ExpireDate] DATETIME NOT NULL, 
    [PassPhrase] VARCHAR(50) NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
	[Served] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME NOT NULL DEFAULT getutcdate(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT getutcdate(), 
    [DelFlag] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [AK_Secret_AccessPhrase] UNIQUE (AccessPhrase) 
)

GO

CREATE INDEX [IX_Secret_AccessPhrase] ON [dbo].[Secret] (AccessPhrase)
