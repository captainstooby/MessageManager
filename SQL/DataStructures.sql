USE [master]
GO

/****** Object:  Database [MediaManagement]    Script Date: 3/27/2018 12:20:15 PM ******/
DROP DATABASE [MediaManagement]
GO

/****** Object:  Database [MediaManagement]    Script Date: 3/27/2018 12:20:15 PM ******/
CREATE DATABASE [MediaManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MediaManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MediaManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MediaManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MediaManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [MediaManagement] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MediaManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MediaManagement] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MediaManagement] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MediaManagement] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MediaManagement] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MediaManagement] SET ARITHABORT OFF 
GO

ALTER DATABASE [MediaManagement] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [MediaManagement] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MediaManagement] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MediaManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MediaManagement] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MediaManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MediaManagement] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MediaManagement] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MediaManagement] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MediaManagement] SET  DISABLE_BROKER 
GO

ALTER DATABASE [MediaManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MediaManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MediaManagement] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MediaManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MediaManagement] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MediaManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [MediaManagement] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MediaManagement] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [MediaManagement] SET  MULTI_USER 
GO

ALTER DATABASE [MediaManagement] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MediaManagement] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MediaManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MediaManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [MediaManagement] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [MediaManagement] SET QUERY_STORE = OFF
GO

USE [MediaManagement]
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [MediaManagement] SET  READ_WRITE 
GO


USE [MediaManagement]
GO

ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [DF_Messages_InsertDate]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 3/27/2018 6:27:54 PM ******/
DROP TABLE [dbo].[Messages]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 3/27/2018 6:27:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessagePath] [varchar](200) NOT NULL,
	[MessageRecordingDate] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_InsertDate]  DEFAULT (getdate()) FOR [InsertDate]
GO


USE [MediaManagement]
GO

/****** Object:  StoredProcedure [dbo].[InsertMessage]    Script Date: 3/27/2018 6:28:13 PM ******/
DROP PROCEDURE [dbo].[InsertMessage]
GO

/****** Object:  StoredProcedure [dbo].[InsertMessage]    Script Date: 3/27/2018 6:28:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[InsertMessage]
	-- Add the parameters for the stored procedure here
	@MessagePath varchar(200), 
	@MessageRecordingDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1
		FROM dbo.Messages 
		WHERE MessagePath = @MessagePath
		AND MessageRecordingDate = @MessageRecordingDate)
	BEGIN
		UPDATE dbo.Messages
		SET ModifyDate = GETDATE()
		WHERE MessagePath = @MessagePath
		AND MessageRecordingDate = @MessageRecordingDate
	END
	
	ELSE
	BEGIN
		-- Insert statements for procedure here
		INSERT INTO dbo.Messages
		(MessagePath, MessageRecordingDate)
		values
		(@MessagePath, @MessageRecordingDate)
	END
    
END
GO

