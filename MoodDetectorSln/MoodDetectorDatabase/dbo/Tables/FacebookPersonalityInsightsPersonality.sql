CREATE TABLE [dbo].[FacebookPersonalityInsightsPersonality]
(
	[FacebookPersonalityInsightsPersonalityId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [FacebookPersonalityInsightsId] BIGINT NOT NULL, 
    [TraitId] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Category] NVARCHAR(50) NULL, 
    [Percentile] FLOAT NULL, 
    [ParentFacebookPersonalityInsightsPersonalityId] BIGINT NULL, 
    CONSTRAINT [FK_FacebookPersonalityInsightsPersonality_FacebookPersonalityInsights] FOREIGN KEY ([FacebookPersonalityInsightsId]) REFERENCES [FacebookPersonalityInsights]([FacebookPersonalityInsightsId]), 
    CONSTRAINT [FK_FacebookPersonalityInsightsPersonality_FacebookPersonalityInsightsPersonality] FOREIGN KEY ([ParentFacebookPersonalityInsightsPersonalityId]) REFERENCES [FacebookPersonalityInsightsPersonality]([FacebookPersonalityInsightsPersonalityId])
)
