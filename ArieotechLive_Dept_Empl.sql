USE [ArieotechLive]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/18/2021 11:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[DepartmentDescription] [nvarchar](100) NOT NULL,
	[DepartmentHead] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/18/2021 11:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[EmployeeCode] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[OfficeEmail] [nvarchar](500) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[JoiningDate] [datetime] NOT NULL,
	[JoiningLocation] [nvarchar](500) NOT NULL,
	[PersonalEmail] [nvarchar](500) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[EmergencyContactPerson] [nvarchar](500) NOT NULL,
	[EmergencyPhoneNumber] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[CurrentAddress] [nvarchar](500) NOT NULL,
	[PermanentAddress] [nvarchar](500) NOT NULL,
	[Designation] [nvarchar](100) NOT NULL,
	[Passport] [bit] NOT NULL,
	[PassportNumber] [nvarchar](50) NOT NULL,
	[PANNumber] [nvarchar](50) NOT NULL,
	[AdharNumber] [nvarchar](50) NOT NULL,
	[UANNumber] [nvarchar](50) NOT NULL,
	[BloodGroup] [nvarchar](10) NOT NULL,
	[TShirtSize] [nvarchar](10) NOT NULL,
	[MaritalStatus] [nvarchar](50) NOT NULL,
	[MotherName] [nvarchar](500) NOT NULL,
	[MotherDOB] [datetime] NOT NULL,
	[FatherName] [nvarchar](500) NOT NULL,
	[FatherDOB] [datetime] NOT NULL,
	[SpouseName] [nvarchar](500) NOT NULL,
	[SpouseDOB] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](500) NOT NULL,
	[DepartmentID] [nvarchar](500) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [DepartmentDescription], [DepartmentHead]) VALUES (1, N'HUMAN RESOURCE', N'HUMAN RESOURCE', N'MONALI ')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [DepartmentDescription], [DepartmentHead]) VALUES (2, N'ADMIN', N'ADMIN', N'XYZ')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [DepartmentDescription], [DepartmentHead]) VALUES (3, N'3EXC', N'FDD', N'TRERE')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [DepartmentDescription], [DepartmentHead]) VALUES (4, N'45645', N'5454511', N'4545')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [DepartmentDescription], [DepartmentHead]) VALUES (5, N'string', N'string', N'string')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [UserId], [EmployeeCode], [FirstName], [MiddleName], [LastName], [OfficeEmail], [Gender], [JoiningDate], [JoiningLocation], [PersonalEmail], [PhoneNumber], [EmergencyContactPerson], [EmergencyPhoneNumber], [BirthDate], [CurrentAddress], [PermanentAddress], [Designation], [Passport], [PassportNumber], [PANNumber], [AdharNumber], [UANNumber], [BloodGroup], [TShirtSize], [MaritalStatus], [MotherName], [MotherDOB], [FatherName], [FatherDOB], [SpouseName], [SpouseDOB], [Active], [CreateOn], [CreatedBy], [DepartmentID]) VALUES (1, 1, N'1', N'AKSHAY', N'RAMESH', N'PATIL', N'AKSHAY.PATIAL@ARIEOTECH.COM', N'MALE', CAST(N'2021-12-10T00:00:00.000' AS DateTime), N'PUNE', N'XYZ', N'123456789', N'987654321', N'123456789', CAST(N'1997-12-10T00:00:00.000' AS DateTime), N'ZXCVB', N'PUNE', N'PUNE', 1, N'123456789', N'111111', N'12345678', N'123456', N'B+', N'M', N'MARRIED', N'NITA', CAST(N'1970-12-20T00:00:00.000' AS DateTime), N'RAM', CAST(N'1970-12-20T00:00:00.000' AS DateTime), N'ANITA', CAST(N'1998-12-21T00:00:00.000' AS DateTime), 1, CAST(N'2021-11-20T00:00:00.000' AS DateTime), N'ABCD', N'2')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [EmployeeCode], [FirstName], [MiddleName], [LastName], [OfficeEmail], [Gender], [JoiningDate], [JoiningLocation], [PersonalEmail], [PhoneNumber], [EmergencyContactPerson], [EmergencyPhoneNumber], [BirthDate], [CurrentAddress], [PermanentAddress], [Designation], [Passport], [PassportNumber], [PANNumber], [AdharNumber], [UANNumber], [BloodGroup], [TShirtSize], [MaritalStatus], [MotherName], [MotherDOB], [FatherName], [FatherDOB], [SpouseName], [SpouseDOB], [Active], [CreateOn], [CreatedBy], [DepartmentID]) VALUES (27, 2, N'2', N'AJAY', N'RAMESH', N'PATIL', N'AJAY.PATIL@ARIEOTECH.COM', N'MALE', CAST(N'2021-11-10T00:00:00.000' AS DateTime), N'MUMBAI', N'PQR', N'987654321', N'987654321', N'123456789', CAST(N'1996-09-30T00:00:00.000' AS DateTime), N'IJFKFM', N'PUNE', N'QAENGINEER', 1, N'123569874', N'555555', N'852741963', N'56125644', N'A+', N'L', N'MARRIED', N'ANITA', CAST(N'1975-11-21T00:00:00.000' AS DateTime), N'SHAM', CAST(N'1970-08-08T00:00:00.000' AS DateTime), N'ERTY', CAST(N'1998-12-12T00:00:00.000' AS DateTime), 1, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'PQRS', N'1')
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
