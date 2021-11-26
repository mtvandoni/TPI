CREATE DATABASE TPI_DB;

USE [TPI_DB]
GO
/****** Object:  Table [dbo].[comentario]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comentario](
	[idComentario] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](200) NOT NULL,
	[fecha] [date] NOT NULL,
	[idProyecto] [int] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_comentario] PRIMARY KEY CLUSTERED 
(
	[idComentario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cursada]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cursada](
	[idCursada] [int] IDENTITY(1,1) NOT NULL,
	[codCursada] [nchar](30),
	[descripcion] [nchar](30)  NULL,
	[fechaInicio] [date]  NULL,
	[fechaFin] [date]  NULL,
 CONSTRAINT [PK_cursada] PRIMARY KEY CLUSTERED 
(
	[idCursada] ASC
	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[entrega]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[entrega](
	[idEntrega] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](100) NOT NULL,
	[idProyecto] [int] NULL,
 CONSTRAINT [PK_entrega] PRIMARY KEY CLUSTERED 
(
	[idEntrega] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[equipo]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipo](
	[idEquipo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nchar](20) NOT NULL,
	[idProyecto] [int] NULL,
 CONSTRAINT [PK_equipo] PRIMARY KEY CLUSTERED
(
	[idEquipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[equipoPersona]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipoPersona](
	[idEquipo] [int] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_equipoPersona] PRIMARY KEY CLUSTERED 
(
	[idEquipo] ASC,
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[persona]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[persona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nchar](50) NOT NULL,
	[DNI] [int] NULL,
	[estado] [char] NOT NULL,
	[Email] [nchar](50) NULL,
	[EmailUnlam] [nchar](50) NULL,
	[carrera] [nchar](50) NULL,
	[password] [nchar](10) NULL,
	[edad] [tinyint]  NULL,
	[descripcion] [varchar](200)  NULL,
	[avatar] [nchar](50)  NULL,
	[idTipo] [tinyint] NULL,
	[idCursada] [int] NULL,
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[proyecto]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[proyecto](
	[idProyecto] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nchar](30) NOT NULL,
	[descripcion] [varchar](600) NOT NULL,
	[propuestaValor] [varchar](300) NOT NULL,
	[repositorio] [varchar](50),
	[cantMeGusta] [int],
	[rutaFoto] [varchar](50),
	[rutaVideo] [varchar](100),
	[idRed] [int],
	[idTipoProyecto] [int] NULL,
	[idCategoria] [int] NULL,
 CONSTRAINT [PK_proyecto] PRIMARY KEY CLUSTERED 
(
	[idProyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[red]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[red](
	[idRed] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](200) NOT NULL,
	[idProyecto] [int] NOT NULL,
	[idTipoRed] [int] NOT NULL,
 CONSTRAINT [PK_redes] PRIMARY KEY CLUSTERED 
(
	[idRed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipoPersona]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoPersona](
	[idTipo] [tinyint] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](20) NULL,
 CONSTRAINT [PK_tipoPersona] PRIMARY KEY CLUSTERED 
(
	[idTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipoProyect]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoProyect](
	[idTipoProyecto] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](20) NOT NULL,
 CONSTRAINT [PK_tipoProyect] PRIMARY KEY CLUSTERED 
(
	[idTipoProyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[categoria](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](20) NOT NULL,
 CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[tipoRed]    Script Date: 30/9/2021 19:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoRed](
	[idTipoRed] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](20) NOT NULL,
 CONSTRAINT [PK_tipoRed] PRIMARY KEY CLUSTERED 
(
	[idTipoRed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[novedad](
	[idNovedad] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[rutaFoto] [varchar](50),
	[rutaVideo] [varchar](100),
 CONSTRAINT [PK_novedad] PRIMARY KEY CLUSTERED 
(
	[idNovedad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[comentario]  WITH CHECK ADD  CONSTRAINT [FK_persona_id] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([id])
GO
ALTER TABLE [dbo].[comentario] CHECK CONSTRAINT [FK_persona_id]
GO
ALTER TABLE [dbo].[comentario]  WITH CHECK ADD  CONSTRAINT [FK_proyect_idProyecto] FOREIGN KEY([idProyecto])
REFERENCES [dbo].[proyecto] ([idProyecto])
GO
ALTER TABLE [dbo].[comentario] CHECK CONSTRAINT [FK_proyect_idProyecto]
GO
ALTER TABLE [dbo].[entrega]  WITH CHECK ADD  CONSTRAINT [FK_entrega_idProyecto] FOREIGN KEY([idProyecto])
REFERENCES [dbo].[proyecto] ([idProyecto])
GO
ALTER TABLE [dbo].[entrega] CHECK CONSTRAINT [FK_entrega_idProyecto]
GO
ALTER TABLE [dbo].[equipoPersona]  WITH CHECK ADD  CONSTRAINT [FK_equipo_idEquipo] FOREIGN KEY([idEquipo])
REFERENCES [dbo].[equipo] ([idEquipo])
GO
ALTER TABLE [dbo].[equipoPersona] CHECK CONSTRAINT [FK_equipo_idEquipo]
GO
ALTER TABLE [dbo].[equipoPersona]  WITH CHECK ADD  CONSTRAINT [FK_person_id] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([id])
GO
ALTER TABLE [dbo].[equipoPersona] CHECK CONSTRAINT [FK_person_id]
GO
ALTER TABLE [dbo].[persona]  WITH CHECK ADD  CONSTRAINT [FK_cursada_idCursada] FOREIGN KEY([idCursada])
REFERENCES [dbo].[cursada] ([idCursada])
GO
ALTER TABLE [dbo].[persona] CHECK CONSTRAINT [FK_cursada_idCursada]
GO
ALTER TABLE [dbo].[persona]  WITH CHECK ADD  CONSTRAINT [FK_persona_tipoPersona] FOREIGN KEY([idTipo])
REFERENCES [dbo].[tipoPersona] ([idTipo])
GO
ALTER TABLE [dbo].[persona] CHECK CONSTRAINT [FK_persona_tipoPersona]
GO
ALTER TABLE [dbo].[proyecto]  WITH CHECK ADD  CONSTRAINT [FK_proyecto_idTipoProyecto] FOREIGN KEY([idTipoProyecto])
REFERENCES [dbo].[tipoProyect] ([idTipoProyecto])
GO
ALTER TABLE [dbo].[proyecto] CHECK CONSTRAINT [FK_proyecto_idTipoProyecto]
GO
ALTER TABLE [dbo].[red]  WITH CHECK ADD  CONSTRAINT [FK_proyecto_idProyecto] FOREIGN KEY([idProyecto])
REFERENCES [dbo].[proyecto] ([idProyecto])
GO
ALTER TABLE [dbo].[red] CHECK CONSTRAINT [FK_proyecto_idProyecto]
GO
ALTER TABLE [dbo].[red]  WITH CHECK ADD  CONSTRAINT [FK_red_idTipoRed] FOREIGN KEY([idTipoRed])
REFERENCES [dbo].[tipoRed] ([idTipoRed])
GO
--ALTER TABLE [dbo].[tipoRed] CHECK CONSTRAINT [FK_red_idRed]
--GO

ALTER TABLE [dbo].[equipo]  WITH CHECK ADD  CONSTRAINT [FK_proyecto_id] FOREIGN KEY([idProyecto])
REFERENCES [dbo].[proyecto] ([idProyecto])
GO

ALTER TABLE [dbo].[proyecto]  WITH CHECK ADD  CONSTRAINT [FK_categoria_idCategoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])
GO