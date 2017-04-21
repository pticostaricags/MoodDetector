CREATE TABLE [dbo].[FacebookPersonalityInsights]
(
	[FacebookPersonalityInsightsId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [WordCount] INT NOT NULL, 
    [ProcessedLanguage] NCHAR(10) NULL, 
    [FacebookProfileId] BIGINT NOT NULL, 
    CONSTRAINT [FK_FacebookPersonalityInsights_FacebookProfile] FOREIGN KEY ([FacebookProfileId]) REFERENCES [FacebookProfile]([FacebookProfileId])
)
