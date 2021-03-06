USE [master]
GO
/****** Object:  Database [HQTZ]    Script Date: 2015/10/13 18:14:31 ******/
CREATE DATABASE [HQTZ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HQTZ', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\HQTZ.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HQTZ_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\HQTZ_log.ldf' , SIZE = 2304KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HQTZ] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HQTZ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HQTZ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HQTZ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HQTZ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HQTZ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HQTZ] SET ARITHABORT OFF 
GO
ALTER DATABASE [HQTZ] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HQTZ] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [HQTZ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HQTZ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HQTZ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HQTZ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HQTZ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HQTZ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HQTZ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HQTZ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HQTZ] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HQTZ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HQTZ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HQTZ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HQTZ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HQTZ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HQTZ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HQTZ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HQTZ] SET RECOVERY FULL 
GO
ALTER DATABASE [HQTZ] SET  MULTI_USER 
GO
ALTER DATABASE [HQTZ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HQTZ] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HQTZ] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HQTZ] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HQTZ', N'ON'
GO
USE [HQTZ]
GO
/****** Object:  Table [dbo].[HQ_Admin]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HQ_Admin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserPsw] [varchar](50) NULL,
	[UserType] [int] NULL,
	[UserRole] [int] NULL,
	[TrueName] [nvarchar](50) NULL,
	[PhoneNum] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_Admin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HQ_Agenter]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HQ_Agenter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[AContent] [nvarchar](max) NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_Agenter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HQ_Article]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HQ_Article](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[TitleImg] [nvarchar](256) NULL,
	[AContent] [nvarchar](max) NULL,
	[ACategory] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_Article1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HQ_BookRoom]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HQ_BookRoom](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NULL,
	[RoomID] [int] NULL,
	[BookStartTime] [datetime] NULL,
	[BookEndTime] [datetime] NULL,
	[CreateOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_BookRoom] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HQ_DisplayContent]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HQ_DisplayContent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[DContent] [nvarchar](max) NULL,
	[DPanelID] [int] NOT NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_DisplayContent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HQ_DisplayPanel]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HQ_DisplayPanel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ImgName] [nvarchar](256) NULL,
	[DPanelType] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_DisplayPanel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HQ_Member]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HQ_Member](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserPsw] [varchar](50) NULL,
	[UserType] [int] NULL,
	[TrueName] [nvarchar](50) NULL,
	[PhoneNum] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_Member] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HQ_Room]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HQ_Room](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [decimal](18, 2) NULL,
	[RCount] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[VillageID] [int] NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[EditBy] [nvarchar](50) NULL,
	[EditOn] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_HQ_Room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[uv_DisplayContent]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[uv_DisplayContent]
as
select s.*,p.Name DPanel_G,p.DPanelType from HQ_DisplayContent s left join HQ_DisplayPanel p
on s.DPanelID=p.ID
GO
/****** Object:  View [dbo].[uv_VillageRoom]    Script Date: 2015/10/13 18:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[uv_VillageRoom]
as
select r.*,c.Name Village_G from HQ_Room r 
left join HQ_DisplayContent c on r.VillageID=c.ID;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'展板ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HQ_DisplayContent', @level2type=N'COLUMN',@level2name=N'DPanelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'度假村ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HQ_Room', @level2type=N'COLUMN',@level2name=N'VillageID'
GO
USE [master]
GO
ALTER DATABASE [HQTZ] SET  READ_WRITE 
GO
