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

