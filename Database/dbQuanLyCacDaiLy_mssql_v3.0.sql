USE [master]
GO
/****** Object:  Database [DBQuanLyCacDaiLy]    Script Date: 6/11/2021 8:01:50 AM ******/
CREATE DATABASE [DBQuanLyCacDaiLy]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBQuanLyCacDaiLy', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.VANTHAT\MSSQL\DATA\DBQuanLyCacDaiLy.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBQuanLyCacDaiLy_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.VANTHAT\MSSQL\DATA\DBQuanLyCacDaiLy_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBQuanLyCacDaiLy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET RECOVERY FULL 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET  MULTI_USER 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBQuanLyCacDaiLy', N'ON'
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET QUERY_STORE = OFF
GO
USE [DBQuanLyCacDaiLy]
GO
/****** Object:  Table [dbo].[BangBaoCaoThang]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BangBaoCaoThang](
	[ID] [int] NOT NULL,
	[Thang] [int] NOT NULL,
	[Nam] [int] NULL,
 CONSTRAINT [PK_BANGBAOCAOTHANG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoCongNo]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCaoCongNo](
	[ID] [int] NOT NULL,
	[NoDau] [decimal](18, 0) NOT NULL,
	[PhatSinh] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_BAOCAOCONGNO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoDoanhSo]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCaoDoanhSo](
	[ID] [int] NOT NULL,
	[SoPhieuXuat] [int] NOT NULL,
	[TongTriGia] [decimal](18, 0) NOT NULL,
	[TyLe] [float] NOT NULL,
 CONSTRAINT [PK_BAOCAODOANHSO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietBaoCao]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietBaoCao](
	[IDDaiLy] [int] NOT NULL,
	[IDBaoCao] [int] NOT NULL,
	[NgayTao] [date] NOT NULL,
 CONSTRAINT [PK_CHITIETBAOCAO] PRIMARY KEY CLUSTERED 
(
	[IDDaiLy] ASC,
	[IDBaoCao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietPhieuXuatHang]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuXuatHang](
	[IDSanPham] [int] NOT NULL,
	[IDPhieuXuatHang] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaBan] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CHITIETPHIEUXUATHANG] PRIMARY KEY CLUSTERED 
(
	[IDSanPham] ASC,
	[IDPhieuXuatHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaiLy]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaiLy](
	[ID] [int] NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
	[DienThoai] [varchar](15) NOT NULL,
	[DiaChi] [nvarchar](500) NOT NULL,
	[NgayTiepNhan] [date] NOT NULL,
	[Quan] [nvarchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[LoaiDaiLy] [int] NOT NULL,
	[IsRemove] [bit] NOT NULL,
 CONSTRAINT [PK_DAILY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiDaiLy]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDaiLy](
	[MaLoai] [int] NOT NULL,
	[SoTienNoToiDa] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_LOAIDAILY] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSanPham]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSanPham](
	[ID] [int] NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LOAISANPHAM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguonNhap]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguonNhap](
	[ID] [int] NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
	[DiaChi] [nvarchar](500) NOT NULL,
	[SoDienThoai] [varchar](10) NOT NULL,
 CONSTRAINT [PK_NGUONNHAP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuDaiLy]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuDaiLy](
	[ID] [int] NOT NULL,
	[NgayLapPhieu] [date] NOT NULL,
	[IDDaiLy] [int] NOT NULL,
 CONSTRAINT [PK_PHIEUDAILY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuThuTien]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThuTien](
	[ID] [int] NOT NULL,
	[SoTienThu] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_PHIEUTHUTIEN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuXuatHang]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuXuatHang](
	[ID] [int] NOT NULL,
	[DonViTinh] [varchar](10) NOT NULL,
	[TongTien] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_PHIEUXUATHANG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuyDinh]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuyDinh](
	[MaQuyDinh] [int] NOT NULL,
	[TenQuyDinh] [nvarchar](100) NULL,
	[GiaTri] [int] NOT NULL,
	[KieuDuLieu] [varchar](10) NOT NULL,
	[TrangThai] [bit] NOT NULL,
 CONSTRAINT [PK_QUYDINH] PRIMARY KEY CLUSTERED 
(
	[MaQuyDinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 6/11/2021 8:01:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[ID] [int] NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
	[DonGia] [decimal](18, 0) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[IDLoaiSanPham] [int] NOT NULL,
	[IDNguonNhap] [int] NOT NULL,
	[HinhAnh] [varchar](100) NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_SANPHAM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[BangBaoCaoThang] ([ID], [Thang], [Nam]) VALUES (1, 12, 2020)
GO
INSERT [dbo].[BaoCaoCongNo] ([ID], [NoDau], [PhatSinh]) VALUES (1, CAST(54590000 AS Decimal(18, 0)), CAST(1000000 AS Decimal(18, 0)))
GO
INSERT [dbo].[BaoCaoDoanhSo] ([ID], [SoPhieuXuat], [TongTriGia], [TyLe]) VALUES (1, 5, CAST(100000000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[ChiTietBaoCao] ([IDDaiLy], [IDBaoCao], [NgayTao]) VALUES (1, 1, CAST(N'2020-12-31' AS Date))
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong], [GiaBan]) VALUES (1, 2, 1, CAST(29000000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong], [GiaBan]) VALUES (12, 2, 1, CAST(25590000 AS Decimal(18, 0)))
GO
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (1, N'Trịnh Trần Phương Tuấn', N'0967794391

', N'Gian hàng L5-15, TTTM Saigon Centre, 92-94 Nam Kỳ Khởi Nghĩa, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2020-01-01' AS Date), N'1', N'Jack97Liam@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (2, N'Phạm Trưởng', N'0933383055', N'33/1A, Nguyễn Đình Chính, Phường 15, Quận Phú Nhuận, Thành Phố Hồ Chí Minh, Phường 15, Phú Nhuận, Thành phố Hồ Chí Minh', CAST(N'2021-01-01' AS Date), N'Phú Nhuận', N'phamtruong@ptmusic.info', 2, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (3, N'Võ Vũ Trường Giang', N'0901202727', N'27 Trần Quốc Thảo, Phường 6, Quận 3, Thành phố Hồ Chí Minh', CAST(N'2020-10-10' AS Date), N'3', N'quangvuatz@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (4, N'Lê Ngọc Minh Hằng', N'0909090909', N'đường Bùi Tá Hán, thuộc phường An Phú, quận 2, TP.HCM', CAST(N'2020-10-20' AS Date), N'2', N'minhhang@gmail.com', 2, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (5, N'Lê Thành Dương', N'0909080809', N'5B – 5C Trần Nhật Duật, Phường Tân Định, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2020-12-12' AS Date), N'1', N'ngokienhuy@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (6, N'Nguyễn Bá Vương Thần Vũ', N'0908090809', N'01 Đinh Tiên Hoàng, phường 1, quận 1, TPHCM', CAST(N'2020-03-30' AS Date), N'1', N'vu@gmail.com', 2, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (7, N'Lâm Cảnh Dương', N'0365666768', N'12 Trương Phước Phan, phường Bình Trị Đông, quận Bình Tân, TPHCM', CAST(N'2020-12-23' AS Date), N'Bình Tân', N'duong@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (8, N'Nguyễn Cô Vy', N'0909909090', N'1 đường Võ Thị Sáu, phường Võ Thị Sáu, quận 3, TPHCM', CAST(N'2019-12-31' AS Date), N'3', N'vy@gmail.com', 2, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (9, N'Hoàng Dược Sư', N'0343536378', N'2 Hòa Bình, phường 3, quận 11, TPHCM', CAST(N'2020-12-20' AS Date), N'11', N'dongta@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (10, N'Âu Dương Phong', N'0902209902', N'1 Phạm Văn Đồng, phường 3, quận Gò Vấp, TPHCM', CAST(N'2020-12-10' AS Date), N'Gò Vấp', N'taydoc@gmail.com', 2, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (11, N'Đoàn Trí Hưng', N'0334455667', N'135 Nam Kỳ Khởi Nghĩa, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2021-02-02' AS Date), N'1', N'namde@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (12, N'Hồng Thất Công', N'0990990999', N'3 Đường Trường Sơn, phường 2, quận Tân Bình, TPHCM', CAST(N'2020-02-05' AS Date), N'Tân Bình', N'baccai@gmail.com', 2, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (13, N'Vương Trùng Dương', N'0999999999', N'208 Nguyễn Hữu Cảnh, Phường 22, Bình Thạnh, Thành phố Hồ Chí Minh', CAST(N'2020-12-01' AS Date), N'Bình Thạnh', N'trungthanthong@gmail.com', 1, 0)
INSERT [dbo].[DaiLy] ([ID], [Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [LoaiDaiLy], [IsRemove]) VALUES (14, N'Chu Bá Thông', N'0888888888', N'161 Xa lộ Hà Nội, Thảo Điền, Quận 2, Thành phố Hồ Chí Minh', CAST(N'2021-02-27' AS Date), N'2', N'chubathong@gmail.com', 2, 0)
GO
INSERT [dbo].[LoaiDaiLy] ([MaLoai], [SoTienNoToiDa]) VALUES (1, CAST(300000000 AS Decimal(18, 0)))
INSERT [dbo].[LoaiDaiLy] ([MaLoai], [SoTienNoToiDa]) VALUES (2, CAST(500000000 AS Decimal(18, 0)))
GO
INSERT [dbo].[LoaiSanPham] ([ID], [Ten]) VALUES (1, N'Dell')
INSERT [dbo].[LoaiSanPham] ([ID], [Ten]) VALUES (2, N'Asus')
INSERT [dbo].[LoaiSanPham] ([ID], [Ten]) VALUES (3, N'HP')
INSERT [dbo].[LoaiSanPham] ([ID], [Ten]) VALUES (4, N'Apple')
INSERT [dbo].[LoaiSanPham] ([ID], [Ten]) VALUES (5, N'Lenovo')
GO
INSERT [dbo].[NguonNhap] ([ID], [Ten], [DiaChi], [SoDienThoai]) VALUES (1, N'Trương Vô Kỵ', N'Núi Võ Đang', N'0909909090')
INSERT [dbo].[NguonNhap] ([ID], [Ten], [DiaChi], [SoDienThoai]) VALUES (2, N'Quách Tĩnh', N'Đào Hoa Đảo', N'0909808090')
INSERT [dbo].[NguonNhap] ([ID], [Ten], [DiaChi], [SoDienThoai]) VALUES (3, N'Hoàng Dung', N'Đào Hoa Đảo', N'0909708090')
GO
INSERT [dbo].[PhieuDaiLy] ([ID], [NgayLapPhieu], [IDDaiLy]) VALUES (1, CAST(N'2021-01-01' AS Date), 1)
INSERT [dbo].[PhieuDaiLy] ([ID], [NgayLapPhieu], [IDDaiLy]) VALUES (2, CAST(N'2020-12-31' AS Date), 8)
GO
INSERT [dbo].[PhieuThuTien] ([ID], [SoTienThu]) VALUES (1, CAST(100000000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([ID], [DonViTinh], [TongTien]) VALUES (2, N'VND', CAST(54590000 AS Decimal(18, 0)))
GO
INSERT [dbo].[QuyDinh] ([MaQuyDinh], [TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (1, N'Số lượng loại đại lý', 2, N'integer', 1)
INSERT [dbo].[QuyDinh] ([MaQuyDinh], [TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (2, N'Số lượng đại lý tối đa trong một quận', 4, N'integer', 1)
GO
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (1, N'Alienware M17-I7 9750H RTX 2060 RAM 16GB SSD 256GB+HDD 1T 17.3"" FHD Windows 10 LIKE NEW 99%', CAST(29000000 AS Decimal(18, 0)), 20, 1, 1, N'Image/1.png', N'Thương Hiệu	DELL
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
Hệ điều hành Windows 10 Home')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (2, N'Dell G5 5500 (70225484) i7-10750H RTX 2070 RAM 16GB 1TB SSD 15.6'''' FHD', CAST(38490000 AS Decimal(18, 0)), 15, 1, 2, N'Image/2.png', N'Thương Hiệu	DELL
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
Hệ điều hành Windows 10 Home')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (3, N'Dell Inspiron (N3I3016W)-SILVER i3-1115G4 8GB SSD 256GB 13.3 FHD', CAST(15990000 AS Decimal(18, 0)), 30, 1, 3, N'Image/3.png', N'Thương Hiệu DELL
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
Hệ điều hành Windows 10 Home')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (4, N'ASUS A512FL - EJ765T I5-10210U MX250 8GB 512GB SSD 15.6" FHD', CAST(15490000 AS Decimal(18, 0)), 30, 2, 1, N'Image/4.png', N'Thương Hiệu ASUS
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
Hệ điều hành WINDOWS 10 Home')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (5, N'ASUS ProArt StudioBook Pro 15 ( W500G5T XS77 ) I7 9750H Quadro RTX 5000 RAM 48GB 2TB SSD', CAST(115918000 AS Decimal(18, 0)), 10, 2, 2, N'Image/5.png', N'Thương Hiệu ASUS
Model Pro Art
CPU Intel® Core™ i7-9750H (6-Core 12 Threads 12MB Cache up to 4.5GHz)
RAM 48GB DDR4-2400MHz
Ổ cứng 1TB + 1TB (2TB total) PCIe NVMe SSD (RAID 0) Hyper Drive
CD/DVD None
Card VGA NVIDIA Quadro RTX 5000 with 16GB GDDR6
Màn hình 15. 6'''' UHD (3840*2160) NanoEdge matte display, 100% Adobe RGB
Kết nối Wifi 802.11 AC + Bluetooth® 5
Tích hợp 1 x Microphone-in jack, 1 x Microphone-in/Headphone-out jack, 2 x Type-A USB, 3.1 (Gen 1), 1 x Type-A USB 3.1 (Gen 2), 1 x Type-C USB 3.1 (Gen 2) with display support, 1 x RJ45 LAN jack for LAN insert, 1 x HDMI, HDMI support 2.0, 1 x AC adapter plug
Bàn phím Illuminated chiclet keyboard with new Hot Keys for greater convenience
Trọng lượng 1.9 Kg
Pin 76 Whr, 4-cell Battery (Integrated)
Hệ điều hành WINDOWS 10 Pro')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (6, N'ASUS ROG Strix G15 G512-IAL013T i5-10300H 1650Ti 4GB 8GB 512GB 15.6" FHD 144Hz', CAST(22990000 AS Decimal(18, 0)), 20, 2, 3, N'Image/6.png', N'Thương Hiệu ASUS
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
Hệ điều hành WINDOWS 10 Home')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (7, N'HP 14 348 G7 ( 9PG86PA ) I3-10110U 4GB 256GB SSD 14" FHD', CAST(11400000 AS Decimal(18, 0)), 30, 3, 1, N'Image/7.png', N'Thương Hiệu HP
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
Hệ điều hành Windows 10')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (8, N'HP Envy 13-ba1028TU ( 2K0B2PA ) i5-1135G7 8GB 512GB SSD 13.3" FHD', CAST(22490000 AS Decimal(18, 0)), 20, 3, 2, N'Image/8.png', N'Thương Hiệu HP
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
Hệ điều hành Windows 10')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (9, N'Macbook Air 13" 2020 Gold MGND3 - Apple M1 256GB SSD', CAST(25590000 AS Decimal(18, 0)), 20, 4, 3, N'Image/9.png', N'Thương Hiệu APPLE
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
Hệ điều hành MacOS')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (10, N'MacBook Pro 16" 2019 Gray 1TB MVVK2', CAST(65490000 AS Decimal(18, 0)), 15, 4, 1, N'Image/10.png', N'Thương Hiệu APPLE
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
Hệ điều hành MacOS')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (11, N'Lenovo Legion 5 Pro 16ACH6H (82JQ001VVN) R7-5800H 16GB 512GB VGA RTX 3060 6GB 16'' WQXGA 165Hz', CAST(43690000 AS Decimal(18, 0)), 20, 5, 2, N'Image/11.png', N'Thương Hiệu Lenovo
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
Hệ điều hành Windows 10')
INSERT [dbo].[SanPham] ([ID], [Ten], [DonGia], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [HinhAnh], [MoTa]) VALUES (12, N'Laptop Lenovo ThinkPad E14 Gen 2 20TA002MVA (i7-1165G7 RAM 8GB 512GB SSD Intel Iris 14"FHD', CAST(20590000 AS Decimal(18, 0)), 20, 5, 3, N'Image/12.png', N'Thương Hiệu Lenovo
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
Hệ điều hành Free Dos')
GO
ALTER TABLE [dbo].[BaoCaoCongNo]  WITH CHECK ADD  CONSTRAINT [BaoCaoCongNo_fk0] FOREIGN KEY([ID])
REFERENCES [dbo].[BangBaoCaoThang] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[BaoCaoCongNo] CHECK CONSTRAINT [BaoCaoCongNo_fk0]
GO
ALTER TABLE [dbo].[BaoCaoDoanhSo]  WITH CHECK ADD  CONSTRAINT [BaoCaoDoanhSo_fk0] FOREIGN KEY([ID])
REFERENCES [dbo].[BangBaoCaoThang] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[BaoCaoDoanhSo] CHECK CONSTRAINT [BaoCaoDoanhSo_fk0]
GO
ALTER TABLE [dbo].[ChiTietBaoCao]  WITH CHECK ADD  CONSTRAINT [ChiTietBaoCao_fk0] FOREIGN KEY([IDDaiLy])
REFERENCES [dbo].[DaiLy] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ChiTietBaoCao] CHECK CONSTRAINT [ChiTietBaoCao_fk0]
GO
ALTER TABLE [dbo].[ChiTietBaoCao]  WITH CHECK ADD  CONSTRAINT [ChiTietBaoCao_fk1] FOREIGN KEY([IDBaoCao])
REFERENCES [dbo].[BangBaoCaoThang] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ChiTietBaoCao] CHECK CONSTRAINT [ChiTietBaoCao_fk1]
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang]  WITH CHECK ADD  CONSTRAINT [ChiTietPhieuXuatHang_fk0] FOREIGN KEY([IDSanPham])
REFERENCES [dbo].[SanPham] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang] CHECK CONSTRAINT [ChiTietPhieuXuatHang_fk0]
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang]  WITH CHECK ADD  CONSTRAINT [ChiTietPhieuXuatHang_fk1] FOREIGN KEY([IDPhieuXuatHang])
REFERENCES [dbo].[PhieuXuatHang] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuXuatHang] CHECK CONSTRAINT [ChiTietPhieuXuatHang_fk1]
GO
ALTER TABLE [dbo].[DaiLy]  WITH CHECK ADD  CONSTRAINT [DaiLy_fk0] FOREIGN KEY([LoaiDaiLy])
REFERENCES [dbo].[LoaiDaiLy] ([MaLoai])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[DaiLy] CHECK CONSTRAINT [DaiLy_fk0]
GO
ALTER TABLE [dbo].[PhieuDaiLy]  WITH CHECK ADD  CONSTRAINT [PhieuDaiLy_fk0] FOREIGN KEY([IDDaiLy])
REFERENCES [dbo].[DaiLy] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[PhieuDaiLy] CHECK CONSTRAINT [PhieuDaiLy_fk0]
GO
ALTER TABLE [dbo].[PhieuThuTien]  WITH CHECK ADD  CONSTRAINT [PhieuThuTien_fk0] FOREIGN KEY([ID])
REFERENCES [dbo].[PhieuDaiLy] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[PhieuThuTien] CHECK CONSTRAINT [PhieuThuTien_fk0]
GO
ALTER TABLE [dbo].[PhieuXuatHang]  WITH CHECK ADD  CONSTRAINT [PhieuXuatHang_fk0] FOREIGN KEY([ID])
REFERENCES [dbo].[PhieuDaiLy] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[PhieuXuatHang] CHECK CONSTRAINT [PhieuXuatHang_fk0]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [SanPham_fk0] FOREIGN KEY([IDLoaiSanPham])
REFERENCES [dbo].[LoaiSanPham] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [SanPham_fk0]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [SanPham_fk1] FOREIGN KEY([IDNguonNhap])
REFERENCES [dbo].[NguonNhap] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [SanPham_fk1]
GO
USE [master]
GO
ALTER DATABASE [DBQuanLyCacDaiLy] SET  READ_WRITE 
GO
