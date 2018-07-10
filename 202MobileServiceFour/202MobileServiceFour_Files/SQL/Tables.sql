--features
CREATE TABLE Feature
(
	FeatureID INT IDENTITY(1,1) PRIMARY KEY,
	FeatureName VARCHAR(50) NOT NULL,
	MainFeature BIT NOT NULL,
	FeatureDescription VARCHAR(5000) NULL,
	Active BIT
)
GO

CREATE PROCEDURE [dbo].[p_Feature_Insert]
(
	 @FeatureName varchar(50)
	,@MainFeature bit
	,@FeatureDescription varchar(5000) = NULL
)
AS
BEGIN
--creted <date>:cpk
--<Notes>
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Feature]
			   ([FeatureName]
			   ,[MainFeature]
			   ,[FeatureDescription]
			   ,[Active])
		 VALUES
			   (@FeatureName --varchar(50)
			   ,@MainFeature --bit
			   ,@FeatureDescription --varchar(5000)
			   ,1)

	SELECT @@IDENTITY

	--GRANT EXECUTE ON [dbo].[proc name] TO [user brackets required.]
END
GO

CREATE PROCEDURE [dbo].[p_Feature_Update]
(
	 @FeatureID int
	,@FeatureName varchar(50)
	,@MainFeature bit
	,@FeatureDescription varchar(5000)
)
AS
BEGIN
--creted <date>:cpk
--<Notes>
	SET NOCOUNT ON;

	UPDATE [dbo].[Feature]
	   SET [FeatureName] = @FeatureName --varchar(50)
		  ,[MainFeature] = @MainFeature --bit
		  ,[FeatureDescription] = @FeatureDescription --varchar(5000)
	 WHERE [FeatureID] = @FeatureID

	--GRANT EXECUTE ON [dbo].[proc name] TO [user brackets required.]
END
GO

CREATE PROCEDURE [dbo].[p_Feature_Delete]
(
	 @FeatureID int
)
AS
BEGIN
--creted <date>:cpk
--<Notes>
	SET NOCOUNT ON;

	UPDATE [dbo].[Feature]
	   SET [Active] = 0
	 WHERE [FeatureID] = @FeatureID

	--GRANT EXECUTE ON [dbo].[proc name] TO [user brackets required.]
END
GO

CREATE PROCEDURE [dbo].[p_Feature_GetActive]
AS
BEGIN
--creted <date>:cpk
--<Notes>
	SET NOCOUNT ON;

	SELECT [FeatureID]
		  ,[FeatureName]
		  ,[MainFeature]
		  ,[FeatureDescription]
		  ,[Active]
	  FROM [dbo].[Feature]
	 WHERE [Active] = 1

	--GRANT EXECUTE ON [dbo].[proc name] TO [user brackets required.]
END
GO

--business
CREATE TABLE Business
(
	BusinessID INT IDENTITY(1,1) PRIMARY KEY,
	BusinessName VARCHAR(100) NULL,
	BusinessEmail VARCHAR(150) NULL,
	BusinessAddress VARCHAR(250) NULL,
	BusinessHoursStart VARCHAR(10) NULL,
	BusinessHoursEnd VARCHAR(10) NULL,
	WebsiteUrl VARCHAR(500) NULL,
	FacebookUrl VARCHAR(500) NULL,
	ImageGalleryUrl VARCHAR(500) NULL,
	Other VARCHAR(5000) NULL,
	TypeOfBusiness VARCHAR(500) NULL,
	AppLink VARCHAR(1500) NULL,
	IsPublic BIT NULL,
	AppStatus VARCHAR(200) NULL,
	UserName NVARCHAR(256) NOT NULL,
	Active BIT
)
GO

CREATE PROCEDURE [dbo].[p_Business_Insert]
(
	 @BusinessName varchar(100)
    ,@BusinessEmail varchar(150)
    ,@BusinessAddress varchar(250)
    ,@BusinessHoursStart varchar(10)
    ,@BusinessHoursEnd varchar(10)
    ,@WebsiteUrl varchar(500)
    ,@FacebookUrl varchar(500)
    ,@ImageGalleryUrl varchar(500)
    ,@Other varchar(5000)
    ,@TypeOfBusiness varchar(500)
    ,@AppLink varchar(1500)
    ,@IsPublic bit
	,@UserName NVARCHAR(256)
    ,@AppStatus varchar(200)
)
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

    INSERT INTO [dbo].[Business]
           ([BusinessName]
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
           ,[Active])
     VALUES
           (@BusinessName --varchar(100)
           ,@BusinessEmail --varchar(150)
           ,@BusinessAddress --varchar(250)
           ,@BusinessHoursStart --varchar(10)
           ,@BusinessHoursEnd --varchar(10)
           ,@WebsiteUrl --varchar(500)
           ,@FacebookUrl --varchar(500)
           ,@ImageGalleryUrl --varchar(500)
           ,@Other --varchar(5000)
           ,@TypeOfBusiness --varchar(500)
           ,@AppLink --varchar(1500)
           ,@IsPublic --bit
           ,@AppStatus --varchar(200)
		   ,@UserName --nvarchar(256)
           ,1)

	EXECUTE [dbo].[p_AppRequest_Insert] @@IDENTITY,NULL

END
GO

--business users
--CREATE TABLE BusinessUsers
--(
--	BusinessUsersID INT IDENTITY(1,1) PRIMARY KEY,
--	ContactName VARCHAR(100) NOT NULL,
--	UserName NVARCHAR(256) NOT NULL,
--	ContactPhone VARCHAR(20) NULL,
--	Active BIT NOT NULL
--)

--app requests
CREATE TABLE AppRequest
(
	AppRequestID INT IDENTITY(1,1) PRIMARY KEY,
	DateRequested DATE NOT NULL,
	BusinessID INT NOT NULL,
	DevStatus VARCHAR(250) NULL,
	Active BIT NOT NULL
)
GO

CREATE PROCEDURE [dbo].[p_AppRequest_Insert]
(
	 @BusinessID int
    ,@DevStatus varchar(250) = NULL
)
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

	INSERT INTO [dbo].[AppRequest]
           ([DateRequested]
           ,[BusinessID]
           ,[DevStatus]
           ,[Active])
     VALUES
           (GETDATE()
           ,@BusinessID --int
           ,@DevStatus --varchar(250)
           ,1)
    
END
GO

--feature requested
CREATE TABLE FeatureRequested
(
	FeatureRequestedID INT IDENTITY(1,1) PRIMARY KEY,
	AppRequestID INT,
	DateRequested DATE NOT NULL,
	DevStatus VARCHAR(250) NULL,
	AssignedTo NVARCHAR(256) NULL,
	Active BIT NOT NULL
)

--error log
CREATE TABLE [dbo].[ErrorLog](
	[ErrorLogID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[StackTrace] [nvarchar](max) NOT NULL,
	[Location] [nvarchar](150) NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[ErrorLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[p_ErrorLog_Insert]
(
	@message NVARCHAR(500),
	@stackTrace NVARCHAR(MAX),
	@location VARCHAR(150)
)
AS
BEGIN
--creted <date>:cpk
--insert app error data into database.

	SET NOCOUNT ON;

	INSERT INTO [dbo].[ErrorLog]
           (
				[Message]
			   ,[StackTrace]
			   ,[Location]
			   ,[Time]
		   )
     VALUES
           (
			    @message
			   ,@stackTrace
			   ,@location
			   ,GETDATE()
		   )

	--GRANT EXECUTE ON [dbo].[p_ErrorLog_Insert] TO 
END




GO