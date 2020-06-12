create database CarShop
go
use CarShop
go

create table KhachHang(
	MaKH char(5) primary key,
	TenKH nvarchar(50),
	SDT varchar(20),
	GioiTinh nvarchar(10),
	Tuoi int check(Tuoi > 0),
	DiaChi nvarchar(100),
	UserName varchar(50),
	Email varchar(100),
	Pass varchar(50),
	IsVip bit
)
go

create table HoaDon(
	MaHD char(5) primary key,
	NgayDat date,
	NgayShip date,
	MaKH char(5) references KhachHang(MaKH) on delete cascade
)	
go

create table LoaiSP(
	MaLoaiSP char(5) primary key,
	TenLoai nvarchar(50),
	MoTa ntext,
	URLAnh varchar(200)
)
go

create table SanPham(
	MaSP char(5) primary key,
	TenSP nvarchar(50),
	SoLuong int,
	DonGia money,
	MoTa ntext,
	GiaKm money,
	URLAnh varchar(200),
	MaLoaiSP char(5) references LoaiSP(MaLoaiSP) on delete cascade
)	
go

create table CTHoaDon(
	MaHD char(5) references HoaDon(MaHD),
	MaSP char(5) references SanPham(MaSP),
	SoLuong int,
	primary key (MaHD, MaSP)
)
go

create table NhaCC(
	MaNCC char(5) primary key,
	TenNCC nvarchar(50),
	DiaChi nvarchar(100),
	SDT varchar(20),
	Email varchar(100)
)
go

create table PhieuNhap(
	MaPN char(5) primary key,
	NgayNhap date,
	MaNCC char(5) references NhaCC(MaNCC) on delete cascade
)
go

create table CTPhieuNhap(
	MaPN char(5) references PhieuNhap(MaPN),
	MaSP char(5) references SanPham(MaSP),
	SoLuong int,
	DonGia money
	primary key (MaPN, MaSP)
)	
go

create table [Admin] (
	MaAdmin char(5) primary key,
	UserAd varchar(50),
	Email varchar(100),
	Pass varchar(50)
)
go

create table Quyen(
	MaQuyen char(5) primary key,
	TenQuyen nvarchar(50)
)
go

create table QuyenAd(
	MaAdmin char(5) references [Admin](MaAdmin),
	MaQuyen char(5) references Quyen(MaQuyen),
	GhiChu ntext
	primary key (MaAdmin, MaQuyen)
)
go

create table Tintuc(
	MaTin char(5) primary key,
	URLAnh varchar(200),
	NoiDung ntext
)
go