CREATE TABLE [dbo].[FacebookProfile]
(
	[FacebookProfileId] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[ProfileId] NVARCHAR(50) NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [DateRecordCreated] DATETIMEOFFSET NOT NULL
)

GO

CREATE INDEX [UQ_FacebookProfile_FacebookUsername] ON [dbo].[FacebookProfile] ([Username])
