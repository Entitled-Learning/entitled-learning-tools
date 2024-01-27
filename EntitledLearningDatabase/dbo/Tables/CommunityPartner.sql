CREATE TABLE [dbo].[CommunityPartner](
	[Name] [nvarchar](100) NOT NULL PRIMARY KEY,
	[PhoneNumber] [nvarchar](20) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[AddressLine1] [nvarchar](255) NULL,
	[AddressLine2] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zipcode] [nvarchar](10) NULL,
	[ContractVersion] [nvarchar](50) NULL,
	[CreatedOn] DATETIME DEFAULT GETDATE(),
	[UpdatedOn] DATETIME NULL
)