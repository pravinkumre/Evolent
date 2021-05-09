	USE [master]
	GO

	IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'demoCRUD')
	BEGIN
	
    CREATE DATABASE [demoCRUD]

    END

	GO
    USE [demoCRUD]
	GO

	SET ANSI_NULLS ON
	GO

	SET QUOTED_IDENTIFIER ON
	GO

	IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name='Contact')
	BEGIN
	
    CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
	 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
	(
		[ContactId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	
	END