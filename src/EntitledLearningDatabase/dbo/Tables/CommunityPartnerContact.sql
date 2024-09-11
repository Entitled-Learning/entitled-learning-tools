CREATE TABLE [dbo].[CommunityPartnerContact](
  [Id] VARCHAR(10) NOT NULL PRIMARY KEY,
  [Prefix] [nvarchar](50) NULL,
  [FirstName] [nvarchar](50) NULL,
  [MiddleName] [nvarchar](50) NULL,
  [LastName] [nvarchar](50) NULL,
  [Suffix] [nvarchar](50) NULL,
  [OfficePhoneNumber] [nvarchar](20) NULL,
  [CellPhoneNumber] [nvarchar](20) NULL,
  [FaxPhoneNumber] [nvarchar](20) NULL,
  [EmailAddress] [nvarchar](100) NULL,
  [AddressLine1] [nvarchar](255) NULL,
  [AddressLine2] [nvarchar](255) NULL,
  [City] [nvarchar](50) NULL,
  [State] [nvarchar](50) NULL,
  [ZipCode] [nvarchar](10) NULL,
  [CommunityPartnerName] [nvarchar](100) NULL,
  [ContractVersion] [nvarchar](50) NULL,
  [CreatedOn] DATETIME DEFAULT GETDATE(),
  [UpdatedOn] DATETIME NULL
  
  FOREIGN KEY ([CommunityPartnerName]) REFERENCES [CommunityPartner]([Name])
)