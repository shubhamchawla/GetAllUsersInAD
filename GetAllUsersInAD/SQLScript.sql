USE [master]
GO
/****** Object:  Database [activeDirectory]    Script Date: 18-04-2017 16:14:04 ******/
CREATE DATABASE [activeDirectory]
GO
ALTER DATABASE [activeDirectory] SET COMPATIBILITY_LEVEL = 100
GO
ALTER DATABASE [activeDirectory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [activeDirectory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [activeDirectory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [activeDirectory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [activeDirectory] SET ARITHABORT OFF 
GO
ALTER DATABASE [activeDirectory] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [activeDirectory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [activeDirectory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [activeDirectory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [activeDirectory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [activeDirectory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [activeDirectory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [activeDirectory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [activeDirectory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [activeDirectory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [activeDirectory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [activeDirectory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [activeDirectory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [activeDirectory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [activeDirectory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [activeDirectory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [activeDirectory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [activeDirectory] SET RECOVERY FULL 
GO
ALTER DATABASE [activeDirectory] SET  MULTI_USER 
GO
ALTER DATABASE [activeDirectory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [activeDirectory] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'activeDirectory', N'ON'
GO
USE [activeDirectory]
GO
/****** Object:  Table [dbo].[ActiveUser]    Script Date: 18-04-2017 16:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActiveUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](max) NULL,
	[LastName] [varchar](max) NULL,
	[Location] [varchar](max) NULL,
	[Title] [varchar](max) NULL,
	[postalCode] [varchar](max) NULL,
	[telephoneNumber] [varchar](max) NULL,
	[whenChanged] [varchar](max) NULL,
	[streetAddress] [varchar](max) NULL,
	[mobile] [varchar](max) NULL,
	[msExchWhenMailboxCreated] [varchar](max) NULL,
	[msExchExtensionAttribute19] [varchar](max) NULL,
	[SAPID] [varchar](max) NULL,
	[Gender] [varchar](max) NULL,
	[SeatCode] [varchar](max) NULL,
	[SAMAccountName] [varchar](max) NULL,
	[UserPrincipalName] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ActiveUser_old]    Script Date: 18-04-2017 16:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActiveUser_old](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](max) NULL,
	[LastName] [varchar](max) NULL,
	[Location] [varchar](max) NULL,
	[Title] [varchar](max) NULL,
	[postalCode] [varchar](max) NULL,
	[telephoneNumber] [varchar](max) NULL,
	[whenChanged] [varchar](max) NULL,
	[streetAddress] [varchar](max) NULL,
	[mobile] [varchar](max) NULL,
	[msExchWhenMailboxCreated] [varchar](max) NULL,
	[msExchExtensionAttribute19] [varchar](max) NULL,
	[SAPID] [varchar](max) NULL,
	[Gender] [varchar](max) NULL,
	[SeatCode] [varchar](max) NULL,
	[SAMAccountName] [varchar](max) NULL,
	[UserPrincipalName] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserPhotoURL]    Script Date: 18-04-2017 16:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPhotoURL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[URL] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[InsertActiveUser]    Script Date: 18-04-2017 16:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertActiveUser](
@FirstName varchar(max) = '', 
@LastName varchar(max) = '', 
@Location varchar(max) = '',
@Title varchar(max) = '',
@postalCode varchar(max) = '',
@telephoneNumber varchar(max) = '',
@whenChanged varchar(max) = '',
@streetAddress varchar(max) = '',
@mobile varchar(max) = '',
@msExchWhenMailboxCreated varchar(max) = '',
@msExchExtensionAttribute19 varchar(max) = '',
@SAPID varchar(max) = '',
@Gender varchar(max) = '',
@SeatCode varchar(max) = '',
@SAMAccountName varchar(max) = '',
@UserPrincipalName varchar(max) = ''
)
as

if exists (select 1 from activeuser where sapid = @SAPID)
return

insert into ActiveUser values (
@FirstName , 
@LastName , 
@Location ,
@Title ,
@postalCode ,
@telephoneNumber ,
@whenChanged ,
@streetAddress ,
@mobile ,
@msExchWhenMailboxCreated ,
@msExchExtensionAttribute19 ,
@SAPID ,
@Gender ,
@SeatCode ,
@SAMAccountName ,
@UserPrincipalName 
)
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUserPhotoURL]    Script Date: 18-04-2017 16:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_InsertUserPhotoURL] @UserID int, @URL varchar(max)
as
insert into UserPhotoURL values (@UserID, @URL)

GO
USE [master]
GO
ALTER DATABASE [activeDirectory] SET  READ_WRITE 
GO
