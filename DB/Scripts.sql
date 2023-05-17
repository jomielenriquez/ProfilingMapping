USE [PROFILINGMAPPINGDB]
GO
/****** Object:  Table [dbo].[TBL_ADMIN]    Script Date: 17/05/2023 8:56:05 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_ADMIN](
	[ADMINID] [uniqueidentifier] NOT NULL,
	[FIRSTNAME] [nvarchar](50) NOT NULL,
	[MIDDLENAME] [nvarchar](50) NULL,
	[LASTNAME] [nvarchar](50) NOT NULL,
	[USERNAME] [nvarchar](50) NOT NULL,
	[PASSWORD] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[CREATEDBY] [nvarchar](50) NULL,
	[UPDATEDDATE] [datetime] NULL,
	[UPDATEDBY] [nvarchar](50) NULL,
 CONSTRAINT [UQ_TBL_ADMIN_USERNAME_PASSWORD] UNIQUE CLUSTERED 
(
	[USERNAME] ASC,
	[PASSWORD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_NAMES]    Script Date: 17/05/2023 8:56:05 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_NAMES](
	[NAMEID] [uniqueidentifier] NOT NULL,
	[FIRSTNAME] [nvarchar](50) NOT NULL,
	[MIDDLENAME] [nvarchar](50) NULL,
	[LASTNAME] [nvarchar](50) NOT NULL,
	[STREET] [nvarchar](150) NULL,
	[SUBDIVISION] [nvarchar](150) NULL,
	[BARANGAY] [nvarchar](150) NULL,
	[CITY] [nvarchar](150) NULL,
	[PROVINCE] [nvarchar](150) NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[CREATEDBY] [nvarchar](50) NULL,
	[UPDATEDDATE] [datetime] NULL,
	[UPDATEDBY] [nvarchar](50) NULL,
	[BIRTHDATE] [datetime] NULL,
	[AGE] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_REQUEST]    Script Date: 17/05/2023 8:56:05 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_REQUEST](
	[REQUESTID] [uniqueidentifier] NOT NULL,
	[NAMEID] [uniqueidentifier] NOT NULL,
	[COMMENT] [nvarchar](500) NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[CREATEDBY] [nvarchar](50) NULL,
	[UPDATEDDATE] [datetime] NULL,
	[UPDATEDBY] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[TBL_ADMIN] ([ADMINID], [FIRSTNAME], [MIDDLENAME], [LASTNAME], [USERNAME], [PASSWORD], [CREATEDDATE], [CREATEDBY], [UPDATEDDATE], [UPDATEDBY]) VALUES (N'017248e0-55e1-4b0c-9253-5784bcc46392', N'Jomiel', N'admin', N'Enriquez', N'admin', N'admin', CAST(N'2023-05-16T09:47:57.300' AS DateTime), NULL, CAST(N'2023-05-16T12:20:36.723' AS DateTime), N'017248e0-55e1-4b0c-9253-5784bcc46392')
INSERT [dbo].[TBL_NAMES] ([NAMEID], [FIRSTNAME], [MIDDLENAME], [LASTNAME], [STREET], [SUBDIVISION], [BARANGAY], [CITY], [PROVINCE], [CREATEDDATE], [CREATEDBY], [UPDATEDDATE], [UPDATEDBY], [BIRTHDATE], [AGE]) VALUES (N'be2d6d9a-729c-44f8-ae8b-2d0a219378a7', N'Jomiel', N'Liquigan', N'Enriquez', N'213 Phase 3', N'Reaville', N'Barangay 7', N'Tanauan City', N'Batangas', CAST(N'2023-05-16T21:54:30.803' AS DateTime), N'017248e0-55e1-4b0c-9253-5784bcc46392', CAST(N'2023-05-16T21:54:55.260' AS DateTime), N'017248e0-55e1-4b0c-9253-5784bcc46392', CAST(N'1997-02-28T00:00:00.000' AS DateTime), 25)
INSERT [dbo].[TBL_REQUEST] ([REQUESTID], [NAMEID], [COMMENT], [CREATEDDATE], [CREATEDBY], [UPDATEDDATE], [UPDATEDBY]) VALUES (N'36d4ad42-9eea-448e-88d9-e82688f7e53f', N'be2d6d9a-729c-44f8-ae8b-2d0a219378a7', N'Testing', CAST(N'2023-05-16T23:23:09.943' AS DateTime), N'017248e0-55e1-4b0c-9253-5784bcc46392', NULL, NULL)
/****** Object:  Index [PK_TBL_ADMIN_ADMINID]    Script Date: 17/05/2023 8:56:06 am ******/
ALTER TABLE [dbo].[TBL_ADMIN] ADD  CONSTRAINT [PK_TBL_ADMIN_ADMINID] PRIMARY KEY NONCLUSTERED 
(
	[ADMINID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_TBL_NAMES_NAMEID]    Script Date: 17/05/2023 8:56:06 am ******/
ALTER TABLE [dbo].[TBL_NAMES] ADD  CONSTRAINT [PK_TBL_NAMES_NAMEID] PRIMARY KEY NONCLUSTERED 
(
	[NAMEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_TBL_REQUEST_REQUESTID]    Script Date: 17/05/2023 8:56:06 am ******/
ALTER TABLE [dbo].[TBL_REQUEST] ADD  CONSTRAINT [PK_TBL_REQUEST_REQUESTID] PRIMARY KEY NONCLUSTERED 
(
	[REQUESTID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TBL_ADMIN] ADD  CONSTRAINT [DF_TBL_ADMIN_ADMINID]  DEFAULT (newid()) FOR [ADMINID]
GO
ALTER TABLE [dbo].[TBL_ADMIN] ADD  CONSTRAINT [DF_TBL_ADMIN_CREATEDDATE]  DEFAULT (getdate()) FOR [CREATEDDATE]
GO
ALTER TABLE [dbo].[TBL_NAMES] ADD  CONSTRAINT [DF_TBL_NAMES_NAMEID]  DEFAULT (newid()) FOR [NAMEID]
GO
ALTER TABLE [dbo].[TBL_NAMES] ADD  CONSTRAINT [DF_TBL_NAMES_CREATEDDATE]  DEFAULT (getdate()) FOR [CREATEDDATE]
GO
ALTER TABLE [dbo].[TBL_REQUEST] ADD  CONSTRAINT [DF_TBL_REQUEST_REQUESTID]  DEFAULT (newid()) FOR [REQUESTID]
GO
ALTER TABLE [dbo].[TBL_REQUEST] ADD  CONSTRAINT [DF_TBL_REQUEST_CREATEDDATE]  DEFAULT (getdate()) FOR [CREATEDDATE]
GO
ALTER TABLE [dbo].[TBL_REQUEST]  WITH CHECK ADD  CONSTRAINT [FK_TBL_REQUEST_NAMEID] FOREIGN KEY([NAMEID])
REFERENCES [dbo].[TBL_NAMES] ([NAMEID])
GO
ALTER TABLE [dbo].[TBL_REQUEST] CHECK CONSTRAINT [FK_TBL_REQUEST_NAMEID]
GO
USE [master]
GO
ALTER DATABASE [PROFILINGMAPPINGDB] SET  READ_WRITE 
GO
