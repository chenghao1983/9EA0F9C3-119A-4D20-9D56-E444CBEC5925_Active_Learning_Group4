--------------------------Structure-----------------  
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] DROP CONSTRAINT [FK_Student_Course_Map_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] DROP CONSTRAINT [FK_Student_Course_Map_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizDetail_QuizDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizDetail]'))
ALTER TABLE [dbo].[QuizDetail] DROP CONSTRAINT [FK_QuizDetail_QuizDetail]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_QuizDetail]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Quiz_Course_Map_Quiz]') AND parent_object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]'))
ALTER TABLE [dbo].[Quiz_Course_Map] DROP CONSTRAINT [FK_Quiz_Course_Map_Quiz]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Quiz_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]'))
ALTER TABLE [dbo].[Quiz_Course_Map] DROP CONSTRAINT [FK_Quiz_Course_Map_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] DROP CONSTRAINT [FK_Instructor_Course_Map_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] DROP CONSTRAINT [FK_Instructor_Course_Map_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor]'))
ALTER TABLE [dbo].[Instructor] DROP CONSTRAINT [FK_Instructor_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Content_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatRecord_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]'))
ALTER TABLE [dbo].[ChatRecord_Course_Map] DROP CONSTRAINT [FK_ChatRecord_Course_Map_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatRecord_Course_Map_ChatRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]'))
ALTER TABLE [dbo].[ChatRecord_Course_Map] DROP CONSTRAINT [FK_ChatRecord_Course_Map_ChatRecord]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail] DROP CONSTRAINT [FK_ChatDetail_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_ChatRecord1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail] DROP CONSTRAINT [FK_ChatDetail_ChatRecord1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_ChatRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail] DROP CONSTRAINT [FK_ChatDetail_ChatRecord]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [FK_Admin_User]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Student_Course_Map]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
DROP TABLE [dbo].[Student]
GO
/****** Object:  Table [dbo].[QuizRecord]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizRecord]') AND type in (N'U'))
DROP TABLE [dbo].[QuizRecord]
GO
/****** Object:  Table [dbo].[QuizDetail]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizDetail]') AND type in (N'U'))
DROP TABLE [dbo].[QuizDetail]
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizAnswer]') AND type in (N'U'))
DROP TABLE [dbo].[QuizAnswer]
GO
/****** Object:  Table [dbo].[Quiz_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Quiz_Course_Map]
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor_Course_Map]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor]
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBVersion]') AND type in (N'U'))
DROP TABLE [dbo].[DBVersion]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
DROP TABLE [dbo].[Course]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
DROP TABLE [dbo].[Content]
GO
/****** Object:  Table [dbo].[ChatRecord_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[ChatRecord_Course_Map]
GO
/****** Object:  Table [dbo].[ChatRecord]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatRecord]') AND type in (N'U'))
DROP TABLE [dbo].[ChatRecord]
GO
/****** Object:  Table [dbo].[ChatDetail]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatDetail]') AND type in (N'U'))
DROP TABLE [dbo].[ChatDetail]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 13/04/2016 21:38:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
DROP TABLE [dbo].[Admin]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Admin](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[UserSid] [int] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ChatDetail]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChatDetail](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[ChatRecordSid] [int] NOT NULL,
	[StuentSid] [int] NULL,
	[InstructorSid] [int] NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_ChatDetail] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ChatRecord]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChatRecord](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](1024) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_ChatRecord] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ChatRecord_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChatRecord_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[ChatRecordSid] [int] NOT NULL,
	[CourseSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_ChatRecord_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Content]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Content](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseSid] [int] NOT NULL,
	[Type] [char](1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[OriginalFileName] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Course](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DBVersion](
	[DBVersion] [nvarchar](255) NULL,
	[CreateDT] [datetime] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[UserSid] [int] NOT NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[InstructorSid] [int] NOT NULL,
	[CourseSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_Instructor_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Quiz_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Quiz_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizSid] [int] NOT NULL,
	[CourseSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_Quiz_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizAnswer](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizDetailSid] [int] NOT NULL,
	[StudentSid] [int] NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizAnswer] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizDetail]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizDetail](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizRecordSid] [int] NOT NULL,
	[Question] [nvarchar](max) NOT NULL,
	[Option1] [nvarchar](max) NULL,
	[Option2] [nvarchar](max) NULL,
	[Option3] [nvarchar](max) NULL,
	[Option4] [nvarchar](max) NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizDetail] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizRecord]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizRecord](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizName] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Quiz] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Student]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [nvarchar](50) NOT NULL,
	[UserSid] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[StudentSid] [int] NOT NULL,
	[CourseSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_Student_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 13/04/2016 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
	[Role] [char](1) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_User] FOREIGN KEY([UserSid])
REFERENCES [dbo].[User] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_ChatRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChatDetail_ChatRecord] FOREIGN KEY([StuentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_ChatRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail] CHECK CONSTRAINT [FK_ChatDetail_ChatRecord]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_ChatRecord1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChatDetail_ChatRecord1] FOREIGN KEY([ChatRecordSid])
REFERENCES [dbo].[ChatRecord] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_ChatRecord1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail] CHECK CONSTRAINT [FK_ChatDetail_ChatRecord1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChatDetail_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatDetail_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatDetail]'))
ALTER TABLE [dbo].[ChatDetail] CHECK CONSTRAINT [FK_ChatDetail_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatRecord_Course_Map_ChatRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]'))
ALTER TABLE [dbo].[ChatRecord_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_ChatRecord_Course_Map_ChatRecord] FOREIGN KEY([ChatRecordSid])
REFERENCES [dbo].[ChatRecord] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatRecord_Course_Map_ChatRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]'))
ALTER TABLE [dbo].[ChatRecord_Course_Map] CHECK CONSTRAINT [FK_ChatRecord_Course_Map_ChatRecord]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatRecord_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]'))
ALTER TABLE [dbo].[ChatRecord_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_ChatRecord_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChatRecord_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChatRecord_Course_Map]'))
ALTER TABLE [dbo].[ChatRecord_Course_Map] CHECK CONSTRAINT [FK_ChatRecord_Course_Map_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor]'))
ALTER TABLE [dbo].[Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_User] FOREIGN KEY([UserSid])
REFERENCES [dbo].[User] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor]'))
ALTER TABLE [dbo].[Instructor] CHECK CONSTRAINT [FK_Instructor_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] CHECK CONSTRAINT [FK_Instructor_Course_Map_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Course_Map_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] CHECK CONSTRAINT [FK_Instructor_Course_Map_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Quiz_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]'))
ALTER TABLE [dbo].[Quiz_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Quiz_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]'))
ALTER TABLE [dbo].[Quiz_Course_Map] CHECK CONSTRAINT [FK_Quiz_Course_Map_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Quiz_Course_Map_Quiz]') AND parent_object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]'))
ALTER TABLE [dbo].[Quiz_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Course_Map_Quiz] FOREIGN KEY([QuizSid])
REFERENCES [dbo].[QuizRecord] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Quiz_Course_Map_Quiz]') AND parent_object_id = OBJECT_ID(N'[dbo].[Quiz_Course_Map]'))
ALTER TABLE [dbo].[Quiz_Course_Map] CHECK CONSTRAINT [FK_Quiz_Course_Map_Quiz]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_QuizDetail] FOREIGN KEY([QuizDetailSid])
REFERENCES [dbo].[QuizDetail] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_QuizDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizDetail_QuizDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizDetail]'))
ALTER TABLE [dbo].[QuizDetail]  WITH CHECK ADD  CONSTRAINT [FK_QuizDetail_QuizDetail] FOREIGN KEY([QuizRecordSid])
REFERENCES [dbo].[QuizRecord] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizDetail_QuizDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizDetail]'))
ALTER TABLE [dbo].[QuizDetail] CHECK CONSTRAINT [FK_QuizDetail_QuizDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([UserSid])
REFERENCES [dbo].[User] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] CHECK CONSTRAINT [FK_Student_Course_Map_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course_Map_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] CHECK CONSTRAINT [FK_Student_Course_Map_Student]
GO
--------------------------Index--------------------- 
--------------------------Function------------------ 
--------------------------View---------------------- 
--------------------------SP------------------------ 
--------------------------Data---------------------- 
--------------------------Others---------------------- 

DECLARE @DBVersion varchar(255)
SET @DBVersion='Schema revision 1.2'
update DBVersion set DBVersion = @DBVersion, CreateDT = '2016-04-13';