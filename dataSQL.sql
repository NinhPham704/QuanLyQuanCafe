USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 1/21/2023 2:49:28 PM ******/
CREATE DATABASE [QuanLyQuanCafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'D:\SQL\QuanLyQuanCafe\QuanLyQuanCafe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'D:\SQL\QuanLyQuanCafe\QuanLyQuanCafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLyQuanCafe] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyQuanCafe', N'ON'
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUERY_STORE = OFF
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[UserDisplayName] [nvarchar](100) NOT NULL,
	[UserPassword] [nvarchar](100) NULL,
	[UserType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[Bill_Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [date] NOT NULL,
	[DateCheckOut] [date] NULL,
	[Table_Id] [int] NOT NULL,
	[Bill_Status] [nvarchar](100) NOT NULL,
	[discount] [int] NULL,
	[TotalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Bill_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[BillInfo_Id] [int] IDENTITY(1,1) NOT NULL,
	[Bill_Id] [int] NOT NULL,
	[Food_Id] [int] NOT NULL,
	[Count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BillInfo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[Food_ID] [int] IDENTITY(1,1) NOT NULL,
	[Food_Name] [nvarchar](100) NOT NULL,
	[FoodCategory_Id] [int] NOT NULL,
	[Price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Food_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[FoodCategory_Id] [int] IDENTITY(1,1) NOT NULL,
	[FoodCategory_Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FoodCategory_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[TableFood_ID] [int] IDENTITY(1,1) NOT NULL,
	[TableFood_Name] [nvarchar](100) NOT NULL,
	[TableFood_Status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TableFood_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'PhamNinh') FOR [UserDisplayName]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [UserPassword]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [UserType]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateCheckIn]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [Bill_Status]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [TotalPrice]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [Count]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT (N'chưa đặt tên') FOR [Food_Name]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[FoodCategory] ADD  DEFAULT (N'chưa đặt tên') FOR [FoodCategory_Name]
GO
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'chưa đặt tên bàn') FOR [TableFood_Name]
GO
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'Bàn trống') FOR [TableFood_Status]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([Table_Id])
REFERENCES [dbo].[TableFood] ([TableFood_ID])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([Bill_Id])
REFERENCES [dbo].[Bill] ([Bill_Id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([Food_Id])
REFERENCES [dbo].[Food] ([Food_ID])
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([FoodCategory_Id])
REFERENCES [dbo].[FoodCategory] ([FoodCategory_Id])
GO
/****** Object:  StoredProcedure [dbo].[UDP_GetListBillByDateAndPage]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC	[dbo].[UDP_GetListBillByDateAndPage]
@checkin date, @checkout date, @page int
as
BEGIN
	DECLARE	@pagerows int = 10
	DECLARE @selectrows int = @pagerows
	DECLARE	@exceptrows int = (@page-1)*@pagerows

	;WITH billshow as(SELECT b.Bill_Id, tf.TableFood_Name, b.DateCheckIn, b.DateCheckOut, b.discount, b.TotalPrice FROM dbo.TableFood tf , dbo.Bill b 
	WHERE B.Table_Id	= TF.TableFood_ID	AND  B.Bill_Status	= N'Đã thanh toán' AND b.DateCheckIn	>= @CHECKIN AND b.DateCheckOut	<= @CHECKOUT)

	SELECT TOP (@selectrows	) * FROM billshow b WHERE b.Bill_Id NOT IN	(SELECT TOP ( @exceptrows) billshow.Bill_Id FROM billshow)	
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUserName]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
as
BEGIN
	SELECT * FROM dbo.Account a	WHERE a.UserName = @userName
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GETLISTBILLBYDATE]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC	[dbo].[USP_GETLISTBILLBYDATE]
@CHECKIN DATE, @CHECKOUT DATE
AS
BEGIN
	SELECT tf.TableFood_Name, b.DateCheckIn, b.DateCheckOut, b.discount, b.TotalPrice FROM dbo.TableFood tf , dbo.Bill b 
	WHERE B.Table_Id	= TF.TableFood_ID	AND B.Bill_Status	= N'Đã thanh toán' AND b.DateCheckIn	>= @CHECKIN AND b.DateCheckOut	<= @CHECKOUT
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GETNUMBERBILLBYDATE]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC	[dbo].[USP_GETNUMBERBILLBYDATE]
@CHECKIN DATE, @CHECKOUT DATE
AS
BEGIN
	SELECT count(*) FROM dbo.TableFood tf , dbo.Bill b 
	WHERE B.Table_Id	= TF.TableFood_ID	AND B.Bill_Status	= N'Đã thanh toán' AND b.DateCheckIn	>= @CHECKIN AND b.DateCheckOut	<= @CHECKOUT
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetTableList]
as SELECT * FROM dbo.TableFood
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERT_BILL]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_INSERT_BILL]
@IDTABLE int
AS
BEGIN
	INSERT dbo.Bill
	(
	    --Bill_Id - column value is auto-generated
	    DateCheckIn,
	    DateCheckOut,
	    Table_Id,
	    Bill_Status,
		discount
	)
	VALUES
	(
	    -- Bill_Id - int
	    GETDATE(), -- DateCheckIn - date
	    NULL, -- DateCheckOut - date
	     @IDTABLE	, -- Table_Id - int
	    N'Chưa thanh toán', -- Bill_Status - nvarchar
		0
	)	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERT_BillInfo]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_INSERT_BillInfo]
@Bill_Id int, @Food_Id int, @Count int
as
BEGIN
	DECLARE @isExitBillInfo int
	DECLARE @foodcount int = 1

	SELECT @isExitBillInfo = (bi.BillInfo_Id) , @foodcount = bi.Count	  FROM dbo.BillInfo bi	
	 
	WHERE bi.Bill_Id = @Bill_Id AND  bi.Food_Id = @Food_Id 

	if(@isExitBillInfo>0)
		BEGIN
			DECLARE @newcount int = @foodcount	+ @Count 
			if( @newcount	> 0)
				UPDATE	dbo.BillInfo
				SET dbo.BillInfo.Count = @newcount WHERE dbo.BillInfo.Food_Id = @Food_Id 
			ELSE
				DELETE	dbo.BillInfo WHERE dbo.BillInfo.Bill_Id = @Bill_Id AND dbo.BillInfo.Food_Id = @Food_Id	
		END
	ELSE
		BEGIN
			INSERT dbo.BillInfo	(Bill_Id,Food_Id,Count	)
			VALUES(	@Bill_Id , 	@Food_Id , @Count)
		END
end
GO
/****** Object:  StoredProcedure [dbo].[USP_LOGIN]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_LOGIN]
@userName nvarchar( 100), @password nvarchar(100)
as
BEGIN
	SELECT	* FROM dbo.Account a WHERE a.UserName= @userName AND a.UserPassword = @password
end
GO
/****** Object:  StoredProcedure [dbo].[USP_SWITCHTABLE]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SWITCHTABLE]
@IDTABLE1 INT, @IDTABLE2 INT
AS
BEGIN
	DECLARE @IDBILL1 INT
	DECLARE @IDBILL2 INT
	
	DECLARE @isTableEmty1 int = 1
	DECLARE @isTableEmty2 int = 1

	SELECT @IDBILL2 = b.Bill_Id FROM dbo.Bill b WHERE b.Table_Id = @IDTABLE2 AND b.Bill_Status = N'Chưa thanh toán'
	SELECT @IDBILL1 = b.Bill_Id FROM dbo.Bill b WHERE b.Table_Id = @IDTABLE1 AND b.Bill_Status = N'Chưa thanh toán'

	IF(@IDBILL1 IS NULL)
		BEGIN 
			INSERT dbo.Bill
			(    DateCheckIn,  DateCheckOut,    Table_Id,    Bill_Status,   discount	)
			VALUES
			(    GETDATE(),	    NULL,    2,   N'Chưa thanh toán',    0 ) 
			SELECT @IDBILL1 = max(b.Bill_Id) FROM dbo.Bill b WHERE b.Table_Id = @IDTABLE1 AND b.Bill_Status = N'Chưa thanh toán'
		END

	SELECT @isTableEmty1 = count(*) FROM dbo.BillInfo bi	WHERE bi.Bill_Id = @IDBILL1 

	IF(@IDBILL2 IS NULL)
		BEGIN 
			INSERT dbo.Bill
			(    DateCheckIn,  DateCheckOut,    Table_Id,    Bill_Status,   discount	)
			VALUES
			(    GETDATE(),	    NULL,    @IDTABLE2,   N'Chưa thanh toán',    0 ) 
			SELECT @IDBILL2 = max(b.Bill_Id) FROM dbo.Bill b WHERE b.Table_Id = @IDTABLE2 AND b.Bill_Status = N'Chưa thanh toán'
		END

	SELECT @isTableEmty2 = count(*) FROM dbo.BillInfo bi	WHERE bi.Bill_Id = @IDBILL2


	SELECT bi.BillInfo_Id INTO IDTEMP FROM dbo.BillInfo bi WHERE bi.Bill_Id = @IDBILL2

	UPDATE dbo.BillInfo	SET  dbo.BillInfo.Bill_Id = @IDBILL2 WHERE dbo.BillInfo.Bill_Id = @IDBILL1 

	UPDATE dbo.BillInfo	SET	dbo.BillInfo.Bill_Id = @IDBILL1 WHERE dbo.BillInfo.BillInfo_Id IN (SELECT * FROM dbo.IDTEMP i )

	DROP TABLE dbo.IDTEMP 
	if(@isTableEmty1 = 0)
		UPDATE dbo.TableFood
		SET   dbo.TableFood.TableFood_Status = N'Bàn trống' WHERE dbo.TableFood.TableFood_ID = @IDTABLE2	
	if(@isTableEmty2 = 0)
		UPDATE dbo.TableFood
		SET   dbo.TableFood.TableFood_Status = N'Bàn trống' WHERE dbo.TableFood.TableFood_ID = @IDTABLE1	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UPDATEACCOUNT]    Script Date: 1/21/2023 2:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UPDATEACCOUNT]
@USERNAME nvarchar(	100), @USERDISPLAYNAME nvarchar(	100), @USERPASSWORD nvarchar(	100), @NEWPASSWORD nvarchar(	100)
AS
BEGIN
	DECLARE	@ISPASSWORD INT = 0
	SELECT @ISPASSWORD =COUNT( *) FROM dbo.Account a	WHERE	A.UserName	= @USERNAME AND a.UserPassword	= @USERPASSWORD

	IF(@ISPASSWORD = 1)
		BEGIN
			IF(@NEWPASSWORD = NULL OR @NEWPASSWORD = '')
				BEGIN
					UPDATE dbo.Account
					SET dbo.Account.UserDisplayName = @USERDISPLAYNAME WHERE dbo.Account.UserName	= @USERNAME
				END
			ELSE
				BEGIN
					UPDATE	dbo.Account
					SET dbo.Account.UserPassword = @NEWPASSWORD WHERE dbo.Account.UserName	= @USERNAME
				END
		END
END
GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE 
GO
