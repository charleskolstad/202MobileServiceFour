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

CREATE PROCEDURE [dbo].[p_Feature_GetByID]
(
	@FeatureID int
)
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

    SELECT [FeatureID]
		  ,[FeatureName]
		  ,[MainFeature]
		  ,[FeatureDescription]
		  ,[Active]
	  FROM [dbo].[Feature]
	 WHERE [FeatureID] = @FeatureID

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

CREATE PROCEDURE [dbo].[p_Business_GetByUser]
(
	@UserName NVARCHAR(256)
)
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
	 WHERE [UserName] = @UserName

END
GO

CREATE PROCEDURE [dbo].[p_Business_Update]
(
	 @BusinessID int
	,@BusinessName varchar(100)
    ,@BusinessEmail varchar(150) = NULL
    ,@BusinessAddress varchar(250) = NULL
    ,@BusinessHoursStart varchar(10) = NULL
    ,@BusinessHoursEnd varchar(10) = NULL
    ,@WebsiteUrl varchar(500) = NULL
    ,@FacebookUrl varchar(500) = NULL
    ,@ImageGalleryUrl varchar(500) = NULL
    ,@Other varchar(5000) = NULL
    ,@TypeOfBusiness varchar(500) = NULL
    ,@AppLink varchar(1500) = NULL
    ,@IsPublic bit
    ,@AppStatus varchar(200) = NULL
    ,@UserName nvarchar(256)
)
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

	UPDATE [dbo].[Business]
	   SET [BusinessName] = COALESCE(@BusinessName, [BusinessName]) --varchar(100)
		  ,[BusinessEmail] = COALESCE(@BusinessEmail, [BusinessEmail]) --varchar(150)
		  ,[BusinessAddress] = COALESCE(@BusinessAddress, [BusinessAddress]) --varchar(250)
		  ,[BusinessHoursStart] = COALESCE(@BusinessHoursStart, [BusinessHoursStart]) --varchar(10)
		  ,[BusinessHoursEnd] = COALESCE(@BusinessHoursEnd, [BusinessHoursEnd]) --varchar(10)
		  ,[WebsiteUrl] = COALESCE(@WebsiteUrl,[WebsiteUrl]) --varchar(500)
		  ,[FacebookUrl] = COALESCE(@FacebookUrl, [FacebookUrl]) --varchar(500)
		  ,[ImageGalleryUrl] = COALESCE(@ImageGalleryUrl, [ImageGalleryUrl]) --varchar(500)
		  ,[Other] = COALESCE(@Other, [Other]) --varchar(5000)
		  ,[TypeOfBusiness] = COALESCE(@TypeOfBusiness, [TypeOfBusiness]) --varchar(500)
		  ,[AppLink] = COALESCE(@AppLink, [AppLink]) --varchar(1500)
		  ,[IsPublic] = @IsPublic --bit
		  ,[AppStatus] = COALESCE(@AppStatus, [AppStatus]) --varchar(200)
		  ,[UserName] = @UserName --nvarchar(256)
	 WHERE [BusinessID] = @BusinessID
    
END
GO

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
    
	INSERT INTO [dbo].[FeatureRequested]
           ([AppRequestID]
           ,[FeatureID]
           ,[DateRequested]
           ,[DevStatus]
           ,[AssignedTo]
           ,[Active])
	SELECT @@IDENTITY
		  ,[FeatureID]
		  ,NULL
		  ,NULL
		  ,NULL
		  ,0
	  FROM [dbo].[Feature]
    
END
GO

--business type
CREATE TABLE BusinessType
(
	BusinessType INT IDENTITY(1,1) PRIMARY KEY,
	TypeName VARCHAR(200) NOT NULL,
	Active BIT
)
GO

CREATE PROCEDURE [dbo].[p_BusinessType_GetActive]
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

    SELECT [BusinessType]
		  ,[TypeName]
		  ,[Active]
	  FROM [dbo].[BusinessType]
	 WHERE [Active] = 1

END
GO

--feature requested
CREATE TABLE FeatureRequested
(
	FeatureRequestedID INT IDENTITY(1,1) PRIMARY KEY,
	AppRequestID INT NOT NULL,
	FeatureID INT NOT NULL,
	DateRequested DATE NULL,
	DevStatus VARCHAR(250) NULL,
	AssignedTo NVARCHAR(256) NULL,
	Active BIT NOT NULL
)
GO

CREATE PROCEDURE [dbo].[p_FeatureRequested_GetByBusinessID]
(
	@BusinessID int
)
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

	SELECT [FeatureRequested].[FeatureRequestedID]
		  ,[FeatureRequested].[AppRequestID]
		  ,[FeatureRequested].[FeatureID]
		  ,[FeatureRequested].[DateRequested]
		  ,[FeatureRequested].[DevStatus]
		  ,[FeatureRequested].[AssignedTo]
		  ,[FeatureRequested].[Active]
	  FROM [dbo].[FeatureRequested]
	INNER JOIN [dbo].[AppRequest] ON
	[FeatureRequested].[AppRequestID] = [AppRequest].[AppRequestID]
	WHERE [AppRequest].[BusinessID] = @BusinessID
    
END
GO

CREATE PROCEDURE [dbo].[p_FeatureRequested_Update]
(
	 @FeatureRequestedID int
	,@AppRequestID int
	,@FeatureID int
	,@DateRequested date = NULL
	,@DevStatus varchar(250) = NULL
	,@AssignedTo nvarchar(256) = NULL
	,@Active bit = NULL
)
AS
BEGIN
	-- cpk:<date>
	-- 
	SET NOCOUNT ON;

	UPDATE [dbo].[FeatureRequested]
	   SET [AppRequestID] = @AppRequestID --int
		  ,[FeatureID] = @FeatureID --int
		  ,[DateRequested] = COALESCE(@DateRequested, [DateRequested]) --date
		  ,[DevStatus] = COALESCE(@DevStatus, [DevStatus]) --varchar(250)
		  ,[AssignedTo] = COALESCE(@AssignedTo, [AssignedTo]) --nvarchar(256)
		  ,[Active] = COALESCE(@Active, [Active]) --bit
	 WHERE [FeatureRequestedID] = @FeatureRequestedID
    
END
GO

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