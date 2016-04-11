ALTER TABLE [dbo].[Student_Course_Map] DROP CONSTRAINT [FK_Student_Course_Map_Student]
GO
ALTER TABLE [dbo].[Student_Course_Map] DROP CONSTRAINT [FK_Student_Course_Map_Course]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[QuizDetail] DROP CONSTRAINT [FK_QuizDetail_QuizDetail]
GO
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_Student]
GO
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_QuizDetail]
GO
ALTER TABLE [dbo].[Quiz_Course_Map] DROP CONSTRAINT [FK_Quiz_Course_Map_Quiz]
GO
ALTER TABLE [dbo].[Quiz_Course_Map] DROP CONSTRAINT [FK_Quiz_Course_Map_Course]
GO
ALTER TABLE [dbo].[Instructor_Course_Map] DROP CONSTRAINT [FK_Instructor_Course_Map_Instructor]
GO
ALTER TABLE [dbo].[Instructor_Course_Map] DROP CONSTRAINT [FK_Instructor_Course_Map_Course]
GO
ALTER TABLE [dbo].[Instructor] DROP CONSTRAINT [FK_Instructor_User]
GO
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Content_Course]
GO
ALTER TABLE [dbo].[ChatRecord_Course_Map] DROP CONSTRAINT [FK_ChatRecord_Course_Map_Course]
GO
ALTER TABLE [dbo].[ChatRecord_Course_Map] DROP CONSTRAINT [FK_ChatRecord_Course_Map_ChatRecord]
GO
ALTER TABLE [dbo].[ChatDetail] DROP CONSTRAINT [FK_ChatDetail_Instructor]
GO
ALTER TABLE [dbo].[ChatDetail] DROP CONSTRAINT [FK_ChatDetail_ChatRecord1]
GO
ALTER TABLE [dbo].[ChatDetail] DROP CONSTRAINT [FK_ChatDetail_ChatRecord]
GO
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [FK_Admin_User]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Student_Course_Map]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Student]
GO
/****** Object:  Table [dbo].[QuizRecord]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[QuizRecord]
GO
/****** Object:  Table [dbo].[QuizDetail]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[QuizDetail]
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[QuizAnswer]
GO
/****** Object:  Table [dbo].[Quiz_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Quiz_Course_Map]
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Instructor_Course_Map]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Instructor]
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[DBVersion]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Course]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Content]
GO
/****** Object:  Table [dbo].[ChatRecord_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[ChatRecord_Course_Map]
GO
/****** Object:  Table [dbo].[ChatRecord]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[ChatRecord]
GO
/****** Object:  Table [dbo].[ChatDetail]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[ChatDetail]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 12/04/2016 00:19:30 ******/
DROP TABLE [dbo].[Admin]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Sid] [int] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatDetail]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatDetail](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[ChatRecordSid] [int] NULL,
	[StuentSid] [int] NULL,
	[InstructorSid] [int] NULL,
	[Message] [nvarchar](1024) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_ChatDetail] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatRecord]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatRecord](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](255) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_ChatRecord] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatRecord_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatRecord_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[ChatRecordSid] [int] NULL,
	[CourseSid] [int] NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_ChatRecord_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Content]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseSid] [int] NULL,
	[Type] [char](1) NULL,
	[Path] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[OriginalFileName] [nvarchar](max) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](max) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBVersion](
	[DBVersion] [nvarchar](255) NULL,
	[CreateDT] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[Sid] [int] NOT NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[InstructorSid] [int] NULL,
	[CourseSid] [int] NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_Instructor_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Quiz_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizSid] [int] NULL,
	[CourseSid] [int] NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_Quiz_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizAnswer](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizDetailSid] [int] NULL,
	[StudentSid] [int] NULL,
	[Answer] [nvarchar](max) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizAnswer] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuizDetail]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizDetail](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizRecordSid] [int] NULL,
	[Question] [nvarchar](max) NULL,
	[Option1] [nvarchar](max) NULL,
	[Option2] [nvarchar](max) NULL,
	[Option3] [nvarchar](max) NULL,
	[Option4] [nvarchar](max) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizDetail] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuizRecord]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizRecord](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizName] nvarchar(max) NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Quiz] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Sid] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[StudentSid] [int] NULL,
	[CourseSid] [int] NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
 CONSTRAINT [PK_Student_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 12/04/2016 00:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NULL,
	[CreateDT] [datetime] NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_User] FOREIGN KEY([Sid])
REFERENCES [dbo].[User] ([Sid])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_User]
GO
ALTER TABLE [dbo].[ChatDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChatDetail_ChatRecord] FOREIGN KEY([StuentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
ALTER TABLE [dbo].[ChatDetail] CHECK CONSTRAINT [FK_ChatDetail_ChatRecord]
GO
ALTER TABLE [dbo].[ChatDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChatDetail_ChatRecord1] FOREIGN KEY([ChatRecordSid])
REFERENCES [dbo].[ChatRecord] ([Sid])
GO
ALTER TABLE [dbo].[ChatDetail] CHECK CONSTRAINT [FK_ChatDetail_ChatRecord1]
GO
ALTER TABLE [dbo].[ChatDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChatDetail_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
ALTER TABLE [dbo].[ChatDetail] CHECK CONSTRAINT [FK_ChatDetail_Instructor]
GO
ALTER TABLE [dbo].[ChatRecord_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_ChatRecord_Course_Map_ChatRecord] FOREIGN KEY([ChatRecordSid])
REFERENCES [dbo].[ChatRecord] ([Sid])
GO
ALTER TABLE [dbo].[ChatRecord_Course_Map] CHECK CONSTRAINT [FK_ChatRecord_Course_Map_ChatRecord]
GO
ALTER TABLE [dbo].[ChatRecord_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_ChatRecord_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
ALTER TABLE [dbo].[ChatRecord_Course_Map] CHECK CONSTRAINT [FK_ChatRecord_Course_Map_Course]
GO
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Course]
GO
ALTER TABLE [dbo].[Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_User] FOREIGN KEY([Sid])
REFERENCES [dbo].[User] ([Sid])
GO
ALTER TABLE [dbo].[Instructor] CHECK CONSTRAINT [FK_Instructor_User]
GO
ALTER TABLE [dbo].[Instructor_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
ALTER TABLE [dbo].[Instructor_Course_Map] CHECK CONSTRAINT [FK_Instructor_Course_Map_Course]
GO
ALTER TABLE [dbo].[Instructor_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Course_Map_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
ALTER TABLE [dbo].[Instructor_Course_Map] CHECK CONSTRAINT [FK_Instructor_Course_Map_Instructor]
GO
ALTER TABLE [dbo].[Quiz_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
ALTER TABLE [dbo].[Quiz_Course_Map] CHECK CONSTRAINT [FK_Quiz_Course_Map_Course]
GO
ALTER TABLE [dbo].[Quiz_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Course_Map_Quiz] FOREIGN KEY([QuizSid])
REFERENCES [dbo].[QuizRecord] ([Sid])
GO
ALTER TABLE [dbo].[Quiz_Course_Map] CHECK CONSTRAINT [FK_Quiz_Course_Map_Quiz]
GO
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_QuizDetail] FOREIGN KEY([QuizDetailSid])
REFERENCES [dbo].[QuizDetail] ([Sid])
GO
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_QuizDetail]
GO
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_Student]
GO
ALTER TABLE [dbo].[QuizDetail]  WITH CHECK ADD  CONSTRAINT [FK_QuizDetail_QuizDetail] FOREIGN KEY([QuizRecordSid])
REFERENCES [dbo].[QuizRecord] ([Sid])
GO
ALTER TABLE [dbo].[QuizDetail] CHECK CONSTRAINT [FK_QuizDetail_QuizDetail]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([Sid])
REFERENCES [dbo].[User] ([Sid])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[Student_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
ALTER TABLE [dbo].[Student_Course_Map] CHECK CONSTRAINT [FK_Student_Course_Map_Course]
GO
ALTER TABLE [dbo].[Student_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course_Map_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
ALTER TABLE [dbo].[Student_Course_Map] CHECK CONSTRAINT [FK_Student_Course_Map_Student]
GO
