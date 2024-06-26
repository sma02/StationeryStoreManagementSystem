USE G2DB
/****** Object:  Table [dbo].[Category]    Script Date: 5/5/2024 7:46:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Picture] [image] NULL,
	[IsDeleted] [bit] NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CK_Unique_Category_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxLog]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxLog](
	[CategoryId] [int] NOT NULL,
	[GST] [float] NOT NULL,
	[AddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_TaxLog] PRIMARY KEY CLUSTERED 
(
	[AddedOn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetCategories_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetCategories_View]
AS
SELECT        C.Id, C.Name, T.GST
FROM            dbo.Category AS C INNER JOIN
                             (SELECT        CategoryId, MAX(AddedOn) AS MaxCreatedOn
                               FROM            dbo.TaxLog
                               GROUP BY CategoryId) AS MD ON C.Id = MD.CategoryId INNER JOIN
                         dbo.TaxLog AS T ON T.CategoryId = MD.CategoryId AND T.AddedOn = MD.MaxCreatedOn
WHERE        (C.IsDeleted = 0)
GO
/****** Object:  Table [dbo].[Company]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CK_Unique_Company_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetCompanies_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[GetCompanies_View]
AS
SELECT Id, Name
FROM Company
WHERE IsDeleted = 0;
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[DiscountAmount] [money] NULL,
	[TaxAmount] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC,
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceLog]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceLog](
	[ProductId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[RetailPrice] [money] NOT NULL,
	[DiscountAmount] [money] NOT NULL,
	[AddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_PriceLog] PRIMARY KEY CLUSTERED 
(
	[AddedOn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nchar](5) NOT NULL,
	[Picture] [image] NULL,
	[CompanyId] [int] NULL,
	[ReorderThreshold] [int] NULL,
	[IsDiscontinued] [bit] NOT NULL,
	[CategoryId] [int] NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CK_Unique_Product_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSupplier]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSupplier](
	[SupplierId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ProductSupplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierStock]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierStock](
	[SupplierId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ShopId] [int] NOT NULL,
	[IsShipment] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetProducts_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetProducts_View]
AS
SELECT        Id, Name, Code,
                             (SELECT        CAST(MAX(dbo.PriceLog.Price) AS DECIMAL(10, 2)) AS Expr1
                               FROM            dbo.PriceLog INNER JOIN
                                                             (SELECT        SupplierId, MAX(AddedOn) AS AddedOn
                                                               FROM            dbo.PriceLog AS PriceLog_5
                                                               WHERE        (ProductId = dbo.Product.Id)
                                                               GROUP BY SupplierId) AS s1 ON s1.AddedOn = dbo.PriceLog.AddedOn
                               WHERE        (dbo.PriceLog.ProductId = dbo.Product.Id)
                               GROUP BY dbo.PriceLog.ProductId) AS Price,
                             (SELECT        CAST(MAX(PriceLog_4.RetailPrice) AS DECIMAL(10, 2)) AS Expr1
                               FROM            dbo.PriceLog AS PriceLog_4 INNER JOIN
                                                             (SELECT        SupplierId, MAX(AddedOn) AS AddedOn
                                                               FROM            dbo.PriceLog AS PriceLog_3
                                                               WHERE        (ProductId = dbo.Product.Id)
                                                               GROUP BY SupplierId) AS s1_2 ON s1_2.AddedOn = PriceLog_4.AddedOn
                               WHERE        (PriceLog_4.ProductId = dbo.Product.Id)
                               GROUP BY PriceLog_4.ProductId) AS RetailPrice,
                             (SELECT        CAST(MAX(PriceLog_2.DiscountAmount) AS DECIMAL(10, 2)) AS Expr1
                               FROM            dbo.PriceLog AS PriceLog_2 INNER JOIN
                                                             (SELECT        SupplierId, MAX(AddedOn) AS AddedOn
                                                               FROM            dbo.PriceLog AS PriceLog_1
                                                               WHERE        (ProductId = dbo.Product.Id)
                                                               GROUP BY SupplierId) AS s1_1 ON s1_1.AddedOn = PriceLog_2.AddedOn
                               WHERE        (PriceLog_2.ProductId = dbo.Product.Id)
                               GROUP BY PriceLog_2.ProductId) AS DiscountAmount,
                             (SELECT        Name
                               FROM            dbo.Company
                               WHERE        (Id = dbo.Product.CompanyId)) AS Company,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            dbo.ProductSupplier
                               WHERE        (dbo.Product.Id = ProductId)) AS [No. Suppliers],
                             (SELECT        Name
                               FROM            dbo.Category
                               WHERE        (Id = dbo.Product.CategoryId)) AS Category, CASE WHEN
                             (SELECT        CASE WHEN
                                                             (SELECT        TOP (1) gst
                                                               FROM            TaxLog
                                                               WHERE        TaxLog.CategoryId = Category.Id) IS NULL THEN 16 ELSE
                                                             (SELECT        TOP (1) gst
                                                               FROM            TaxLog
                                                               WHERE        TaxLog.CategoryId = Category.Id) END
                               FROM            Category
                               WHERE        Category.Id = Product .CategoryId) IS NULL THEN 16 ELSE
                             (SELECT        CASE WHEN
                                                             (SELECT        TOP (1) gst
                                                               FROM            TaxLog
                                                               WHERE        TaxLog.CategoryId = Category.Id) IS NULL THEN 16 ELSE
                                                             (SELECT        TOP (1) gst
                                                               FROM            TaxLog
                                                               WHERE        TaxLog.CategoryId = Category.Id) END
                               FROM            Category
                               WHERE        Category.Id = Product .CategoryId) END AS GST,
                             (SELECT        CASE WHEN SUM(Stock) IS NULL THEN 0 ELSE SUM(Stock) END AS Expr1
                               FROM            dbo.SupplierStock
                               WHERE        (ProductId = dbo.Product.Id)) -
                             (SELECT        CASE WHEN SUM(Quantity) IS NULL THEN 0 ELSE SUM(Quantity) END AS Expr1
                               FROM            dbo.OrderDetail
                               WHERE        (ProductId = dbo.Product.Id)) AS Stock
FROM            dbo.Product
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nchar](3) NOT NULL,
	[Contact] [nvarchar](30) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Country] [int] NULL,
	[City] [int] NULL,
	[StreetAddress] [nvarchar](25) NULL,
	[PostalCode] [nvarchar](6) NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Town] [nvarchar](25) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CK_Unique_Supplier_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetShipments_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetShipments_View]
AS
SELECT        dbo.Supplier.Id, dbo.Supplier.Name, dbo.SupplierStock.AddedOn
FROM            dbo.Supplier INNER JOIN
                         dbo.SupplierStock ON dbo.Supplier.Id = dbo.SupplierStock.SupplierId
WHERE        (dbo.SupplierStock.IsShipment = 1)
GROUP BY dbo.Supplier.Id, dbo.Supplier.Name, dbo.SupplierStock.AddedOn
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](30) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetSuppliers_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetSuppliers_View]
AS
SELECT        dbo.Supplier.Id, dbo.Supplier.Name, dbo.Supplier.Contact, dbo.Supplier.Email, dbo.Supplier.StreetAddress, dbo.Supplier.Town, l1.Value AS City, l2.Value AS Country, dbo.Supplier.PostalCode
FROM            dbo.Supplier LEFT OUTER JOIN
                         dbo.Lookup AS l1 ON l1.Id = dbo.Supplier.City LEFT OUTER JOIN
                         dbo.Lookup AS l2 ON l2.Id = dbo.Supplier.Country
WHERE        (dbo.Supplier.IsDeleted = 0)
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NULL,
	[CNIC] [nchar](13) NOT NULL,
	[Contact] [nvarchar](30) NOT NULL,
	[Gender] [int] NULL,
	[DateOfBirth] [date] NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Town] [nvarchar](25) NULL,
	[City] [int] NULL,
	[StreetAddress] [nvarchar](25) NULL,
	[PostalCode] [nvarchar](6) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[UserId] [int] NOT NULL,
	[Username] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LoginFailedCount] [smallint] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[ProfilePicture] [image] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEndAt] [datetime] NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CK_Unique_UserAccount_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetEmployees_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[GetEmployees_View]
AS
SELECT
	U.Id,
    UA.Username,
    CONCAT(U.FirstName, ' ', U.LastName) AS Name,
    U.CNIC,
    U.Contact,
    RL.Value AS Role,
    SL.Value AS Status,
    G.Value AS Gender
FROM UserAccount UA
JOIN [User] U ON UA.UserId = U.Id
JOIN Employee E ON U.Id = E.Id
JOIN Lookup RL ON E.Role = RL.Id
JOIN Lookup SL ON E.Status = SL.Id
JOIN Lookup G ON U.Gender = G.Id
WHERE Sl.Value='Active' AND SL.Category='Status';
GO
/****** Object:  View [dbo].[GetCustomers_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetCustomers_View]
AS
SELECT U.Id, CNIC, CONCAT(FirstName,' ',LastName ) Name, Contact, G.Value Gender 
FROM [User] U
LEFT JOIN Employee E ON U.Id = E.Id
LEFT JOIN Lookup G ON U.Gender = G.Id
WHERE E.Id IS NULL


GO
/****** Object:  Table [dbo].[Notification]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[AddedBy] [int] NULL,
	[ViewedAt] [datetime] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetNotifications_View]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetNotifications_View] AS
SELECT N.Id, N.UserId,
	   CASE WHEN AddedBy IS NULL THEN 'SYSTEM' ELSE  CONCAT(U.FirstName, ' ', U.LastName) END [From], 
       CASE WHEN ViewedAt IS NULL THEN 'NO' ELSE 'YES' END IsViewed, 
       Content AS [Notification] 
FROM Notification N 
LEFT JOIN Employee E ON N.AddedBy = E.Id
LEFT JOIN [User] U ON E.Id = U.Id;

GO
/****** Object:  Table [dbo].[Application]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicantId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[ApprovedBy] [int] NULL,
	[ApprovedAt] [datetime] NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Claim]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claim](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Claim] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC,
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeSalary]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSalary](
	[EmployeeId] [int] NOT NULL,
	[Salary] [money] NOT NULL,
	[AddedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[Timestamp] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
	[Town] [nvarchar](25) NULL,
	[City] [nvarchar](25) NULL,
	[StreetAddress] [nvarchar](25) NULL,
	[PostalCode] [nvarchar](6) NULL,
	[ShopId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatusLog]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusLog](
	[OrderId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentDues]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDues](
	[CustomerId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[AddedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetRepayments_View]    Script Date: 05/05/2024 7:49:05 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[GetRepayments_View] 
AS
SELECT U.Id, CONCAT(U.FirstName, ' ', U.LastName) AS "Customer Name", U.CNIC, U.Contact, L.Value Gender, U.AddedOn AS "Registered On", ABS(COALESCE(SUM(pd.Amount), 0)) AS "Total Pending"
FROM [User] U
LEFT JOIN Employee E ON U.Id = E.Id
LEFT JOIN PaymentDues PD ON U.Id = PD.CustomerId
JOIN Lookup L ON U.Gender = L.Id
WHERE E.Id IS NULL
GROUP BY U.Id, U.FirstName, U.LastName, U.CNIC, U.Contact, L.Value, U.AddedOn
GO
/****** Object:  Table [dbo].[Review]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[CustomerId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Comment] [nvarchar](100) NULL,
	[Rating] [float] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shop]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shop](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Town] [nvarchar](25) NULL,
	[City] [int] NULL,
	[StreetAddress] [nvarchar](25) NULL,
	[PostalCode] [nvarchar](6) NULL,
	[Status] [int] NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Contact] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_sHOP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[UserId] [int] NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[LogoutTime] [datetime] NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_Employee] FOREIGN KEY([ApprovedBy])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_Employee]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_Lookup]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_User] FOREIGN KEY([ApplicantId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_User]
GO
ALTER TABLE [dbo].[Claim]  WITH CHECK ADD  CONSTRAINT [FK_Claim_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Claim] CHECK CONSTRAINT [FK_Claim_Lookup]
GO
ALTER TABLE [dbo].[Claim]  WITH CHECK ADD  CONSTRAINT [FK_Claim_Lookup1] FOREIGN KEY([Type])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Claim] CHECK CONSTRAINT [FK_Claim_Lookup1]
GO
ALTER TABLE [dbo].[Claim]  WITH CHECK ADD  CONSTRAINT [FK_Claim_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[Claim] CHECK CONSTRAINT [FK_Claim_Order]
GO
ALTER TABLE [dbo].[Claim]  WITH CHECK ADD  CONSTRAINT [FK_Claim_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Claim] CHECK CONSTRAINT [FK_Claim_Product]
GO
ALTER TABLE [dbo].[Claim]  WITH CHECK ADD  CONSTRAINT [FK_Claim_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Claim] CHECK CONSTRAINT [FK_Claim_Supplier]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Lookup]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Lookup1] FOREIGN KEY([Role])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Lookup1]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Shop]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_User]
GO
ALTER TABLE [dbo].[EmployeeSalary]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalary_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeSalary] CHECK CONSTRAINT [FK_EmployeeSalary_Employee]
GO
ALTER TABLE [dbo].[EmployeeSalary]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalary_Employee1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeSalary] CHECK CONSTRAINT [FK_EmployeeSalary_Employee1]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Employee]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Lookup1] FOREIGN KEY([Type])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Lookup1]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Shop]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Supplier]
GO
ALTER TABLE [dbo].[OrderStatusLog]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatusLog_Employee] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[OrderStatusLog] CHECK CONSTRAINT [FK_OrderStatusLog_Employee]
GO
ALTER TABLE [dbo].[OrderStatusLog]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatusLog_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[OrderStatusLog] CHECK CONSTRAINT [FK_OrderStatusLog_Lookup]
GO
ALTER TABLE [dbo].[OrderStatusLog]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatusLog_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderStatusLog] CHECK CONSTRAINT [FK_OrderStatusLog_Order]
GO
ALTER TABLE [dbo].[PaymentDues]  WITH CHECK ADD  CONSTRAINT [FK_PaymentDues_User] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PaymentDues] CHECK CONSTRAINT [FK_PaymentDues_User]
GO
ALTER TABLE [dbo].[PriceLog]  WITH CHECK ADD  CONSTRAINT [FK_PriceLog_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[PriceLog] CHECK CONSTRAINT [FK_PriceLog_Product]
GO
ALTER TABLE [dbo].[PriceLog]  WITH CHECK ADD  CONSTRAINT [FK_PriceLog_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[PriceLog] CHECK CONSTRAINT [FK_PriceLog_Supplier]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Company]
GO
ALTER TABLE [dbo].[ProductSupplier]  WITH CHECK ADD  CONSTRAINT [FK_ProductSupplier_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductSupplier] CHECK CONSTRAINT [FK_ProductSupplier_Product]
GO
ALTER TABLE [dbo].[ProductSupplier]  WITH CHECK ADD  CONSTRAINT [FK_ProductSupplier_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[ProductSupplier] CHECK CONSTRAINT [FK_ProductSupplier_Supplier]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Product]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
ALTER TABLE [dbo].[Shop]  WITH CHECK ADD  CONSTRAINT [FK_Shop_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Shop] CHECK CONSTRAINT [FK_Shop_Lookup]
GO
ALTER TABLE [dbo].[Shop]  WITH CHECK ADD  CONSTRAINT [FK_Shop_Lookup1] FOREIGN KEY([City])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Shop] CHECK CONSTRAINT [FK_Shop_Lookup1]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Lookup] FOREIGN KEY([City])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Lookup]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Lookup1] FOREIGN KEY([Country])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Lookup1]
GO
ALTER TABLE [dbo].[SupplierStock]  WITH CHECK ADD  CONSTRAINT [FK_SupplierStock_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[SupplierStock] CHECK CONSTRAINT [FK_SupplierStock_Product]
GO
ALTER TABLE [dbo].[SupplierStock]  WITH CHECK ADD  CONSTRAINT [FK_SupplierStock_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[SupplierStock] CHECK CONSTRAINT [FK_SupplierStock_Shop]
GO
ALTER TABLE [dbo].[SupplierStock]  WITH CHECK ADD  CONSTRAINT [FK_SupplierStock_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[SupplierStock] CHECK CONSTRAINT [FK_SupplierStock_Supplier]
GO
ALTER TABLE [dbo].[TaxLog]  WITH CHECK ADD  CONSTRAINT [FK_TaxLog_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[TaxLog] CHECK CONSTRAINT [FK_TaxLog_Category]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Lookup] FOREIGN KEY([Gender])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Lookup]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Lookup1] FOREIGN KEY([City])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Lookup1]
GO
ALTER TABLE [dbo].[UserAccount]  WITH CHECK ADD  CONSTRAINT [FK_UserAccount_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserAccount] CHECK CONSTRAINT [FK_UserAccount_User]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLogin_Employee] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_EmployeeLogin_Employee]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [CK_Application] CHECK  (([ApprovedAt] IS NULL AND [ApprovedBy] IS NULL OR [ApprovedAt] IS NOT NULL AND [ApprovedBy] IS NOT NULL))
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [CK_Application]
GO


SET IDENTITY_INSERT [dbo].[Lookup] ON 
GO

INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (1, N'Active', N'ShopStatus')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (2, N'Active', N'Status')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (3, N'Inactive', N'Status')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (4, N'Admin', N'Role')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (5, N'Manager', N'Role')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (6, N'Cashier', N'Role')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (7, N'Male', N'Gender')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (8, N'Female', N'Gender')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (9, N'Lahore', N'CityPakistan')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (10, N'Karachi', N'CityPakistan')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (11, N'Islamabad', N'CityPakistan')
GO
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (12, N'Pakistan', N'Country')
GO
SET IDENTITY_INSERT [dbo].[Lookup] OFF
GO

/****** Object:  UserDefinedTableType [dbo].[udtt_ProductSupplierPrice]    Script Date: 5/5/2024 7:46:24 PM ******/
CREATE TYPE [dbo].[udtt_ProductSupplierPrice] AS TABLE(
	[ProductId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[RetailPrice] [money] NOT NULL,
	[DiscountAmount] [money] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[udtt_ProductSuppliers]    Script Date: 5/5/2024 7:46:24 PM ******/
CREATE TYPE [dbo].[udtt_ProductSuppliers] AS TABLE(
	[SupplierId] [int] NULL,
	[ProductId] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[udtt_Shipment]    Script Date: 5/5/2024 7:46:24 PM ******/
CREATE TYPE [dbo].[udtt_Shipment] AS TABLE(
	[SupplierId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Stock] [int] NOT NULL
)
GO


/****** Object:  StoredProcedure [dbo].[CheckCredentials]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckCredentials]
    @Username NVARCHAR(30),
    @Password NVARCHAR(30),
    @UserId int OUTPUT
AS
BEGIN
    DECLARE @StoredPasswordHash NVARCHAR(128);
    DECLARE @InputPasswordHash NVARCHAR(128);

    SELECT @StoredPasswordHash = PasswordHash
    FROM UserAccount
    WHERE Username = @Username;

    DECLARE @Hash VARBINARY(64);
    SET @Hash = HASHBYTES('SHA2_256', @Password);
    SET @InputPasswordHash = CONVERT(NVARCHAR(128), @Hash, 2);

    IF @StoredPasswordHash = @InputPasswordHash
        SELECT @UserId = UserId
		FROM UserAccount
		WHERE Username = @Username
		
	ELSE
        SET @UserId = NULL; 
END;

GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomPassword]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateRandomPassword]
    @Password NVARCHAR(MAX) OUTPUT,
    @Length INT
AS
BEGIN
    DECLARE @Chars NVARCHAR(MAX) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    DECLARE @RandomIndex INT;
    DECLARE @Counter INT = 1;
    SET @Password = '';

    WHILE @Counter <= @Length
    BEGIN
        SET @RandomIndex = ROUND(RAND() * (LEN(@Chars) - 1) + 1, 0);
        SET @Password = @Password + SUBSTRING(@Chars, @RandomIndex, 1);
        SET @Counter = @Counter + 1;
    END;
END;

GO
/****** Object:  StoredProcedure [dbo].[HashPassword]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HashPassword]
    @Password NVARCHAR(100),  -- Adjust the length as needed
    @PasswordHash NVARCHAR(128) OUTPUT
AS
BEGIN
    DECLARE @Hash VARBINARY(64);  -- SHA-256 produces a 64-byte hash

    -- Compute the hash of the password using SHA-256
    SET @Hash = HASHBYTES('SHA2_256', @Password);

    -- Convert binary hash to hexadecimal string
    SET @PasswordHash = CONVERT(NVARCHAR(128), @Hash, 2);
END;

GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ResetPassword]
    @UserId INT,
    @NewPassword NVARCHAR(30) OUTPUT
AS
BEGIN
    -- Generate new password
    EXEC dbo.GenerateRandomPassword @NewPassword OUTPUT, 5;
	
	DECLARE @Hash VARBINARY(64);

    SET @Hash = HASHBYTES('SHA2_256', @NewPassword);
	 

    UPDATE UserAccount
    SET PasswordHash = CONVERT(NVARCHAR(128), @Hash, 2)
    WHERE UserId = @UserId;

    -- Output the new password
    SELECT @NewPassword AS NewPassword;
END;

GO
/****** Object:  StoredProcedure [dbo].[stpDeleteCategory]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpDeleteCategory]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Category
    SET IsDeleted = 1,
        UpdatedOn = GETDATE()
    WHERE Id = @Id;

    -- Optionally, you can also delete corresponding records from TaxLog
    -- DELETE FROM TaxLog WHERE CategoryId = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[stpDeleteCompany]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpDeleteCompany]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Company
    SET IsDeleted = 1,
        UpdatedOn = GETDATE()
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[stpDeleteEmployee]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[stpDeleteEmployee]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @StatusId int
	SELECT @StatusId = Id FROM Lookup WHERE Value = 'Inactive';

    UPDATE Employee Set Status = @StatusId WHERE Id = @Id
	UPDATE [User] SET UpdatedOn = CURRENT_TIMESTAMP WHERE Id = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[stpDeleteProduct]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpDeleteProduct]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Product
    SET IsDiscontinued = 1,
        UpdatedOn = GETDATE()
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[stpDeleteProductSuppliers]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpDeleteProductSuppliers]
@ProductSuppliers udtt_ProductSuppliers READONLY
AS
SET NOCOUNT ON;

UPDATE ProductSupplier
SET IsDeleted=1
FROM ProductSupplier
JOIN @ProductSuppliers p1
ON p1.ProductId=ProductSupplier.ProductId AND p1.SupplierId=ProductSupplier.SupplierId;

GO
/****** Object:  StoredProcedure [dbo].[stpDeleteSupplier]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpDeleteSupplier]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Supplier
    SET IsDeleted = 1,
        UpdatedOn = GETDATE()
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertCategory]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertCategory]
    @Name NVARCHAR(30),
    @GST float
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CategoryId INT;

    INSERT INTO Category (Name, IsDeleted, AddedOn)
    VALUES (@Name, 0, GETDATE());

    SET @CategoryId = SCOPE_IDENTITY();
	
	IF @GST=-1
    BEGIN
		DECLARE @dGST float = 16
		INSERT INTO TaxLog (CategoryId, GST, AddedOn)
		VALUES (@CategoryId, @dGST, GETDATE());
    END
	
	ELSE
	BEGIN
		INSERT INTO TaxLog (CategoryId, GST, AddedOn)
		VALUES (@CategoryId, @GST, GETDATE());
	END
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertCompany]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertCompany]
    @Name NVARCHAR(60)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Company (Name, AddedOn, IsDeleted)
    VALUES (@Name, GETDATE(), 0);
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertCustomer]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpInsertCustomer]
(
    @FirstName NVARCHAR(30),
    @LastName NVARCHAR(30),
    @CNIC NVARCHAR(13),
    @Contact NVARCHAR(11),
    @Gender int = NULL,
    @DateOfBirth DATE = NULL,
    @Town NVARCHAR(25) = NULL,
    @City int = NULL,
    @StreetAddress NVARCHAR(25) = NULL,
    @PostalCode NVARCHAR(6) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [User] (FirstName, LastName, CNIC, Contact, Gender, DateOfBirth, Town, City, StreetAddress, PostalCode, AddedOn)
    VALUES (@FirstName, @LastName, @CNIC, @Contact, @Gender, @DateOfBirth, @Town, @City, @StreetAddress, @PostalCode, CURRENT_TIMESTAMP);

END

GO
/****** Object:  StoredProcedure [dbo].[stpInsertEmployee]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertEmployee]
(
    @FirstName NVARCHAR(30),
    @LastName NVARCHAR(30),
    @CNIC NVARCHAR(13),
    @Contact NVARCHAR(11),
    @Gender int = NULL,
    @DateOfBirth DATE = NULL,
    @Town NVARCHAR(25) = NULL,
    @City int = NULL,
    @StreetAddress NVARCHAR(25) = NULL,
    @PostalCode NVARCHAR(6) = NULL,
    @Username NVARCHAR(20),
    @Email NVARCHAR(MAX),
    @EmailConfirmed BIT = 1,
    @LoginFailedCount SMALLINT = 0,
    @TwoFactorEnabled BIT = 0,
    @Password NVARCHAR(5),
    @ProfilePicture IMAGE = NULL,
    @LockoutEnabled BIT = 0,
    @LockoutEndAt DATETIME = NULL,
    @Role int,
    @Salary MONEY
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT;
	DECLARE @PasswordHash NVARCHAR(128);

	DECLARE @Status INT = (SELECT Id FROM Lookup WHERE Lookup.Category='Status' AND Lookup.Value='Active')
	DECLARE @ShopId INT = (SELECT TOP(1) Id FROM Shop)

	EXEC HashPassword @Password, @PasswordHash OUTPUT;

    INSERT INTO [User] (FirstName, LastName, CNIC, Contact, Gender, DateOfBirth, Town, City, StreetAddress, PostalCode, AddedOn)
    VALUES (@FirstName, @LastName, @CNIC, @Contact, @Gender, @DateOfBirth, @Town, @City, @StreetAddress, @PostalCode, CURRENT_TIMESTAMP);

    SET @UserId = SCOPE_IDENTITY();

    INSERT INTO UserAccount (UserId, Username, Email, EmailConfirmed, LoginFailedCount, TwoFactorEnabled, PasswordHash, ProfilePicture, LockoutEnabled, LockoutEndAt)
    VALUES (@UserId, @Username, @Email, @EmailConfirmed, @LoginFailedCount, @TwoFactorEnabled, @PasswordHash, @ProfilePicture, @LockoutEnabled, @LockoutEndAt);

    INSERT INTO Employee (Id, Role, Status, ShopId)
    VALUES (@UserId, @Role, @Status, @ShopId);

    INSERT INTO EmployeeSalary (EmployeeId, Salary, AddedOn)
    VALUES (@UserId, @Salary, CURRENT_TIMESTAMP);

END

GO
/****** Object:  StoredProcedure [dbo].[stpInsertNotification]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertNotification] (
    @UserId INT,
    @Content NVARCHAR(100),
    @AddedBy INT = NULL
)
AS
BEGIN
    INSERT INTO Notification (UserId, Content, AddedOn, AddedBy)
    VALUES (@UserId, @Content, CURRENT_TIMESTAMP, @AddedBy)
END

GO
/****** Object:  StoredProcedure [dbo].[stpInsertProduct]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertProduct]
@Name nvarchar(30),
@Code nvarchar(5),
@CompanyId int null,
@ReorderThreshold int,
@CategoryId int null
AS
BEGIN
INSERT INTO Product(Name,Code,CompanyId,ReorderThreshold,CategoryId,IsDiscontinued,AddedOn)
VALUES (@Name,@Code,@CompanyId,@ReorderThreshold,@CategoryId,0,CURRENT_TIMESTAMP);

SELECT CAST(scope_identity() AS int)
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertProductSupplierPrice]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertProductSupplierPrice]
@ProductSupplierPrice udtt_ProductSupplierPrice READONLY
AS
INSERT INTO PriceLog(ProductId,SupplierId,Price,RetailPrice,DiscountAmount,AddedOn)
SELECT ProductId,SupplierId,Price,RetailPrice,DiscountAmount,CURRENT_TIMESTAMP
FROM @ProductSupplierPrice
GO
/****** Object:  StoredProcedure [dbo].[stpInsertProductSuppliers]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertProductSuppliers]
@ProductSuppliers udtt_ProductSuppliers READONLY
AS
SET NOCOUNT ON;

UPDATE ProductSupplier
SET IsDeleted=0,AddedOn=CURRENT_TIMESTAMP
FROM ProductSupplier
JOIN @ProductSuppliers p1
ON p1.ProductId=ProductSupplier.ProductId AND p1.SupplierId=ProductSupplier.SupplierId;

INSERT INTO ProductSupplier (SupplierId,ProductId,AddedOn,IsDeleted)
SELECT p2.SupplierId,p2.ProductId,CURRENT_TIMESTAMP,0
FROM @ProductSuppliers p2
LEFT JOIN ProductSupplier
ON ProductSupplier.ProductId=p2.ProductId AND ProductSupplier.SupplierId=p2.SupplierId
WHERE ProductSupplier.ProductId IS NULL
GO
/****** Object:  StoredProcedure [dbo].[stpInsertShipment]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertShipment]
@Shipment udtt_Shipment READONLY
AS
INSERT INTO SupplierStock(SupplierId,ProductId,Stock,ShopId,AddedOn,IsShipment)
SELECT s.SupplierId,s.ProductId,s.Stock,(SELECT TOP(1) Id FROM Shop),CURRENT_TIMESTAMP,1
FROM @Shipment s


GO
/****** Object:  StoredProcedure [dbo].[stpInsertSupplier]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertSupplier]
@Name nvarchar(30),
@Code nvarchar(3),
@Contact nvarchar(30),
@Email nvarchar(max),
@StreetAddress nvarchar(25),
@Town nvarchar(25),
@City int,
@Country nvarchar(25),
@PostalCode nvarchar(6)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Supplier(Name,Code,Contact,Email,StreetAddress,Town,City,PostalCode,IsDeleted,AddedOn)
	VALUES (@Name,@Code,@Contact,@Email,@StreetAddress,@Town,@City,@PostalCode,0,CURRENT_TIMESTAMP)

	SELECT CAST(scope_identity() AS int)
END
GO
/****** Object:  StoredProcedure [dbo].[stpUpdateCategory]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpUpdateCategory]
    @Id INT,
    @Name NVARCHAR(30),
    @GST float
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingGST float;

    -- Retrieve the existing GST for the category
    SELECT @ExistingGST = GST
    FROM TaxLog
    WHERE CategoryId = @Id;

    -- Update the category name
    UPDATE Category
    SET Name = @Name,
        UpdatedOn = GETDATE()
    WHERE Id = @Id;

    -- Check if there's an existing GST entry for the category
    IF @ExistingGST IS NOT NULL
    BEGIN
        -- If GST is different, insert a new record into TaxLog
        IF @ExistingGST <> @GST
        BEGIN
            INSERT INTO TaxLog (CategoryId, GST, AddedOn)
            VALUES (@Id, @GST, GETDATE());
        END
    END
    ELSE
    BEGIN
        -- If no existing GST entry, insert a new record into TaxLog
        INSERT INTO TaxLog (CategoryId, GST, AddedOn)
        VALUES (@Id, @GST, GETDATE());
    END
END
GO
/****** Object:  StoredProcedure [dbo].[stpUpdateCompany]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpUpdateCompany]
    @Id INT,
    @Name NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Company
    SET Name = @Name,
        UpdatedOn = GETDATE()
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[stpUpdateNotification]    Script Date: 5/5/2024 7:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpUpdateNotification] (@Id INT)
AS
BEGIN
	UPDATE Notification SET ViewedAt = CURRENT_TIMESTAMP WHERE Id = @Id
END
GO
/****** Object:  View [dbo].[GetCustomerRepayments]    Script Date: 06/05/2024 8:35:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetCustomerRepayments_View] AS
SELECT AddedOn "Timestamp", Amount , CASE WHEN Amount > 0 THEN 'Incoming' WHEN Amount < 0 THEN 'Outgoing' END "Type", CustomerId  
FROM PaymentDues
GO
/****** Object:  View [dbo].[GetRepayments_View]    Script Date: 06/05/2024 8:35:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/****** Object:  StoredProcedure [dbo].[stpInsertPaymentDues]    Script Date: 06/05/2024 8:35:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertPaymentDues] 
@CustomerId int,
@Amount money
AS
INSERT INTO PaymentDues (CustomerId,Amount,AddedOn) VALUES (@CustomerId,@Amount,CURRENT_TIMESTAMP)
GO


USE [G2DB]
GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 09/05/2024 6:33:42 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[ResetPassword]
    @UserId INT,
    @NewPassword NVARCHAR(5) = NULL OUTPUT -- Default value set to NULL
AS
BEGIN
    -- Check if @NewPassword is provided, if not, generate a random password
    IF @NewPassword IS NULL OR @NewPassword = ''
    BEGIN
        -- Generate new password
        EXEC dbo.GenerateRandomPassword @NewPassword OUTPUT, 5;
    END
	
    -- Hash the password
    DECLARE @Hash VARBINARY(64);
    SET @Hash = HASHBYTES('SHA2_256', @NewPassword);
	 
    -- Update the UserAccount table with the new password hash
    UPDATE UserAccount
    SET PasswordHash = CONVERT(NVARCHAR(128), @Hash, 2)
    WHERE UserId = @UserId;

    -- Output the new password
    SELECT @NewPassword AS NewPassword;
END;


GO
/****** Object:  StoredProcedure [dbo].[stpInsertLoginTime]    Script Date: 09/05/2024 6:33:42 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertLoginTime] @UserId int
AS
INSERT into userlogin (UserId,LoginTime) values (@UserId, CURRENT_TIMESTAMP)
GO
/****** Object:  StoredProcedure [dbo].[stpInsertLogoutTime]    Script Date: 09/05/2024 6:33:42 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stpInsertLogoutTime]
(
    @UserId INT
)
AS
BEGIN
    UPDATE UserLogin
    SET LogoutTime = CURRENT_TIMESTAMP
    WHERE UserId = @UserId AND LogoutTime IS NULL;
END;
GO

CREATE TYPE udtt_OrderProducts AS TABLE
(ProductId int
,SupplierId int
,Price money
,DiscountAmount money
,TaxAmount money
,Quantity int)
GO


CREATE PROCEDURE stpInsertOrder
@OrderProducts udtt_OrderProducts READONLY,
@EmployeeId int,
@CustomerId int = NULL,
@PaymentDues money = NULL
AS
BEGIN
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE 
BEGIN TRAN

DECLARE @ShopId int = (SELECT TOP(1) Id from Shop)
DECLARE @Type int = (SELECT Id FROM Lookup WHERE Lookup.Category='OrderType' AND Lookup.Value='Pos')
DECLARE @CustomerPaymentId int = @CustomerId


INSERT INTO [Order](EmployeeId,CustomerId,ShopId,Type,Timestamp)
VALUES (@EmployeeId,@CustomerId,@ShopId,@Type,CURRENT_TIMESTAMP)

IF @PaymentDues IS NOT NULL
BEGIN
EXEC stpInsertPaymentDues @CustomerId=@CustomerPaymentId,@Amount=@PaymentDues
END

DECLARE @Identity int = SCOPE_IDENTITY() 

INSERT INTO [OrderDetail](OrderId,ProductId,SupplierId,Price,DiscountAmount,TaxAmount,Quantity)
SELECT @Identity,op.ProductId,op.SupplierId,op.Price,op.DiscountAmount,op.TaxAmount,op.Quantity
FROM @OrderProducts op

COMMIT TRAN
(SELECT @Identity)
END
GO


CREATE TYPE udtt_StockChanges AS TABLE
(
ProductId int,
SupplierId int,
quantity int,
[description] nvarchar(100) NULL
)
Go

CREATE PROCEDURE stpInsertStockChanges
@StockChanges udtt_StockChanges READONLY
AS
BEGIN

DECLARE @ShopId int = (SELECT TOP(1) Id FROM Shop)

INSERT INTO SupplierStock(SupplierId,ProductId,ShopId,Stock,[Description],IsShipment,AddedOn)
SELECT sc.SupplierId,sc.ProductId,@ShopId,sc.quantity,sc.[description],0,CURRENT_TIMESTAMP
FROM @StockChanges sc
END
GO