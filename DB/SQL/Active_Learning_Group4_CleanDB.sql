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
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizQuestion_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizQuestion]'))
ALTER TABLE [dbo].[QuizQuestion] DROP CONSTRAINT [FK_QuizQuestion_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizOption_QuizQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizOption]'))
ALTER TABLE [dbo].[QuizOption] DROP CONSTRAINT [FK_QuizOption_QuizQuestion]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_QuizOption]
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
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [FK_Admin_User]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Student_Course_Map]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
DROP TABLE [dbo].[Student]
GO
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizQuestion]') AND type in (N'U'))
DROP TABLE [dbo].[QuizQuestion]
GO
/****** Object:  Table [dbo].[QuizOption]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizOption]') AND type in (N'U'))
DROP TABLE [dbo].[QuizOption]
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizAnswer]') AND type in (N'U'))
DROP TABLE [dbo].[QuizAnswer]
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor_Course_Map]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor]
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBVersion]') AND type in (N'U'))
DROP TABLE [dbo].[DBVersion]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
DROP TABLE [dbo].[Course]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
DROP TABLE [dbo].[Content]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Chat]') AND type in (N'U'))
DROP TABLE [dbo].[Chat]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 19/04/2016 21:09:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
DROP TABLE [dbo].[Admin]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 19/04/2016 21:09:16 ******/
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
/****** Object:  Table [dbo].[Chat]    Script Date: 19/04/2016 21:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Chat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Chat](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseSid] [int] NOT NULL,
	[StudentSid] [int] NULL,
	[InstructorSid] [int] NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Content]    Script Date: 19/04/2016 21:09:16 ******/
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
/****** Object:  Table [dbo].[Course]    Script Date: 19/04/2016 21:09:16 ******/
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
/****** Object:  Table [dbo].[DBVersion]    Script Date: 19/04/2016 21:09:16 ******/
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
/****** Object:  Table [dbo].[Instructor]    Script Date: 19/04/2016 21:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[UserSid] [int] NOT NULL,
	[Qualification] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 19/04/2016 21:09:16 ******/
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
 CONSTRAINT [PK_Instructor_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 19/04/2016 21:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizAnswer](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizQuestionSid] [int] NOT NULL,
	[QuizOptionSid] [int] NOT NULL,
	[StudentSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizAnswer] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizOption]    Script Date: 19/04/2016 21:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizOption]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizOption](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizQuestionSid] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizOption] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC,
	[QuizQuestionSid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 19/04/2016 21:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizQuestion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizQuestion](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
	[CourseSid] [int] NOT NULL,
 CONSTRAINT [PK_QuizQuestion] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Student]    Script Date: 19/04/2016 21:09:16 ******/
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
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 19/04/2016 21:09:16 ******/
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
 CONSTRAINT [PK_Student_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 19/04/2016 21:09:16 ******/
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
	[FullName] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Role] [char](1) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Student]
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_QuizOption] FOREIGN KEY([QuizOptionSid], [QuizQuestionSid])
REFERENCES [dbo].[QuizOption] ([Sid], [QuizQuestionSid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_QuizOption]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizOption_QuizQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizOption]'))
ALTER TABLE [dbo].[QuizOption]  WITH CHECK ADD  CONSTRAINT [FK_QuizOption_QuizQuestion] FOREIGN KEY([QuizQuestionSid])
REFERENCES [dbo].[QuizQuestion] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizOption_QuizQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizOption]'))
ALTER TABLE [dbo].[QuizOption] CHECK CONSTRAINT [FK_QuizOption_QuizQuestion]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizQuestion_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizQuestion]'))
ALTER TABLE [dbo].[QuizQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuizQuestion_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizQuestion_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizQuestion]'))
ALTER TABLE [dbo].[QuizQuestion] CHECK CONSTRAINT [FK_QuizQuestion_Course]
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
---  [dbo].[User]  --

DELETE from [dbo].[Student]
GO
DELETE from [dbo].[Instructor]
Go
DELETE from [dbo].[Admin]
GO
DELETE FROM  [dbo].[User] 
GO

SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (1, N'Admin1', N'/pgdZNxfSu2XUHjBNgaB4PlwinBAUmTH', 'Super Admin', 1, CAST(N'2016-04-13 23:15:17.527' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'A',N'ZB/sYHfiuxnSFd/ZoMZMfNqAW+MpQu5x ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (2, N'Instructor1', N'Knn5u702hV4P2EqaUDzadGiPZ4zuT79n', 'Kent White', 1, CAST(N'2016-04-13 23:17:34.010' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'I',N'C0xyFiz5b3ZacMTRPwKleI0eFaSy0x8C ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (3, N'Instructor2', N'XofVKJP8RDq8YzvKGD/iAsLJeXfZ5Rjq', 'Eric Johnson', 0, CAST(N'2016-04-13 23:17:41.910' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'I',N'0OTW6ch2NjxC1q7sIYl586hQMzftY+Wd ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (4, N'Student1', N'AoXSVf0r1gWxZ/CDMetgnEkUHwtcYNnK', 'Tom Cruise', 1, CAST(N'2016-04-13 23:18:59.883' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'S',N'XKNeMNMgPoVjx6oZcIQb50fIfYL3UCdC ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (5, N'Student2', N'xdGMVptwdgRfXywbtvE0NiOd1J7cja78', 'Walker Smith', 0, CAST(N'2016-04-13 23:21:00.933' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'S',N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO

---  [dbo].[Admin]  -- 

SET IDENTITY_INSERT [dbo].[Admin] ON 
GO

INSERT [dbo].[Admin]([Sid],[UserSid]) VALUES (1,1)
GO

SET IDENTITY_INSERT [dbo].[Admin] OFF
GO

---  [dbo].[Instructor]  -- 

SET IDENTITY_INSERT [dbo].[Instructor] ON 
GO

INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (1,2,N'Doctorate')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (2,3,N'Mater degree')
GO

SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO

---  [dbo].[Student]  -- 

SET IDENTITY_INSERT [dbo].[Student] ON 
GO

INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (1,4,'SE24')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (2,5,'SE24')
GO

SET IDENTITY_INSERT [dbo].[Student] OFF
GO

-- [dbo].[Course] --

DELETE FROM [dbo].[Course]
SET IDENTITY_INSERT [dbo].[Course] ON
GO

INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (1, 'Enterprise .NET - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (2, 'Object Oriented Software Development- SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (3, 'Project Initiation and Scope Management - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (4, 'Scope Management and Risk Management - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (5, 'Risk Management and Work Breakdown Structure - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (6, 'Object Oriented Software Development - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (7, 'Scheduling and Producing Project Plans - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (8, 'Advanced Project Estimation - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (9, 'Project Tracking and Control - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (10, 'People Management - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (11, 'Computational Intelligence - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (12, 'Agile Software Project Management - SE24', GETDATE())
GO

SET IDENTITY_INSERT [dbo].[Course] OFF
GO

--------------------------Others---------------------- 
-- DB version --

delete from DBVersion
go

insert into DBVersion ( DBVersion, CreateDT) values ('Schema revision 1.4.1', '2016-04-22')
go