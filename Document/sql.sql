USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_Admin]    Script Date: 08/16/2015 18:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HZ_Admin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](32) NULL,
	[UPassword] [varchar](64) NULL,
	[UserRole] [int] NULL,
	[Email] [nvarchar](64) NULL,
	[Phone] [varchar](32) NULL,
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_HZ_ADMIN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


------------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_Catagory]    Script Date: 08/16/2015 18:19:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HZ_Catagory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](32) NULL,
	[Name] [nvarchar](64) NULL,
	[CType] [int] NULL,
	[ParentID] [int] NULL,
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [nvarchar](32) NULL,
	[EditOn] [datetime] NULL,
	[EditBy] [nvarchar](32) NULL,
 CONSTRAINT [PK_HZ_CATAGORY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


-------------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_Context]    Script Date: 08/16/2015 18:19:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HZ_Context](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Context] [nvarchar](max) NOT NULL,
	[CType] [int] NOT NULL,
	[IsTop] [int] NULL,
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [nvarchar](32) NULL,
	[EditOn] [datetime] NULL,
	[EditBy] [nvarchar](32) NULL,
	[CategoryID] [int] NULL,
 CONSTRAINT [PK_HZ_CONTEXT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


---------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_ImageBook]    Script Date: 08/16/2015 18:19:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HZ_ImageBook](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](32) NULL,
	[Name] [nvarchar](32) NULL,
	[Type] [int] NULL,
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [nvarchar](32) NULL,
 CONSTRAINT [PK_HZ_IMAGEBOOK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


----------------------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_Images]    Script Date: 08/16/2015 18:19:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HZ_Images](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](32) NULL,
	[Name] [nvarchar](64) NULL,
	[FileName] [varchar](64) NULL,
	[BookID] [int] NULL,
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [nvarchar](32) NULL,
 CONSTRAINT [PK_HZ_IMAGES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


--------------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_MsgRes]    Script Date: 08/16/2015 18:19:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HZ_MsgRes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Context] [nvarchar](256) NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](64) NULL,
	[Type] [int] NULL,
	[IsDelete] [int] NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_HZ_MSGRES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'询价单与询价单回复、访客留言与回复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HZ_MsgRes'
GO


------------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_Product]    Script Date: 08/16/2015 18:19:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HZ_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](32) NULL,
	[Name] [nvarchar](32) NULL,
	[ProductCategory] [int] NULL,
	[Sort]  AS ([ID]),
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
	[CreateBy] [nvarchar](32) NULL,
	[EditOn] [datetime] NULL,
	[EditBy] [nvarchar](32) NULL,
 CONSTRAINT [PK_HZ_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_ProductComment]    Script Date: 08/16/2015 18:20:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HZ_ProductComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Context] [nvarchar](max) NULL,
	[IsDelete] [int] NULL,
	[CommentUID] [int] NULL,
	[CommentUName] [nvarchar](32) NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_HZ_PRODUCTCOMMENT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--------------------------------------
USE [qds167337431_db]
GO

/****** Object:  Table [dbo].[HZ_User]    Script Date: 08/16/2015 18:20:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HZ_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](32) NULL,
	[UPassword] [varchar](64) NULL,
	[UserLevel] [int] NULL,
	[UserType] [int] NULL,
	[Email] [nvarchar](64) NULL,
	[Phone] [varchar](32) NULL,
	[IsDelete] [int] NULL,
	[Remark] [nvarchar](256) NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_HZ_USER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


