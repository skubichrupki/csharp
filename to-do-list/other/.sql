-- task table
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [todo].[task](
	[task_id] [int] IDENTITY(1,1) NOT NULL,
	[task_desc] [varchar](255) NOT NULL,
	[status_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[task_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [todo].[task]  WITH CHECK ADD  CONSTRAINT [status_id] FOREIGN KEY([status_id])
REFERENCES [todo].[status] ([status_id])
GO
ALTER TABLE [todo].[task] CHECK CONSTRAINT [status_id]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fk to status table' , @level0type=N'SCHEMA',@level0name=N'todo', @level1type=N'TABLE',@level1name=N'task', @level2type=N'CONSTRAINT',@level2name=N'status_id'
GO

-- status table
CREATE TABLE [todo].[status](
	[status_id] [int] IDENTITY(1,1) NOT NULL,
	[status_desc] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- task view
CREATE VIEW [todo].[v_task]
AS
    SELECT task_id, task_desc, status_desc 
    FROM [todo].[task] as task 
    INNER JOIN [todo].[status] AS stat ON stat.status_id = task.status_id
GO
