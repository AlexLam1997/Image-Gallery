--IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WelcomeEmailResources')
--	BEGIN

CREATE TABLE Users
(
	[ID] INT IDENTITY(1, 1), 
	[Username] NVARCHAR(50),
	[PW] NVARCHAR(50),
	[Salt] NVARCHAR(255),
	CONSTRAINT [Users_PK] PRIMARY KEY ([Id])
);

CREATE TABLE Images
( 
	[ID] INT IDENTITY(1, 1), 
    [FileName] NVARCHAR NOT NULL,
	[StorageName] UNIQUEIDENTIFIER NOT NULL,
	[Uploaded] DATETIME NOT NULL default GETDATE(), 
	CONSTRAINT [Images_PK] PRIMARY KEY ([Id])
) 

CREATE TABLE ImageAssociations
(
	[UserId] INT NOT NULL,
	[ImageId] INT NOT NULL,
	CONSTRAINT [FK_ImageAssociations_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
	CONSTRAINT [FK_ImageAssociations_Images] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Images] ([Id]),
	CONSTRAINT [ImageAssociations_PK] PRIMARY KEY CLUSTERED ([UserId] ASC, [ImageId] ASC)
)