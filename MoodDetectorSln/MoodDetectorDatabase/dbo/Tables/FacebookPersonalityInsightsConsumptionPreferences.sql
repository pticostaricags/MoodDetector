CREATE TABLE [dbo].[FacebookPersonalityInsightsConsumptionPreferences]
(
	[FacebookPersonalityInsightsConsumptionPreferencesId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
	[FacebookPersonalityInsightsId] BIGINT NOT NULL,
    [PreferenceCategoryId] NVARCHAR(MAX) NOT NULL, 
    [PreferenceCategoryName] NVARCHAR(MAX) NULL, 
    [ParentFacebookPersonalityInsightsConsumptionPreferencesId] BIGINT NULL, 
	[PreferenceId] NVARCHAR(MAX) NULL,
    [PreferenceName] NVARCHAR(MAX) NULL, 
    [PreferenceScore] FLOAT NULL, 
    CONSTRAINT [FK_FacebookPersonalityInsightsConsumptionPreferences_FacebookPersonalityInsightsConsumptionPreferences] FOREIGN KEY ([ParentFacebookPersonalityInsightsConsumptionPreferencesId]) REFERENCES [FacebookPersonalityInsightsConsumptionPreferences]([FacebookPersonalityInsightsConsumptionPreferencesId]), 
    CONSTRAINT [FK_FacebookPersonalityInsightsConsumptionPreferences_FacebookPersonalityInsights] FOREIGN KEY ([FacebookPersonalityInsightsId]) REFERENCES [FacebookPersonalityInsights]([FacebookPersonalityInsightsId])
)
