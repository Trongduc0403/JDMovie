USE [dbDACN]
GO
ALTER TABLE [dbo].[TheLoaiPhimLe] DROP CONSTRAINT [FK_TheLoaiPhimLe_TheLoai]
GO
ALTER TABLE [dbo].[TheLoaiPhimLe] DROP CONSTRAINT [FK_TheLoaiPhimLe_DSPhimLe]
GO
ALTER TABLE [dbo].[TheLoaiPhimBo] DROP CONSTRAINT [FK_TheLoaiPhimBo_TheLoai]
GO
ALTER TABLE [dbo].[TheLoaiPhimBo] DROP CONSTRAINT [FK_TheLoaiPhimBo_DSPhimBo]
GO
ALTER TABLE [dbo].[LichSu] DROP CONSTRAINT [FK__LichSu__Idtk__4222D4EF]
GO
ALTER TABLE [dbo].[LichSu] DROP CONSTRAINT [FK__LichSu__IDPhim__412EB0B6]
GO
ALTER TABLE [dbo].[HopPhim] DROP CONSTRAINT [FK_HopPhim_TaiKhoan]
GO
ALTER TABLE [dbo].[HopPhim] DROP CONSTRAINT [FK_HopPhim_DSPhimBo]
GO
ALTER TABLE [dbo].[DSPhimLe] DROP CONSTRAINT [FK_DSPhimLe_QuocGia]
GO
ALTER TABLE [dbo].[DSPhimLe] DROP CONSTRAINT [FK_DSPhimLe_Nam]
GO
ALTER TABLE [dbo].[DSPhimBo] DROP CONSTRAINT [FK_DSPhimBo_QuocGia]
GO
ALTER TABLE [dbo].[DSPhimBo] DROP CONSTRAINT [FK_DSPhimBo_Nam]
GO
ALTER TABLE [dbo].[CTTapPhim] DROP CONSTRAINT [FK_CTTapPhim_DSPhimBo]
GO
/****** Object:  Table [dbo].[tintucphim]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tintucphim]') AND type in (N'U'))
DROP TABLE [dbo].[tintucphim]
GO
/****** Object:  Table [dbo].[TheLoaiPhimLe]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheLoaiPhimLe]') AND type in (N'U'))
DROP TABLE [dbo].[TheLoaiPhimLe]
GO
/****** Object:  Table [dbo].[TheLoaiPhimBo]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheLoaiPhimBo]') AND type in (N'U'))
DROP TABLE [dbo].[TheLoaiPhimBo]
GO
/****** Object:  Table [dbo].[TheLoai]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheLoai]') AND type in (N'U'))
DROP TABLE [dbo].[TheLoai]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiKhoan]') AND type in (N'U'))
DROP TABLE [dbo].[TaiKhoan]
GO
/****** Object:  Table [dbo].[QuocGia]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuocGia]') AND type in (N'U'))
DROP TABLE [dbo].[QuocGia]
GO
/****** Object:  Table [dbo].[Nam]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nam]') AND type in (N'U'))
DROP TABLE [dbo].[Nam]
GO
/****** Object:  Table [dbo].[LichSu]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LichSu]') AND type in (N'U'))
DROP TABLE [dbo].[LichSu]
GO
/****** Object:  Table [dbo].[HopPhim]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HopPhim]') AND type in (N'U'))
DROP TABLE [dbo].[HopPhim]
GO
/****** Object:  Table [dbo].[gioithieu]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gioithieu]') AND type in (N'U'))
DROP TABLE [dbo].[gioithieu]
GO
/****** Object:  Table [dbo].[DSPhimLe]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DSPhimLe]') AND type in (N'U'))
DROP TABLE [dbo].[DSPhimLe]
GO
/****** Object:  Table [dbo].[DSPhimBo]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DSPhimBo]') AND type in (N'U'))
DROP TABLE [dbo].[DSPhimBo]
GO
/****** Object:  Table [dbo].[CTTapPhim]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CTTapPhim]') AND type in (N'U'))
DROP TABLE [dbo].[CTTapPhim]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 28/11/2021 10:15:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banner]') AND type in (N'U'))
DROP TABLE [dbo].[Banner]
GO
USE [master]
GO
/****** Object:  Database [dbDACN]    Script Date: 28/11/2021 10:15:41 PM ******/
DROP DATABASE [dbDACN]
GO
/****** Object:  Database [dbDACN]    Script Date: 28/11/2021 10:15:41 PM ******/
CREATE DATABASE [dbDACN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbDACN', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbDACN.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbDACN_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbDACN_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbDACN] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbDACN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbDACN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbDACN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbDACN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbDACN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbDACN] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbDACN] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [dbDACN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbDACN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbDACN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbDACN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbDACN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbDACN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbDACN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbDACN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbDACN] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbDACN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbDACN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbDACN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbDACN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbDACN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbDACN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbDACN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbDACN] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbDACN] SET  MULTI_USER 
GO
ALTER DATABASE [dbDACN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbDACN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbDACN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbDACN] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [dbDACN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbDACN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [dbDACN] SET QUERY_STORE = OFF
GO
USE [dbDACN]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Img] [nvarchar](max) NULL,
	[IDPhim] [int] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTTapPhim]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTTapPhim](
	[IDPhim] [int] IDENTITY(1,1) NOT NULL,
	[TapPhim] [int] NULL,
	[ID] [int] NULL,
	[Link] [nvarchar](max) NULL,
 CONSTRAINT [PK_CTTapPhim] PRIMARY KEY CLUSTERED 
(
	[IDPhim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DSPhimBo]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DSPhimBo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenPhim] [nvarchar](max) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[NamPhatHanh] [int] NULL,
	[ThoiLuong] [nvarchar](15) NULL,
	[Img] [nvarchar](max) NULL,
	[MaQG] [int] NULL,
	[LuotXem] [int] NULL,
 CONSTRAINT [PK_DSPhim1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DSPhimLe]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DSPhimLe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenPhim] [nvarchar](max) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[NamPhatHanh] [int] NULL,
	[ThoiLuong] [nvarchar](15) NULL,
	[Img] [nvarchar](max) NULL,
	[MaQG] [int] NULL,
	[LuotXem] [int] NULL,
	[Link] [nvarchar](max) NULL,
 CONSTRAINT [PK_DSPhimLe] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gioithieu]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gioithieu](
	[idgioitin] [int] IDENTITY(1,1) NOT NULL,
	[noidung] [nvarchar](max) NULL,
	[sdtlien] [nvarchar](max) NULL,
 CONSTRAINT [PK_gioithieu] PRIMARY KEY CLUSTERED 
(
	[idgioitin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HopPhim]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopPhim](
	[Idtk] [int] NOT NULL,
	[IDPhim] [int] NOT NULL,
	[k] [nchar](10) NULL,
 CONSTRAINT [PK_HopPhim1] PRIMARY KEY CLUSTERED 
(
	[Idtk] ASC,
	[IDPhim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSu]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSu](
	[Idtk] [int] NOT NULL,
	[IDPhim] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nam]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nam](
	[MaNam] [int] IDENTITY(1,1) NOT NULL,
	[TenNam] [int] NULL,
 CONSTRAINT [PK_Nam] PRIMARY KEY CLUSTERED 
(
	[MaNam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuocGia]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuocGia](
	[MaQG] [int] IDENTITY(1,1) NOT NULL,
	[TenQG] [nvarchar](50) NULL,
 CONSTRAINT [PK_Quốc Gia] PRIMARY KEY CLUSTERED 
(
	[MaQG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[Idtk] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[HoTen] [nvarchar](60) NULL,
	[MatKhau] [varchar](50) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Quyen] [bit] NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[Idtk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheLoai]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheLoai](
	[IDTheLoai] [int] IDENTITY(1,1) NOT NULL,
	[TenTheLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_TheLoai] PRIMARY KEY CLUSTERED 
(
	[IDTheLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheLoaiPhimBo]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheLoaiPhimBo](
	[IDPhimBo] [int] NOT NULL,
	[IDTheLoai] [int] NOT NULL,
	[k] [nchar](10) NULL,
 CONSTRAINT [PK_TheLoaiPhimBo] PRIMARY KEY CLUSTERED 
(
	[IDPhimBo] ASC,
	[IDTheLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheLoaiPhimLe]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheLoaiPhimLe](
	[IDPhimLe] [int] NOT NULL,
	[IDTheLoai] [int] NOT NULL,
	[k] [nchar](10) NULL,
 CONSTRAINT [PK_TheLoaiPhimLe] PRIMARY KEY CLUSTERED 
(
	[IDPhimLe] ASC,
	[IDTheLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tintucphim]    Script Date: 28/11/2021 10:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tintucphim](
	[idtintuc] [int] IDENTITY(1,1) NOT NULL,
	[tieude] [nvarchar](max) NULL,
	[tomtat] [nvarchar](max) NULL,
	[noidung] [nvarchar](max) NULL,
	[hinhanh] [nvarchar](max) NULL,
	[ngaycapnhat] [date] NULL,
	[luotxem] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idtintuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DSPhimLe] ON 

INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (1, N'Squid Game', N'akhoon', 4, N'90 phút', N'10029213345873.jpg', 4, NULL, NULL)
INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (2, N'Squid Game', NULL, 8, N'90 phút', N'9c515eef27985bd444b5b11b69c878d1214227308.jpg', 2, NULL, NULL)
INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (3, N'Phương', N'Ối dồi ôi', 12, N'3 Tiếng', N'159208178_1418720451810990_3434911924191104558_n214418134.jpg', 5, NULL, NULL)
INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (4, N'Tris ngu', N'khong co gi de viet ca', 7, N'6 minute', N'134077060_832998253931386_703688087823580273_n212322829.jpg', 2, NULL, NULL)
INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (5, N'End game', N'akhoon', 12, N'9 tập', N'Avengers_Endgame_bia_teaser215626187.jpg', 4, NULL, NULL)
INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (6, N'qweqweqwe ', N'êtrltjerkltq', 12, N' fweklj', N'675357212024187.jpg', 2, NULL, NULL)
INSERT [dbo].[DSPhimLe] ([ID], [TenPhim], [NoiDung], [NamPhatHanh], [ThoiLuong], [Img], [MaQG], [LuotXem], [Link]) VALUES (7, N'aaaaaaaaaaaaaaaa', N'bbbbbbbbbbbbbbbbbb', 8, N' fweklj', N'525682e503c4c89a91d5211341816.jpg', 6, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DSPhimLe] OFF
GO
SET IDENTITY_INSERT [dbo].[Nam] ON 

INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (1, 2010)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (2, 2011)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (3, 2012)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (4, 2013)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (5, 2014)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (6, 2015)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (7, 2016)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (8, 2017)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (9, 2018)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (10, 2019)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (11, 2020)
INSERT [dbo].[Nam] ([MaNam], [TenNam]) VALUES (12, 2021)
SET IDENTITY_INSERT [dbo].[Nam] OFF
GO
SET IDENTITY_INSERT [dbo].[QuocGia] ON 

INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (1, N'England')
INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (2, N'Việt Nam')
INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (4, N'Mỹ')
INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (5, N'Canada')
INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (6, N'Trung Quốc')
INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (7, N'Nhật Bản')
INSERT [dbo].[QuocGia] ([MaQG], [TenQG]) VALUES (8, N'Hàn Quốc')
SET IDENTITY_INSERT [dbo].[QuocGia] OFF
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([Idtk], [Email], [HoTen], [MatKhau], [Avatar], [Quyen]) VALUES (1, N'Trongduc0403@gmail.com', N'Phan Nguyễn Trọng Đức', N'716a5f9ab9fe759b7c7baa0816fad585', NULL, 0)
INSERT [dbo].[TaiKhoan] ([Idtk], [Email], [HoTen], [MatKhau], [Avatar], [Quyen]) VALUES (3, N'Person1@gmail.com', N'Person23', N'96e79218965eb72c92a549dd5a330112', NULL, 0)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[TheLoai] ON 

INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (1, N'Hành động')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (2, N'Kinh dị')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (3, N'Hài')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (4, N'Chính kịch')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (5, N'Trẻ em')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (6, N'Giật gân')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (7, N'Lãng mạn')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (8, N'Tình cảm')
SET IDENTITY_INSERT [dbo].[TheLoai] OFF
GO
INSERT [dbo].[TheLoaiPhimLe] ([IDPhimLe], [IDTheLoai], [k]) VALUES (1, 1, NULL)
INSERT [dbo].[TheLoaiPhimLe] ([IDPhimLe], [IDTheLoai], [k]) VALUES (3, 3, NULL)
INSERT [dbo].[TheLoaiPhimLe] ([IDPhimLe], [IDTheLoai], [k]) VALUES (7, 1, NULL)
GO
ALTER TABLE [dbo].[CTTapPhim]  WITH CHECK ADD  CONSTRAINT [FK_CTTapPhim_DSPhimBo] FOREIGN KEY([ID])
REFERENCES [dbo].[DSPhimBo] ([ID])
GO
ALTER TABLE [dbo].[CTTapPhim] CHECK CONSTRAINT [FK_CTTapPhim_DSPhimBo]
GO
ALTER TABLE [dbo].[DSPhimBo]  WITH CHECK ADD  CONSTRAINT [FK_DSPhimBo_Nam] FOREIGN KEY([NamPhatHanh])
REFERENCES [dbo].[Nam] ([MaNam])
GO
ALTER TABLE [dbo].[DSPhimBo] CHECK CONSTRAINT [FK_DSPhimBo_Nam]
GO
ALTER TABLE [dbo].[DSPhimBo]  WITH CHECK ADD  CONSTRAINT [FK_DSPhimBo_QuocGia] FOREIGN KEY([MaQG])
REFERENCES [dbo].[QuocGia] ([MaQG])
GO
ALTER TABLE [dbo].[DSPhimBo] CHECK CONSTRAINT [FK_DSPhimBo_QuocGia]
GO
ALTER TABLE [dbo].[DSPhimLe]  WITH CHECK ADD  CONSTRAINT [FK_DSPhimLe_Nam] FOREIGN KEY([NamPhatHanh])
REFERENCES [dbo].[Nam] ([MaNam])
GO
ALTER TABLE [dbo].[DSPhimLe] CHECK CONSTRAINT [FK_DSPhimLe_Nam]
GO
ALTER TABLE [dbo].[DSPhimLe]  WITH CHECK ADD  CONSTRAINT [FK_DSPhimLe_QuocGia] FOREIGN KEY([MaQG])
REFERENCES [dbo].[QuocGia] ([MaQG])
GO
ALTER TABLE [dbo].[DSPhimLe] CHECK CONSTRAINT [FK_DSPhimLe_QuocGia]
GO
ALTER TABLE [dbo].[HopPhim]  WITH CHECK ADD  CONSTRAINT [FK_HopPhim_DSPhimBo] FOREIGN KEY([IDPhim])
REFERENCES [dbo].[DSPhimBo] ([ID])
GO
ALTER TABLE [dbo].[HopPhim] CHECK CONSTRAINT [FK_HopPhim_DSPhimBo]
GO
ALTER TABLE [dbo].[HopPhim]  WITH CHECK ADD  CONSTRAINT [FK_HopPhim_TaiKhoan] FOREIGN KEY([Idtk])
REFERENCES [dbo].[TaiKhoan] ([Idtk])
GO
ALTER TABLE [dbo].[HopPhim] CHECK CONSTRAINT [FK_HopPhim_TaiKhoan]
GO
ALTER TABLE [dbo].[LichSu]  WITH CHECK ADD FOREIGN KEY([IDPhim])
REFERENCES [dbo].[DSPhimBo] ([ID])
GO
ALTER TABLE [dbo].[LichSu]  WITH CHECK ADD  CONSTRAINT [FK__LichSu__Idtk__4222D4EF] FOREIGN KEY([Idtk])
REFERENCES [dbo].[TaiKhoan] ([Idtk])
GO
ALTER TABLE [dbo].[LichSu] CHECK CONSTRAINT [FK__LichSu__Idtk__4222D4EF]
GO
ALTER TABLE [dbo].[TheLoaiPhimBo]  WITH CHECK ADD  CONSTRAINT [FK_TheLoaiPhimBo_DSPhimBo] FOREIGN KEY([IDPhimBo])
REFERENCES [dbo].[DSPhimBo] ([ID])
GO
ALTER TABLE [dbo].[TheLoaiPhimBo] CHECK CONSTRAINT [FK_TheLoaiPhimBo_DSPhimBo]
GO
ALTER TABLE [dbo].[TheLoaiPhimBo]  WITH CHECK ADD  CONSTRAINT [FK_TheLoaiPhimBo_TheLoai] FOREIGN KEY([IDTheLoai])
REFERENCES [dbo].[TheLoai] ([IDTheLoai])
GO
ALTER TABLE [dbo].[TheLoaiPhimBo] CHECK CONSTRAINT [FK_TheLoaiPhimBo_TheLoai]
GO
ALTER TABLE [dbo].[TheLoaiPhimLe]  WITH CHECK ADD  CONSTRAINT [FK_TheLoaiPhimLe_DSPhimLe] FOREIGN KEY([IDPhimLe])
REFERENCES [dbo].[DSPhimLe] ([ID])
GO
ALTER TABLE [dbo].[TheLoaiPhimLe] CHECK CONSTRAINT [FK_TheLoaiPhimLe_DSPhimLe]
GO
ALTER TABLE [dbo].[TheLoaiPhimLe]  WITH CHECK ADD  CONSTRAINT [FK_TheLoaiPhimLe_TheLoai] FOREIGN KEY([IDTheLoai])
REFERENCES [dbo].[TheLoai] ([IDTheLoai])
GO
ALTER TABLE [dbo].[TheLoaiPhimLe] CHECK CONSTRAINT [FK_TheLoaiPhimLe_TheLoai]
GO
USE [master]
GO
ALTER DATABASE [dbDACN] SET  READ_WRITE 
GO
