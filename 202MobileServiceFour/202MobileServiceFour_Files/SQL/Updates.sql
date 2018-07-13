CREATE PROCEDURE [dbo].[p_Business_GetActive]
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

	SELECT [BusinessID]
		  ,[BusinessName]
		  ,[BusinessEmail]
		  ,[BusinessAddress]
		  ,[BusinessHoursStart]
		  ,[BusinessHoursEnd]
		  ,[WebsiteUrl]
		  ,[FacebookUrl]
		  ,[ImageGalleryUrl]
		  ,[Other]
		  ,[TypeOfBusiness]
		  ,[AppLink]
		  ,[IsPublic]
		  ,[AppStatus]
		  ,[UserName]
		  ,[Active]
	  FROM [dbo].[Business]
	 WHERE [Active] = 1
    
END
GO