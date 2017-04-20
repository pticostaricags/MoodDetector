CREATE TABLE [dbo].[FacebookUserPostSentiment]
(
	[FacebookUserPostSentimentId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [FacebookUserPostId] BIGINT NOT NULL, 
    [Score] FLOAT NOT NULL, 
    CONSTRAINT [FK_FacebookUserPostSentiment_FacebookUserPost] FOREIGN KEY ([FacebookUserPostId]) REFERENCES [FacebookUserPost]([FacebookUserPostId])
)
