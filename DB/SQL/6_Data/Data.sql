---  [dbo].[User]  --
DELETE  from [dbo].[Admin]
DELETE from [dbo].[Instructor]
DELETE from [dbo].[Student]
DELETE FROM  [dbo].[User] 
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role]) VALUES (1, N'Admin1', N'1234', 'Super Admin', 1, CAST(N'2016-04-13 23:15:17.527' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'A')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role]) VALUES (2, N'Instructor1', N'1234', 'Kent White', 1, CAST(N'2016-04-13 23:17:34.010' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'I')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role]) VALUES (3, N'Instructor2', N'1234', 'Eric Johnson', 0, CAST(N'2016-04-13 23:17:41.910' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'I')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role]) VALUES (4, N'Student1', N'1234', 'Tom Cruise', 1, CAST(N'2016-04-13 23:18:59.883' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'S')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role]) VALUES (5, N'Student2', N'1234', 'Walker Smith', 0, CAST(N'2016-04-13 23:21:00.933' AS DateTime), CAST(N'2015-01-01 00:00:00.000' AS DateTime), NULL, N'S')
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

INSERT [dbo].[Instructor]([Sid],[UserSid]) VALUES (1,2)
GO
INSERT [dbo].[Instructor]([Sid],[UserSid]) VALUES (2,3)
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


