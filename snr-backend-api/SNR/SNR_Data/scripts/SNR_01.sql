USE [SNR]
GO
/****** Object:  Table [dbo].[ConsignmentMaster]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsignmentMaster](
	[consignmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[origin] [varchar](50) NULL,
	[destination] [varchar](50) NULL,
	[date] [date] NULL,
	[consignerId] [bigint] NULL,
	[consigneeId] [bigint] NULL,
	[packageType] [varchar](50) NULL,
	[pieces] [bigint] NULL,
	[weight] [varchar](50) NULL,
	[deliveryStatus] [varchar](50) NULL,
	[photo] [varbinary](max) NULL,
	[parcelValue] [bigint] NULL,
	[grossAmount] [bigint] NULL,
	[otherCharges] [bigint] NULL,
	[igst] [varchar](50) NULL,
	[sgst] [varchar](50) NULL,
	[cgst] [varchar](50) NULL,
	[netAmount] [bigint] NULL,
	[idDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ConsignmentMaster] PRIMARY KEY CLUSTERED 
(
	[consignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerMaster]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerMaster](
	[customerId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](50) NULL,
	[mobile] [varchar](50) NULL,
	[gstNo] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[isDeleted] [bit] NOT NULL,
	[createdDate] [date] NULL,
 CONSTRAINT [PK_CustomerMaster] PRIMARY KEY CLUSTERED 
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RateMaster]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateMaster](
	[rateId] [int] IDENTITY(1,1) NOT NULL,
	[city] [varchar](50) NULL,
	[ratePerKG] [varchar](50) NULL,
 CONSTRAINT [PK_RateMaster] PRIMARY KEY CLUSTERED 
(
	[rateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[mobile] [varchar](50) NULL,
	[role] [varchar](50) NOT NULL,
	[createdDate] [date] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ConsignmentMaster] ADD  CONSTRAINT [DF_ConsignmentMaster_idDeleted]  DEFAULT ((0)) FOR [idDeleted]
GO
ALTER TABLE [dbo].[CustomerMaster] ADD  CONSTRAINT [DF_CustomerMaster_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[ConsignmentMaster]  WITH CHECK ADD  CONSTRAINT [FK_ConsigneeId] FOREIGN KEY([consigneeId])
REFERENCES [dbo].[CustomerMaster] ([customerId])
GO
ALTER TABLE [dbo].[ConsignmentMaster] CHECK CONSTRAINT [FK_ConsigneeId]
GO
ALTER TABLE [dbo].[ConsignmentMaster]  WITH CHECK ADD  CONSTRAINT [FK_ConsignmentMaster_CustomerMaster] FOREIGN KEY([consignerId])
REFERENCES [dbo].[CustomerMaster] ([customerId])
GO
ALTER TABLE [dbo].[ConsignmentMaster] CHECK CONSTRAINT [FK_ConsignmentMaster_CustomerMaster]
GO
/****** Object:  StoredProcedure [dbo].[spCustomer_Delete]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shailesh
-- Create date: 29th Jan 2024
-- Description:	Simple insert update of employee records
-- =============================================
CREATE PROCEDURE [dbo].[spCustomer_Delete]
@CustomerId int,
@flag int output
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Customer SET IsDeleted = 1 WHERE CustomerId = @CustomerId
	SELECT @flag = @CustomerId
END
GO
/****** Object:  StoredProcedure [dbo].[spCustomer_GetAll_ById]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shailesh
-- Create date: 29th Jan 2024
-- Description:	Simple insert update of customer records
-- =============================================
CREATE PROCEDURE [dbo].[spCustomer_GetAll_ById]
@CustomerId int = null	
AS
BEGIN
	SET NOCOUNT ON;
	IF(@CustomerId is NULL OR @CustomerId = 0)
	BEGIN
		SELECT * FROM Customer WHERE IsDeleted = 0
	END
	ELSE
	BEGIN
		SELECT * FROM Customer WHERE customerId = @CustomerId AND IsDeleted = 0
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spCustomer_Insert_Update]    Script Date: 23-02-2024 22:45:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shailesh
-- Create date: 29th Jan 2024
-- Description:	Simple insert update of employee records
-- =============================================
CREATE PROCEDURE [dbo].[spCustomer_Insert_Update] 
	-- Add the parameters for the stored procedure here
@CustomerId int = null,
@Name nvarchar(50),
@Email nvarchar(50),
@Mobile nvarchar(50),
@GstNo nvarchar(50),
@Address nvarchar(50),
@City nvarchar(50),
@State nvarchar(50),
@flag int output
	
AS
BEGIN
	SET NOCOUNT ON;
	IF(@CustomerId is null OR @CustomerId = 0)
	BEGIN
		insert into CustomerMaster values  (@Name,@Email,@Mobile, @GstNo,@Address,@City,@State,0,GETDATE())
		SELECT @flag = (SELECT SCOPE_IDENTITY())
	END
	ELSE
	BEGIN
		UPDATE CustomerMaster SET name = @Name, email= @Email,gstNo = @GstNo, mobile =@Mobile, address = @Address,
		city = @City,state=@State WHERE customerId = @CustomerId
		SELECT @flag = @CustomerId
	END
END
GO
