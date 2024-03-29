USE [master]
GO
/****** Object:  Database [RamandTech]    Script Date: 10/4/2023 3:12:02 PM ******/
CREATE DATABASE [RamandTech]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RamandTech', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\RamandTech.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RamandTech_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\RamandTech_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RamandTech] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RamandTech].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RamandTech] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RamandTech] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RamandTech] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RamandTech] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RamandTech] SET ARITHABORT OFF 
GO
ALTER DATABASE [RamandTech] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RamandTech] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RamandTech] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RamandTech] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RamandTech] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RamandTech] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RamandTech] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RamandTech] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RamandTech] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RamandTech] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RamandTech] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RamandTech] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RamandTech] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RamandTech] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RamandTech] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RamandTech] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RamandTech] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RamandTech] SET RECOVERY FULL 
GO
ALTER DATABASE [RamandTech] SET  MULTI_USER 
GO
ALTER DATABASE [RamandTech] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RamandTech] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RamandTech] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RamandTech] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RamandTech] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RamandTech', N'ON'
GO
ALTER DATABASE [RamandTech] SET QUERY_STORE = OFF
GO
USE [RamandTech]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/4/2023 3:12:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (1, N'Mahsa', N'Mousavi', N'user1', N'3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D')
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (2, N'Ehsan', N'Mousavi', N'user2', N'3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D')
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (3, N'Mehrdad', N'Tat', N'user3', N'3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D')
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (4, N'Farzad', N'Tat', N'user4', N'3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D')
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (5, N'Mina', N'Bahari', N'user5', N'3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D')
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (6, N'Bahar', N'Naderi', N'user6', N'3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserName]    Script Date: 10/4/2023 3:12:03 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserName] ON [dbo].[User]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserById]    Script Date: 10/4/2023 3:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_GetUserById] 
@Id int
	
AS
BEGIN
	
	SET NOCOUNT ON;

 select * from [dbo].[User]  where Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserByUserNameAndPassword]    Script Date: 10/4/2023 3:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Description:	Get User by userName and password
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUserByUserNameAndPassword] 
	@UserName nvarchar(50),
	@Password nvarchar(50)
	
AS
BEGIN
	
	SET NOCOUNT ON;

 select * from [dbo].[User] where Username=@UserName and Password=@Password
END


GO
/****** Object:  StoredProcedure [dbo].[SP_GetUsers]    Script Date: 10/4/2023 3:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_GetUsers] 

	
AS
BEGIN
	
	SET NOCOUNT ON;

 select * from [dbo].[User] 
END
GO
USE [master]
GO
ALTER DATABASE [RamandTech] SET  READ_WRITE 
GO
