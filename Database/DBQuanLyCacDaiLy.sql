USE MASTER
GO
CREATE DATABASE [DBQuanLyCacDaiLy]
GO
USE [DBQuanLyCacDaiLy]
GO
/****** Object:  FullTextCatalog [daily_ctlg]    Script Date: 2021-07-08 22:28:44 ******/
CREATE FULLTEXT CATALOG [daily_ctlg] WITH ACCENT_SENSITIVITY = OFF
GO
/****** Object:  FullTextCatalog [nguonnhap_ctlg]    Script Date: 2021-07-08 22:28:44 ******/
CREATE FULLTEXT CATALOG [nguonnhap_ctlg] WITH ACCENT_SENSITIVITY = OFF
GO
/****** Object:  FullTextCatalog [nhanvien_ctlg]    Script Date: 2021-07-08 22:28:44 ******/
CREATE FULLTEXT CATALOG [nhanvien_ctlg] WITH ACCENT_SENSITIVITY = OFF
GO
/****** Object:  FullTextCatalog [sanpham_ctlg]    Script Date: 2021-07-08 22:28:44 ******/
CREATE FULLTEXT CATALOG [sanpham_ctlg] WITH ACCENT_SENSITIVITY = OFF
GO
/****** Object:  Table [dbo].[ChiTietPhieuXuatHang]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuXuatHang](
	[IdSanPham] [int] NOT NULL,
	[IdPhieuXuatHang] [int] NOT NULL,
	[GiaBan] [decimal](18, 0) NOT NULL,
	[SoLuong] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSanPham] ASC,
	[IdPhieuXuatHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaiLy]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaiLy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[DienThoai] [varchar](20) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[NgayTiepNhan] [datetime] NOT NULL,
	[Quan] [nvarchar](30) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[IdLoaiDaiLy] [int] NOT NULL,
	[IsRemove] [bit] NOT NULL,
	[HinhAnh] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonViTinh]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonViTinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhAnhSanPham]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HinhAnhSanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[HinhAnh] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiDaiLy]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDaiLy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](30) NOT NULL,
	[SoTienNoToiDa] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSanPham]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[HinhAnh] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguonNhap]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguonNhap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[SoDienThoai] [varchar](20) NOT NULL,
	[HinhAnh] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[DienThoai] [varchar](20) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[Email] [varchar](100) NULL,
	[HinhAnh] [nvarchar](max) NULL,
	[VaiTro] [int] NOT NULL,
	[isRemove] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuDaiLy]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuDaiLy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDaiLy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuThuTien]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThuTien](
	[IdPhieuDaiLy] [int] NOT NULL,
	[NgayThuTien] [datetime] NOT NULL,
	[SoTienThu] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPhieuDaiLy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuXuatHang]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuXuatHang](
	[IdPhieuDaiLy] [int] NOT NULL,
	[NgayLapPhieu] [datetime] NOT NULL,
	[TongTien] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPhieuDaiLy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuyDinh]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuyDinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenQuyDinh] [nvarchar](max) NOT NULL,
	[GiaTri] [float] NOT NULL,
	[KieuDuLieu] [nvarchar](20) NOT NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[GiaNhap] [decimal](18, 0) NOT NULL,
	[GiaBan] [decimal](18, 0) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[IdLoaiSanPham] [int] NOT NULL,
	[IdNguonNhap] [int] NOT NULL,
	[IdDonViTinh] [int] NOT NULL,
	[HinhAnh] [nvarchar](max) NULL,
	[MoTa] [nvarchar](max) NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 2021-07-08 22:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[Id] [int] NOT NULL,
	[TenDangNhap] [varchar](100) NOT NULL,
	[MatKhau] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (1, 21, CAST(30000000 AS Decimal(18, 0)), 5)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (3, 6, CAST(15990000 AS Decimal(18, 0)), 5)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (4, 4, CAST(15490000 AS Decimal(18, 0)), 5)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (4, 9, CAST(15490000 AS Decimal(18, 0)), 5)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (6, 8, CAST(22990000 AS Decimal(18, 0)), 2)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (7, 4, CAST(11400000 AS Decimal(18, 0)), 4)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (7, 12, CAST(12400000 AS Decimal(18, 0)), 6)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (8, 4, CAST(22490000 AS Decimal(18, 0)), 2)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (8, 8, CAST(22490000 AS Decimal(18, 0)), 4)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (8, 10, CAST(22490000 AS Decimal(18, 0)), 3)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (8, 11, CAST(22490000 AS Decimal(18, 0)), 5)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (9, 4, CAST(900000 AS Decimal(18, 0)), 1)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (9, 21, CAST(26590000 AS Decimal(18, 0)), 5)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (11, 4, CAST(43690000 AS Decimal(18, 0)), 3)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (11, 9, CAST(43690000 AS Decimal(18, 0)), 4)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (11, 22, CAST(43690000 AS Decimal(18, 0)), 10)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (12, 5, CAST(20590000 AS Decimal(18, 0)), 7)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (12, 10, CAST(20590000 AS Decimal(18, 0)), 2)
INSERT [dbo].[ChiTietPhieuXuatHang] ([IdSanPham], [IdPhieuXuatHang], [GiaBan], [SoLuong]) VALUES (12, 22, CAST(21590000 AS Decimal(18, 0)), 2)
GO
SET IDENTITY_INSERT [dbo].[DaiLy] ON 

INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (1, N'Đại lý gia đình Trịnh Trần Phương Tuấn', N'0967794391', N'Gian hàng L5-15, TTTM Saigon Centre, 92-94 Nam Kỳ Khởi Nghĩa, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'1', N'Jack97Liam@gmail.com', 1, 0, N'Images/DaiLy/1.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (2, N'Đại lý Phạm Trưởng', N'0933383055', N'33/1A, Nguyễn Đình Chính, Phường 15, Quận Phú Nhuận, Thành Phố Hồ Chí Minh, Phường 15, Phú Nhuận, Thành phố Hồ Chí Minh', CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Phú Nhuận', N'phamtruong@ptmusic.info', 2, 0, N'Images/DaiLy/db36a04e-b930-42d7-8474-6e13884b7b62.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (3, N'Võ Vũ Trường Giang', N'0901202727', N'27 Trần Quốc Thảo, Phường 6, Quận 3, Thành phố Hồ Chí Minh', CAST(N'2020-10-10T00:00:00.000' AS DateTime), N'3', N'quangvuatz@gmail.com', 1, 1, N'Images/DaiLy/3.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (4, N'Đại lý phụ kiện Lê Ngọc Minh Hằng', N'0909090909', N'đường Bùi Tá Hán, thuộc phường An Phú, quận 2, TP.HCM', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'2', N'minhhang@gmail.com', 2, 0, N'Images/DaiLy/4.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (5, N'Đại lý đồ điện tử Lê Thành Dương', N'0909080809', N'5B – 5C Trần Nhật Duật, Phường Tân Định, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'1', N'ngokienhuy@gmail.com', 1, 0, N'Images/DaiLy/5.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (6, N'Đại lý gia đình Nguyễn Bá Vương Thần Vũ', N'0908090809', N'01 Đinh Tiên Hoàng, phường 1, quận 1, TPHCM', CAST(N'2020-03-30T00:00:00.000' AS DateTime), N'1', N'vu@gmail.com', 2, 0, N'Images/DaiLy/6.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (7, N'Đại lý điện máy Lâm Cảnh Dương', N'0365666768', N'12 Trương Phước Phan, phường Bình Trị Đông, quận Bình Tân, TPHCM', CAST(N'2020-12-23T00:00:00.000' AS DateTime), N'Bình Tân', N'duong@gmail.com', 1, 0, N'Images/DaiLy/7.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (8, N'Đại lý tư nhân Nguyễn Cô Vy', N'0909909090', N'1 đường Võ Thị Sáu, phường Võ Thị Sáu, quận 3, TPHCM', CAST(N'2019-12-31T00:00:00.000' AS DateTime), N'3', N'vy@gmail.com', 2, 0, N'Images/DaiLy/8.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (9, N'Đại lý gia đình Hoàng Dược Sư', N'0343536378', N'2 Hòa Bình, phường 3, quận 11, TPHCM', CAST(N'2020-12-20T00:00:00.000' AS DateTime), N'11', N'dongta@gmail.com', 1, 0, N'Images/DaiLy/9.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (10, N'Đại lý Âu Dương Phong', N'0902209902', N'1 Phạm Văn Đồng, phường 3, quận Gò Vấp, TPHCM', CAST(N'2020-12-10T00:00:00.000' AS DateTime), N'Gò Vấp', N'taydoc@gmail.com', 2, 0, N'Images/DaiLy/10.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (11, N'Đại lý laptop Đoàn Trí Hưng 2', N'0334455667', N'135 Nam Kỳ Khởi Nghĩa, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2021-02-02T00:00:00.000' AS DateTime), N'2', N'namdesdffsd@gmail.com', 2, 0, N'Images/DaiLy/64b90819-d711-441f-b2cf-db8ea42ceece.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (12, N'Hồng Thất Công', N'0990990999', N'3 Đường Trường Sơn, phường 2, quận Tân Bình, TPHCM', CAST(N'2020-02-05T00:00:00.000' AS DateTime), N'Tân Bình', N'baccai@gmail.com', 2, 0, N'Images/DaiLy/12.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (13, N'Đại lý điện máy gia dụng Vương Trùng Dương', N'0999999999', N'208 Nguyễn Hữu Cảnh, Phường 22, Bình Thạnh, Thành phố Hồ Chí Minh', CAST(N'2020-12-01T00:00:00.000' AS DateTime), N'Bình Thạnh', N'trungthanthong@gmail.com', 1, 0, N'Images/DaiLy/13.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (14, N'Chu Bá Thông', N'0888888888', N'161 Xa lộ Hà Nội, Thảo Điền, Quận 2, Thành phố Hồ Chí Minh', CAST(N'2021-02-27T00:00:00.000' AS DateTime), N'2', N'chubathong@gmail.com', 2, 0, N'Images/DaiLy/14.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (15, N'sahdjksasn', N'23784292947389', N'skjdhklajsao;ljadjk', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'ksdfjk;dsfjksdfjk', N'sdfjkhdsfslkjsdk@jhsddj.hxdfdh', 1, 1, N'Images/Daily/927f1859-e15d-4604-afc3-05347262ab25.png')
INSERT [dbo].[DaiLy] ([Id], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (16, N'ấdsadsa', N'35344', N'sfđsfdsds', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'1', N'xcvvvdsdfs@jhsaj.sduh', 2, 0, N'Images/Daily/388835e3-914e-4c35-93c1-33bdc81ceb60.jpg')
SET IDENTITY_INSERT [dbo].[DaiLy] OFF
GO
SET IDENTITY_INSERT [dbo].[DonViTinh] ON 

INSERT [dbo].[DonViTinh] ([Id], [Ten]) VALUES (1, N'Cái')
INSERT [dbo].[DonViTinh] ([Id], [Ten]) VALUES (2, N'Con')
INSERT [dbo].[DonViTinh] ([Id], [Ten]) VALUES (3, N'Củ')
SET IDENTITY_INSERT [dbo].[DonViTinh] OFF
GO
SET IDENTITY_INSERT [dbo].[HinhAnhSanPham] ON 

INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (1, 1, N'Images/Product/1.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (2, 1, N'Images/Product/1.1.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (3, 1, N'Images/Product/1.2.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (4, 1, N'Images/Product/1.3.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (5, 1, N'Images/Product/1.4.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (6, 1, N'Images/Product/1.5.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (7, 1, N'Images/Product/1.6.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (8, 2, N'Images/Product/2.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (9, 2, N'Images/Product/2.1.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (10, 2, N'Images/Product/2.2.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (11, 2, N'Images/Product/2.3.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (12, 2, N'Images/Product/2.4.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (13, 13, N'W:\Images_Coding\Icon_Images\left-arrow.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (15, 13, N'W:\Images_Coding\Icon_Images\search.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (17, 13, N'W:\Images_Coding\Icon_Images\left-arrow.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (18, 13, N'Images/Product/9be0be14-6e60-44e4-ab33-f10168b81d75.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (19, 14, N'W:\Images_Coding\Icon_Images\left-arrow.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (20, 14, N'W:\Images_Coding\Icon_Images\right-arrow-angle.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (21, 14, N'W:\Images_Coding\Icon_Images\search.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (22, 14, N'W:\Images_Coding\Icon_Images\search-cricel.png')
INSERT [dbo].[HinhAnhSanPham] ([Id], [IdSanPham], [HinhAnh]) VALUES (23, 14, N'W:\Images_Coding\Icon_Images\left-arrow.png')
SET IDENTITY_INSERT [dbo].[HinhAnhSanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiDaiLy] ON 

INSERT [dbo].[LoaiDaiLy] ([Id], [Ten], [SoTienNoToiDa]) VALUES (1, N'1', CAST(300000000 AS Decimal(18, 0)))
INSERT [dbo].[LoaiDaiLy] ([Id], [Ten], [SoTienNoToiDa]) VALUES (2, N'2', CAST(500000000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[LoaiDaiLy] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiSanPham] ON 

INSERT [dbo].[LoaiSanPham] ([Id], [Ten], [HinhAnh]) VALUES (1, N'Dell', N'Images/Category/dell.png')
INSERT [dbo].[LoaiSanPham] ([Id], [Ten], [HinhAnh]) VALUES (2, N'Asus', N'Images/Category/asus.png')
INSERT [dbo].[LoaiSanPham] ([Id], [Ten], [HinhAnh]) VALUES (3, N'HP', N'Images/Category/hp.png')
INSERT [dbo].[LoaiSanPham] ([Id], [Ten], [HinhAnh]) VALUES (4, N'Apple', N'Images/Category/apple.png')
INSERT [dbo].[LoaiSanPham] ([Id], [Ten], [HinhAnh]) VALUES (5, N'Lenovo', N'Images/Category/lenovo.png')
INSERT [dbo].[LoaiSanPham] ([Id], [Ten], [HinhAnh]) VALUES (7, N'Củ chuối', N'Images/Product/8632f40b-08a6-4fdf-a89d-9d7c140bd98f.png')
SET IDENTITY_INSERT [dbo].[LoaiSanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[NguonNhap] ON 

INSERT [dbo].[NguonNhap] ([Id], [Ten], [DiaChi], [SoDienThoai], [HinhAnh]) VALUES (1, N'Trương Vô Kỵ', N'Núi Võ Đang', N'0909909090', NULL)
INSERT [dbo].[NguonNhap] ([Id], [Ten], [DiaChi], [SoDienThoai], [HinhAnh]) VALUES (2, N'ITLap', N'21M Nguyễn Văn Trỗi, P. 12, Phú Nhuận, Thành phố Hồ Chí Minh', N'0909808090', N'Images/ImportSource/itlap.png')
INSERT [dbo].[NguonNhap] ([Id], [Ten], [DiaChi], [SoDienThoai], [HinhAnh]) VALUES (3, N'Hoàng Dung', N'Đào Hoa Đảo', N'0909708090', NULL)
SET IDENTITY_INSERT [dbo].[NguonNhap] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([Id], [Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro], [isRemove]) VALUES (1, N'Phạm Văn Thật', N'0352358161', N'Tiền Giang', N'18120568@student.hcmus.edu.vn', N'Images/Staff/That.jpg', 1, 0)
INSERT [dbo].[NhanVien] ([Id], [Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro], [isRemove]) VALUES (2, N'Phạm Minh Vương', N'0988788146', N'Bình Định', N'18120655@student.hcmus.edu.vn', N'Images/Staff/5e37313d-a625-4b69-a21d-ad9f71d18909.jpg', 1, 0)
INSERT [dbo].[NhanVien] ([Id], [Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro], [isRemove]) VALUES (3, N'Nguyễn Bá Vương Thần Vũ', N'0909909009', N'Thành phố Hồ Chí Minh', N'nbvtvu@gmail.com', N'Images/Staff/Vu.jpg', 2, 0)
INSERT [dbo].[NhanVien] ([Id], [Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro], [isRemove]) VALUES (4, N'Trần Dần', N'0900553884', N'Nhà xóm chùa, huyện Nhảy cóc', N'uong@emal.chdjh', NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
GO
SET IDENTITY_INSERT [dbo].[PhieuDaiLy] ON 

INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (4, 3)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (5, 4)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (6, 5)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (8, 7)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (9, 8)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (10, 9)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (11, 10)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (12, 11)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (14, 1)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (15, 2)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (16, 4)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (18, 4)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (21, 11)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (22, 2)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (23, 2)
INSERT [dbo].[PhieuDaiLy] ([Id], [IdDaiLy]) VALUES (24, 2)
SET IDENTITY_INSERT [dbo].[PhieuDaiLy] OFF
GO
INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [NgayThuTien], [SoTienThu]) VALUES (14, CAST(N'2021-06-10T00:00:00.000' AS DateTime), CAST(29000000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [NgayThuTien], [SoTienThu]) VALUES (15, CAST(N'2021-07-05T00:00:00.000' AS DateTime), CAST(200940000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [NgayThuTien], [SoTienThu]) VALUES (16, CAST(N'2021-07-09T00:00:00.000' AS DateTime), CAST(104900000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [NgayThuTien], [SoTienThu]) VALUES (18, CAST(N'2021-07-21T00:00:00.000' AS DateTime), CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [NgayThuTien], [SoTienThu]) VALUES (23, CAST(N'2021-07-07T00:00:00.000' AS DateTime), CAST(30000000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [NgayThuTien], [SoTienThu]) VALUES (24, CAST(N'2021-07-05T00:00:00.000' AS DateTime), CAST(200940000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (4, CAST(N'2021-04-10T00:00:00.000' AS DateTime), CAST(300000000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (5, CAST(N'2021-05-14T00:00:00.000' AS DateTime), CAST(144130000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (6, CAST(N'2021-06-13T00:00:00.000' AS DateTime), CAST(79950000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (8, CAST(N'2021-06-15T00:00:00.000' AS DateTime), CAST(135940000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (9, CAST(N'2021-06-15T00:00:00.000' AS DateTime), CAST(252210000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (10, CAST(N'2021-06-15T00:00:00.000' AS DateTime), CAST(108650000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (11, CAST(N'2021-06-16T00:00:00.000' AS DateTime), CAST(112450000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (12, CAST(N'2021-06-17T00:00:00.000' AS DateTime), CAST(74400000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (21, CAST(N'2021-07-07T00:00:00.000' AS DateTime), CAST(282950000 AS Decimal(18, 0)))
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [NgayLapPhieu], [TongTien]) VALUES (22, CAST(N'2021-07-07T00:00:00.000' AS DateTime), CAST(480080000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[QuyDinh] ON 

INSERT [dbo].[QuyDinh] ([Id], [TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (1, N'SO_LUONG_LOAI_DAI_LY', 2, N'integer', 1)
INSERT [dbo].[QuyDinh] ([Id], [TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (2, N'SO_LUONG_DAI_LY_TOI_DA_TRONG_MOT_QUAN', 4, N'integer', 1)
SET IDENTITY_INSERT [dbo].[QuyDinh] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (1, N'Alienware M17-I7 9750H RTX 2060 RAM 16GB SSD 256GB+HDD 1T 17.3"" FHD Windows 10 LIKE NEW 99%', CAST(29000000 AS Decimal(18, 0)), CAST(29000000 AS Decimal(18, 0)), 5, 1, 1, 1, N'Images/Product/1.png', N'Thương Hiệu	DELL
Model Alienware M17
CPU Intel® Core™ i7-9750H (6-Core 12 Threads 12MB Cache up to 4.50GHz)
RAM 16GB DDR4
Ổ cứng 256GB SSD + 1000GB HDD
CD/DVD None
Card VGA GeForce RTX 2060 6GB GDDR6
Màn hình 17.3"  FHD (1920x1080) 
Kết nối Wifi 802.11 AC + Bluetooth® 4.1
Tích hợp USB 3.1 Gen 1 port with PowerShare,USB 3.1 Gen 1 port, Thunderbolt 3 (USB Type-C) port,HDMI 2.0 port; microphone/headphone port (configurable),headset port,Mini DisplayPort 1.3,external graphics port
Bàn phím Backlit Keyboard 
Trọng lượng 2.6kg
Pin 99 Whr, 4-cell Battery (Integrated)
Hệ điều hành Windows 10 Home', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (2, N'Dell G5 5500 (70225484) i7-10750H RTX 2070 RAM 16GB 1TB SSD 15.6'''' FHD', CAST(38490000 AS Decimal(18, 0)), CAST(38490000 AS Decimal(18, 0)), 0, 1, 2, 1, N'Images/Product/2.png', N'Thương Hiệu	DELL
Model G5 5500
CPU Intel® Core™ i7-10750H (6-Core 12 Threads 12MB Cache up to 5.0GHz)
RAM 16GB DDR4-2933MHz
Ổ cứng 1TB SSD M.2 PCIe
CD/DVD None
Card VGA NVIDIA GeForce RTX 2070 8GB Max-Q + Intel UHD Graphics
Màn hình 15.6" FHD (1920 x 1080) IPS, 300Hz, Anti-Glare
Kết nối Killer™ Wi-Fi 6 AX1650 (2x2) 802.11ax Wireless
Tích hợp 1x USB 3.1 Gen 2 Type-C Thunderbolt™ 3, 1x USB 3.2 Gen 1, 2x USB 2.0, 1x HDMI 2.0, 1x Mini Displayport, 1x RJ-45, 1x Finger Print
Bàn phím US Backlit Keyboard
Trọng lượng 2.34kg
Pin 68 Whr, 4-cell Battery (Integrated)
Hệ điều hành Windows 10 Home', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (3, N'Dell Inspiron (N3I3016W)-SILVER i3-1115G4 8GB SSD 256GB 13.3 FHD', CAST(15990000 AS Decimal(18, 0)), CAST(15990000 AS Decimal(18, 0)), 20, 1, 3, 1, N'Images/Product/3.png', N'Thương Hiệu DELL
Model Inspiron 5301
CPU Intel® Core™ i3-1115G4 (2-Core 4 Threads 8MB Cache up to 4.1Hz)
RAM 8GB Onboard LPDDR4X Buss 4267MHz
Ổ cứng 256GB SSD M.2 PCIe
CD/DVD None
Card VGA Intel Iris Xe Graphics
Màn hình 13.3 inch FHD (1920 x 1080) Anti-Glare Non-Touch Narrow Border 300nits 95% sRGB WVA Display
Kết nối Wifi 802.11 AC + Bluetooth® v5.0
Tích hợp 1 x USB 3.2 Gen 2 Type-C port with DisplayPort/PowerDelivery, 2 x USB 3.2 Gen 1 Type-A ports, 1 x HDMI 1.4b, 1 x Universal Audio Jack
Bàn phím backlit keyboard
Trọng lượng 1.06kg
Pin 40 Whr, 3-cell Battery (Integrated)
Hệ điều hành Windows 10 Home', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (4, N'ASUS A512FL - EJ765T I5-10210U MX250 8GB 512GB SSD 15.6" FHD', CAST(15490000 AS Decimal(18, 0)), CAST(15490000 AS Decimal(18, 0)), 15, 2, 1, 1, N'Images/Product/4.png', N'Thương Hiệu ASUS
Model VIVOBOOK
CPU Intel® Core™ i5-10210U (4-Core 8 Threads 6MB Cache up to 3.9Hz)
RAM 8GB DDR4-2666MHz
Ổ cứng 512GB SSD
CD/DVD None
Card VGA UHD Graphics 630
Màn hình 15.6" FHD (1920 x 1080) 60Hz, Anti-Glare with 45% NTSC
Kết nối Wifi 802.11 AC + Bluetooth® 4.2
Tích hợp Microphone-in/Headphone-out jack, Type C USB3.0 (USB3.1 GEN1), USB 3.0 port(s), USB 2.0 port(s), HDMI, Finger Print
Bàn phím Full-sized  keyboard
Trọng lượng 1.66 Kg
Pin 37 Whr, 2-cell Battery (Integrated)
Hệ điều hành WINDOWS 10 Home', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (5, N'ASUS ProArt StudioBook Pro 15 ( W500G5T XS77 ) I7 9750H Quadro RTX 5000 RAM 48GB 2TB SSD', CAST(115918000 AS Decimal(18, 0)), CAST(115918000 AS Decimal(18, 0)), 10, 2, 2, 1, N'Images/Product/5.png', N'Thương Hiệu ASUS
Model Pro Art
CPU Intel® Core™ i7-9750H (6-Core 12 Threads 12MB Cache up to 4.5GHz)
RAM 48GB DDR4-2400MHz
Ổ cứng 1TB + 1TB (2TB total) PCIe NVMe SSD (RAID 0) Hyper Drive
CD/DVD None
Card VGA NVIDIA Quadro RTX 5000 with 16GB GDDR6
Màn hình 15. 6'''' UHD (3840*2160) NanoEdge matte display, 100% Adobe RGB
Kết nối Wifi 802.11 AC + Bluetooth® 5
Tích hợp 1 x Microphone-in jack, 1 x Microphone-in/Headphone-out jack, 2 x Type-A USB, 3.1 (Gen 1), 1 x Type-A USB 3.1 (Gen 2), 1 x Type-C USB 3.1 (Gen 2) with display support, 1 x RJ45 LAN jack for LAN INSERT INTO, 1 x HDMI, HDMI support 2.0, 1 x AC adapter plug
Bàn phím Illuminated chiclet keyboard with new Hot Keys for greater convenience
Trọng lượng 1.9 Kg
Pin 76 Whr, 4-cell Battery (Integrated)
Hệ điều hành WINDOWS 10 Pro', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (6, N'ASUS ROG Strix G15 G512-IAL013T i5-10300H 1650Ti 4GB 8GB 512GB 15.6" FHD 144Hz', CAST(22990000 AS Decimal(18, 0)), CAST(22990000 AS Decimal(18, 0)), 15, 2, 3, 1, N'Images/Product/6.png', N'Thương Hiệu ASUS
Model ROG STRIX G15
CPU Intel Core i5-10300H 2.5GHz up to 4.5GHz 8MB
RAM 8GB DDR4 3200MHz
Ổ cứng 512GB SSD
CD/DVD None
Card VGA NVIDIA GeForce GTX 1650Ti 4GB GDDR6 + Intel UHD Graphics
Màn hình 15.6" FHD (1920 x 1080) IPS, 144Hz, Wide View, 250nits, Narrow Bezel, Non-Glare with 45% NTSC, 67% sRGB
Kết nối Wifi 802.11 AC + Bluetooth® V5.0
Tích hợp 1x USB3.2 Gen2 Type-C support DisplayPort™ 3x USB3.2 Gen 1 Type-A 1x HDMI (HDMI 2.0b support 4K HDR), HDCP SPEC 2.2 1x Audio combo jack: Mic-in and Head phone 1x LAN RJ-45 jack
Bàn phím RGB KB
Trọng lượng 2.4 Kg
Pin 48 Whr, 3-cell Battery (Integrated)
Hệ điều hành WINDOWS 10 Home', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (7, N'HP 14 348 G7 ( 9PG86PA ) I3-10110U 4GB 256GB SSD 14" FHD', CAST(11400000 AS Decimal(18, 0)), CAST(11400000 AS Decimal(18, 0)), 20, 3, 1, 1, N'Images/Product/7.png', N'Thương Hiệu HP
Model 348 G7
CPU Intel® Core™ i3-10110U(2-Core 4 Threads 4MB Cache up to 4.1Hz)
RAM 4GB DDR4-2666MHz 
Ổ cứng 256GB SSD
CD/DVD None
Card VGA Intel® HD Graphics 
Màn hình 14" FHD (1920x1080) IPS WLED-backlit, 220 nits, 45% NTSC
Kết nối Wifi 802.11 AC + Bluetooth® 5.0
Tích hợp 3 x USB 3.1 Gen 1, 1 x USB 3.1 Type-C™, 1x HDMI 1.4
Trọng lượng 1.50 kg
Pin 41 Whr
Hệ điều hành Windows 10', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (8, N'HP Envy 13-ba1028TU ( 2K0B2PA ) i5-1135G7 8GB 512GB SSD 13.3" FHD', CAST(22490000 AS Decimal(18, 0)), CAST(22490000 AS Decimal(18, 0)), 8, 3, 2, 1, N'Images/Product/8.png', N'Thương Hiệu HP
Model Envy 13
CPU Intel Core i5-1135G7(4-Core 8 Threads 8MB Cache up to 4.2Hz)
RAM 8GB DDR4 3200MHz (Onboard)
Ổ cứng 512GB PCIe® NVMe™ M.2 SSD
CD/DVD None
Card VGA Intel Iris Xe Graphics
Màn hình 13.3" FHD (1920 x 1080) IPS with 72% NTSC, BrightView Micro-Edge, 300 nits
Kết nối Wifi 802.11 AC + Bluetooth® 5.0
Tích hợp 1 x Thunderbolt 4 with USB 4 Type-C, 1x SuperSpeed USB Type-A (HP Sleep and Charge), 1x SuperSpeed USB Type-A
Bàn phím Backlit keyboard
Trọng lượng 1.31 kg
Pin 53 Whr, 3-cell
Hệ điều hành Windows 10', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (9, N'Macbook Air 13" 2020 Gold MGND3 - Apple M1 256GB SSD', CAST(25590000 AS Decimal(18, 0)), CAST(25590000 AS Decimal(18, 0)), 5, 4, 3, 1, N'Images/Product/9.png', N'Thương Hiệu APPLE
Model Macbook Air 13
CPU Apple M1 chip with 8-core CPU
RAM 8GB PDDR4X-4266MHz SDRAM
Ổ cứng 512GB SSD
CD/DVD None
Card VGA 7‑core GPU , 16-core Neural Engine
Màn hình 13.3" (2560x1600) Retina display backlit LED, IPS, 500 nits brightness
Kết nối 802.11ac WI-FI wireless networking; IEEE 802.11a/b/g/n compatible; Bluetooth 5.0 wireless technology
Tích hợp Two Thunderbolt / USB 4 ports
Bàn phím Keyboard LED
Trọng lượng 1.4 Kg
Pin 58.2 Whr
Hệ điều hành MacOS', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (10, N'MacBook Pro 16" 2019 Gray 1TB MVVK2', CAST(65490000 AS Decimal(18, 0)), CAST(65490000 AS Decimal(18, 0)), 11, 4, 1, 1, N'Images/Product/10.png', N'Thương Hiệu APPLE
Model Macbook Pro 16
CPU 2.3GHz 8-core 9th-generation Intel Core i9 processor
Turbo Boost up to 4.8GHz
RAM 16GB DDR4-2400MHz
Ổ cứng 1T SSD
CD/DVD None
Card VGA AMD Radeon Pro 5500M with 4GB
Màn hình 16" (3072 x 1920) Retina display with IPS technology, True Tone technology
Kết nối 802.11ac WI-FI wireless networking; IEEE 802.11a/b/g/n compatible; Bluetooth 5.0 wireless technology
Tích hợp USB-C (4 cổng) Thunderbolt 3
Bàn phím keyboard LED
Trọng lượng 2.0 Kg
Pin Built‑in 100‑watt‑hour lithium‑polymer battery
Hệ điều hành MacOS', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (11, N'Lenovo Legion 5 Pro', CAST(43690000 AS Decimal(18, 0)), CAST(43690000 AS Decimal(18, 0)), 10, 5, 2, 1, N'W:\University\Subject\ThietKePhanMem\Project\QuanLyDaiLy\TKPM_QuanLyDaiLy_DesktopApp\Source\QuanLyDaiLyMVVM\QuanLyDaiLyMVVM\bin\Debug\Images\Product\11.png', N'Thương Hiệu Lenovo
Model LEGION 5
CPU AMD Ryzen 7 5800H 3.2GHz up to 4.4GHz 16MB
RAM 16GB (8GBx2) DDR4 3200MHz (2x SO-DIMM socket, up to 32GB SDRAM)
Ổ cứng 512GB SSD M.2 2280 PCIe 3.0x4 NVMe (Còn trống 1 khe SSD M.2 PCIE)
CD/DVD None
Card VGA NVIDIA GeForce RTX 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W
Màn hình 16" WQXGA (2560 x 1600) IPS 500nits Anti-glare, 165Hz, 100% sRGB, Dolby Vision, HDR 400, Free-Sync, G-Sync, DC dimmer
Kết nối 802.11ax 2x2
Tích hợp 41x USB-C 3.2 Gen 2 (support data transfer, Power Delivery and DisplayPort 1.4) 1x USB-C 3.2 Gen 2 (support data transfer, Power Delivery and DisplayPort 1.4) 4x USB 3.2 Gen 1 (one Always On) 1x HDMI 2.11x Ethernet (RJ-45) 1x headphone / microphone combo jack (3.5mm) 1x power connector
Bàn phím Backlit Keyboard 
Trọng lượng 2.45 kg
Pin 4 Cell 80 WHr
Hệ điều hành Windows 10', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (12, N'Laptop Lenovo ThinkPad E14 Gen 2 20TA002MVA (i7-1165G7 RAM 8GB 512GB SSD Intel Iris 14"FHD', CAST(20590000 AS Decimal(18, 0)), CAST(20590000 AS Decimal(18, 0)), 17, 5, 3, 1, N'Images/Product/12.png', N'Thương Hiệu Lenovo
Model THINKPAD E14 Gen 2
CPU Intel® Core™ i7-1165G7 (tối đa 4.70 GHz, 12MB)
RAM 8GB DDR4 3200Mhz 
Ổ cứng 512GB SSD M.2 2242 PCIe 3.0x4 NVMe
CD/DVD None
Card VGA Integrated Intel Iris Xe Graphics
Màn hình 14.0 inch FHD (1920x1080) IPS 250nits Anti-glare
Kết nối Wifi 802.11 AC + Bluetooth® 5.1
Tích hợp 1x USB 2.0 1x USB 3.2 Gen 1 (Always On) 1x Thunderbolt 4 / USB4 40Gbps (support data transfer, Power Delivery 3.0 and DisplayPort 1.4)
Bàn phím Backlit Keyboard 
Trọng lượng 1.59kg
Pin 3 Cells 45 Whrs
Hệ điều hành Free Dos', 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (13, N'Gì cũng được jhsdasjh', CAST(18000000 AS Decimal(18, 0)), CAST(20000000 AS Decimal(18, 0)), 20, 7, 2, 3, N'Images/Product/cfe3235c-e1e2-464f-b178-cd97e3699c6b.png', N'klsdfmkldsdlmsdldsdlkk 
jhsadb
sajkdjkszjkscfdsdjksdbjksdbjksdvbjk/lsdvbjkjk', 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (14, N'xcdcdxcxcxv', CAST(21321221 AS Decimal(18, 0)), CAST(20000000 AS Decimal(18, 0)), 20, 7, 2, 3, N'Images/Product/c54a369a-f166-4f0f-a4df-7b6526d0d542.png', N'sajkdjkszjkscfdsdjksdbjksdbjksdvbjk/lsdvbjkjk', 1)
INSERT [dbo].[SanPham] ([Id], [Ten], [GiaNhap], [GiaBan], [SoLuong], [IdLoaiSanPham], [IdNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (15, N'ewfđ', CAST(23433 AS Decimal(18, 0)), CAST(3232323 AS Decimal(18, 0)), 2, 5, 3, 2, N'Images/Product/98d28848-a57a-4cb2-b4a2-f7ed02e3b830.png', NULL, 1)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES (1, N'that', N'db69fc039dcbd2962cb4d28f5891aae1')
INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES (2, N'vuong', N'db69fc039dcbd2962cb4d28f5891aae1')
INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES (3, N'vu', N'0b8b946432f1ac91f0b07bd5f8df6587')
INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES (4, N'trandan', N'ccf5d885346fc01f0ca95698142cd103')
GO
/****** Object:  FullTextIndex     Script Date: 2021-07-08 22:28:45 ******/
CREATE FULLTEXT INDEX ON [dbo].[DaiLy](
[DiaChi] LANGUAGE 'Vietnamese', 
[DienThoai] LANGUAGE 'Vietnamese', 
[Email] LANGUAGE 'Vietnamese', 
[Quan] LANGUAGE 'Vietnamese', 
[Ten] LANGUAGE 'Vietnamese')
KEY INDEX [PK__DaiLy__3214EC07940C403A]ON ([daily_ctlg], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO
/****** Object:  FullTextIndex     Script Date: 2021-07-08 22:28:45 ******/
CREATE FULLTEXT INDEX ON [dbo].[NguonNhap](
[DiaChi] LANGUAGE 'Vietnamese', 
[SoDienThoai] LANGUAGE 'Vietnamese', 
[Ten] LANGUAGE 'Vietnamese')
KEY INDEX [PK__NguonNha__3214EC07940C403A]ON ([nguonnhap_ctlg], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO
/****** Object:  FullTextIndex     Script Date: 2021-07-08 22:28:45 ******/
CREATE FULLTEXT INDEX ON [dbo].[NhanVien](
[Ten] LANGUAGE 'Vietnamese')
KEY INDEX [PK__NhanVien__3214EC07940C403A]ON ([nhanvien_ctlg], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO
/****** Object:  FullTextIndex     Script Date: 2021-07-08 22:28:45 ******/
CREATE FULLTEXT INDEX ON [dbo].[SanPham](
[MoTa] LANGUAGE 'Vietnamese', 
[Ten] LANGUAGE 'Vietnamese')
KEY INDEX [PK__SanPham__3214EC079CC1B5B8]ON ([sanpham_ctlg], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO
ALTER TABLE [dbo].[NhanVien] ADD  DEFAULT ((0)) FOR [isRemove]
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang]  WITH CHECK ADD  CONSTRAINT [ChiTietPhieuXuatHang_fk0] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang] CHECK CONSTRAINT [ChiTietPhieuXuatHang_fk0]
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang]  WITH CHECK ADD  CONSTRAINT [ChiTietPhieuXuatHang_fk1] FOREIGN KEY([IdPhieuXuatHang])
REFERENCES [dbo].[PhieuXuatHang] ([IdPhieuDaiLy])
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang] CHECK CONSTRAINT [ChiTietPhieuXuatHang_fk1]
GO
ALTER TABLE [dbo].[DaiLy]  WITH CHECK ADD  CONSTRAINT [DaiLy_fk0] FOREIGN KEY([IdLoaiDaiLy])
REFERENCES [dbo].[LoaiDaiLy] ([Id])
GO
ALTER TABLE [dbo].[DaiLy] CHECK CONSTRAINT [DaiLy_fk0]
GO
ALTER TABLE [dbo].[HinhAnhSanPham]  WITH CHECK ADD  CONSTRAINT [HinhAnhSanPham_fk0] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HinhAnhSanPham] CHECK CONSTRAINT [HinhAnhSanPham_fk0]
GO
ALTER TABLE [dbo].[PhieuDaiLy]  WITH CHECK ADD  CONSTRAINT [PhieuDaiLy_fk0] FOREIGN KEY([IdDaiLy])
REFERENCES [dbo].[DaiLy] ([Id])
GO
ALTER TABLE [dbo].[PhieuDaiLy] CHECK CONSTRAINT [PhieuDaiLy_fk0]
GO
ALTER TABLE [dbo].[PhieuThuTien]  WITH CHECK ADD  CONSTRAINT [PhieuThuTien_fk0] FOREIGN KEY([IdPhieuDaiLy])
REFERENCES [dbo].[PhieuDaiLy] ([Id])
GO
ALTER TABLE [dbo].[PhieuThuTien] CHECK CONSTRAINT [PhieuThuTien_fk0]
GO
ALTER TABLE [dbo].[PhieuXuatHang]  WITH CHECK ADD  CONSTRAINT [PhieuXuatHang_fk0] FOREIGN KEY([IdPhieuDaiLy])
REFERENCES [dbo].[PhieuDaiLy] ([Id])
GO
ALTER TABLE [dbo].[PhieuXuatHang] CHECK CONSTRAINT [PhieuXuatHang_fk0]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [SanPham_fk0] FOREIGN KEY([IdLoaiSanPham])
REFERENCES [dbo].[LoaiSanPham] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [SanPham_fk0]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [SanPham_fk1] FOREIGN KEY([IdNguonNhap])
REFERENCES [dbo].[NguonNhap] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [SanPham_fk1]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [SanPham_fk2] FOREIGN KEY([IdDonViTinh])
REFERENCES [dbo].[DonViTinh] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [SanPham_fk2]
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD  CONSTRAINT [TaiKhoan_fk0] FOREIGN KEY([Id])
REFERENCES [dbo].[NhanVien] ([Id])
GO
ALTER TABLE [dbo].[TaiKhoan] CHECK CONSTRAINT [TaiKhoan_fk0]
GO
