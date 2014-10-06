USE master
GO

ALTER DATABASE SPE SET SINGLE_USER WITH ROLLBACK IMMEDIATE 
GO

DROP DATABASE SPE
GO

CREATE DATABASE SPE
GO

USE [SPE]
GO

/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     6/25/2013 2:10:03 PM                         */
/*==============================================================*/


if exists (select 1
            from  sysindexes
           where  id    = object_id('AgendaAmbiente')
            and   name  = 'IX_IDAMBIENTE'
            and   indid > 0
            and   indid < 255)
   drop index AgendaAmbiente.IX_IDAMBIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AgendaAmbiente')
            and   type = 'U')
   drop table AgendaAmbiente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AgendaComponente')
            and   name  = 'IX_IDUNIDADE'
            and   indid > 0
            and   indid < 255)
   drop index AgendaComponente.IX_IDUNIDADE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AgendaComponente')
            and   name  = 'IX_IDCOMPONENTE'
            and   indid > 0
            and   indid < 255)
   drop index AgendaComponente.IX_IDCOMPONENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AgendaComponente')
            and   type = 'U')
   drop table AgendaComponente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AgendaDocente')
            and   name  = 'IX_IDTIPOATIVIDADE'
            and   indid > 0
            and   indid < 255)
   drop index AgendaDocente.IX_IDTIPOATIVIDADE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AgendaDocente')
            and   name  = 'IX_IDDOCENTE'
            and   indid > 0
            and   indid < 255)
   drop index AgendaDocente.IX_IDDOCENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AgendaDocente')
            and   type = 'U')
   drop table AgendaDocente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Ambiente')
            and   name  = 'IX_IDLOCALAMBIENTE'
            and   indid > 0
            and   indid < 255)
   drop index Ambiente.IX_IDLOCALAMBIENTE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Ambiente')
            and   name  = 'IX_IDAGENDCOMPAMB'
            and   indid > 0
            and   indid < 255)
   drop index Ambiente.IX_IDAGENDCOMPAMB
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Ambiente')
            and   name  = 'IX_IDTIPOAMBIENTE'
            and   indid > 0
            and   indid < 255)
   drop index Ambiente.IX_IDTIPOAMBIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Ambiente')
            and   type = 'U')
   drop table Ambiente
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AreaAtuacao')
            and   type = 'U')
   drop table AreaAtuacao
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CBO')
            and   type = 'U')
   drop table CBO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CR')
            and   name  = 'UX_IDMODALIDADE'
            and   indid > 0
            and   indid < 255)
   drop index CR.UX_IDMODALIDADE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CR')
            and   type = 'U')
   drop table CR
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Calendario')
            and   name  = 'IX_IDCALENDARIO'
            and   indid > 0
            and   indid < 255)
   drop index Calendario.IX_IDCALENDARIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Calendario')
            and   name  = 'IX_IDMODALIDADE'
            and   indid > 0
            and   indid < 255)
   drop index Calendario.IX_IDMODALIDADE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Calendario')
            and   type = 'U')
   drop table Calendario
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CatMaterial')
            and   name  = 'IX_IDCATMATERIAL'
            and   indid > 0
            and   indid < 255)
   drop index CatMaterial.IX_IDCATMATERIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CatMaterial')
            and   type = 'U')
   drop table CatMaterial
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CatServico')
            and   name  = 'IX_IDCATSERV'
            and   indid > 0
            and   indid < 255)
   drop index CatServico.IX_IDCATSERV
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CatServico')
            and   type = 'U')
   drop table CatServico
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CatUsuario')
            and   type = 'U')
   drop table CatUsuario
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Competencia')
            and   type = 'U')
   drop table Competencia
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CompetenciaDocente')
            and   name  = 'IX_IDCOMPETENCIA'
            and   indid > 0
            and   indid < 255)
   drop index CompetenciaDocente.IX_IDCOMPETENCIA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CompetenciaDocente')
            and   name  = 'IX_IDDOCENTE'
            and   indid > 0
            and   indid < 255)
   drop index CompetenciaDocente.IX_IDDOCENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CompetenciaDocente')
            and   type = 'U')
   drop table CompetenciaDocente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CompetenciaEmpresa')
            and   name  = 'IX_IDCOMPETENCIA'
            and   indid > 0
            and   indid < 255)
   drop index CompetenciaEmpresa.IX_IDCOMPETENCIA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CompetenciaEmpresa')
            and   name  = 'IX_IDEMPRESA'
            and   indid > 0
            and   indid < 255)
   drop index CompetenciaEmpresa.IX_IDEMPRESA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CompetenciaEmpresa')
            and   type = 'U')
   drop table CompetenciaEmpresa
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Componente')
            and   type = 'U')
   drop table Componente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ComponenteTipoAmbiente')
            and   name  = 'IX_IDCOMPONENTE'
            and   indid > 0
            and   indid < 255)
   drop index ComponenteTipoAmbiente.IX_IDCOMPONENTE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ComponenteTipoAmbiente')
            and   name  = 'IX_IDTIPOAMBIENTE'
            and   indid > 0
            and   indid < 255)
   drop index ComponenteTipoAmbiente.IX_IDTIPOAMBIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ComponenteTipoAmbiente')
            and   type = 'U')
   drop table ComponenteTipoAmbiente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CrUnidade')
            and   name  = 'IX_IDUNIDADE'
            and   indid > 0
            and   indid < 255)
   drop index CrUnidade.IX_IDUNIDADE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CrUnidade')
            and   name  = 'IX_IDCR'
            and   indid > 0
            and   indid < 255)
   drop index CrUnidade.IX_IDCR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CrUnidade')
            and   type = 'U')
   drop table CrUnidade
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Curso')
            and   name  = 'IX_IDCR'
            and   indid > 0
            and   indid < 255)
   drop index Curso.IX_IDCR
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Curso')
            and   name  = 'IX_IDUSUARIO'
            and   indid > 0
            and   indid < 255)
   drop index Curso.IX_IDUSUARIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Curso')
            and   name  = 'IX_IDTURNO'
            and   indid > 0
            and   indid < 255)
   drop index Curso.IX_IDTURNO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Curso')
            and   name  = 'IX_IDUNIDADE'
            and   indid > 0
            and   indid < 255)
   drop index Curso.IX_IDUNIDADE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Curso')
            and   name  = 'IX_IDMATRIZ'
            and   indid > 0
            and   indid < 255)
   drop index Curso.IX_IDMATRIZ
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Curso')
            and   type = 'U')
   drop table Curso
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CursoMaterial')
            and   name  = 'CURSO_MATERIAL2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CursoMaterial.CURSO_MATERIAL2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CursoMaterial')
            and   name  = 'CURSO_MATERIAL_FK'
            and   indid > 0
            and   indid < 255)
   drop index CursoMaterial.CURSO_MATERIAL_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CursoMaterial')
            and   type = 'U')
   drop table CursoMaterial
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CursoServico')
            and   name  = 'IX_IDSERVICO'
            and   indid > 0
            and   indid < 255)
   drop index CursoServico.IX_IDSERVICO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CursoServico')
            and   name  = 'IX_IDCURSO'
            and   indid > 0
            and   indid < 255)
   drop index CursoServico.IX_IDCURSO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CursoServico')
            and   type = 'U')
   drop table CursoServico
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Docente')
            and   name  = 'IX_IDTIPOCONTRATO'
            and   indid > 0
            and   indid < 255)
   drop index Docente.IX_IDTIPOCONTRATO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Docente')
            and   name  = 'IX_IDAGENDACOMPDOC'
            and   indid > 0
            and   indid < 255)
   drop index Docente.IX_IDAGENDACOMPDOC
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Docente')
            and   name  = 'IX_IDEMPRESA'
            and   indid > 0
            and   indid < 255)
   drop index Docente.IX_IDEMPRESA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Docente')
            and   type = 'U')
   drop table Docente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Empresa')
            and   name  = 'IX_IDAGENCOMPEMP'
            and   indid > 0
            and   indid < 255)
   drop index Empresa.IX_IDAGENCOMPEMP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Empresa')
            and   type = 'U')
   drop table Empresa
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LocalAmbiente')
            and   type = 'U')
   drop table LocalAmbiente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Material')
            and   name  = 'IX_IDCATMATERIAL'
            and   indid > 0
            and   indid < 255)
   drop index Material.IX_IDCATMATERIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Material')
            and   type = 'U')
   drop table Material
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Matriz')
            and   name  = 'IX_IDCBO'
            and   indid > 0
            and   indid < 255)
   drop index Matriz.IX_IDCBO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Matriz')
            and   name  = 'IX_IDMODALIDADE'
            and   indid > 0
            and   indid < 255)
   drop index Matriz.IX_IDMODALIDADE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Matriz')
            and   name  = 'IX_IDAREAATUACAO'
            and   indid > 0
            and   indid < 255)
   drop index Matriz.IX_IDAREAATUACAO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Matriz')
            and   type = 'U')
   drop table Matriz
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MatrizComponente')
            and   name  = 'IX_IDCOMPONENTE'
            and   indid > 0
            and   indid < 255)
   drop index MatrizComponente.IX_IDCOMPONENTE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MatrizComponente')
            and   name  = 'IX_IDMATRIZ'
            and   indid > 0
            and   indid < 255)
   drop index MatrizComponente.IX_IDMATRIZ
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MatrizComponente')
            and   type = 'U')
   drop table MatrizComponente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MatrizMaterial')
            and   name  = 'IX_IDMATERIAL'
            and   indid > 0
            and   indid < 255)
   drop index MatrizMaterial.IX_IDMATERIAL
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MatrizMaterial')
            and   name  = 'IX_IDMATRIZ'
            and   indid > 0
            and   indid < 255)
   drop index MatrizMaterial.IX_IDMATRIZ
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MatrizMaterial')
            and   type = 'U')
   drop table MatrizMaterial
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MatrizServico')
            and   name  = 'IX_IDSERVICO'
            and   indid > 0
            and   indid < 255)
   drop index MatrizServico.IX_IDSERVICO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MatrizServico')
            and   name  = 'IX_IDMATRIZ'
            and   indid > 0
            and   indid < 255)
   drop index MatrizServico.IX_IDMATRIZ
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MatrizServico')
            and   type = 'U')
   drop table MatrizServico
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Modalidade')
            and   type = 'U')
   drop table Modalidade
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ModalidadeUnidade')
            and   name  = 'IX_IDUNIDADE'
            and   indid > 0
            and   indid < 255)
   drop index ModalidadeUnidade.IX_IDUNIDADE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ModalidadeUnidade')
            and   name  = 'IX_IDMODALIDADE'
            and   indid > 0
            and   indid < 255)
   drop index ModalidadeUnidade.IX_IDMODALIDADE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ModalidadeUnidade')
            and   type = 'U')
   drop table ModalidadeUnidade
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrientadorArea')
            and   name  = 'IX_IDUSUARIO'
            and   indid > 0
            and   indid < 255)
   drop index OrientadorArea.IX_IDUSUARIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrientadorArea')
            and   name  = 'IX_IDAREAATUACAO'
            and   indid > 0
            and   indid < 255)
   drop index OrientadorArea.IX_IDAREAATUACAO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrientadorArea')
            and   type = 'U')
   drop table OrientadorArea
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RecursoAmbiente')
            and   name  = 'IX_IDAMBIENTE'
            and   indid > 0
            and   indid < 255)
   drop index RecursoAmbiente.IX_IDAMBIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RecursoAmbiente')
            and   type = 'U')
   drop table RecursoAmbiente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Servico')
            and   name  = 'IX_IDCATSERV'
            and   indid > 0
            and   indid < 255)
   drop index Servico.IX_IDCATSERV
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Servico')
            and   type = 'U')
   drop table Servico
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TipoAmbiente')
            and   type = 'U')
   drop table TipoAmbiente
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TipoAtividade')
            and   name  = 'IX_IDTIPOATIVIDADE'
            and   indid > 0
            and   indid < 255)
   drop index TipoAtividade.IX_IDTIPOATIVIDADE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TipoAtividade')
            and   type = 'U')
   drop table TipoAtividade
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TipoContrato')
            and   type = 'U')
   drop table TipoContrato
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Turno')
            and   type = 'U')
   drop table Turno
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UnCurricular')
            and   name  = 'IX_IDCOMPONENTE'
            and   indid > 0
            and   indid < 255)
   drop index UnCurricular.IX_IDCOMPONENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UnCurricular')
            and   type = 'U')
   drop table UnCurricular
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Unidade')
            and   type = 'U')
   drop table Unidade
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Usuario')
            and   name  = 'IX_IDCATUSUARIO'
            and   indid > 0
            and   indid < 255)
   drop index Usuario.IX_IDCATUSUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Usuario')
            and   type = 'U')
   drop table Usuario
go

/*==============================================================*/
/* Table: AgendaAmbiente                                        */
/*==============================================================*/
create table AgendaAmbiente (
   Id                   int                  not null,
   IdAmbiente           int                  null,
   DataIni              datetime             null,
   DataFim              datetime             null,
   HoraIni              datetime             null,
   HoraFim              datetime             null,
   DiasSemana           varchar(7)           null,
   constraint PK_AGENDAAMBIENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDAMBIENTE                                         */
/*==============================================================*/
create index IX_IDAMBIENTE on AgendaAmbiente (
IdAmbiente ASC
)
go

/*==============================================================*/
/* Table: AgendaComponente                                      */
/*==============================================================*/
create table AgendaComponente (
   Id                   int                  not null,
   IdComponente         int                  null,
   IdUnidade            int                  null,
   DataIni              datetime             null,
   DataFim              datetime             null,
   HoraIni              datetime             null,
   HoraFim              datetime             null,
   StDefinir            bit                  null,
   DiasSemana           varchar(7)           null,
   constraint PK_AGENDACOMPONENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDCOMPONENTE                                       */
/*==============================================================*/
create index IX_IDCOMPONENTE on AgendaComponente (
IdComponente ASC
)
go

/*==============================================================*/
/* Index: IX_IDUNIDADE                                          */
/*==============================================================*/
create index IX_IDUNIDADE on AgendaComponente (
IdUnidade ASC
)
go

/*==============================================================*/
/* Table: AgendaDocente                                         */
/*==============================================================*/
create table AgendaDocente (
   Id                   int                  not null,
   IdDocente            int                  null,
   IdTipoAtividade      int                  null,
   DescricaoAtiv        varchar(512)         null,
   DataIni              datetime             null,
   DataFim              datetime             null,
   HoraIni              datetime             null,
   HoraFim              datetime             null,
   DiasSemana           varchar(7)           null,
   CH                   smallint             null,
   constraint PK_AGENDADOCENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDDOCENTE                                          */
/*==============================================================*/
create index IX_IDDOCENTE on AgendaDocente (
IdDocente ASC
)
go

/*==============================================================*/
/* Index: IX_IDTIPOATIVIDADE                                    */
/*==============================================================*/
create index IX_IDTIPOATIVIDADE on AgendaDocente (
IdTipoAtividade ASC
)
go

/*==============================================================*/
/* Table: Ambiente                                              */
/*==============================================================*/
create table Ambiente (
   Id                   int                  not null,
   IdLocalAmbiente      int                  null,
   IdTipoAmbiente       int                  null,
   IdAgendCompAmb       int                  null,
   Capac                smallint             null,
   constraint PK_AMBIENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDTIPOAMBIENTE                                     */
/*==============================================================*/
create index IX_IDTIPOAMBIENTE on Ambiente (
IdTipoAmbiente ASC
)
go

/*==============================================================*/
/* Index: IX_IDAGENDCOMPAMB                                     */
/*==============================================================*/
create index IX_IDAGENDCOMPAMB on Ambiente (
IdAgendCompAmb ASC
)
go

/*==============================================================*/
/* Index: IX_IDLOCALAMBIENTE                                    */
/*==============================================================*/
create index IX_IDLOCALAMBIENTE on Ambiente (
IdLocalAmbiente ASC
)
go

/*==============================================================*/
/* Table: AreaAtuacao                                           */
/*==============================================================*/
create table AreaAtuacao (
   Id                   int                  not null,
   Nome                 varchar(256)         null,
   constraint PK_AREAATUACAO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: CBO                                                   */
/*==============================================================*/
create table CBO (
   Id                   int                  not null,
   constraint PK_CBO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: CR                                                    */
/*==============================================================*/
create table CR (
   Id                   int                  not null,
   IdModalidade         int                  null,
   constraint PK_CR primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: UX_IDMODALIDADE                                       */
/*==============================================================*/
create index UX_IDMODALIDADE on CR (
IdModalidade ASC
)
go

/*==============================================================*/
/* Table: Calendario                                            */
/*==============================================================*/
create table Calendario (
   Id                   int                  not null,
   IdModalidade         int                  null,
   IdCalendario         int                  null,
   Ano                  char(10)             null,
   constraint PK_CALENDARIO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDMODALIDADE                                       */
/*==============================================================*/
create index IX_IDMODALIDADE on Calendario (
IdModalidade ASC
)
go

/*==============================================================*/
/* Index: IX_IDCALENDARIO                                       */
/*==============================================================*/
create index IX_IDCALENDARIO on Calendario (
IdCalendario ASC
)
go

/*==============================================================*/
/* Table: CatMaterial                                           */
/*==============================================================*/
create table CatMaterial (
   Id                   int                  not null,
   IdCatMaterial        int                  null,
   NomeCat              varchar(64)          null,
   constraint PK_CATMATERIAL primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDCATMATERIAL                                      */
/*==============================================================*/
create index IX_IDCATMATERIAL on CatMaterial (
IdCatMaterial ASC
)
go

/*==============================================================*/
/* Table: CatServico                                            */
/*==============================================================*/
create table CatServico (
   Id                   int                  not null,
   Cat_ID_CATEGORIA_SERVICO int                  null,
   NomeCatServ          varchar(64)          null,
   constraint PK_CATSERVICO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDCATSERV                                          */
/*==============================================================*/
create index IX_IDCATSERV on CatServico (
Cat_ID_CATEGORIA_SERVICO ASC
)
go

/*==============================================================*/
/* Table: CatUsuario                                            */
/*==============================================================*/
create table CatUsuario (
   Id                   int                  not null,
   constraint PK_CATUSUARIO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: Competencia                                           */
/*==============================================================*/
create table Competencia (
   Id                   int                  not null,
   constraint PK_COMPETENCIA primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: CompetenciaDocente                                    */
/*==============================================================*/
create table CompetenciaDocente (
   IdDocente            int                  not null,
   IdCompetencia        int                  not null,
   constraint PK_COMPETENCIADOCENTE primary key (IdDocente, IdCompetencia)
)
go

/*==============================================================*/
/* Index: IX_IDDOCENTE                                          */
/*==============================================================*/
create index IX_IDDOCENTE on CompetenciaDocente (
IdDocente ASC
)
go

/*==============================================================*/
/* Index: IX_IDCOMPETENCIA                                      */
/*==============================================================*/
create index IX_IDCOMPETENCIA on CompetenciaDocente (
IdCompetencia ASC
)
go

/*==============================================================*/
/* Table: CompetenciaEmpresa                                    */
/*==============================================================*/
create table CompetenciaEmpresa (
   ID_EMPRESA           int                  not null,
   ID_COMPETENCIA       int                  not null,
   constraint PK_COMPETENCIAEMPRESA primary key (ID_EMPRESA, ID_COMPETENCIA)
)
go

/*==============================================================*/
/* Index: IX_IDEMPRESA                                          */
/*==============================================================*/
create index IX_IDEMPRESA on CompetenciaEmpresa (
ID_EMPRESA ASC
)
go

/*==============================================================*/
/* Index: IX_IDCOMPETENCIA                                      */
/*==============================================================*/
create index IX_IDCOMPETENCIA on CompetenciaEmpresa (
ID_COMPETENCIA ASC
)
go

/*==============================================================*/
/* Table: Componente                                            */
/*==============================================================*/
create table Componente (
   Id                   int                  not null,
   Nome                 varchar(256)         null,
   CH                   smallint             null,
   constraint PK_COMPONENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: ComponenteTipoAmbiente                                */
/*==============================================================*/
create table ComponenteTipoAmbiente (
   IdTipoAmbiente       int                  not null,
   IdComponente         int                  not null,
   constraint PK_COMPONENTETIPOAMBIENTE primary key (IdTipoAmbiente, IdComponente)
)
go

/*==============================================================*/
/* Index: IX_IDTIPOAMBIENTE                                     */
/*==============================================================*/
create index IX_IDTIPOAMBIENTE on ComponenteTipoAmbiente (
IdTipoAmbiente ASC
)
go

/*==============================================================*/
/* Index: IX_IDCOMPONENTE                                       */
/*==============================================================*/
create index IX_IDCOMPONENTE on ComponenteTipoAmbiente (
IdComponente ASC
)
go

/*==============================================================*/
/* Table: CrUnidade                                             */
/*==============================================================*/
create table CrUnidade (
   IdCr                 int                  not null,
   IdUnidade            int                  not null,
   constraint PK_CRUNIDADE primary key (IdCr, IdUnidade)
)
go

/*==============================================================*/
/* Index: IX_IDCR                                               */
/*==============================================================*/
create index IX_IDCR on CrUnidade (
IdCr ASC
)
go

/*==============================================================*/
/* Index: IX_IDUNIDADE                                          */
/*==============================================================*/
create index IX_IDUNIDADE on CrUnidade (
IdUnidade ASC
)
go

/*==============================================================*/
/* Table: Curso                                                 */
/*==============================================================*/
create table Curso (
   Id                   int                  not null,
   IdUnidade            int                  null,
   IdTurno              int                  null,
   IdCr                 int                  null,
   IdMatriz             int                  null,
   IdUsuario            int                  null,
   QtdeVagas            smallint             null,
   DataFim              datetime             null,
   Status               smallint             null,
   TipoOferta           smallint             null,
   Preco                money                null,
   Evento               int                  null,
   constraint PK_CURSO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDMATRIZ                                           */
/*==============================================================*/
create index IX_IDMATRIZ on Curso (
IdMatriz ASC
)
go

/*==============================================================*/
/* Index: IX_IDUNIDADE                                          */
/*==============================================================*/
create index IX_IDUNIDADE on Curso (
IdUnidade ASC
)
go

/*==============================================================*/
/* Index: IX_IDTURNO                                            */
/*==============================================================*/
create index IX_IDTURNO on Curso (
IdTurno ASC
)
go

/*==============================================================*/
/* Index: IX_IDUSUARIO                                          */
/*==============================================================*/
create index IX_IDUSUARIO on Curso (
IdUsuario ASC
)
go

/*==============================================================*/
/* Index: IX_IDCR                                               */
/*==============================================================*/
create index IX_IDCR on Curso (
IdCr ASC
)
go

/*==============================================================*/
/* Table: CursoMaterial                                         */
/*==============================================================*/
create table CursoMaterial (
   ID_CURSO             int                  not null,
   IdMaterial           int                  not null,
   constraint PK_CURSOMATERIAL primary key (ID_CURSO, IdMaterial)
)
go

/*==============================================================*/
/* Index: CURSO_MATERIAL_FK                                     */
/*==============================================================*/
create index CURSO_MATERIAL_FK on CursoMaterial (
ID_CURSO ASC
)
go

/*==============================================================*/
/* Index: CURSO_MATERIAL2_FK                                    */
/*==============================================================*/
create index CURSO_MATERIAL2_FK on CursoMaterial (
IdMaterial ASC
)
go

/*==============================================================*/
/* Table: CursoServico                                          */
/*==============================================================*/
create table CursoServico (
   IdCurso              int                  not null,
   IdServico            int                  not null,
   constraint PK_CURSOSERVICO primary key (IdCurso, IdServico)
)
go

/*==============================================================*/
/* Index: IX_IDCURSO                                            */
/*==============================================================*/
create index IX_IDCURSO on CursoServico (
IdCurso ASC
)
go

/*==============================================================*/
/* Index: IX_IDSERVICO                                          */
/*==============================================================*/
create index IX_IDSERVICO on CursoServico (
IdServico ASC
)
go

/*==============================================================*/
/* Table: Docente                                               */
/*==============================================================*/
create table Docente (
   Id                   int                  not null,
   IdAgendaCompDocente  int                  null,
   IdTipoContrato       int                  null,
   IdEmpresa            int                  null,
   Cpf                  varchar(15)          null,
   Email                varchar(64)          null,
   Sexo                 tinyint              null,
   Tel                  varchar(32)          null,
   NivelFuncao          smallint             null,
   constraint PK_DOCENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDEMPRESA                                          */
/*==============================================================*/
create index IX_IDEMPRESA on Docente (
IdEmpresa ASC
)
go

/*==============================================================*/
/* Index: IX_IDAGENDACOMPDOC                                    */
/*==============================================================*/
create index IX_IDAGENDACOMPDOC on Docente (
IdAgendaCompDocente ASC
)
go

/*==============================================================*/
/* Index: IX_IDTIPOCONTRATO                                     */
/*==============================================================*/
create index IX_IDTIPOCONTRATO on Docente (
IdTipoContrato ASC
)
go

/*==============================================================*/
/* Table: Empresa                                               */
/*==============================================================*/
create table Empresa (
   ID_EMPRESA           int                  not null,
   IdAgendaCompEmp      int                  null,
   NomeFantasia         varchar(128)         null,
   Cnpj                 varchar(17)          null,
   Email                varchar(64)          null,
   Tel                  varchar(32)          null,
   Contato              varchar(64)          null,
   constraint PK_EMPRESA primary key nonclustered (ID_EMPRESA)
)
go

/*==============================================================*/
/* Index: IX_IDAGENCOMPEMP                                      */
/*==============================================================*/
create index IX_IDAGENCOMPEMP on Empresa (
IdAgendaCompEmp ASC
)
go

/*==============================================================*/
/* Table: LocalAmbiente                                         */
/*==============================================================*/
create table LocalAmbiente (
   Id                   int                  not null,
   "Desc"               varchar(64)          null,
   constraint PK_LOCALAMBIENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: Material                                              */
/*==============================================================*/
create table Material (
   Id                   int                  not null,
   ID_CATEGORIA_MAT     int                  null,
   constraint PK_MATERIAL primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDCATMATERIAL                                      */
/*==============================================================*/
create index IX_IDCATMATERIAL on Material (
ID_CATEGORIA_MAT ASC
)
go

/*==============================================================*/
/* Table: Matriz                                                */
/*==============================================================*/
create table Matriz (
   Id                   int                  not null,
   IdModalidade         int                  null,
   IdAreaAtuacao        int                  null,
   IdCbo                int                  null,
   Preco                money                null,
   constraint PK_MATRIZ primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDAREAATUACAO                                      */
/*==============================================================*/
create index IX_IDAREAATUACAO on Matriz (
IdAreaAtuacao ASC
)
go

/*==============================================================*/
/* Index: IX_IDMODALIDADE                                       */
/*==============================================================*/
create index IX_IDMODALIDADE on Matriz (
IdModalidade ASC
)
go

/*==============================================================*/
/* Index: IX_IDCBO                                              */
/*==============================================================*/
create index IX_IDCBO on Matriz (
IdCbo ASC
)
go

/*==============================================================*/
/* Table: MatrizComponente                                      */
/*==============================================================*/
create table MatrizComponente (
   IdMatriz             int                  not null,
   IdComponente         int                  not null,
   constraint PK_MATRIZCOMPONENTE primary key (IdMatriz, IdComponente)
)
go

/*==============================================================*/
/* Index: IX_IDMATRIZ                                           */
/*==============================================================*/
create index IX_IDMATRIZ on MatrizComponente (
IdMatriz ASC
)
go

/*==============================================================*/
/* Index: IX_IDCOMPONENTE                                       */
/*==============================================================*/
create index IX_IDCOMPONENTE on MatrizComponente (
IdComponente ASC
)
go

/*==============================================================*/
/* Table: MatrizMaterial                                        */
/*==============================================================*/
create table MatrizMaterial (
   ID_MATRIZ            int                  not null,
   ID_MATERIAL          int                  not null,
   constraint PK_MATRIZMATERIAL primary key (ID_MATRIZ, ID_MATERIAL)
)
go

/*==============================================================*/
/* Index: IX_IDMATRIZ                                           */
/*==============================================================*/
create index IX_IDMATRIZ on MatrizMaterial (
ID_MATRIZ ASC
)
go

/*==============================================================*/
/* Index: IX_IDMATERIAL                                         */
/*==============================================================*/
create index IX_IDMATERIAL on MatrizMaterial (
ID_MATERIAL ASC
)
go

/*==============================================================*/
/* Table: MatrizServico                                         */
/*==============================================================*/
create table MatrizServico (
   IdMatriz             int                  not null,
   ID_SERVICO           int                  not null,
   constraint PK_MATRIZSERVICO primary key (IdMatriz, ID_SERVICO)
)
go

/*==============================================================*/
/* Index: IX_IDMATRIZ                                           */
/*==============================================================*/
create index IX_IDMATRIZ on MatrizServico (
IdMatriz ASC
)
go

/*==============================================================*/
/* Index: IX_IDSERVICO                                          */
/*==============================================================*/
create index IX_IDSERVICO on MatrizServico (
ID_SERVICO ASC
)
go

/*==============================================================*/
/* Table: Modalidade                                            */
/*==============================================================*/
create table Modalidade (
   ID_MODALIDADE        int                  not null,
   Nome                 varchar(256)         null,
   constraint PK_MODALIDADE primary key nonclustered (ID_MODALIDADE)
)
go

/*==============================================================*/
/* Table: ModalidadeUnidade                                     */
/*==============================================================*/
create table ModalidadeUnidade (
   IdUnidade            int                  not null,
   IdModalidade         int                  not null,
   constraint PK_MODALIDADEUNIDADE primary key (IdUnidade, IdModalidade)
)
go

/*==============================================================*/
/* Index: IX_IDMODALIDADE                                       */
/*==============================================================*/
create index IX_IDMODALIDADE on ModalidadeUnidade (
IdUnidade ASC
)
go

/*==============================================================*/
/* Index: IX_IDUNIDADE                                          */
/*==============================================================*/
create index IX_IDUNIDADE on ModalidadeUnidade (
IdModalidade ASC
)
go

/*==============================================================*/
/* Table: OrientadorArea                                        */
/*==============================================================*/
create table OrientadorArea (
   IdAreaAtuacao        int                  not null,
   IdUsuario            int                  not null,
   constraint PK_ORIENTADORAREA primary key (IdAreaAtuacao, IdUsuario)
)
go

/*==============================================================*/
/* Index: IX_IDAREAATUACAO                                      */
/*==============================================================*/
create index IX_IDAREAATUACAO on OrientadorArea (
IdAreaAtuacao ASC
)
go

/*==============================================================*/
/* Index: IX_IDUSUARIO                                          */
/*==============================================================*/
create index IX_IDUSUARIO on OrientadorArea (
IdUsuario ASC
)
go

/*==============================================================*/
/* Table: RecursoAmbiente                                       */
/*==============================================================*/
create table RecursoAmbiente (
   idRecurso            int                  not null,
   IdAmbiente           int                  null,
   Tipo                 varchar(64)          null,
   "Desc"               varchar(64)          null,
   constraint PK_RECURSOAMBIENTE primary key nonclustered (idRecurso)
)
go

/*==============================================================*/
/* Index: IX_IDAMBIENTE                                         */
/*==============================================================*/
create index IX_IDAMBIENTE on RecursoAmbiente (
IdAmbiente ASC
)
go

/*==============================================================*/
/* Table: Servico                                               */
/*==============================================================*/
create table Servico (
   Id                   int                  not null,
   ID_CATEGORIA_SERVICO int                  null,
   NomeServ             varchar(64)          null,
   constraint PK_SERVICO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDCATSERV                                          */
/*==============================================================*/
create index IX_IDCATSERV on Servico (
ID_CATEGORIA_SERVICO ASC
)
go

/*==============================================================*/
/* Table: TipoAmbiente                                          */
/*==============================================================*/
create table TipoAmbiente (
   Id                   int                  not null,
   "Desc"               varchar(64)          null,
   constraint PK_TIPOAMBIENTE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: TipoAtividade                                         */
/*==============================================================*/
create table TipoAtividade (
   Id                   int                  not null,
   Tip_ID_TIPO_ATIVIDADE int                  null,
   Nome                 varchar(256)         null,
   Interna              bit                  null,
   constraint PK_TIPOATIVIDADE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDTIPOATIVIDADE                                    */
/*==============================================================*/
create index IX_IDTIPOATIVIDADE on TipoAtividade (
Tip_ID_TIPO_ATIVIDADE ASC
)
go

/*==============================================================*/
/* Table: TipoContrato                                          */
/*==============================================================*/
create table TipoContrato (
   ID_TIPO_CONTRATO     int                  not null,
   constraint PK_TIPOCONTRATO primary key nonclustered (ID_TIPO_CONTRATO)
)
go

/*==============================================================*/
/* Table: Turno                                                 */
/*==============================================================*/
create table Turno (
   Id                   int                  not null,
   constraint PK_TURNO primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: UnCurricular                                          */
/*==============================================================*/
create table UnCurricular (
   Id                   int                  not null,
   IdComponente         int                  null,
   Nome                 varchar(256)         null,
   constraint PK_UNCURRICULAR primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: IX_IDCOMPONENTE                                       */
/*==============================================================*/
create index IX_IDCOMPONENTE on UnCurricular (
IdComponente ASC
)
go

/*==============================================================*/
/* Table: Unidade                                               */
/*==============================================================*/
create table Unidade (
   ID_UNIDADE           int                  not null,
   "Desc"               varchar(64)          null,
   constraint PK_UNIDADE primary key nonclustered (ID_UNIDADE)
)
go

/*==============================================================*/
/* Table: Usuario                                               */
/*==============================================================*/
create table Usuario (
   ID_USUARIO           int                  not null,
   IdCatUsuario         int                  null,
   Email                varchar(64)          null,
   Tel                  varchar(32)          null,
   Cpf                  varchar(15)          null,
   constraint PK_USUARIO primary key nonclustered (ID_USUARIO)
)
go

/*==============================================================*/
/* Index: IX_IDCATUSUARIO                                       */
/*==============================================================*/
create index IX_IDCATUSUARIO on Usuario (
IdCatUsuario ASC
)
go