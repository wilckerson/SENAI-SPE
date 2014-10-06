
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/12/2013 15:11:29
-- Generated from EDMX file: E:\Idevelop SPE\SPE\R1\Projeto\Senai.SPE.Domain\SPEContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SPE];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AGENDAAM_AMBIENTE__AMBIENTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgendaAmbiente] DROP CONSTRAINT [FK_AGENDAAM_AMBIENTE__AMBIENTE];
GO
IF OBJECT_ID(N'[dbo].[FK_AGENDACO_AGENDA_CO_COMPONEN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgendaComponente] DROP CONSTRAINT [FK_AGENDACO_AGENDA_CO_COMPONEN];
GO
IF OBJECT_ID(N'[dbo].[FK_AGENDACO_DOCENTE_U_UNIDADE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgendaComponente] DROP CONSTRAINT [FK_AGENDACO_DOCENTE_U_UNIDADE];
GO
IF OBJECT_ID(N'[dbo].[FK_AGENDADO_AGENDA_DO_TIPOATIV]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgendaDocente] DROP CONSTRAINT [FK_AGENDADO_AGENDA_DO_TIPOATIV];
GO
IF OBJECT_ID(N'[dbo].[FK_AGENDADO_DOCENTE_A_DOCENTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgendaDocente] DROP CONSTRAINT [FK_AGENDADO_DOCENTE_A_DOCENTE];
GO
IF OBJECT_ID(N'[dbo].[FK_AMBIENTE_AGENDA_CO_AGENDACO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ambiente] DROP CONSTRAINT [FK_AMBIENTE_AGENDA_CO_AGENDACO];
GO
IF OBJECT_ID(N'[dbo].[FK_AMBIENTE_AMBIENTE__LOCALAMB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ambiente] DROP CONSTRAINT [FK_AMBIENTE_AMBIENTE__LOCALAMB];
GO
IF OBJECT_ID(N'[dbo].[FK_AMBIENTE_AMBIENTE__TIPOAMBI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ambiente] DROP CONSTRAINT [FK_AMBIENTE_AMBIENTE__TIPOAMBI];
GO
IF OBJECT_ID(N'[dbo].[FK_CALENDAR_CALENDARI_CALENDAR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calendario] DROP CONSTRAINT [FK_CALENDAR_CALENDARI_CALENDAR];
GO
IF OBJECT_ID(N'[dbo].[FK_CALENDAR_CALENDARI_MODALIDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calendario] DROP CONSTRAINT [FK_CALENDAR_CALENDARI_MODALIDA];
GO
IF OBJECT_ID(N'[dbo].[FK_CATMATER_CATEGORIA_CATMATER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CatMaterial] DROP CONSTRAINT [FK_CATMATER_CATEGORIA_CATMATER];
GO
IF OBJECT_ID(N'[dbo].[FK_CATSERVI_CATEGORIA_CATSERVI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CatServico] DROP CONSTRAINT [FK_CATSERVI_CATEGORIA_CATSERVI];
GO
IF OBJECT_ID(N'[dbo].[FK_COMPONEN_COMPONENT_COMPONEN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ComponenteTipoAmbiente] DROP CONSTRAINT [FK_COMPONEN_COMPONENT_COMPONEN];
GO
IF OBJECT_ID(N'[dbo].[FK_COMPONEN_COMPONENT_TIPOAMBI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ComponenteTipoAmbiente] DROP CONSTRAINT [FK_COMPONEN_COMPONENT_TIPOAMBI];
GO
IF OBJECT_ID(N'[dbo].[FK_CR_MODALIDE__MODALIDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CR] DROP CONSTRAINT [FK_CR_MODALIDE__MODALIDA];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_CURSO_CR_CR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_CURSO_CURSO_CR_CR];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_CURSO_TUR_TURNO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_CURSO_CURSO_TUR_TURNO];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_CURSO_UNI_UNIDADE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_CURSO_CURSO_UNI_UNIDADE];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_CURSO_USU_USUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_CURSO_CURSO_USU_USUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_MA_CURSO_MAT_CURSO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CursoMaterial] DROP CONSTRAINT [FK_CURSO_MA_CURSO_MAT_CURSO];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_MA_CURSO_MAT_MATERIAL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CursoMaterial] DROP CONSTRAINT [FK_CURSO_MA_CURSO_MAT_MATERIAL];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_MATRIZ_CU_MATRIZ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_CURSO_MATRIZ_CU_MATRIZ];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_SE_CURSO_SER_CURSO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CursoServico] DROP CONSTRAINT [FK_CURSO_SE_CURSO_SER_CURSO];
GO
IF OBJECT_ID(N'[dbo].[FK_CURSO_SE_CURSO_SER_SERVICO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CursoServico] DROP CONSTRAINT [FK_CURSO_SE_CURSO_SER_SERVICO];
GO
IF OBJECT_ID(N'[dbo].[FK_DOCENTE_AGENDA_CO_AGENDACO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Docente] DROP CONSTRAINT [FK_DOCENTE_AGENDA_CO_AGENDACO];
GO
IF OBJECT_ID(N'[dbo].[FK_DOCENTE_DOCENTE_T_TIPOCONT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Docente] DROP CONSTRAINT [FK_DOCENTE_DOCENTE_T_TIPOCONT];
GO
IF OBJECT_ID(N'[dbo].[FK_DOCENTE_EMPRESA_D_EMPRESA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Docente] DROP CONSTRAINT [FK_DOCENTE_EMPRESA_D_EMPRESA];
GO
IF OBJECT_ID(N'[dbo].[FK_EMPRESA_AGENDA_CO_AGENDACO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Empresa] DROP CONSTRAINT [FK_EMPRESA_AGENDA_CO_AGENDACO];
GO
IF OBJECT_ID(N'[dbo].[FK_MATERIAL_MATERIAL__CATMATER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Material] DROP CONSTRAINT [FK_MATERIAL_MATERIAL__CATMATER];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_C_MATRIZ_CO_COMPONEN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MatrizComponente] DROP CONSTRAINT [FK_MATRIZ_C_MATRIZ_CO_COMPONEN];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_C_MATRIZ_CO_MATRIZ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MatrizComponente] DROP CONSTRAINT [FK_MATRIZ_C_MATRIZ_CO_MATRIZ];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_M_MATRIZ_MA_MATERIAL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MatrizMaterial] DROP CONSTRAINT [FK_MATRIZ_M_MATRIZ_MA_MATERIAL];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_M_MATRIZ_MA_MATRIZ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MatrizMaterial] DROP CONSTRAINT [FK_MATRIZ_M_MATRIZ_MA_MATRIZ];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_MATRIZ_AR_AREAATUA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matriz] DROP CONSTRAINT [FK_MATRIZ_MATRIZ_AR_AREAATUA];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_MATRIZ_CB_CBO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matriz] DROP CONSTRAINT [FK_MATRIZ_MATRIZ_CB_CBO];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_MATRIZ_MO_MODALIDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matriz] DROP CONSTRAINT [FK_MATRIZ_MATRIZ_MO_MODALIDA];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_S_MATRIZ_SE_MATRIZ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MatrizServico] DROP CONSTRAINT [FK_MATRIZ_S_MATRIZ_SE_MATRIZ];
GO
IF OBJECT_ID(N'[dbo].[FK_MATRIZ_S_MATRIZ_SE_SERVICO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MatrizServico] DROP CONSTRAINT [FK_MATRIZ_S_MATRIZ_SE_SERVICO];
GO
IF OBJECT_ID(N'[dbo].[FK_MODALIDA_MODALIDAD_MODALIDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModalidadeUnidade] DROP CONSTRAINT [FK_MODALIDA_MODALIDAD_MODALIDA];
GO
IF OBJECT_ID(N'[dbo].[FK_MODALIDA_MODALIDAD_UNIDADE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModalidadeUnidade] DROP CONSTRAINT [FK_MODALIDA_MODALIDAD_UNIDADE];
GO
IF OBJECT_ID(N'[dbo].[FK_ORIENTAD_ORIENTADO_AREAATUA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AreaOrientador] DROP CONSTRAINT [FK_ORIENTAD_ORIENTADO_AREAATUA];
GO
IF OBJECT_ID(N'[dbo].[FK_ORIENTAD_ORIENTADO_USUARIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AreaOrientador] DROP CONSTRAINT [FK_ORIENTAD_ORIENTADO_USUARIO];
GO
IF OBJECT_ID(N'[dbo].[FK_RECURSOA_AMBIENTE__AMBIENTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AmbienteRecurso] DROP CONSTRAINT [FK_RECURSOA_AMBIENTE__AMBIENTE];
GO
IF OBJECT_ID(N'[dbo].[FK_RELATION_RELATIONS_CR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrUNidade] DROP CONSTRAINT [FK_RELATION_RELATIONS_CR];
GO
IF OBJECT_ID(N'[dbo].[FK_RELATION_RELATIONS_UNIDADE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrUNidade] DROP CONSTRAINT [FK_RELATION_RELATIONS_UNIDADE];
GO
IF OBJECT_ID(N'[dbo].[FK_SERVICO_SERVICO_C_CATSERVI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Servico] DROP CONSTRAINT [FK_SERVICO_SERVICO_C_CATSERVI];
GO
IF OBJECT_ID(N'[dbo].[FK_TIPOATIV_TIPO_ATIV_TIPOATIV]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TipoAtividade] DROP CONSTRAINT [FK_TIPOATIV_TIPO_ATIV_TIPOATIV];
GO
IF OBJECT_ID(N'[dbo].[FK_UNCURRIC_COMPONENT_COMPONEN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UnCurricular] DROP CONSTRAINT [FK_UNCURRIC_COMPONENT_COMPONEN];
GO
IF OBJECT_ID(N'[dbo].[FK_USUARIO_CATEGORIA_CATUSUAR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_USUARIO_CATEGORIA_CATUSUAR];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AgendaAmbiente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgendaAmbiente];
GO
IF OBJECT_ID(N'[dbo].[AgendaComponente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgendaComponente];
GO
IF OBJECT_ID(N'[dbo].[AgendaDocente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgendaDocente];
GO
IF OBJECT_ID(N'[dbo].[Ambiente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ambiente];
GO
IF OBJECT_ID(N'[dbo].[AmbienteRecurso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AmbienteRecurso];
GO
IF OBJECT_ID(N'[dbo].[AreaAtuacao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AreaAtuacao];
GO
IF OBJECT_ID(N'[dbo].[AreaOrientador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AreaOrientador];
GO
IF OBJECT_ID(N'[dbo].[Calendario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calendario];
GO
IF OBJECT_ID(N'[dbo].[CatMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CatMaterial];
GO
IF OBJECT_ID(N'[dbo].[CatServico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CatServico];
GO
IF OBJECT_ID(N'[dbo].[CatUsuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CatUsuario];
GO
IF OBJECT_ID(N'[dbo].[CBO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CBO];
GO
IF OBJECT_ID(N'[dbo].[Componente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Componente];
GO
IF OBJECT_ID(N'[dbo].[ComponenteTipoAmbiente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComponenteTipoAmbiente];
GO
IF OBJECT_ID(N'[dbo].[CR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CR];
GO
IF OBJECT_ID(N'[dbo].[CrUNidade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrUNidade];
GO
IF OBJECT_ID(N'[dbo].[Curso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Curso];
GO
IF OBJECT_ID(N'[dbo].[CursoMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CursoMaterial];
GO
IF OBJECT_ID(N'[dbo].[CursoServico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CursoServico];
GO
IF OBJECT_ID(N'[dbo].[Docente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Docente];
GO
IF OBJECT_ID(N'[dbo].[Empresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empresa];
GO
IF OBJECT_ID(N'[dbo].[LocalAmbiente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocalAmbiente];
GO
IF OBJECT_ID(N'[dbo].[Material]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Material];
GO
IF OBJECT_ID(N'[dbo].[Matriz]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Matriz];
GO
IF OBJECT_ID(N'[dbo].[MatrizComponente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MatrizComponente];
GO
IF OBJECT_ID(N'[dbo].[MatrizMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MatrizMaterial];
GO
IF OBJECT_ID(N'[dbo].[MatrizServico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MatrizServico];
GO
IF OBJECT_ID(N'[dbo].[Modalidade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modalidade];
GO
IF OBJECT_ID(N'[dbo].[ModalidadeUnidade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModalidadeUnidade];
GO
IF OBJECT_ID(N'[dbo].[Servico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Servico];
GO
IF OBJECT_ID(N'[dbo].[TipoAmbiente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoAmbiente];
GO
IF OBJECT_ID(N'[dbo].[TipoAtividade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoAtividade];
GO
IF OBJECT_ID(N'[dbo].[TipoContrato]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoContrato];
GO
IF OBJECT_ID(N'[dbo].[Turno]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Turno];
GO
IF OBJECT_ID(N'[dbo].[UnCurricular]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnCurricular];
GO
IF OBJECT_ID(N'[dbo].[Unidade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Unidade];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AgendaAmbiente'
CREATE TABLE [dbo].[AgendaAmbiente] (
    [IdAgendaAmbiente] int  NOT NULL,
    [IdAmbiente] int  NULL,
    [DataIni] datetime  NULL,
    [DataFim] datetime  NULL,
    [HoraIni] datetime  NULL,
    [HoraFim] datetime  NULL,
    [DiasSemana] varchar(7)  NULL
);
GO

-- Creating table 'AgendaComponente'
CREATE TABLE [dbo].[AgendaComponente] (
    [IdAgendaComponente] int  NOT NULL,
    [IdComponente] int  NULL,
    [IdUnidade] int  NULL,
    [DataIni] datetime  NULL,
    [DataFim] datetime  NULL,
    [HoraIni] datetime  NULL,
    [HoraFim] datetime  NULL,
    [STDefinir] bit  NULL,
    [DiasSemana] varchar(7)  NULL
);
GO

-- Creating table 'AgendaDocente'
CREATE TABLE [dbo].[AgendaDocente] (
    [IdAgendaDocente] int  NOT NULL,
    [IdDocente] int  NULL,
    [IdTipoAtividade] int  NULL,
    [DescrricaoAtiv] varchar(512)  NULL,
    [DataIni] datetime  NULL,
    [DataFim] datetime  NULL,
    [HoraIni] datetime  NULL,
    [HoraFim] datetime  NULL,
    [DiasSemana] varchar(7)  NULL,
    [CH] smallint  NULL
);
GO

-- Creating table 'Ambiente'
CREATE TABLE [dbo].[Ambiente] (
    [IdAmbiente] int  NOT NULL,
    [IdLoc] int  NULL,
    [IdTipoAmbiente] int  NULL,
    [IdAgendaComponente] int  NULL,
    [Capac] smallint  NULL
);
GO

-- Creating table 'AmbienteRecurso'
CREATE TABLE [dbo].[AmbienteRecurso] (
    [ID_RECURSO] int  NOT NULL,
    [IdAmbiente] int  NULL,
    [Tipo] varchar(64)  NULL,
    [Descr] varchar(64)  NULL
);
GO

-- Creating table 'AreaAtuacao'
CREATE TABLE [dbo].[AreaAtuacao] (
    [IdAreaAtuacao] int  NOT NULL,
    [Nome] varchar(256)  NULL
);
GO

-- Creating table 'Calendario'
CREATE TABLE [dbo].[Calendario] (
    [IdCalendario] int  NOT NULL,
    [IdModalidade] int  NULL,
    [CalIdCalendario] int  NULL,
    [ANO] char(10)  NULL
);
GO

-- Creating table 'CatMaterial'
CREATE TABLE [dbo].[CatMaterial] (
    [ID_CATEGORIA_MAT] int  NOT NULL,
    [CatIdCategoriaMat] int  NULL,
    [NomeCat] varchar(64)  NULL
);
GO

-- Creating table 'CatServico'
CREATE TABLE [dbo].[CatServico] (
    [IdCategoriaServico] int  NOT NULL,
    [CatIdCategoriaServico] int  NULL,
    [NomeCatServico] varchar(64)  NULL
);
GO

-- Creating table 'CatUsuario'
CREATE TABLE [dbo].[CatUsuario] (
    [IdCatUsuario] int  NOT NULL,
    [NomeCatUsuario] char(10)  NULL
);
GO

-- Creating table 'CBO'
CREATE TABLE [dbo].[CBO] (
    [IdCBO] int  NOT NULL,
    [Descrricao] char(10)  NULL
);
GO

-- Creating table 'Componente'
CREATE TABLE [dbo].[Componente] (
    [IdComponente] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(256)  NULL,
    [CH] smallint  NULL
);
GO

-- Creating table 'CR'
CREATE TABLE [dbo].[CR] (
    [IdCR] int  NOT NULL,
    [IdModalidade] int  NULL,
    [Nome] varchar(256)  NULL
);
GO

-- Creating table 'Curso'
CREATE TABLE [dbo].[Curso] (
    [IdCurso] int  NOT NULL,
    [IdUnidade] int  NULL,
    [IdTurno] int  NULL,
    [IdCR] int  NULL,
    [IdMatriz] int  NULL,
    [IdUsuario] int  NULL,
    [QtdeVagas] smallint  NULL,
    [DataFim] datetime  NULL,
    [Status] smallint  NULL,
    [TipoOferta] smallint  NULL,
    [Preco] decimal(19,4)  NULL,
    [Evento] int  NULL
);
GO

-- Creating table 'Docente'
CREATE TABLE [dbo].[Docente] (
    [IdDocente] int  NOT NULL,
    [IdAgendaComponente] int  NULL,
    [IdTipoContrato] int  NULL,
    [IdEmpresa] int  NULL,
    [CPF] varchar(15)  NULL,
    [Email] varchar(64)  NULL,
    [Sexo] tinyint  NULL,
    [Tel] varchar(32)  NULL,
    [NivelFuncao] smallint  NULL,
    [Nome] varchar(256)  NULL
);
GO

-- Creating table 'Empresa'
CREATE TABLE [dbo].[Empresa] (
    [IdEmpresa] int  NOT NULL,
    [IdAgendaComponente] int  NULL,
    [NomeFantasia] varchar(128)  NULL,
    [CNPJ] varchar(17)  NULL,
    [Email] varchar(64)  NULL,
    [Tel] varchar(32)  NULL,
    [Contato] varchar(64)  NULL
);
GO

-- Creating table 'LocalAmbiente'
CREATE TABLE [dbo].[LocalAmbiente] (
    [IdLoc] int  NOT NULL,
    [Descr] varchar(64)  NULL
);
GO

-- Creating table 'Material'
CREATE TABLE [dbo].[Material] (
    [IdMaterial] int  NOT NULL,
    [Descr] char(40)  NULL,
    [ID_CATEGORIA_MAT] int  NULL
);
GO

-- Creating table 'Matriz'
CREATE TABLE [dbo].[Matriz] (
    [IdMatriz] int  NOT NULL,
    [IdModalidade] int  NULL,
    [IdAreaAtuacao] int  NULL,
    [IdCBO] int  NULL,
    [Preco] decimal(19,4)  NULL,
    [Nome] varchar(250)  NULL
);
GO

-- Creating table 'Modalidade'
CREATE TABLE [dbo].[Modalidade] (
    [IdModalidade] int  NOT NULL,
    [Nome] varchar(256)  NULL
);
GO

-- Creating table 'Servico'
CREATE TABLE [dbo].[Servico] (
    [IdServico] int  NOT NULL,
    [IdCategoriaServico] int  NULL,
    [NomeServ] varchar(64)  NULL
);
GO

-- Creating table 'TipoAmbiente'
CREATE TABLE [dbo].[TipoAmbiente] (
    [IdTipoAmbiente] int  NOT NULL,
    [Descr] varchar(64)  NULL
);
GO

-- Creating table 'TipoAtividade'
CREATE TABLE [dbo].[TipoAtividade] (
    [IdTipoAtividade] int  NOT NULL,
    [TIP_ID_TIPO_ATIVIDADE] int  NULL,
    [Nome] varchar(256)  NULL,
    [Interna] bit  NULL
);
GO

-- Creating table 'TipoContrato'
CREATE TABLE [dbo].[TipoContrato] (
    [IdTipoContrato] int  NOT NULL,
    [Descr] varchar(256)  NULL
);
GO

-- Creating table 'Turno'
CREATE TABLE [dbo].[Turno] (
    [IdTurno] int  NOT NULL,
    [Descr] varchar(256)  NULL
);
GO

-- Creating table 'UnCurricular'
CREATE TABLE [dbo].[UnCurricular] (
    [IvUnCurricular] int  NOT NULL,
    [IdComponente] int  NULL,
    [Nome] varchar(256)  NULL
);
GO

-- Creating table 'Unidade'
CREATE TABLE [dbo].[Unidade] (
    [IdUnidade] int  NOT NULL,
    [Descr] varchar(64)  NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] int  NOT NULL,
    [IdCatUsuario] int  NULL,
    [Email] varchar(64)  NULL,
    [Tel] varchar(32)  NULL,
    [CPF] varchar(15)  NULL,
    [Nome] varchar(80)  NULL
);
GO

-- Creating table 'AreaOrientador'
CREATE TABLE [dbo].[AreaOrientador] (
    [AreaAtuacao_IdAreaAtuacao] int  NOT NULL,
    [Usuario_IdUsuario] int  NOT NULL
);
GO

-- Creating table 'ComponenteTipoAmbiente'
CREATE TABLE [dbo].[ComponenteTipoAmbiente] (
    [Componente_IdComponente] int  NOT NULL,
    [TipoAmbiente_IdTipoAmbiente] int  NOT NULL
);
GO

-- Creating table 'CrUNidade'
CREATE TABLE [dbo].[CrUNidade] (
    [CR_IdCR] int  NOT NULL,
    [Unidade_IdUnidade] int  NOT NULL
);
GO

-- Creating table 'CursoMaterial'
CREATE TABLE [dbo].[CursoMaterial] (
    [Curso_IdCurso] int  NOT NULL,
    [Material_IdMaterial] int  NOT NULL
);
GO

-- Creating table 'CursoServico'
CREATE TABLE [dbo].[CursoServico] (
    [Curso_IdCurso] int  NOT NULL,
    [Servico_IdServico] int  NOT NULL
);
GO

-- Creating table 'MatrizComponente'
CREATE TABLE [dbo].[MatrizComponente] (
    [Componente_IdComponente] int  NOT NULL,
    [Matriz_IdMatriz] int  NOT NULL
);
GO

-- Creating table 'MatrizMaterial'
CREATE TABLE [dbo].[MatrizMaterial] (
    [Material_IdMaterial] int  NOT NULL,
    [Matriz_IdMatriz] int  NOT NULL
);
GO

-- Creating table 'MatrizServico'
CREATE TABLE [dbo].[MatrizServico] (
    [Matriz_IdMatriz] int  NOT NULL,
    [Servico_IdServico] int  NOT NULL
);
GO

-- Creating table 'ModalidadeUnidade'
CREATE TABLE [dbo].[ModalidadeUnidade] (
    [Modalidade_IdModalidade] int  NOT NULL,
    [Unidade_IdUnidade] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAgendaAmbiente] in table 'AgendaAmbiente'
ALTER TABLE [dbo].[AgendaAmbiente]
ADD CONSTRAINT [PK_AgendaAmbiente]
    PRIMARY KEY CLUSTERED ([IdAgendaAmbiente] ASC);
GO

-- Creating primary key on [IdAgendaComponente] in table 'AgendaComponente'
ALTER TABLE [dbo].[AgendaComponente]
ADD CONSTRAINT [PK_AgendaComponente]
    PRIMARY KEY CLUSTERED ([IdAgendaComponente] ASC);
GO

-- Creating primary key on [IdAgendaDocente] in table 'AgendaDocente'
ALTER TABLE [dbo].[AgendaDocente]
ADD CONSTRAINT [PK_AgendaDocente]
    PRIMARY KEY CLUSTERED ([IdAgendaDocente] ASC);
GO

-- Creating primary key on [IdAmbiente] in table 'Ambiente'
ALTER TABLE [dbo].[Ambiente]
ADD CONSTRAINT [PK_Ambiente]
    PRIMARY KEY CLUSTERED ([IdAmbiente] ASC);
GO

-- Creating primary key on [ID_RECURSO] in table 'AmbienteRecurso'
ALTER TABLE [dbo].[AmbienteRecurso]
ADD CONSTRAINT [PK_AmbienteRecurso]
    PRIMARY KEY CLUSTERED ([ID_RECURSO] ASC);
GO

-- Creating primary key on [IdAreaAtuacao] in table 'AreaAtuacao'
ALTER TABLE [dbo].[AreaAtuacao]
ADD CONSTRAINT [PK_AreaAtuacao]
    PRIMARY KEY CLUSTERED ([IdAreaAtuacao] ASC);
GO

-- Creating primary key on [IdCalendario] in table 'Calendario'
ALTER TABLE [dbo].[Calendario]
ADD CONSTRAINT [PK_Calendario]
    PRIMARY KEY CLUSTERED ([IdCalendario] ASC);
GO

-- Creating primary key on [ID_CATEGORIA_MAT] in table 'CatMaterial'
ALTER TABLE [dbo].[CatMaterial]
ADD CONSTRAINT [PK_CatMaterial]
    PRIMARY KEY CLUSTERED ([ID_CATEGORIA_MAT] ASC);
GO

-- Creating primary key on [IdCategoriaServico] in table 'CatServico'
ALTER TABLE [dbo].[CatServico]
ADD CONSTRAINT [PK_CatServico]
    PRIMARY KEY CLUSTERED ([IdCategoriaServico] ASC);
GO

-- Creating primary key on [IdCatUsuario] in table 'CatUsuario'
ALTER TABLE [dbo].[CatUsuario]
ADD CONSTRAINT [PK_CatUsuario]
    PRIMARY KEY CLUSTERED ([IdCatUsuario] ASC);
GO

-- Creating primary key on [IdCBO] in table 'CBO'
ALTER TABLE [dbo].[CBO]
ADD CONSTRAINT [PK_CBO]
    PRIMARY KEY CLUSTERED ([IdCBO] ASC);
GO

-- Creating primary key on [IdComponente] in table 'Componente'
ALTER TABLE [dbo].[Componente]
ADD CONSTRAINT [PK_Componente]
    PRIMARY KEY CLUSTERED ([IdComponente] ASC);
GO

-- Creating primary key on [IdCR] in table 'CR'
ALTER TABLE [dbo].[CR]
ADD CONSTRAINT [PK_CR]
    PRIMARY KEY CLUSTERED ([IdCR] ASC);
GO

-- Creating primary key on [IdCurso] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [PK_Curso]
    PRIMARY KEY CLUSTERED ([IdCurso] ASC);
GO

-- Creating primary key on [IdDocente] in table 'Docente'
ALTER TABLE [dbo].[Docente]
ADD CONSTRAINT [PK_Docente]
    PRIMARY KEY CLUSTERED ([IdDocente] ASC);
GO

-- Creating primary key on [IdEmpresa] in table 'Empresa'
ALTER TABLE [dbo].[Empresa]
ADD CONSTRAINT [PK_Empresa]
    PRIMARY KEY CLUSTERED ([IdEmpresa] ASC);
GO

-- Creating primary key on [IdLoc] in table 'LocalAmbiente'
ALTER TABLE [dbo].[LocalAmbiente]
ADD CONSTRAINT [PK_LocalAmbiente]
    PRIMARY KEY CLUSTERED ([IdLoc] ASC);
GO

-- Creating primary key on [IdMaterial] in table 'Material'
ALTER TABLE [dbo].[Material]
ADD CONSTRAINT [PK_Material]
    PRIMARY KEY CLUSTERED ([IdMaterial] ASC);
GO

-- Creating primary key on [IdMatriz] in table 'Matriz'
ALTER TABLE [dbo].[Matriz]
ADD CONSTRAINT [PK_Matriz]
    PRIMARY KEY CLUSTERED ([IdMatriz] ASC);
GO

-- Creating primary key on [IdModalidade] in table 'Modalidade'
ALTER TABLE [dbo].[Modalidade]
ADD CONSTRAINT [PK_Modalidade]
    PRIMARY KEY CLUSTERED ([IdModalidade] ASC);
GO

-- Creating primary key on [IdServico] in table 'Servico'
ALTER TABLE [dbo].[Servico]
ADD CONSTRAINT [PK_Servico]
    PRIMARY KEY CLUSTERED ([IdServico] ASC);
GO

-- Creating primary key on [IdTipoAmbiente] in table 'TipoAmbiente'
ALTER TABLE [dbo].[TipoAmbiente]
ADD CONSTRAINT [PK_TipoAmbiente]
    PRIMARY KEY CLUSTERED ([IdTipoAmbiente] ASC);
GO

-- Creating primary key on [IdTipoAtividade] in table 'TipoAtividade'
ALTER TABLE [dbo].[TipoAtividade]
ADD CONSTRAINT [PK_TipoAtividade]
    PRIMARY KEY CLUSTERED ([IdTipoAtividade] ASC);
GO

-- Creating primary key on [IdTipoContrato] in table 'TipoContrato'
ALTER TABLE [dbo].[TipoContrato]
ADD CONSTRAINT [PK_TipoContrato]
    PRIMARY KEY CLUSTERED ([IdTipoContrato] ASC);
GO

-- Creating primary key on [IdTurno] in table 'Turno'
ALTER TABLE [dbo].[Turno]
ADD CONSTRAINT [PK_Turno]
    PRIMARY KEY CLUSTERED ([IdTurno] ASC);
GO

-- Creating primary key on [IvUnCurricular] in table 'UnCurricular'
ALTER TABLE [dbo].[UnCurricular]
ADD CONSTRAINT [PK_UnCurricular]
    PRIMARY KEY CLUSTERED ([IvUnCurricular] ASC);
GO

-- Creating primary key on [IdUnidade] in table 'Unidade'
ALTER TABLE [dbo].[Unidade]
ADD CONSTRAINT [PK_Unidade]
    PRIMARY KEY CLUSTERED ([IdUnidade] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [AreaAtuacao_IdAreaAtuacao], [Usuario_IdUsuario] in table 'AreaOrientador'
ALTER TABLE [dbo].[AreaOrientador]
ADD CONSTRAINT [PK_AreaOrientador]
    PRIMARY KEY NONCLUSTERED ([AreaAtuacao_IdAreaAtuacao], [Usuario_IdUsuario] ASC);
GO

-- Creating primary key on [Componente_IdComponente], [TipoAmbiente_IdTipoAmbiente] in table 'ComponenteTipoAmbiente'
ALTER TABLE [dbo].[ComponenteTipoAmbiente]
ADD CONSTRAINT [PK_ComponenteTipoAmbiente]
    PRIMARY KEY NONCLUSTERED ([Componente_IdComponente], [TipoAmbiente_IdTipoAmbiente] ASC);
GO

-- Creating primary key on [CR_IdCR], [Unidade_IdUnidade] in table 'CrUNidade'
ALTER TABLE [dbo].[CrUNidade]
ADD CONSTRAINT [PK_CrUNidade]
    PRIMARY KEY NONCLUSTERED ([CR_IdCR], [Unidade_IdUnidade] ASC);
GO

-- Creating primary key on [Curso_IdCurso], [Material_IdMaterial] in table 'CursoMaterial'
ALTER TABLE [dbo].[CursoMaterial]
ADD CONSTRAINT [PK_CursoMaterial]
    PRIMARY KEY NONCLUSTERED ([Curso_IdCurso], [Material_IdMaterial] ASC);
GO

-- Creating primary key on [Curso_IdCurso], [Servico_IdServico] in table 'CursoServico'
ALTER TABLE [dbo].[CursoServico]
ADD CONSTRAINT [PK_CursoServico]
    PRIMARY KEY NONCLUSTERED ([Curso_IdCurso], [Servico_IdServico] ASC);
GO

-- Creating primary key on [Componente_IdComponente], [Matriz_IdMatriz] in table 'MatrizComponente'
ALTER TABLE [dbo].[MatrizComponente]
ADD CONSTRAINT [PK_MatrizComponente]
    PRIMARY KEY NONCLUSTERED ([Componente_IdComponente], [Matriz_IdMatriz] ASC);
GO

-- Creating primary key on [Material_IdMaterial], [Matriz_IdMatriz] in table 'MatrizMaterial'
ALTER TABLE [dbo].[MatrizMaterial]
ADD CONSTRAINT [PK_MatrizMaterial]
    PRIMARY KEY NONCLUSTERED ([Material_IdMaterial], [Matriz_IdMatriz] ASC);
GO

-- Creating primary key on [Matriz_IdMatriz], [Servico_IdServico] in table 'MatrizServico'
ALTER TABLE [dbo].[MatrizServico]
ADD CONSTRAINT [PK_MatrizServico]
    PRIMARY KEY NONCLUSTERED ([Matriz_IdMatriz], [Servico_IdServico] ASC);
GO

-- Creating primary key on [Modalidade_IdModalidade], [Unidade_IdUnidade] in table 'ModalidadeUnidade'
ALTER TABLE [dbo].[ModalidadeUnidade]
ADD CONSTRAINT [PK_ModalidadeUnidade]
    PRIMARY KEY NONCLUSTERED ([Modalidade_IdModalidade], [Unidade_IdUnidade] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdAmbiente] in table 'AgendaAmbiente'
ALTER TABLE [dbo].[AgendaAmbiente]
ADD CONSTRAINT [FK_AGENDAAM_AMBIENTE__AMBIENTE]
    FOREIGN KEY ([IdAmbiente])
    REFERENCES [dbo].[Ambiente]
        ([IdAmbiente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AGENDAAM_AMBIENTE__AMBIENTE'
CREATE INDEX [IX_FK_AGENDAAM_AMBIENTE__AMBIENTE]
ON [dbo].[AgendaAmbiente]
    ([IdAmbiente]);
GO

-- Creating foreign key on [IdComponente] in table 'AgendaComponente'
ALTER TABLE [dbo].[AgendaComponente]
ADD CONSTRAINT [FK_AGENDACO_AGENDA_CO_COMPONEN]
    FOREIGN KEY ([IdComponente])
    REFERENCES [dbo].[Componente]
        ([IdComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AGENDACO_AGENDA_CO_COMPONEN'
CREATE INDEX [IX_FK_AGENDACO_AGENDA_CO_COMPONEN]
ON [dbo].[AgendaComponente]
    ([IdComponente]);
GO

-- Creating foreign key on [IdUnidade] in table 'AgendaComponente'
ALTER TABLE [dbo].[AgendaComponente]
ADD CONSTRAINT [FK_AGENDACO_DOCENTE_U_UNIDADE]
    FOREIGN KEY ([IdUnidade])
    REFERENCES [dbo].[Unidade]
        ([IdUnidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AGENDACO_DOCENTE_U_UNIDADE'
CREATE INDEX [IX_FK_AGENDACO_DOCENTE_U_UNIDADE]
ON [dbo].[AgendaComponente]
    ([IdUnidade]);
GO

-- Creating foreign key on [IdAgendaComponente] in table 'Ambiente'
ALTER TABLE [dbo].[Ambiente]
ADD CONSTRAINT [FK_AMBIENTE_AGENDA_CO_AGENDACO]
    FOREIGN KEY ([IdAgendaComponente])
    REFERENCES [dbo].[AgendaComponente]
        ([IdAgendaComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AMBIENTE_AGENDA_CO_AGENDACO'
CREATE INDEX [IX_FK_AMBIENTE_AGENDA_CO_AGENDACO]
ON [dbo].[Ambiente]
    ([IdAgendaComponente]);
GO

-- Creating foreign key on [IdAgendaComponente] in table 'Docente'
ALTER TABLE [dbo].[Docente]
ADD CONSTRAINT [FK_DOCENTE_AGENDA_CO_AGENDACO]
    FOREIGN KEY ([IdAgendaComponente])
    REFERENCES [dbo].[AgendaComponente]
        ([IdAgendaComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DOCENTE_AGENDA_CO_AGENDACO'
CREATE INDEX [IX_FK_DOCENTE_AGENDA_CO_AGENDACO]
ON [dbo].[Docente]
    ([IdAgendaComponente]);
GO

-- Creating foreign key on [IdAgendaComponente] in table 'Empresa'
ALTER TABLE [dbo].[Empresa]
ADD CONSTRAINT [FK_EMPRESA_AGENDA_CO_AGENDACO]
    FOREIGN KEY ([IdAgendaComponente])
    REFERENCES [dbo].[AgendaComponente]
        ([IdAgendaComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EMPRESA_AGENDA_CO_AGENDACO'
CREATE INDEX [IX_FK_EMPRESA_AGENDA_CO_AGENDACO]
ON [dbo].[Empresa]
    ([IdAgendaComponente]);
GO

-- Creating foreign key on [IdTipoAtividade] in table 'AgendaDocente'
ALTER TABLE [dbo].[AgendaDocente]
ADD CONSTRAINT [FK_AGENDADO_AGENDA_DO_TIPOATIV]
    FOREIGN KEY ([IdTipoAtividade])
    REFERENCES [dbo].[TipoAtividade]
        ([IdTipoAtividade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AGENDADO_AGENDA_DO_TIPOATIV'
CREATE INDEX [IX_FK_AGENDADO_AGENDA_DO_TIPOATIV]
ON [dbo].[AgendaDocente]
    ([IdTipoAtividade]);
GO

-- Creating foreign key on [IdDocente] in table 'AgendaDocente'
ALTER TABLE [dbo].[AgendaDocente]
ADD CONSTRAINT [FK_AGENDADO_DOCENTE_A_DOCENTE]
    FOREIGN KEY ([IdDocente])
    REFERENCES [dbo].[Docente]
        ([IdDocente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AGENDADO_DOCENTE_A_DOCENTE'
CREATE INDEX [IX_FK_AGENDADO_DOCENTE_A_DOCENTE]
ON [dbo].[AgendaDocente]
    ([IdDocente]);
GO

-- Creating foreign key on [IdLoc] in table 'Ambiente'
ALTER TABLE [dbo].[Ambiente]
ADD CONSTRAINT [FK_AMBIENTE_AMBIENTE__LOCALAMB]
    FOREIGN KEY ([IdLoc])
    REFERENCES [dbo].[LocalAmbiente]
        ([IdLoc])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AMBIENTE_AMBIENTE__LOCALAMB'
CREATE INDEX [IX_FK_AMBIENTE_AMBIENTE__LOCALAMB]
ON [dbo].[Ambiente]
    ([IdLoc]);
GO

-- Creating foreign key on [IdTipoAmbiente] in table 'Ambiente'
ALTER TABLE [dbo].[Ambiente]
ADD CONSTRAINT [FK_AMBIENTE_AMBIENTE__TIPOAMBI]
    FOREIGN KEY ([IdTipoAmbiente])
    REFERENCES [dbo].[TipoAmbiente]
        ([IdTipoAmbiente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AMBIENTE_AMBIENTE__TIPOAMBI'
CREATE INDEX [IX_FK_AMBIENTE_AMBIENTE__TIPOAMBI]
ON [dbo].[Ambiente]
    ([IdTipoAmbiente]);
GO

-- Creating foreign key on [IdAmbiente] in table 'AmbienteRecurso'
ALTER TABLE [dbo].[AmbienteRecurso]
ADD CONSTRAINT [FK_RECURSOA_AMBIENTE__AMBIENTE]
    FOREIGN KEY ([IdAmbiente])
    REFERENCES [dbo].[Ambiente]
        ([IdAmbiente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RECURSOA_AMBIENTE__AMBIENTE'
CREATE INDEX [IX_FK_RECURSOA_AMBIENTE__AMBIENTE]
ON [dbo].[AmbienteRecurso]
    ([IdAmbiente]);
GO

-- Creating foreign key on [IdAreaAtuacao] in table 'Matriz'
ALTER TABLE [dbo].[Matriz]
ADD CONSTRAINT [FK_MATRIZ_MATRIZ_AR_AREAATUA]
    FOREIGN KEY ([IdAreaAtuacao])
    REFERENCES [dbo].[AreaAtuacao]
        ([IdAreaAtuacao])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MATRIZ_MATRIZ_AR_AREAATUA'
CREATE INDEX [IX_FK_MATRIZ_MATRIZ_AR_AREAATUA]
ON [dbo].[Matriz]
    ([IdAreaAtuacao]);
GO

-- Creating foreign key on [CalIdCalendario] in table 'Calendario'
ALTER TABLE [dbo].[Calendario]
ADD CONSTRAINT [FK_CALENDAR_CALENDARI_CALENDAR]
    FOREIGN KEY ([CalIdCalendario])
    REFERENCES [dbo].[Calendario]
        ([IdCalendario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CALENDAR_CALENDARI_CALENDAR'
CREATE INDEX [IX_FK_CALENDAR_CALENDARI_CALENDAR]
ON [dbo].[Calendario]
    ([CalIdCalendario]);
GO

-- Creating foreign key on [IdModalidade] in table 'Calendario'
ALTER TABLE [dbo].[Calendario]
ADD CONSTRAINT [FK_CALENDAR_CALENDARI_MODALIDA]
    FOREIGN KEY ([IdModalidade])
    REFERENCES [dbo].[Modalidade]
        ([IdModalidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CALENDAR_CALENDARI_MODALIDA'
CREATE INDEX [IX_FK_CALENDAR_CALENDARI_MODALIDA]
ON [dbo].[Calendario]
    ([IdModalidade]);
GO

-- Creating foreign key on [CatIdCategoriaMat] in table 'CatMaterial'
ALTER TABLE [dbo].[CatMaterial]
ADD CONSTRAINT [FK_CATMATER_CATEGORIA_CATMATER]
    FOREIGN KEY ([CatIdCategoriaMat])
    REFERENCES [dbo].[CatMaterial]
        ([ID_CATEGORIA_MAT])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CATMATER_CATEGORIA_CATMATER'
CREATE INDEX [IX_FK_CATMATER_CATEGORIA_CATMATER]
ON [dbo].[CatMaterial]
    ([CatIdCategoriaMat]);
GO

-- Creating foreign key on [ID_CATEGORIA_MAT] in table 'Material'
ALTER TABLE [dbo].[Material]
ADD CONSTRAINT [FK_MATERIAL_MATERIAL__CATMATER]
    FOREIGN KEY ([ID_CATEGORIA_MAT])
    REFERENCES [dbo].[CatMaterial]
        ([ID_CATEGORIA_MAT])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MATERIAL_MATERIAL__CATMATER'
CREATE INDEX [IX_FK_MATERIAL_MATERIAL__CATMATER]
ON [dbo].[Material]
    ([ID_CATEGORIA_MAT]);
GO

-- Creating foreign key on [CatIdCategoriaServico] in table 'CatServico'
ALTER TABLE [dbo].[CatServico]
ADD CONSTRAINT [FK_CATSERVI_CATEGORIA_CATSERVI]
    FOREIGN KEY ([CatIdCategoriaServico])
    REFERENCES [dbo].[CatServico]
        ([IdCategoriaServico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CATSERVI_CATEGORIA_CATSERVI'
CREATE INDEX [IX_FK_CATSERVI_CATEGORIA_CATSERVI]
ON [dbo].[CatServico]
    ([CatIdCategoriaServico]);
GO

-- Creating foreign key on [IdCategoriaServico] in table 'Servico'
ALTER TABLE [dbo].[Servico]
ADD CONSTRAINT [FK_SERVICO_SERVICO_C_CATSERVI]
    FOREIGN KEY ([IdCategoriaServico])
    REFERENCES [dbo].[CatServico]
        ([IdCategoriaServico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SERVICO_SERVICO_C_CATSERVI'
CREATE INDEX [IX_FK_SERVICO_SERVICO_C_CATSERVI]
ON [dbo].[Servico]
    ([IdCategoriaServico]);
GO

-- Creating foreign key on [IdCatUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_USUARIO_CATEGORIA_CATUSUAR]
    FOREIGN KEY ([IdCatUsuario])
    REFERENCES [dbo].[CatUsuario]
        ([IdCatUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_USUARIO_CATEGORIA_CATUSUAR'
CREATE INDEX [IX_FK_USUARIO_CATEGORIA_CATUSUAR]
ON [dbo].[Usuario]
    ([IdCatUsuario]);
GO

-- Creating foreign key on [IdCBO] in table 'Matriz'
ALTER TABLE [dbo].[Matriz]
ADD CONSTRAINT [FK_MATRIZ_MATRIZ_CB_CBO]
    FOREIGN KEY ([IdCBO])
    REFERENCES [dbo].[CBO]
        ([IdCBO])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MATRIZ_MATRIZ_CB_CBO'
CREATE INDEX [IX_FK_MATRIZ_MATRIZ_CB_CBO]
ON [dbo].[Matriz]
    ([IdCBO]);
GO

-- Creating foreign key on [IdComponente] in table 'UnCurricular'
ALTER TABLE [dbo].[UnCurricular]
ADD CONSTRAINT [FK_UNCURRIC_COMPONENT_COMPONEN]
    FOREIGN KEY ([IdComponente])
    REFERENCES [dbo].[Componente]
        ([IdComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UNCURRIC_COMPONENT_COMPONEN'
CREATE INDEX [IX_FK_UNCURRIC_COMPONENT_COMPONEN]
ON [dbo].[UnCurricular]
    ([IdComponente]);
GO

-- Creating foreign key on [IdModalidade] in table 'CR'
ALTER TABLE [dbo].[CR]
ADD CONSTRAINT [FK_CR_MODALIDE__MODALIDA]
    FOREIGN KEY ([IdModalidade])
    REFERENCES [dbo].[Modalidade]
        ([IdModalidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CR_MODALIDE__MODALIDA'
CREATE INDEX [IX_FK_CR_MODALIDE__MODALIDA]
ON [dbo].[CR]
    ([IdModalidade]);
GO

-- Creating foreign key on [IdCR] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_CURSO_CURSO_CR_CR]
    FOREIGN KEY ([IdCR])
    REFERENCES [dbo].[CR]
        ([IdCR])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CURSO_CURSO_CR_CR'
CREATE INDEX [IX_FK_CURSO_CURSO_CR_CR]
ON [dbo].[Curso]
    ([IdCR]);
GO

-- Creating foreign key on [IdTurno] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_CURSO_CURSO_TUR_TURNO]
    FOREIGN KEY ([IdTurno])
    REFERENCES [dbo].[Turno]
        ([IdTurno])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CURSO_CURSO_TUR_TURNO'
CREATE INDEX [IX_FK_CURSO_CURSO_TUR_TURNO]
ON [dbo].[Curso]
    ([IdTurno]);
GO

-- Creating foreign key on [IdUnidade] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_CURSO_CURSO_UNI_UNIDADE]
    FOREIGN KEY ([IdUnidade])
    REFERENCES [dbo].[Unidade]
        ([IdUnidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CURSO_CURSO_UNI_UNIDADE'
CREATE INDEX [IX_FK_CURSO_CURSO_UNI_UNIDADE]
ON [dbo].[Curso]
    ([IdUnidade]);
GO

-- Creating foreign key on [IdUsuario] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_CURSO_CURSO_USU_USUARIO]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuario]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CURSO_CURSO_USU_USUARIO'
CREATE INDEX [IX_FK_CURSO_CURSO_USU_USUARIO]
ON [dbo].[Curso]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdMatriz] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_CURSO_MATRIZ_CU_MATRIZ]
    FOREIGN KEY ([IdMatriz])
    REFERENCES [dbo].[Matriz]
        ([IdMatriz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CURSO_MATRIZ_CU_MATRIZ'
CREATE INDEX [IX_FK_CURSO_MATRIZ_CU_MATRIZ]
ON [dbo].[Curso]
    ([IdMatriz]);
GO

-- Creating foreign key on [IdTipoContrato] in table 'Docente'
ALTER TABLE [dbo].[Docente]
ADD CONSTRAINT [FK_DOCENTE_DOCENTE_T_TIPOCONT]
    FOREIGN KEY ([IdTipoContrato])
    REFERENCES [dbo].[TipoContrato]
        ([IdTipoContrato])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DOCENTE_DOCENTE_T_TIPOCONT'
CREATE INDEX [IX_FK_DOCENTE_DOCENTE_T_TIPOCONT]
ON [dbo].[Docente]
    ([IdTipoContrato]);
GO

-- Creating foreign key on [IdEmpresa] in table 'Docente'
ALTER TABLE [dbo].[Docente]
ADD CONSTRAINT [FK_DOCENTE_EMPRESA_D_EMPRESA]
    FOREIGN KEY ([IdEmpresa])
    REFERENCES [dbo].[Empresa]
        ([IdEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DOCENTE_EMPRESA_D_EMPRESA'
CREATE INDEX [IX_FK_DOCENTE_EMPRESA_D_EMPRESA]
ON [dbo].[Docente]
    ([IdEmpresa]);
GO

-- Creating foreign key on [IdModalidade] in table 'Matriz'
ALTER TABLE [dbo].[Matriz]
ADD CONSTRAINT [FK_MATRIZ_MATRIZ_MO_MODALIDA]
    FOREIGN KEY ([IdModalidade])
    REFERENCES [dbo].[Modalidade]
        ([IdModalidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MATRIZ_MATRIZ_MO_MODALIDA'
CREATE INDEX [IX_FK_MATRIZ_MATRIZ_MO_MODALIDA]
ON [dbo].[Matriz]
    ([IdModalidade]);
GO

-- Creating foreign key on [TIP_ID_TIPO_ATIVIDADE] in table 'TipoAtividade'
ALTER TABLE [dbo].[TipoAtividade]
ADD CONSTRAINT [FK_TIPOATIV_TIPO_ATIV_TIPOATIV]
    FOREIGN KEY ([TIP_ID_TIPO_ATIVIDADE])
    REFERENCES [dbo].[TipoAtividade]
        ([IdTipoAtividade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TIPOATIV_TIPO_ATIV_TIPOATIV'
CREATE INDEX [IX_FK_TIPOATIV_TIPO_ATIV_TIPOATIV]
ON [dbo].[TipoAtividade]
    ([TIP_ID_TIPO_ATIVIDADE]);
GO

-- Creating foreign key on [AreaAtuacao_IdAreaAtuacao] in table 'AreaOrientador'
ALTER TABLE [dbo].[AreaOrientador]
ADD CONSTRAINT [FK_AreaOrientador_AreaAtuacao]
    FOREIGN KEY ([AreaAtuacao_IdAreaAtuacao])
    REFERENCES [dbo].[AreaAtuacao]
        ([IdAreaAtuacao])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Usuario_IdUsuario] in table 'AreaOrientador'
ALTER TABLE [dbo].[AreaOrientador]
ADD CONSTRAINT [FK_AreaOrientador_Usuario]
    FOREIGN KEY ([Usuario_IdUsuario])
    REFERENCES [dbo].[Usuario]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AreaOrientador_Usuario'
CREATE INDEX [IX_FK_AreaOrientador_Usuario]
ON [dbo].[AreaOrientador]
    ([Usuario_IdUsuario]);
GO

-- Creating foreign key on [Componente_IdComponente] in table 'ComponenteTipoAmbiente'
ALTER TABLE [dbo].[ComponenteTipoAmbiente]
ADD CONSTRAINT [FK_ComponenteTipoAmbiente_Componente]
    FOREIGN KEY ([Componente_IdComponente])
    REFERENCES [dbo].[Componente]
        ([IdComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TipoAmbiente_IdTipoAmbiente] in table 'ComponenteTipoAmbiente'
ALTER TABLE [dbo].[ComponenteTipoAmbiente]
ADD CONSTRAINT [FK_ComponenteTipoAmbiente_TipoAmbiente]
    FOREIGN KEY ([TipoAmbiente_IdTipoAmbiente])
    REFERENCES [dbo].[TipoAmbiente]
        ([IdTipoAmbiente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ComponenteTipoAmbiente_TipoAmbiente'
CREATE INDEX [IX_FK_ComponenteTipoAmbiente_TipoAmbiente]
ON [dbo].[ComponenteTipoAmbiente]
    ([TipoAmbiente_IdTipoAmbiente]);
GO

-- Creating foreign key on [CR_IdCR] in table 'CrUNidade'
ALTER TABLE [dbo].[CrUNidade]
ADD CONSTRAINT [FK_CrUNidade_CR]
    FOREIGN KEY ([CR_IdCR])
    REFERENCES [dbo].[CR]
        ([IdCR])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Unidade_IdUnidade] in table 'CrUNidade'
ALTER TABLE [dbo].[CrUNidade]
ADD CONSTRAINT [FK_CrUNidade_Unidade]
    FOREIGN KEY ([Unidade_IdUnidade])
    REFERENCES [dbo].[Unidade]
        ([IdUnidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CrUNidade_Unidade'
CREATE INDEX [IX_FK_CrUNidade_Unidade]
ON [dbo].[CrUNidade]
    ([Unidade_IdUnidade]);
GO

-- Creating foreign key on [Curso_IdCurso] in table 'CursoMaterial'
ALTER TABLE [dbo].[CursoMaterial]
ADD CONSTRAINT [FK_CursoMaterial_Curso]
    FOREIGN KEY ([Curso_IdCurso])
    REFERENCES [dbo].[Curso]
        ([IdCurso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Material_IdMaterial] in table 'CursoMaterial'
ALTER TABLE [dbo].[CursoMaterial]
ADD CONSTRAINT [FK_CursoMaterial_Material]
    FOREIGN KEY ([Material_IdMaterial])
    REFERENCES [dbo].[Material]
        ([IdMaterial])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CursoMaterial_Material'
CREATE INDEX [IX_FK_CursoMaterial_Material]
ON [dbo].[CursoMaterial]
    ([Material_IdMaterial]);
GO

-- Creating foreign key on [Curso_IdCurso] in table 'CursoServico'
ALTER TABLE [dbo].[CursoServico]
ADD CONSTRAINT [FK_CursoServico_Curso]
    FOREIGN KEY ([Curso_IdCurso])
    REFERENCES [dbo].[Curso]
        ([IdCurso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Servico_IdServico] in table 'CursoServico'
ALTER TABLE [dbo].[CursoServico]
ADD CONSTRAINT [FK_CursoServico_Servico]
    FOREIGN KEY ([Servico_IdServico])
    REFERENCES [dbo].[Servico]
        ([IdServico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CursoServico_Servico'
CREATE INDEX [IX_FK_CursoServico_Servico]
ON [dbo].[CursoServico]
    ([Servico_IdServico]);
GO

-- Creating foreign key on [Componente_IdComponente] in table 'MatrizComponente'
ALTER TABLE [dbo].[MatrizComponente]
ADD CONSTRAINT [FK_MatrizComponente_Componente]
    FOREIGN KEY ([Componente_IdComponente])
    REFERENCES [dbo].[Componente]
        ([IdComponente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Matriz_IdMatriz] in table 'MatrizComponente'
ALTER TABLE [dbo].[MatrizComponente]
ADD CONSTRAINT [FK_MatrizComponente_Matriz]
    FOREIGN KEY ([Matriz_IdMatriz])
    REFERENCES [dbo].[Matriz]
        ([IdMatriz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrizComponente_Matriz'
CREATE INDEX [IX_FK_MatrizComponente_Matriz]
ON [dbo].[MatrizComponente]
    ([Matriz_IdMatriz]);
GO

-- Creating foreign key on [Material_IdMaterial] in table 'MatrizMaterial'
ALTER TABLE [dbo].[MatrizMaterial]
ADD CONSTRAINT [FK_MatrizMaterial_Material]
    FOREIGN KEY ([Material_IdMaterial])
    REFERENCES [dbo].[Material]
        ([IdMaterial])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Matriz_IdMatriz] in table 'MatrizMaterial'
ALTER TABLE [dbo].[MatrizMaterial]
ADD CONSTRAINT [FK_MatrizMaterial_Matriz]
    FOREIGN KEY ([Matriz_IdMatriz])
    REFERENCES [dbo].[Matriz]
        ([IdMatriz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrizMaterial_Matriz'
CREATE INDEX [IX_FK_MatrizMaterial_Matriz]
ON [dbo].[MatrizMaterial]
    ([Matriz_IdMatriz]);
GO

-- Creating foreign key on [Matriz_IdMatriz] in table 'MatrizServico'
ALTER TABLE [dbo].[MatrizServico]
ADD CONSTRAINT [FK_MatrizServico_Matriz]
    FOREIGN KEY ([Matriz_IdMatriz])
    REFERENCES [dbo].[Matriz]
        ([IdMatriz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Servico_IdServico] in table 'MatrizServico'
ALTER TABLE [dbo].[MatrizServico]
ADD CONSTRAINT [FK_MatrizServico_Servico]
    FOREIGN KEY ([Servico_IdServico])
    REFERENCES [dbo].[Servico]
        ([IdServico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrizServico_Servico'
CREATE INDEX [IX_FK_MatrizServico_Servico]
ON [dbo].[MatrizServico]
    ([Servico_IdServico]);
GO

-- Creating foreign key on [Modalidade_IdModalidade] in table 'ModalidadeUnidade'
ALTER TABLE [dbo].[ModalidadeUnidade]
ADD CONSTRAINT [FK_ModalidadeUnidade_Modalidade]
    FOREIGN KEY ([Modalidade_IdModalidade])
    REFERENCES [dbo].[Modalidade]
        ([IdModalidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Unidade_IdUnidade] in table 'ModalidadeUnidade'
ALTER TABLE [dbo].[ModalidadeUnidade]
ADD CONSTRAINT [FK_ModalidadeUnidade_Unidade]
    FOREIGN KEY ([Unidade_IdUnidade])
    REFERENCES [dbo].[Unidade]
        ([IdUnidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ModalidadeUnidade_Unidade'
CREATE INDEX [IX_FK_ModalidadeUnidade_Unidade]
ON [dbo].[ModalidadeUnidade]
    ([Unidade_IdUnidade]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------