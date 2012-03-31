USE [test]
GO

/****** Object:  Table [dbo].[customers]    Script Date: 03/31/2012 13:05:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[customers](
	[serno] [int] IDENTITY(1,1) NOT NULL,
	[secondName] [nvarchar](max) NOT NULL,
	[firstName] [nvarchar](max) NOT NULL,
	[middleName] [nvarchar](max) NULL,
	[country] [nvarchar](max) NULL,
	[passportData] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NULL,
	[phone] [nvarchar](max) NULL,
	[details] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[serno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [test]
GO

/****** Object:  Table [dbo].[dict_percents]    Script Date: 03/31/2012 13:06:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[dict_percents](
	[serno] [int] NOT NULL,
	[type] [int] NOT NULL,
	[from_amount] [int] NOT NULL,
	[to_amount] [int] NOT NULL,
	[monthly] [float] NOT NULL,
	[yearly] [float] NOT NULL,
	[ratio] [float] NOT NULL,
 CONSTRAINT [PK_dict_percents] PRIMARY KEY CLUSTERED 
(
	[serno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [test]
GO

/****** Object:  Table [dbo].[item_types]    Script Date: 03/31/2012 13:07:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[item_types](
	[serno] [int] NOT NULL,
	[name] [nvarchar](max) NULL,
	[parameters] [nvarchar](max) NULL,
	[price] [nvarchar](max) NULL
) ON [PRIMARY]

GO

USE [test]
GO

/****** Object:  Table [dbo].[users]    Script Date: 03/31/2012 13:07:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[users](
	[username] [nvarchar](50) NOT NULL,
	[password] [varchar](50) NULL,
	[full_name] [nvarchar](50) NULL,
	[role] [nchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [test]
GO

/****** Object:  StoredProcedure [dbo].[add_customer]    Script Date: 03/31/2012 13:07:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Artak
-- Create date: 21.03.2012
-- Description:	Add Customer Procedure
-- =============================================
CREATE PROCEDURE [dbo].[add_customer]
	@sname nvarchar(MAX),
	@fname nvarchar(MAX),	
	@mname nvarchar(MAX),
	@country nvarchar(MAX),
	@passport nvarchar(MAX),
	@address nvarchar(MAX),
	@phone nvarchar(MAX),
	@details nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
    INSERT INTO customers 
    (secondName,firstName,middleName,country,passportData,address,phone,details) 
    VALUES
    (@sname, @fname, @mname, @country, @passport, @address, @phone, @details)	
END

GO

USE [test]
GO

/****** Object:  StoredProcedure [dbo].[get_percents]    Script Date: 03/31/2012 13:08:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Artak
-- Create date: 23.03.2012
-- Description:	Get Percents List Procedure
-- =============================================
CREATE PROCEDURE [dbo].[get_percents]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM dict_percents	
END

GO

USE [test]
GO

/****** Object:  StoredProcedure [dbo].[search_customer]    Script Date: 03/31/2012 13:08:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Artak
-- Create date: 19.03.2012
-- Description:	Search Customer Procedure
-- =============================================
CREATE PROCEDURE [dbo].[search_customer]
	@sname nvarchar(MAX),
	@fname nvarchar(MAX),	
	@mname nvarchar(MAX),
	@passport nvarchar(MAX),
	@phone nvarchar(MAX),
	@address nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT @sname = RTRIM(@sname) + '%';
    SELECT @fname = RTRIM(@fname) + '%';
    SELECT @mname = RTRIM(@mname) + '%';
    SELECT @passport = RTRIM(@passport);
    SELECT @phone = '%' + RTRIM(@phone) + '%';
    SELECT @address = '%' + RTRIM(@address) + '%';
    
    if @passport = ''
    BEGIN
		SELECT TOP 10 * FROM customers 
		WHERE RTRIM(secondName) like @sname
		and RTRIM(firstName) like @fname
		and RTRIM(middleName) like @mname
		and RTRIM(phone) like @phone
		and RTRIM(address) like @address
	END
	ELSE
	BEGIN
		SELECT TOP 10 * FROM customers WHERE
		RTRIM(passportData) = @passport
	END
	
END

GO

