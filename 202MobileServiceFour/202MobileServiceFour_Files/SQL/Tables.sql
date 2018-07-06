--features
CREATE TABLE Features
(
	FeaturesID INT IDENTITY(1,1) PRIMARY KEY,
	FeatureName VARCHAR(50) NOT NULL,
	MainFeature BIT NOT NULL,
	FeatureDescription VARCHAR(5000) NULL,
	Ative BIT
)

--business
CREATE TABLE Business
(
	BusinessID INT IDENTITY(1,1) PRIMARY KEY,
	BusinessName VARCHAR(100) NOT NULL,
	BusinessEmail VARCHAR(150) NOT NULL,
	BusinessAddress VARCHAR(250) NULL,
	BusinessHoursStart VARCHAR(10) NULL,
	BusinessHoursEnd VARCHAR(10) NULL,
	WebsiteUrl VARCHAR(500) NULL,
	FacebookUrl VARCHAR(500) NULL,
	ImageGalleryUrl VARCHAR(500) NULL,
	Other VARCHAR(5000) NULL,
	TypeOfBusiness VARCHAR(500) NOT NULL,
	AppLink VARCHAR(1500) NULL,
	IsPublic BIT NULL,
	AppStatus VARCHAR(200) NULL,
	Active BIT
)

--business users
CREATE TABLE BusinessUsers
(
	BusinessUsersID INT IDENTITY(1,1) PRIMARY KEY,
	ContactName VARCHAR(100) NOT NULL,
	UserName NVARCHAR(256) NOT NULL,
	ContactPhone VARCHAR(20) NULL,
	Active BIT NOT NULL
)

--app requests
CREATE TABLE AppRequest
(
	AppRequestID INT IDENTITY(1,1) PRIMARY KEY,
	DateRequested DATE NOT NULL,
	BusinessID INT NOT NULL,
	DevStatus VARCHAR(250) NULL,
	Active BIT NOT NULL
)

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