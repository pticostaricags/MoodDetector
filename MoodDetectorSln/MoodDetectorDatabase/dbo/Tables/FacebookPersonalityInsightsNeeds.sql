CREATE TABLE [dbo].[FacebookPersonalityInsightsNeeds]
(
	[FacebookPersonalityInsightsNeedsId] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[FacebookPersonalityInsightsId] BIGINT NOT NULL, 
    [TraitId] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Category] NVARCHAR(50) NULL, 
    [Percentile] FLOAT NULL, 
    CONSTRAINT [FK_FacebookPersonalityInsightsNeeds_FacebookPersonalityInsight] FOREIGN KEY ([FacebookPersonalityInsightsId]) REFERENCES [FacebookPersonalityInsights]([FacebookPersonalityInsightsId])
)
