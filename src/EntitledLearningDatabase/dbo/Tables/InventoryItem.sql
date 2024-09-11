CREATE TABLE [dbo].[InventoryItem]
(
    [Id] [UNIQUEIDENTIFIER] PRIMARY KEY,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[Cost] [decimal](18, 2) NULL,
	[PhysicalLocation] [nvarchar](50) NULL,
	[ExpirationDate] [nvarchar](50) NULL,
	[Sku] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[ContractVersion] [nvarchar](50) NULL,
	[CreatedOn] DATETIME DEFAULT GETDATE(),
	[UpdatedOn] DATETIME NULL
)
