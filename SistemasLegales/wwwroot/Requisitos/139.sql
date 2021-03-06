USE [CALIDAD]
GO
/****** Object:  Database [CALIDAD]    Script Date: 1/10/18 9:54:13 ******/

ALTER DATABASE [CALIDAD] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CALIDAD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CALIDAD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CALIDAD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CALIDAD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CALIDAD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CALIDAD] SET ARITHABORT OFF 
GO
ALTER DATABASE [CALIDAD] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CALIDAD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CALIDAD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CALIDAD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CALIDAD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CALIDAD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CALIDAD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CALIDAD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CALIDAD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CALIDAD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CALIDAD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CALIDAD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CALIDAD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CALIDAD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CALIDAD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CALIDAD] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CALIDAD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CALIDAD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CALIDAD] SET  MULTI_USER 
GO
ALTER DATABASE [CALIDAD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CALIDAD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CALIDAD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CALIDAD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CALIDAD] SET DELAYED_DURABILITY = DISABLED 
GO


/****** Object:  User [DESKTOP-95R0B9N\Nestor Yasmani Valle]    Script Date: 1/10/18 9:54:13 ******/

/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/10/18 9:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Analisis]    Script Date: 1/10/18 9:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Analisis](
	[AnalisisId] [bigint] NOT NULL,
	[FechaAnalisis] [datetime] NOT NULL,
	[CodigoRegistro] [varchar](50) NOT NULL,
	[NumeroOrden] [varchar](50) NOT NULL,
	[ProductoId] [int] NOT NULL,
	[ClienteId] [bigint] NOT NULL,
	[MaquinaId] [bigint] NOT NULL,
	[Resultado] [varchar](50) NOT NULL,
	[Observaciones] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Analisis] PRIMARY KEY CLUSTERED 
(
	[AnalisisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnalisisMaterial]    Script Date: 1/10/18 9:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalisisMaterial](
	[AnalisisMateriaId] [bigint] NOT NULL,
	[AnalisisId] [bigint] NOT NULL,
	[MateriaId] [bigint] NOT NULL,
 CONSTRAINT [PK_AnalisisMateria] PRIMARY KEY CLUSTERED 
(
	[AnalisisMateriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[Name] [varchar](256) NULL,
	[LastName] [varchar](256) NULL,
	[Address] [varchar](256) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Caracteristica]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Caracteristica](
	[CaracteristicaId] [bigint] NOT NULL,
	[TipoMaterialId] [bigint] NULL,
	[DescripcionCaracteristica] [varchar](50) NOT NULL,
	[TipoCaracteristica] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Caracteristica] PRIMARY KEY CLUSTERED 
(
	[CaracteristicaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClaseProducto]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaseProducto](
	[ClaseProductoId] [int] NOT NULL,
	[ClaseDescripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TIPO_ESPECIFIACION] PRIMARY KEY CLUSTERED 
(
	[ClaseProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [bigint] NOT NULL,
	[CodigoCliente] [varchar](150) NOT NULL,
	[NombreCliente] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleAnalisis]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleAnalisis](
	[DetalleAnalisisId] [bigint] NOT NULL,
	[AnalisisId] [bigint] NOT NULL,
	[EspecificacionId] [bigint] NOT NULL,
	[Resultado] [varchar](150) NOT NULL,
	[RangoReferenciaActual] [varchar](150) NOT NULL,
	[Aprobado] [bit] NOT NULL,
 CONSTRAINT [PK_DetalleAnalisis] PRIMARY KEY CLUSTERED 
(
	[DetalleAnalisisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especificacion]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especificacion](
	[EspecificacionId] [bigint] NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[TipoEspecificacion] [varchar](50) NOT NULL,
	[ClaseEspecificacion] [varchar](50) NULL,
 CONSTRAINT [PK_ESPECIFICACION] PRIMARY KEY CLUSTERED 
(
	[EspecificacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maquina]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maquina](
	[MaquinaId] [bigint] NOT NULL,
	[NombreMaquina] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Maquina] PRIMARY KEY CLUSTERED 
(
	[MaquinaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[MaterialId] [bigint] NOT NULL,
	[TipoMaterialId] [bigint] NOT NULL,
	[StockDisponible] [decimal](18, 5) NOT NULL,
	[UnidadMedida] [varchar](50) NOT NULL,
	[ProveedorId] [bigint] NOT NULL,
	[CodigoIngreso] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MaterialCaracteristica] PRIMARY KEY CLUSTERED 
(
	[MaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialCaracteristica]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialCaracteristica](
	[MaterialCaracteristicaId] [bigint] IDENTITY(1,1) NOT NULL,
	[CaracteristicaId] [bigint] NOT NULL,
	[MaterialId] [bigint] NOT NULL,
	[ValorCaracteristica] [varchar](250) NOT NULL,
 CONSTRAINT [PK_CaracteristicaMaterial] PRIMARY KEY CLUSTERED 
(
	[MaterialCaracteristicaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MigraCaracteristicaMaterialAlan$]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MigraCaracteristicaMaterialAlan$](
	[1] [nvarchar](255) NULL,
	[2] [nvarchar](255) NULL,
	[3] [float] NULL,
	[4] [float] NULL,
	[5] [float] NULL,
	[6] [float] NULL,
	[7] [float] NULL,
	[8] [float] NULL,
	[9] [float] NULL,
	[10] [float] NULL,
	[11] [float] NULL,
	[12] [nvarchar](255) NULL,
	[13] [nvarchar](255) NULL,
	[14] [nvarchar](255) NULL,
	[15] [nvarchar](255) NULL,
	[ID] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MigraCaracteristicaMaterialZinc$]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MigraCaracteristicaMaterialZinc$](
	[16] [nvarchar](255) NULL,
	[17] [float] NULL,
	[18] [float] NULL,
	[19] [float] NULL,
	[20] [float] NULL,
	[21] [float] NULL,
	[22] [float] NULL,
	[23] [float] NULL,
	[ID] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MigraProducto$]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MigraProducto$](
	[ProductoId] [float] NULL,
	[CodigoProducto] [nvarchar](255) NULL,
	[DescripcionProducto] [nvarchar](255) NULL,
	[ObservacionProducto] [nvarchar](255) NULL,
	[DescripcionProductoFinal] [nvarchar](255) NULL,
	[CodigoProductoFinal] [nvarchar](255) NULL,
	[Grado] [nvarchar](255) NULL,
	[ClaseProductoId] [float] NULL,
	[DimensionMinima] [nvarchar](255) NULL,
	[DimensionMaxima] [nvarchar](255) NULL,
	[Nominal] [float] NULL,
	[DefUsuario1] [nvarchar](255) NULL,
	[DefUsuario2] [nvarchar](255) NULL,
	[Clase] [nvarchar](255) NULL,
	[EsNumero] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MigrarEspecifiProducto$]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MigrarEspecifiProducto$](
	[COL00001] [float] NULL,
	[COL00002] [float] NULL,
	[COL00003] [float] NULL,
	[COL00004] [float] NULL,
	[COL00005] [float] NULL,
	[COL00006] [nvarchar](255) NULL,
	[COL00007] [nvarchar](255) NULL,
	[COL00008] [float] NULL,
	[COL00009] [nvarchar](255) NULL,
	[COL00010] [float] NULL,
	[COL00011] [float] NULL,
	[COL00012] [nvarchar](255) NULL,
	[COL00013] [nvarchar](255) NULL,
	[COL00014] [nvarchar](255) NULL,
	[COL00015] [float] NULL,
	[COL00016] [nvarchar](255) NULL,
	[COL00017] [nvarchar](255) NULL,
	[COL00018] [nvarchar](255) NULL,
	[COL00019] [nvarchar](255) NULL,
	[COL00020] [nvarchar](255) NULL,
	[COL00021] [nvarchar](255) NULL,
	[COL00022] [float] NULL,
	[COL00023] [nvarchar](255) NULL,
	[COL00024] [float] NULL,
	[COL00025] [float] NULL,
	[COL00026] [nvarchar](255) NULL,
	[COL00027] [nvarchar](255) NULL,
	[COL00028] [nvarchar](255) NULL,
	[COL00029] [nvarchar](255) NULL,
	[COL00030] [nvarchar](255) NULL,
	[COL00031] [nvarchar](255) NULL,
	[COL00032] [nvarchar](255) NULL,
	[COL00033] [nvarchar](255) NULL,
	[COL00034] [nvarchar](255) NULL,
	[COL00035] [nvarchar](255) NULL,
	[COL00036] [nvarchar](255) NULL,
	[COL00037] [nvarchar](255) NULL,
	[COL00038] [nvarchar](255) NULL,
	[COL00039] [float] NULL,
	[COL00040] [float] NULL,
	[COL00041] [nvarchar](255) NULL,
	[COL00042] [nvarchar](255) NULL,
	[COL00043] [nvarchar](255) NULL,
	[COL00044] [nvarchar](255) NULL,
	[COL00045] [nvarchar](255) NULL,
	[COL00046] [nvarchar](255) NULL,
	[COL00047] [nvarchar](255) NULL,
	[COL00048] [nvarchar](255) NULL,
	[COL00049] [nvarchar](255) NULL,
	[COL00050] [nvarchar](255) NULL,
	[COL00051] [nvarchar](255) NULL,
	[COL00052] [nvarchar](255) NULL,
	[COL00053] [nvarchar](255) NULL,
	[COL00054] [nvarchar](255) NULL,
	[COL00055] [nvarchar](255) NULL,
	[COL00056] [nvarchar](255) NULL,
	[COL00057] [nvarchar](255) NULL,
	[COL00058] [float] NULL,
	[COL00059] [nvarchar](255) NULL,
	[COL00060] [nvarchar](255) NULL,
	[COL00061] [nvarchar](255) NULL,
	[COL00062] [nvarchar](255) NULL,
	[COL00063] [nvarchar](255) NULL,
	[COL00064] [nvarchar](255) NULL,
	[COL00065] [nvarchar](255) NULL,
	[COL00066] [nvarchar](255) NULL,
	[COL00067] [nvarchar](255) NULL,
	[COL00068] [nvarchar](255) NULL,
	[COL00069] [nvarchar](255) NULL,
	[COL00070] [nvarchar](255) NULL,
	[COL00071] [nvarchar](255) NULL,
	[COL00072] [nvarchar](255) NULL,
	[COL00073] [nvarchar](255) NULL,
	[COL00074] [nvarchar](255) NULL,
	[COL00075] [nvarchar](255) NULL,
	[COL00076] [nvarchar](255) NULL,
	[COL00077] [nvarchar](255) NULL,
	[COL00078] [nvarchar](255) NULL,
	[COL00079] [nvarchar](255) NULL,
	[COL00080] [nvarchar](255) NULL,
	[COL00081] [nvarchar](255) NULL,
	[COL00082] [nvarchar](255) NULL,
	[COL00083] [nvarchar](255) NULL,
	[COL00084] [nvarchar](255) NULL,
	[COL00085] [nvarchar](255) NULL,
	[COL00086] [nvarchar](255) NULL,
	[COL00087] [nvarchar](255) NULL,
	[COL00088] [nvarchar](255) NULL,
	[COL00089] [nvarchar](255) NULL,
	[COL00090] [nvarchar](255) NULL,
	[COL00091] [nvarchar](255) NULL,
	[COL00092] [nvarchar](255) NULL,
	[COL00093] [nvarchar](255) NULL,
	[COL00094] [nvarchar](255) NULL,
	[COL00095] [nvarchar](255) NULL,
	[COL00096] [nvarchar](255) NULL,
	[COL00097] [nvarchar](255) NULL,
	[COL00098] [nvarchar](255) NULL,
	[COL00099] [nvarchar](255) NULL,
	[COL00100] [nvarchar](255) NULL,
	[COL00101] [nvarchar](255) NULL,
	[COL00102] [nvarchar](255) NULL,
	[COL00103] [nvarchar](255) NULL,
	[COL00104] [nvarchar](255) NULL,
	[COL00105] [nvarchar](255) NULL,
	[COL00106] [nvarchar](255) NULL,
	[COL00107] [nvarchar](255) NULL,
	[COL00108] [nvarchar](255) NULL,
	[COL00109] [nvarchar](255) NULL,
	[COL00110] [nvarchar](255) NULL,
	[COL00111] [nvarchar](255) NULL,
	[COL00112] [nvarchar](255) NULL,
	[COL00113] [nvarchar](255) NULL,
	[COL00114] [nvarchar](255) NULL,
	[COL00115] [nvarchar](255) NULL,
	[COL00116] [nvarchar](255) NULL,
	[COL00117] [nvarchar](255) NULL,
	[COL00118] [nvarchar](255) NULL,
	[COL00119] [nvarchar](255) NULL,
	[COL00120] [nvarchar](255) NULL,
	[COL00121] [nvarchar](255) NULL,
	[COL00122] [nvarchar](255) NULL,
	[COL00123] [nvarchar](255) NULL,
	[COL00124] [nvarchar](255) NULL,
	[COL00125] [nvarchar](255) NULL,
	[COL00126] [nvarchar](255) NULL,
	[COL00127] [nvarchar](255) NULL,
	[COL00128] [nvarchar](255) NULL,
	[COL00129] [nvarchar](255) NULL,
	[COL00130] [nvarchar](255) NULL,
	[COL00131] [nvarchar](255) NULL,
	[COL00132] [nvarchar](255) NULL,
	[COL00133] [nvarchar](255) NULL,
	[COL00134] [nvarchar](255) NULL,
	[COL00135] [nvarchar](255) NULL,
	[COL00136] [nvarchar](255) NULL,
	[COL00137] [nvarchar](255) NULL,
	[COL00138] [nvarchar](255) NULL,
	[COL00139] [nvarchar](255) NULL,
	[COL00140] [nvarchar](255) NULL,
	[COL00141] [nvarchar](255) NULL,
	[COL00142] [nvarchar](255) NULL,
	[COL00143] [nvarchar](255) NULL,
	[COL00144] [nvarchar](255) NULL,
	[COL00145] [nvarchar](255) NULL,
	[COL00146] [nvarchar](255) NULL,
	[COL00147] [nvarchar](255) NULL,
	[COL00148] [float] NULL,
	[COL00149] [float] NULL,
	[COL00150] [nvarchar](255) NULL,
	[COL00151] [nvarchar](255) NULL,
	[COL00152] [float] NULL,
	[COL00153] [nvarchar](255) NULL,
	[COL00154] [nvarchar](255) NULL,
	[COL00155] [nvarchar](255) NULL,
	[COL00156] [nvarchar](255) NULL,
	[COL00157] [nvarchar](255) NULL,
	[COL00158] [nvarchar](255) NULL,
	[COL00159] [nvarchar](255) NULL,
	[COL00160] [nvarchar](255) NULL,
	[COL00161] [nvarchar](255) NULL,
	[COL00162] [nvarchar](255) NULL,
	[COL00163] [nvarchar](255) NULL,
	[COL00164] [nvarchar](255) NULL,
	[COL00165] [nvarchar](255) NULL,
	[COL00166] [nvarchar](255) NULL,
	[COL00167] [nvarchar](255) NULL,
	[COL00168] [nvarchar](255) NULL,
	[COL00169] [nvarchar](255) NULL,
	[COL00170] [nvarchar](255) NULL,
	[COL00171] [nvarchar](255) NULL,
	[COL00172] [float] NULL,
	[COL00173] [nvarchar](255) NULL,
	[COL00174] [nvarchar](255) NULL,
	[COL00175] [nvarchar](255) NULL,
	[COL00176] [nvarchar](255) NULL,
	[COL00177] [nvarchar](255) NULL,
	[COL00178] [nvarchar](255) NULL,
	[COL00179] [nvarchar](255) NULL,
	[COL00180] [nvarchar](255) NULL,
	[COL00181] [nvarchar](255) NULL,
	[COL00182] [nvarchar](255) NULL,
	[COL00183] [nvarchar](255) NULL,
	[COL00184] [nvarchar](255) NULL,
	[COL00185] [nvarchar](255) NULL,
	[COL00186] [nvarchar](255) NULL,
	[COL00187] [nvarchar](255) NULL,
	[COL00188] [nvarchar](255) NULL,
	[COL00189] [nvarchar](255) NULL,
	[COL00190] [nvarchar](255) NULL,
	[COL00191] [nvarchar](255) NULL,
	[COL00192] [nvarchar](255) NULL,
	[COL00193] [nvarchar](255) NULL,
	[COL00194] [nvarchar](255) NULL,
	[COL00195] [nvarchar](255) NULL,
	[COL00196] [nvarchar](255) NULL,
	[COL00197] [nvarchar](255) NULL,
	[COL00198] [nvarchar](255) NULL,
	[COL00199] [nvarchar](255) NULL,
	[COL00200] [nvarchar](255) NULL,
	[COL00201] [nvarchar](255) NULL,
	[COL00202] [nvarchar](255) NULL,
	[COL00203] [nvarchar](255) NULL,
	[COL00204] [nvarchar](255) NULL,
	[COL00205] [nvarchar](255) NULL,
	[COL00206] [nvarchar](255) NULL,
	[COL00207] [nvarchar](255) NULL,
	[COL00208] [nvarchar](255) NULL,
	[COL00209] [nvarchar](255) NULL,
	[COL00210] [nvarchar](255) NULL,
	[COL00211] [nvarchar](255) NULL,
	[COL00212] [nvarchar](255) NULL,
	[COL00213] [nvarchar](255) NULL,
	[COL00214] [nvarchar](255) NULL,
	[COL00215] [nvarchar](255) NULL,
	[COL00216] [nvarchar](255) NULL,
	[COL00217] [nvarchar](255) NULL,
	[COL00218] [nvarchar](255) NULL,
	[COL00219] [nvarchar](255) NULL,
	[COL00220] [nvarchar](255) NULL,
	[COL00221] [nvarchar](255) NULL,
	[COL00222] [float] NULL,
	[COL00223] [nvarchar](255) NULL,
	[COL00224] [float] NULL,
	[COL00225] [nvarchar](255) NULL,
	[COL00226] [float] NULL,
	[COL00227] [nvarchar](255) NULL,
	[COL00228] [float] NULL,
	[COL00229] [nvarchar](255) NULL,
	[COL00230] [float] NULL,
	[COL00231] [nvarchar](255) NULL,
	[COL00232] [nvarchar](255) NULL,
	[COL00233] [nvarchar](255) NULL,
	[COL00234] [nvarchar](255) NULL,
	[COL00235] [nvarchar](255) NULL,
	[COL00236] [nvarchar](255) NULL,
	[COL00237] [nvarchar](255) NULL,
	[COL00238] [nvarchar](255) NULL,
	[COL00239] [nvarchar](255) NULL,
	[COL00240] [nvarchar](255) NULL,
	[COL00241] [nvarchar](255) NULL,
	[COL00242] [nvarchar](255) NULL,
	[COL00243] [nvarchar](255) NULL,
	[COL00244] [nvarchar](255) NULL,
	[COL00245] [nvarchar](255) NULL,
	[COL00246] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ProductoId] [int] NOT NULL,
	[CodigoProducto] [varchar](50) NOT NULL,
	[DescripcionProducto] [varchar](250) NULL,
	[ObservacionProducto] [varchar](500) NULL,
	[DescripcionProductoFinal] [varchar](250) NULL,
	[CodigoProductoFinal] [varchar](50) NULL,
	[Grado] [varchar](50) NULL,
	[ClaseProductoId] [int] NULL,
	[DimensionMinima] [decimal](18, 5) NULL,
	[DimensionMaxima] [decimal](18, 5) NULL,
	[Nominal] [decimal](18, 5) NULL,
	[DefUsuario1] [varchar](50) NULL,
	[DefUsuario2] [varchar](50) NULL,
 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED 
(
	[ProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoEspecificacion]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoEspecificacion](
	[ProductoEspecificacionId] [bigint] NOT NULL,
	[ProductoId] [int] NOT NULL,
	[EspecificacionId] [bigint] NOT NULL,
	[ValorEsperado] [varchar](50) NULL,
	[RangoMinimo] [decimal](18, 9) NULL,
	[RangoMaximo] [decimal](18, 9) NULL,
 CONSTRAINT [PK_PRODUCTO_ESPECIFICACION] PRIMARY KEY CLUSTERED 
(
	[ProductoEspecificacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 1/10/18 9:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[ProveedorId] [bigint] NOT NULL,
	[CodigoProveedor] [varchar](50) NOT NULL,
	[NombreProveedor] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[ProveedorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoMaterial]    Script Date: 1/10/18 9:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoMaterial](
	[TipoMaterialId] [bigint] NOT NULL,
	[DescripcionTipoMaterial] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TipoMaterial] PRIMARY KEY CLUSTERED 
(
	[TipoMaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 1/10/18 9:54:15 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 1/10/18 9:54:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 1/10/18 9:54:15 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 1/10/18 9:54:15 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 1/10/18 9:54:15 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 1/10/18 9:54:15 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 1/10/18 9:54:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_Active]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Analisis]  WITH CHECK ADD  CONSTRAINT [FK_Analisis_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Analisis] CHECK CONSTRAINT [FK_Analisis_Cliente]
GO
ALTER TABLE [dbo].[Analisis]  WITH CHECK ADD  CONSTRAINT [FK_Analisis_Maquina] FOREIGN KEY([MaquinaId])
REFERENCES [dbo].[Maquina] ([MaquinaId])
GO
ALTER TABLE [dbo].[Analisis] CHECK CONSTRAINT [FK_Analisis_Maquina]
GO
ALTER TABLE [dbo].[Analisis]  WITH CHECK ADD  CONSTRAINT [FK_Analisis_Producto] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Producto] ([ProductoId])
GO
ALTER TABLE [dbo].[Analisis] CHECK CONSTRAINT [FK_Analisis_Producto]
GO
ALTER TABLE [dbo].[AnalisisMaterial]  WITH CHECK ADD  CONSTRAINT [FK_AnalisisMaterial_Analisis] FOREIGN KEY([AnalisisId])
REFERENCES [dbo].[Analisis] ([AnalisisId])
GO
ALTER TABLE [dbo].[AnalisisMaterial] CHECK CONSTRAINT [FK_AnalisisMaterial_Analisis]
GO
ALTER TABLE [dbo].[AnalisisMaterial]  WITH CHECK ADD  CONSTRAINT [FK_AnalisisMaterial_Material] FOREIGN KEY([MateriaId])
REFERENCES [dbo].[Material] ([MaterialId])
GO
ALTER TABLE [dbo].[AnalisisMaterial] CHECK CONSTRAINT [FK_AnalisisMaterial_Material]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DetalleAnalisis]  WITH CHECK ADD  CONSTRAINT [FK_DetalleAnalisis_Analisis] FOREIGN KEY([AnalisisId])
REFERENCES [dbo].[Analisis] ([AnalisisId])
GO
ALTER TABLE [dbo].[DetalleAnalisis] CHECK CONSTRAINT [FK_DetalleAnalisis_Analisis]
GO
ALTER TABLE [dbo].[DetalleAnalisis]  WITH CHECK ADD  CONSTRAINT [FK_DetalleAnalisis_Especificacion] FOREIGN KEY([EspecificacionId])
REFERENCES [dbo].[Especificacion] ([EspecificacionId])
GO
ALTER TABLE [dbo].[DetalleAnalisis] CHECK CONSTRAINT [FK_DetalleAnalisis_Especificacion]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_Proveedor] FOREIGN KEY([ProveedorId])
REFERENCES [dbo].[Proveedor] ([ProveedorId])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_Proveedor]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_TipoMaterial] FOREIGN KEY([TipoMaterialId])
REFERENCES [dbo].[TipoMaterial] ([TipoMaterialId])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_TipoMaterial]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_ClaseProducto] FOREIGN KEY([ClaseProductoId])
REFERENCES [dbo].[ClaseProducto] ([ClaseProductoId])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_ClaseProducto]
GO
ALTER TABLE [dbo].[ProductoEspecificacion]  WITH CHECK ADD  CONSTRAINT [FK_ProductoEspecificacion_Especificacion] FOREIGN KEY([EspecificacionId])
REFERENCES [dbo].[Especificacion] ([EspecificacionId])
GO
ALTER TABLE [dbo].[ProductoEspecificacion] CHECK CONSTRAINT [FK_ProductoEspecificacion_Especificacion]
GO
ALTER TABLE [dbo].[ProductoEspecificacion]  WITH CHECK ADD  CONSTRAINT [FK_ProductoEspecificacion_Producto] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Producto] ([ProductoId])
GO
ALTER TABLE [dbo].[ProductoEspecificacion] CHECK CONSTRAINT [FK_ProductoEspecificacion_Producto]
GO
USE [master]
GO
ALTER DATABASE [CALIDAD] SET  READ_WRITE 
GO
