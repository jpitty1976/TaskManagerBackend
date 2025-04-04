USE [TaskManager]
GO
/****** Object:  Table [dbo].[TasksList]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksList](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[TaskTitle] [varchar](100) NOT NULL,
	[TaskDescription] [varchar](250) NULL,
	[TaskStatus] [int] NOT NULL,
 CONSTRAINT [PK_TasksList] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStates]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStates](
	[TaskStateID] [int] NOT NULL,
	[TaskStateDescription] [varchar](150) NOT NULL,
 CONSTRAINT [PK_TaskStates] PRIMARY KEY CLUSTERED 
(
	[TaskStateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TasksList]  WITH CHECK ADD  CONSTRAINT [FK_TasksList_TaskStates] FOREIGN KEY([TaskStatus])
REFERENCES [dbo].[TaskStates] ([TaskStateID])
GO
ALTER TABLE [dbo].[TasksList] CHECK CONSTRAINT [FK_TasksList_TaskStates]
GO
/****** Object:  StoredProcedure [dbo].[ChangeTaskStatus]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangeTaskStatus] (@Id INT, @Status INT)
AS
UPDATE Tasks SET Status=@Status WHERE Id=@Id
GO
/****** Object:  StoredProcedure [dbo].[CreateTask]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateTask] (@Title NVARCHAR(100), @Description NVARCHAR(MAX), @Status INT)
AS
INSERT INTO TasksList (TaskTitle, TaskDescription, TaskStatus) VALUES (@Title, @Description, @Status)
GO
/****** Object:  StoredProcedure [dbo].[DeleteTask]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTask] (@Id INT)
AS
DELETE FROM TasksList WHERE TaskID=@Id
GO
/****** Object:  StoredProcedure [dbo].[ShowAllTasks]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowAllTasks]
AS
SELECT [TaskID],[TaskTitle],[TaskDescription],[TaskStatus]
	FROM TasksList 

SELECT DB_NAME();
GO
/****** Object:  StoredProcedure [dbo].[UpdateTask]    Script Date: 3/26/2025 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTask] (@Id INT, @Title NVARCHAR(100), @Description NVARCHAR(MAX), @Status INT)
AS
UPDATE TasksList SET TaskTitle=@Title, TaskDescription=@Description, TaskStatus=@Status WHERE TaskID=@Id
GO
