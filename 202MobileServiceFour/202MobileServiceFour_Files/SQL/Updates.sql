ALTER TABLE [dbo].[Business]
 ADD [BusinessPhone] varchar(15) NULL,
	 [BusinessImage] varchar(1000) NULL

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
		  ,[BusinessPhone]
		  ,[BusinessImage]
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