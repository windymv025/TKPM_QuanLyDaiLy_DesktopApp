USE MASTER
GO
CREATE DATABASE [DBQuanLyCacDaiLy]
GO
USE [DBQuanLyCacDaiLy]
GO
CREATE TABLE [DaiLy] (
	Id int identity(1,1) NOT NULL,
	Ten nvarchar(max) NOT NULL,
	DienThoai varchar(20) NOT NULL,
	DiaChi nvarchar(max) NOT NULL,
	NgayTiepNhan Datetime NOT NULL,
	Quan nvarchar(30) NOT NULL,
	Email varchar(100) NOT NULL,
	IdLoaiDaiLy int NOT NULL,
	IsRemove bit NOT NULL,
	HinhAnh nvarchar(max),
	PRIMARY KEY (Id)
)
GO
CREATE TABLE [LoaiDaiLy] (
	Id int identity(1,1) not null,
	Ten nvarchar(30) NOT NULL,
	SoTienNoToiDa decimal NOT NULL,
	primary key(Id)
)
GO
CREATE TABLE [QuyDinh] (
	Id int identity(1,1) NOT NULL,
	TenQuyDinh nvarchar(max) NOT NULL,
	GiaTri float NOT NULL,
	KieuDuLieu nvarchar(20) NOT NULL,
	TrangThai bit NOT NULL,
	primary key(Id)
)
GO
CREATE TABLE [PhieuDaiLy] (
	Id int identity(1,1) NOT NULL,
	NgayLapPhieu Datetime NOT NULL,
	IdDaiLy int NOT NULL,
	primary key(Id)
)
GO
CREATE TABLE [PhieuThuTien] (
	IdPhieuDaiLy int NOT NULL,
	SoTienThu decimal NOT NULL,
	primary key(IdPhieuDaiLy)
)
GO
CREATE TABLE [PhieuXuatHang] (
	IdPhieuDaiLy int NOT NULL,
	TongTien decimal NOT NULL,
	primary key(IdPhieuDaiLy)
)
GO
CREATE TABLE [LoaiSanPham] (
	Id int identity(1,1) NOT NULL,
	Ten nvarchar(max) NOT NULL,
	HinhAnh nvarchar(max) NOT NULL,
	primary key(Id)
)
GO
CREATE TABLE [SanPham] (
	Id int identity(1,1) NOT NULL,
	Ten nvarchar(max) NOT NULL,
	GiaNhap decimal NOT NULL,
	GiaBan decimal not null,
	SoLuong int NOT NULL,
	IdLoaiSanPham int NOT NULL,
	IdNguonNhap int NOT NULL,
	IdDonViTinh int NOT NULL,
	HinhAnh nvarchar(max),
	MoTa nvarchar(max),
	TrangThai bit NOT NULL, --0 là đã xóa, --1 là chưa xóa
	primary key(Id)
)
GO
CREATE TABLE [NguonNhap] (
	Id int identity(1,1) NOT NULL,
	Ten nvarchar(max) NOT NULL,
	DiaChi nvarchar(max) NOT NULL,
	SoDienThoai varchar(20) NOT NULL,
	HinhAnh nvarchar(max),
	primary key(Id)
)
GO
CREATE TABLE [ChiTietPhieuXuatHang] (
	IdSanPham int NOT NULL,
	IdPhieuXuatHang int NOT NULL,
	SoLuong int NOT NULL,
	primary key(IdSanPham, IDPhieuXuatHang)
)
GO
CREATE TABLE [DonViTinh] (
	Id int identity(1,1) not null,
	Ten nvarchar(30) not null,
	primary key(Id)
)
GO
CREATE TABLE [HinhAnhSanPham] (
	Id int identity(1,1) not null,
	IdSanPham int not null,
	HinhAnh nvarchar(max),
	primary key(Id)
)
GO
CREATE TABLE [NhanVien] (
	Id int identity(1,1) not null,
	Ten nvarchar(max) not null,
	DienThoai varchar(20) not null,
	DiaChi nvarchar(max) not null,
	Email varchar(100),
	HinhAnh nvarchar(max),
	VaiTro int,			/*1 - admin, 2 - staff*/
	primary key(Id)
)
GO
CREATE TABLE [TaiKhoan] (
	Id int not null,
	TenDangNhap varchar(100) not null,
	MatKhau nvarchar(128),
	primary key(Id)
)
GO

ALTER TABLE [TaiKhoan] ADD CONSTRAINT [TaiKhoan_fk0] FOREIGN KEY ([Id]) REFERENCES [NhanVien]([Id])
GO
ALTER TABLE [DaiLy] ADD CONSTRAINT [DaiLy_fk0] FOREIGN KEY ([IdLoaiDaiLy]) REFERENCES [LoaiDaiLy]([Id])
GO

ALTER TABLE [PhieuDaiLy] ADD CONSTRAINT [PhieuDaiLy_fk0] FOREIGN KEY ([IdDaiLy]) REFERENCES [DaiLy]([Id])
GO

ALTER TABLE [PhieuThuTien] ADD CONSTRAINT [PhieuThuTien_fk0] FOREIGN KEY ([IdPhieuDaiLy]) REFERENCES [PhieuDaiLy]([Id])
GO

ALTER TABLE [PhieuXuatHang] ADD CONSTRAINT [PhieuXuatHang_fk0] FOREIGN KEY ([IdPhieuDaiLy]) REFERENCES [PhieuDaiLy]([Id])
GO

ALTER TABLE [SanPham] ADD CONSTRAINT [SanPham_fk0] FOREIGN KEY ([IdLoaiSanPham]) REFERENCES [LoaiSanPham]([Id])
GO

ALTER TABLE [SanPham] ADD CONSTRAINT [SanPham_fk1] FOREIGN KEY ([IdNguonNhap]) REFERENCES [NguonNhap]([Id])
GO

ALTER TABLE [SanPham] ADD CONSTRAINT [SanPham_fk2] FOREIGN KEY ([IdDonViTinh]) REFERENCES [DonViTinh]([Id])
GO

ALTER TABLE [ChiTietPhieuXuatHang] ADD CONSTRAINT [ChiTietPhieuXuatHang_fk0] FOREIGN KEY ([IdSanPham]) REFERENCES [SanPham]([Id])
GO

ALTER TABLE [ChiTietPhieuXuatHang] ADD CONSTRAINT [ChiTietPhieuXuatHang_fk1] FOREIGN KEY ([IdPhieuXuatHang]) REFERENCES [PhieuXuatHang]([IdPhieuDaiLy])
GO

ALTER TABLE [HinhAnhSanPham] ADD CONSTRAINT [HinhAnhSanPham_fk0] FOREIGN KEY ([IdSanPham]) REFERENCES [SanPham]([Id])
GO




/* Thêm data */
INSERT [dbo].[NhanVien] ([Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro]) VALUES (N'Phạm Văn Thật', '0352358161', N'Tiền Giang', '18120568@student.hcmus.edu.vn', N'Images/Staff/That.jpg', 1)
GO
INSERT [dbo].[NhanVien] ([Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro]) VALUES (N'Phạm Minh Vương', '0988788146', N'Bình Định', '18120655@student.hcmus.edu.vn',  N'Images/Staff/Vuong.jpg', 1)
GO
INSERT [dbo].[NhanVien] ([Ten], [DienThoai], [DiaChi], [Email], [HinhAnh], [VaiTro]) VALUES (N'Nguyễn Bá Vương Thần Vũ', '0909909009', N'Thành phố Hồ Chí Minh', 'nbvtvu@gmail.com',  N'Images/Staff/Vu.jpg', 2)
GO

INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES ('1', 'that','db69fc039dcbd2962cb4d28f5891aae1')		/*admin*/
GO
INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES ('2', 'vuong','db69fc039dcbd2962cb4d28f5891aae1')
GO
INSERT [dbo].[TaiKhoan] ([Id], [TenDangNhap], [MatKhau]) VALUES ('3', 'vu','978aae9bb6bee8fb75de3e4830a1be46')			/*staff*/
GO

INSERT [dbo].[LoaiDaiLy] ([Ten], [SoTienNoToiDa]) VALUES ('1', CAST(300000000 AS Decimal(18, 0)))
GO
INSERT [dbo].[LoaiDaiLy] ([Ten], [SoTienNoToiDa]) VALUES ('2', CAST(500000000 AS Decimal(18, 0)))
GO

INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý gia đình Trịnh Trần Phương Tuấn', N'0967794391', N'Gian hàng L5-15, TTTM Saigon Centre, 92-94 Nam Kỳ Khởi Nghĩa, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2020-01-01' AS Datetime), N'1', N'Jack97Liam@gmail.com', 1, 0, N'Images/DaiLy/1.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý gia đình Phạm Trưởng', N'0933383055', N'33/1A, Nguyễn Đình Chính, Phường 15, Quận Phú Nhuận, Thành Phố Hồ Chí Minh, Phường 15, Phú Nhuận, Thành phố Hồ Chí Minh', CAST(N'2021-01-01' AS Datetime), N'Phú Nhuận', N'phamtruong@ptmusic.info', 2, 0, N'Images/DaiLy/2.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Võ Vũ Trường Giang', N'0901202727', N'27 Trần Quốc Thảo, Phường 6, Quận 3, Thành phố Hồ Chí Minh', CAST(N'2020-10-10' AS Datetime), N'3', N'quangvuatz@gmail.com', 1, 0, N'Images/DaiLy/3.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý phụ kiện Lê Ngọc Minh Hằng', N'0909090909', N'đường Bùi Tá Hán, thuộc phường An Phú, quận 2, TP.HCM', CAST(N'2020-10-20' AS Datetime), N'2', N'minhhang@gmail.com', 2, 0, N'Images/DaiLy/4.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý đồ điện tử Lê Thành Dương', N'0909080809', N'5B – 5C Trần Nhật Duật, Phường Tân Định, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2020-12-12' AS Datetime), N'1', N'ngokienhuy@gmail.com', 1, 0, N'Images/DaiLy/5.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý gia đình Nguyễn Bá Vương Thần Vũ', N'0908090809', N'01 Đinh Tiên Hoàng, phường 1, quận 1, TPHCM', CAST(N'2020-03-30' AS Datetime), N'1', N'vu@gmail.com', 2, 0, N'Images/DaiLy/6.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý điện máy Lâm Cảnh Dương', N'0365666768', N'12 Trương Phước Phan, phường Bình Trị Đông, quận Bình Tân, TPHCM', CAST(N'2020-12-23' AS Datetime), N'Bình Tân', N'duong@gmail.com', 1, 0, N'Images/DaiLy/7.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý tư nhân Nguyễn Cô Vy', N'0909909090', N'1 đường Võ Thị Sáu, phường Võ Thị Sáu, quận 3, TPHCM', CAST(N'2019-12-31' AS Datetime), N'3', N'vy@gmail.com', 2, 0, N'Images/DaiLy/8.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý gia đình Hoàng Dược Sư', N'0343536378', N'2 Hòa Bình, phường 3, quận 11, TPHCM', CAST(N'2020-12-20' AS Datetime), N'11', N'dongta@gmail.com', 1, 0, N'Images/DaiLy/9.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý Âu Dương Phong', N'0902209902', N'1 Phạm Văn Đồng, phường 3, quận Gò Vấp, TPHCM', CAST(N'2020-12-10' AS Datetime), N'Gò Vấp', N'taydoc@gmail.com', 2, 0, N'Images/DaiLy/10.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý laptop Đoàn Trí Hưng', N'0334455667', N'135 Nam Kỳ Khởi Nghĩa, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh', CAST(N'2021-02-02' AS Datetime), N'1', N'namde@gmail.com', 1, 0, N'Images/DaiLy/11.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Hồng Thất Công', N'0990990999', N'3 Đường Trường Sơn, phường 2, quận Tân Bình, TPHCM', CAST(N'2020-02-05' AS Datetime), N'Tân Bình', N'baccai@gmail.com', 2, 0, N'Images/DaiLy/12.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Đại lý điện máy gia dụng Vương Trùng Dương', N'0999999999', N'208 Nguyễn Hữu Cảnh, Phường 22, Bình Thạnh, Thành phố Hồ Chí Minh', CAST(N'2020-12-01' AS Datetime), N'Bình Thạnh', N'trungthanthong@gmail.com', 1, 0, N'Images/DaiLy/13.png')
GO
INSERT [dbo].[DaiLy] ([Ten], [DienThoai], [DiaChi], [NgayTiepNhan], [Quan], [Email], [IdLoaiDaiLy], [IsRemove], [HinhAnh]) VALUES (N'Chu Bá Thông', N'0888888888', N'161 Xa lộ Hà Nội, Thảo Điền, Quận 2, Thành phố Hồ Chí Minh', CAST(N'2021-02-27' AS Datetime), N'2', N'chubathong@gmail.com', 2, 0, N'Images/DaiLy/14.png')
GO

INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2020-12-01' AS Datetime), 1)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2020-12-01' AS Datetime), 1)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2020-12-31' AS Datetime), 2)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-04-10' AS Datetime), 3)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-05-14' AS Datetime), 4)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-13' AS Datetime), 5)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-13' AS Datetime), 6)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-15' AS Datetime), 7)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-15' AS Datetime), 8)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-15' AS Datetime), 9)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-16' AS Datetime), 10)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-17' AS Datetime), 11)
GO
INSERT [dbo].[PhieuDaiLy] ([NgayLapPhieu], [IDDaiLy]) VALUES (CAST(N'2021-06-17' AS Datetime), 12)
GO


INSERT [dbo].[PhieuThuTien] ([IdPhieuDaiLy], [SoTienThu]) VALUES (2, CAST(29000000 AS Decimal(18, 0)))
GO

INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (1, CAST(29000000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (3, CAST(230940000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (4, CAST(15990000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (5, CAST(154900000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (6, CAST(115918000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (7, CAST(22900000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (8, CAST(11400000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (9, CAST(22490000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (10, CAST(25590000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (11, CAST(65490000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (12, CAST(43690000 AS Decimal(18, 0)))
GO
INSERT [dbo].[PhieuXuatHang] ([IdPhieuDaiLy], [TongTien]) VALUES (13, CAST(20590000 AS Decimal(18, 0)))
GO

INSERT [dbo].[LoaiSanPham] ([Ten], [HinhAnh]) VALUES (N'Dell', N'Images/Category/dell.png')
GO
INSERT [dbo].[LoaiSanPham] ([Ten], [HinhAnh]) VALUES (N'Asus', N'Images/Category/asus.png')
GO
INSERT [dbo].[LoaiSanPham] ([Ten], [HinhAnh]) VALUES (N'HP', N'Images/Category/hp.png')
GO
INSERT [dbo].[LoaiSanPham] ([Ten], [HinhAnh]) VALUES (N'Apple', N'Images/Category/apple.png')
GO
INSERT [dbo].[LoaiSanPham] ([Ten], [HinhAnh]) VALUES (N'Lenovo', N'Images/Category/lenovo.png')
GO

INSERT [dbo].[NguonNhap] ([Ten], [DiaChi], [SoDienThoai], [HinhAnh]) VALUES (N'Trương Vô Kỵ', N'Núi Võ Đang', N'0909909090', NULL)
GO
INSERT [dbo].[NguonNhap] ([Ten], [DiaChi], [SoDienThoai], [HinhAnh]) VALUES (N'ITLap', N'21M Nguyễn Văn Trỗi, P. 12, Phú Nhuận, Thành phố Hồ Chí Minh', N'0909808090', N'Images/ImportSource/itlap.png')
GO
INSERT [dbo].[NguonNhap] ([Ten], [DiaChi], [SoDienThoai], [HinhAnh]) VALUES (N'Hoàng Dung', N'Đào Hoa Đảo', N'0909708090', NULL)
GO

INSERT [dbo].[DonViTinh] ([Ten]) VALUES (N'Cái')
GO
INSERT [dbo].[DonViTinh] ([Ten]) VALUES (N'Con')
GO

INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh] , [HinhAnh], [MoTa], [TrangThai]) VALUES (N'Alienware M17-I7 9750H RTX 2060 RAM 16GB SSD 256GB+HDD 1T 17.3"" FHD Windows 10 LIKE NEW 99%', CAST(29000000 AS Decimal(18, 0)), CAST(29000000 AS Decimal(18, 0)), 20, 1, 1, 1, N'Images/Product/1.png', N'Thương Hiệu	DELL
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
Hệ điều hành Windows 10 Home',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'Dell G5 5500 (70225484) i7-10750H RTX 2070 RAM 16GB 1TB SSD 15.6'''' FHD', CAST(38490000 AS Decimal(18, 0)), CAST(38490000 AS Decimal(18, 0)), 15, 1, 2, 1, N'Images/Product/2.png', N'Thương Hiệu	DELL
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
Hệ điều hành Windows 10 Home',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'Dell Inspiron (N3I3016W)-SILVER i3-1115G4 8GB SSD 256GB 13.3 FHD', CAST(15990000 AS Decimal(18, 0)), CAST(15990000 AS Decimal(18, 0)), 30, 1, 3, 1, N'Images/Product/3.png', N'Thương Hiệu DELL
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
Hệ điều hành Windows 10 Home',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'ASUS A512FL - EJ765T I5-10210U MX250 8GB 512GB SSD 15.6" FHD', CAST(15490000 AS Decimal(18, 0)), CAST(15490000 AS Decimal(18, 0)), 30, 2, 1, 1, N'Images/Product/4.png', N'Thương Hiệu ASUS
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
Hệ điều hành WINDOWS 10 Home',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'ASUS ProArt StudioBook Pro 15 ( W500G5T XS77 ) I7 9750H Quadro RTX 5000 RAM 48GB 2TB SSD', CAST(115918000 AS Decimal(18, 0)), CAST(115918000 AS Decimal(18, 0)), 10, 2, 2, 1, N'Images/Product/5.png', N'Thương Hiệu ASUS
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
Hệ điều hành WINDOWS 10 Pro',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'ASUS ROG Strix G15 G512-IAL013T i5-10300H 1650Ti 4GB 8GB 512GB 15.6" FHD 144Hz', CAST(22990000 AS Decimal(18, 0)), CAST(22990000 AS Decimal(18, 0)), 20, 2, 3, 1, N'Images/Product/6.png', N'Thương Hiệu ASUS
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
Hệ điều hành WINDOWS 10 Home',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'HP 14 348 G7 ( 9PG86PA ) I3-10110U 4GB 256GB SSD 14" FHD', CAST(11400000 AS Decimal(18, 0)), CAST(11400000 AS Decimal(18, 0)), 30, 3, 1, 1, N'Images/Product/7.png', N'Thương Hiệu HP
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
Hệ điều hành Windows 10',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'HP Envy 13-ba1028TU ( 2K0B2PA ) i5-1135G7 8GB 512GB SSD 13.3" FHD', CAST(22490000 AS Decimal(18, 0)), CAST(22490000 AS Decimal(18, 0)), 20, 3, 2, 1, N'Images/Product/8.png', N'Thương Hiệu HP
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
Hệ điều hành Windows 10',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'Macbook Air 13" 2020 Gold MGND3 - Apple M1 256GB SSD', CAST(25590000 AS Decimal(18, 0)), CAST(25590000 AS Decimal(18, 0)), 20, 4, 3, 1, N'Images/Product/9.png', N'Thương Hiệu APPLE
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
Hệ điều hành MacOS',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'MacBook Pro 16" 2019 Gray 1TB MVVK2', CAST(65490000 AS Decimal(18, 0)), CAST(65490000 AS Decimal(18, 0)), 15, 4, 1, 1, N'Images/Product/10.png', N'Thương Hiệu APPLE
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
Hệ điều hành MacOS',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'Lenovo Legion 5 Pro 16ACH6H (82JQ001VVN) R7-5800H 16GB 512GB VGA RTX 3060 6GB 16'' WQXGA 165Hz', CAST(43690000 AS Decimal(18, 0)), CAST(43690000 AS Decimal(18, 0)), 20, 5, 2, 1, N'Images/Product/11.png', N'Thương Hiệu Lenovo
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
Hệ điều hành Windows 10',
1)
GO
INSERT [dbo].[SanPham] ([Ten], [GiaNhap], [GiaBan], [SoLuong], [IDLoaiSanPham], [IDNguonNhap], [IdDonViTinh], [HinhAnh], [MoTa], [TrangThai]) VALUES (N'Laptop Lenovo ThinkPad E14 Gen 2 20TA002MVA (i7-1165G7 RAM 8GB 512GB SSD Intel Iris 14"FHD', CAST(20590000 AS Decimal(18, 0)), CAST(20590000 AS Decimal(18, 0)), 20, 5, 3, 1, N'Images/Product/12.png', N'Thương Hiệu Lenovo
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
Hệ điều hành Free Dos',
1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (1, 1, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (2, 3, 6)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (3, 4, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (4, 5, 10)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (5, 6, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (6, 7, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (7, 8, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (8, 9, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (9, 10, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (10, 11, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (11, 12, 1)
GO
INSERT [dbo].[ChiTietPhieuXuatHang] ([IDSanPham], [IDPhieuXuatHang], [SoLuong]) VALUES (12, 13, 1)
GO

INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.1.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.2.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.3.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.4.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.5.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (1, N'Images/Product/1.6.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (2, N'Images/Product/2.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (2, N'Images/Product/2.1.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (2, N'Images/Product/2.2.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (2, N'Images/Product/2.3.png')
GO
INSERT [dbo].[HinhAnhSanPham] ([IDSanPham], [HinhAnh]) VALUES (2, N'Images/Product/2.4.png')
GO

INSERT [dbo].[QuyDinh] ([TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (N'SO_LUONG_LOAI_DAI_LY', 2, N'integer', 1)
INSERT [dbo].[QuyDinh] ([TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (N'SO_LUONG_DAI_LY_TOI_DA_TRONG_MOT_QUAN', 4, N'integer', 1)
INSERT [dbo].[QuyDinh] ([TenQuyDinh], [GiaTri], [KieuDuLieu], [TrangThai]) VALUES (N'LAI_XUAT', 0.23, N'float', 1)
GO


/* Báo cáo doanh số

select DaiLy.Id, DaiLy.Ten, Sum(PhieuXuatHang.TongTien) as 'Sum'
from DaiLy, PhieuDaiLy, PhieuXuatHang
where DaiLy.Id = PhieuDaiLy.IdDaiLy and PhieuDaiLy.Id = PhieuXuatHang.IdPhieuDaiLy
group by DaiLy.Id, DaiLy.Ten
*/