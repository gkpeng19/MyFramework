USE [master]
GO
/****** Object:  Database [BaseWeb]    Script Date: 2015/10/13 18:13:52 ******/
CREATE DATABASE [BaseWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BaseWeb', FILENAME = N'D:\Gkpeng\MyFramework\G.BaseWeb\DB\BaseWeb.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BaseWeb_log', FILENAME = N'D:\Gkpeng\MyFramework\G.BaseWeb\DB\BaseWeb_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BaseWeb] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BaseWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BaseWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BaseWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BaseWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BaseWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BaseWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BaseWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BaseWeb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BaseWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BaseWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BaseWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BaseWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BaseWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BaseWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BaseWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BaseWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BaseWeb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BaseWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BaseWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BaseWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BaseWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BaseWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BaseWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BaseWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BaseWeb] SET RECOVERY FULL 
GO
ALTER DATABASE [BaseWeb] SET  MULTI_USER 
GO
ALTER DATABASE [BaseWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BaseWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BaseWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BaseWeb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BaseWeb', N'ON'
GO
USE [BaseWeb]
GO
/****** Object:  Table [dbo].[BW_Menu]    Script Date: 2015/10/13 18:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BW_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](32) NULL,
	[Name] [nvarchar](32) NULL,
	[ParentID] [int] NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [varchar](32) NULL,
	[EditOn] [datetime] NULL,
	[EditBy] [varchar](32) NULL,
	[Remark] [nvarchar](256) NULL,
	[IsDelete] [int] NULL,
	[MenuType] [int] NULL,
 CONSTRAINT [PK_BW_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BW_Role]    Script Date: 2015/10/13 18:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BW_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleCode] [varchar](32) NULL,
	[RoleName] [nvarchar](32) NULL,
	[ParentID] [int] NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [varchar](32) NULL,
	[EditOn] [datetime] NULL,
	[EditBy] [varchar](32) NULL,
	[Remark] [nvarchar](256) NULL,
	[IsDelete] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BW_RoleMenu]    Script Date: 2015/10/13 18:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BW_RoleMenu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[MenuID] [int] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_BW_RoleMenu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BW_User]    Script Date: 2015/10/13 18:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BW_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](32) NULL,
	[UserPsw] [varchar](256) NULL,
	[UserType] [int] NULL,
	[UserRole] [int] NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [varchar](32) NULL,
	[EditOn] [datetime] NULL,
	[EditBy] [varchar](32) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_BW_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[uv_bw_user]    Script Date: 2015/10/13 18:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[uv_bw_user]
as
select u.*,r.RoleName UserRole_G from bw_user u
left join BW_Role r on u.UserRole=r.ID
GO
/****** Object:  View [dbo].[uv_rolemenu]    Script Date: 2015/10/13 18:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[uv_rolemenu]
as
select m.*,rm.RoleID,rm.MenuID from BW_RoleMenu rm 
left join BW_Menu m on rm.MenuID=m.ID
GO
USE [master]
GO
ALTER DATABASE [BaseWeb] SET  READ_WRITE 
GO
