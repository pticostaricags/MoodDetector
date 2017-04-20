CREATE TABLE [dbo].[FacebookUserPostKeyPhrases]
(
	[FacebookUserPostKeyPhrasesId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [FacebookUserPostId] BIGINT NOT NULL, 
    [KeyPhrases] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_FacebookUserPostKeyPhrases_FacebookUserPost] FOREIGN KEY ([FacebookUserPostId]) REFERENCES [FacebookUserPost]([FacebookUserPostId])
)
