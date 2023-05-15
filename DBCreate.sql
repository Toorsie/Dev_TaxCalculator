USE [Dev_TaxCalculator]
GO
/****** Object:  Schema [entity]    Script Date: 2023/05/15 22:35:38 ******/
CREATE SCHEMA [entity]
GO
/****** Object:  Schema [enum]    Script Date: 2023/05/15 22:35:38 ******/
CREATE SCHEMA [enum]
GO
/****** Object:  Schema [lookup]    Script Date: 2023/05/15 22:35:38 ******/
CREATE SCHEMA [lookup]
GO
/****** Object:  Schema [master]    Script Date: 2023/05/15 22:35:38 ******/
CREATE SCHEMA [master]
GO
/****** Object:  Table [enum].[RateType]    Script Date: 2023/05/15 22:35:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [enum].[RateType](
	[RowId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Display] [nvarchar](100) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_RateType] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [enum].[TaxCalculationType]    Script Date: 2023/05/15 22:35:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [enum].[TaxCalculationType](
	[RowId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Display] [nvarchar](100) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_TaxCalculationType] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[PostalCode]    Script Date: 2023/05/15 22:35:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[PostalCode](
	[RowId] [bigint] IDENTITY(1,1) NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PostalCode] [nvarchar](50) NOT NULL,
	[TaxCalculationTypeId] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_PostalCode] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[PostalCodeTaxRate]    Script Date: 2023/05/15 22:35:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[PostalCodeTaxRate](
	[RowId] [bigint] IDENTITY(1,1) NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PostalCodeId] [bigint] NOT NULL,
	[Rate] [decimal](12, 4) NOT NULL,
	[RateTypeId] [int] NOT NULL,
	[FromValue] [money] NOT NULL,
	[ToValue] [money] NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_PostalCodeTaxRate] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [master].[SubmittedTax]    Script Date: 2023/05/15 22:35:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [master].[SubmittedTax](
	[RowId] [bigint] IDENTITY(1,1) NOT NULL,
	[RowGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PostalCodeId] [bigint] NOT NULL,
	[Amount] [money] NOT NULL,
	[Year] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_SubmittedTax] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [enum].[RateType] ([RowId], [Name], [Display], [Description]) VALUES (1, N'Number', NULL, NULL)
GO
INSERT [enum].[RateType] ([RowId], [Name], [Display], [Description]) VALUES (2, N'Percentage', NULL, NULL)
GO
INSERT [enum].[TaxCalculationType] ([RowId], [Name], [Display], [Description]) VALUES (1, N'Progressive', N'Progressive', NULL)
GO
INSERT [enum].[TaxCalculationType] ([RowId], [Name], [Display], [Description]) VALUES (2, N'FlatValue', N'Flat Value', NULL)
GO
INSERT [enum].[TaxCalculationType] ([RowId], [Name], [Display], [Description]) VALUES (3, N'FlatRate', N'Flat Rate', NULL)
GO
SET IDENTITY_INSERT [lookup].[PostalCode] ON 
GO
INSERT [lookup].[PostalCode] ([RowId], [RowGuid], [PostalCode], [TaxCalculationTypeId], [Created], [Updated]) VALUES (1, N'04934a29-9a54-4669-9a11-57d515245b8a', N'7441', 1, CAST(N'2023-05-11T17:50:08.350' AS DateTime), CAST(N'2023-05-11T17:50:08.350' AS DateTime))
GO
INSERT [lookup].[PostalCode] ([RowId], [RowGuid], [PostalCode], [TaxCalculationTypeId], [Created], [Updated]) VALUES (2, N'26b6b1ac-8722-437f-943e-0b21c08b186d', N'A100', 2, CAST(N'2023-05-11T17:50:14.527' AS DateTime), CAST(N'2023-05-11T17:50:14.527' AS DateTime))
GO
INSERT [lookup].[PostalCode] ([RowId], [RowGuid], [PostalCode], [TaxCalculationTypeId], [Created], [Updated]) VALUES (3, N'ce81a5a4-746f-4ab7-9257-83e4d8585104', N'7000', 3, CAST(N'2023-05-11T17:50:19.393' AS DateTime), CAST(N'2023-05-11T17:50:19.393' AS DateTime))
GO
INSERT [lookup].[PostalCode] ([RowId], [RowGuid], [PostalCode], [TaxCalculationTypeId], [Created], [Updated]) VALUES (4, N'4f459c4e-42ab-4e4e-a22f-301471d21723', N'1000', 1, CAST(N'2023-05-11T17:50:24.470' AS DateTime), CAST(N'2023-05-11T17:50:24.470' AS DateTime))
GO
SET IDENTITY_INSERT [lookup].[PostalCode] OFF
GO
SET IDENTITY_INSERT [lookup].[PostalCodeTaxRate] ON 
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (1, N'efe2a0f6-8c6a-4f00-bfe4-f97d5405f0cd', 1, CAST(10.0000 AS Decimal(12, 4)), 2, 0.0000, 8350.0000, CAST(N'2023-05-11T17:51:41.010' AS DateTime), CAST(N'2023-05-11T17:51:41.010' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (2, N'1ad3f17d-0d03-4f45-a95b-d942ac2fe528', 1, CAST(15.0000 AS Decimal(12, 4)), 2, 8351.0000, 33950.0000, CAST(N'2023-05-11T17:52:00.667' AS DateTime), CAST(N'2023-05-11T17:52:00.667' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (3, N'f7ed49b4-d447-4e3c-afbe-7f9446427577', 1, CAST(25.0000 AS Decimal(12, 4)), 2, 33951.0000, 82250.0000, CAST(N'2023-05-11T17:52:24.860' AS DateTime), CAST(N'2023-05-11T17:52:24.860' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (4, N'10f63fd1-09b7-4e8a-b6bf-b00022eaecb1', 1, CAST(28.0000 AS Decimal(12, 4)), 2, 82251.0000, 171550.0000, CAST(N'2023-05-11T17:52:48.240' AS DateTime), CAST(N'2023-05-11T17:52:48.240' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (5, N'8c7ba1d6-3c1e-4b05-a15c-2facefd18a27', 1, CAST(33.0000 AS Decimal(12, 4)), 2, 171551.0000, 372950.0000, CAST(N'2023-05-11T17:53:07.970' AS DateTime), CAST(N'2023-05-11T17:53:07.970' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (6, N'75364218-9848-4ede-b94e-1eadc1a27231', 1, CAST(35.0000 AS Decimal(12, 4)), 2, 372951.0000, NULL, CAST(N'2023-05-11T17:53:27.073' AS DateTime), CAST(N'2023-05-11T17:53:27.073' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (7, N'b8b460ea-d5ff-4da6-b0ee-abcae7bd1b38', 2, CAST(5.0000 AS Decimal(12, 4)), 2, 0.0000, 199999.0000, CAST(N'2023-05-11T17:54:43.760' AS DateTime), CAST(N'2023-05-11T17:54:43.760' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (8, N'53a468e4-503c-4186-8559-b4dd16c7d219', 2, CAST(10000.0000 AS Decimal(12, 4)), 1, 200000.0000, NULL, CAST(N'2023-05-11T17:54:58.107' AS DateTime), CAST(N'2023-05-11T17:54:58.107' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (9, N'403132af-4c77-4af3-8d2c-fb00a7321555', 3, CAST(17.5000 AS Decimal(12, 4)), 2, 0.0000, NULL, CAST(N'2023-05-11T17:55:29.700' AS DateTime), CAST(N'2023-05-11T17:55:29.700' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (10, N'8ae07339-4442-4175-851c-d2adb39c114e', 4, CAST(10.0000 AS Decimal(12, 4)), 2, 0.0000, 8350.0000, CAST(N'2023-05-11T18:01:49.723' AS DateTime), CAST(N'2023-05-11T18:01:49.723' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (11, N'945208a9-6ee2-4889-a0ef-3d7f8e7ae213', 4, CAST(15.0000 AS Decimal(12, 4)), 2, 8351.0000, 33950.0000, CAST(N'2023-05-11T18:01:49.723' AS DateTime), CAST(N'2023-05-11T18:01:49.723' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (12, N'652a04a9-cf75-42ff-8e84-1869cd50e833', 4, CAST(25.0000 AS Decimal(12, 4)), 2, 33951.0000, 82250.0000, CAST(N'2023-05-11T18:01:49.723' AS DateTime), CAST(N'2023-05-11T18:01:49.723' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (13, N'477b55ab-d9e1-4b1e-a1fe-9f2f6ec9b8bb', 4, CAST(28.0000 AS Decimal(12, 4)), 2, 82251.0000, 171550.0000, CAST(N'2023-05-11T18:01:49.723' AS DateTime), CAST(N'2023-05-11T18:01:49.723' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (14, N'77b79e60-85bb-4dfe-bd11-18707e185b54', 4, CAST(33.0000 AS Decimal(12, 4)), 2, 171551.0000, 372950.0000, CAST(N'2023-05-11T18:01:49.723' AS DateTime), CAST(N'2023-05-11T18:01:49.723' AS DateTime))
GO
INSERT [lookup].[PostalCodeTaxRate] ([RowId], [RowGuid], [PostalCodeId], [Rate], [RateTypeId], [FromValue], [ToValue], [Created], [Updated]) VALUES (15, N'b612bb86-74b0-455f-8f92-79f976d516a5', 4, CAST(35.0000 AS Decimal(12, 4)), 2, 372951.0000, NULL, CAST(N'2023-05-11T18:01:49.723' AS DateTime), CAST(N'2023-05-11T18:01:49.723' AS DateTime))
GO
SET IDENTITY_INSERT [lookup].[PostalCodeTaxRate] OFF
GO
ALTER TABLE [lookup].[PostalCode] ADD  CONSTRAINT [DF_PostalCode_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
GO
ALTER TABLE [lookup].[PostalCode] ADD  CONSTRAINT [DF_PostalCode_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [lookup].[PostalCode] ADD  CONSTRAINT [DF_PostalCode_Updated]  DEFAULT (getdate()) FOR [Updated]
GO
ALTER TABLE [lookup].[PostalCodeTaxRate] ADD  CONSTRAINT [DF_PostalCodeTaxRate_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
GO
ALTER TABLE [lookup].[PostalCodeTaxRate] ADD  CONSTRAINT [DF_PostalCodeTaxRate_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [lookup].[PostalCodeTaxRate] ADD  CONSTRAINT [DF_PostalCodeTaxRate_Updated]  DEFAULT (getdate()) FOR [Updated]
GO
ALTER TABLE [master].[SubmittedTax] ADD  CONSTRAINT [DF_SubmittedTax_RowGuid]  DEFAULT (newid()) FOR [RowGuid]
GO
ALTER TABLE [master].[SubmittedTax] ADD  CONSTRAINT [DF_SubmittedTax_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [master].[SubmittedTax] ADD  CONSTRAINT [DF_SubmittedTax_Updated]  DEFAULT (getdate()) FOR [Updated]
GO
ALTER TABLE [lookup].[PostalCode]  WITH CHECK ADD  CONSTRAINT [FK_PostalCode_TaxCalculationType] FOREIGN KEY([TaxCalculationTypeId])
REFERENCES [enum].[TaxCalculationType] ([RowId])
GO
ALTER TABLE [lookup].[PostalCode] CHECK CONSTRAINT [FK_PostalCode_TaxCalculationType]
GO
ALTER TABLE [lookup].[PostalCodeTaxRate]  WITH CHECK ADD  CONSTRAINT [FK_PostalCodeTaxRate_PostalCode] FOREIGN KEY([PostalCodeId])
REFERENCES [lookup].[PostalCode] ([RowId])
GO
ALTER TABLE [lookup].[PostalCodeTaxRate] CHECK CONSTRAINT [FK_PostalCodeTaxRate_PostalCode]
GO
ALTER TABLE [lookup].[PostalCodeTaxRate]  WITH CHECK ADD  CONSTRAINT [FK_PostalCodeTaxRate_RateType] FOREIGN KEY([RateTypeId])
REFERENCES [enum].[RateType] ([RowId])
GO
ALTER TABLE [lookup].[PostalCodeTaxRate] CHECK CONSTRAINT [FK_PostalCodeTaxRate_RateType]
GO
ALTER TABLE [master].[SubmittedTax]  WITH CHECK ADD  CONSTRAINT [FK_SubmittedTax_PostalCode] FOREIGN KEY([PostalCodeId])
REFERENCES [lookup].[PostalCode] ([RowId])
GO
ALTER TABLE [master].[SubmittedTax] CHECK CONSTRAINT [FK_SubmittedTax_PostalCode]
GO
