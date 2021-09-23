use CoreBlogDb;
SET IDENTITY_INSERT [dbo].[Blog] ON
INSERT INTO [dbo].[Blog] ([BlogID], [BlogTitle], [BlogContent], [BlogThumbnailImage], [BlogImage], [BlogCreateDate], [BlogStatus], [CategoryID]) VALUES (1, N'C# ile Asekron Metotlar', NULL, NULL, NULL, N'2021-09-23 00:00:00', 1, 1)
INSERT INTO [dbo].[Blog] ([BlogID], [BlogTitle], [BlogContent], [BlogThumbnailImage], [BlogImage], [BlogCreateDate], [BlogStatus], [CategoryID]) VALUES (2, N'Python ile Veri Analizi', NULL, NULL, NULL, N'2021-09-23 00:00:00', 1, 1)
INSERT INTO [dbo].[Blog] ([BlogID], [BlogTitle], [BlogContent], [BlogThumbnailImage], [BlogImage], [BlogCreateDate], [BlogStatus], [CategoryID]) VALUES (3, N'Kimyager : Walter White', NULL, NULL, NULL, N'2021-09-23 00:00:00', 1, 4)
INSERT INTO [dbo].[Blog] ([BlogID], [BlogTitle], [BlogContent], [BlogThumbnailImage], [BlogImage], [BlogCreateDate], [BlogStatus], [CategoryID]) VALUES (4, N'Into The Night', NULL, NULL, NULL, N'2021-09-23 00:00:00', 1, 4)
SET IDENTITY_INSERT [dbo].[Blog] OFF
