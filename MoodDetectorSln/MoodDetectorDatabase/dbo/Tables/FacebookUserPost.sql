CREATE TABLE [dbo].[FacebookUserPost]
(
	[FacebookUserPostId] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[PostId] NVARCHAR(50) NOT NULL, 
	[FacebookProfileId] BIGINT NOT NULL,
    [Caption] NVARCHAR(500) NULL, 
    [Description] NVARCHAR(MAX) NULL,
	[From] NVARCHAR(500) NULL, 
    [Name] NVARCHAR(500) NULL, 
    [Status_Type] NVARCHAR(500) NULL, 
    [Story] NVARCHAR(MAX) NULL, 
    [Type] NVARCHAR(500) NULL, 
    [Message] NVARCHAR(MAX) NULL, 
    [DatePosted] DATETIMEOFFSET NOT NULL, 
    CONSTRAINT [FK_FacebookUserPost_FacebookProfile] FOREIGN KEY ([FacebookProfileId]) REFERENCES [FacebookProfile]([FacebookProfileId])
)
