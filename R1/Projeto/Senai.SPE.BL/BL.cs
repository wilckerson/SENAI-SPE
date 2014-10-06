using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senai.SPE.BL;
using Senai.SPE.Domain;
using System.Linq.Expressions;
using Repository;
using System.Transactions;
using SPERepository;
using System.Data.Entity;


namespace BusinessLogic
{
    public class SPEBL
    {

        private TransactionRepository transactionScope;

        public void SetTransactionScope(TransactionRepository transaction)
        {
            transactionScope = transaction;
        }

        #region Properties

        public DbContext Context
        {
            get
            {
                return this.Context;
            }
        }

        private AgendaAmbienteBL _agendaAmbienteBL;
        public AgendaAmbienteBL AgendaAmbiente
        {
            get
            {
                return _agendaAmbienteBL ?? (_agendaAmbienteBL = new AgendaAmbienteBL(transactionScope));
            }
        }

        private AgendaComponenteBL _agendaComponenteBL;
        public AgendaComponenteBL AgendaComponente
        {
            get
            {
                return _agendaComponenteBL ?? (_agendaComponenteBL = new AgendaComponenteBL(transactionScope));
            }
        }

        private AgendaDocenteBL _agendaDocenteBL;
        public AgendaDocenteBL AgendaDocente
        {
            get
            {
                return _agendaDocenteBL ?? (_agendaDocenteBL = new AgendaDocenteBL(transactionScope));
            }
        }


        private AmbienteBL _ambienteBL;
        public AmbienteBL Ambiente
        {
            get
            {
                return _ambienteBL ?? (_ambienteBL = new AmbienteBL(transactionScope));
            }
        }

        private AreaAtuacaoBL _areaAtuacaoBL;
        public AreaAtuacaoBL AreaAtuacao
        {
            get
            {
                return _areaAtuacaoBL ?? (_areaAtuacaoBL = new AreaAtuacaoBL(transactionScope));
            }
        }

        private SPERepository<CalendarioCivil> _calendarioCivilBL;
        public SPERepository<CalendarioCivil> CalendarioCivil
        {
            get
            {
                return _calendarioCivilBL ?? (_calendarioCivilBL = new SPERepository<CalendarioCivil>(transactionScope));
            }
        }

        private AgendamentoBL _agendamentoBL;
        public AgendamentoBL Agendamento
        {
            get
            {
                return _agendamentoBL ?? (_agendamentoBL = new AgendamentoBL(transactionScope));
            }
        }

        private SPERepository<CatMaterial> _catMaterialBL;
        public SPERepository<CatMaterial> CatMaterial
        {
            get
            {
                return _catMaterialBL ?? (_catMaterialBL = new SPERepository<CatMaterial>(transactionScope));
            }
        }

        private SPERepository<AgendaEvento> _agendaEventoBL;
        public SPERepository<AgendaEvento> AgendaEvento
        {
            get
            {
                return _agendaEventoBL ?? (_agendaEventoBL = new SPERepository<AgendaEvento>(transactionScope));
            }
        }

        private SPERepository<CatServico> _catServicolBL;
        public SPERepository<CatServico> CatServico
        {
            get
            {
                return _catServicolBL ?? (_catServicolBL = new SPERepository<CatServico>(transactionScope));
            }
        }

        private CatUsuarioBL _catUsuariolBL;
        public CatUsuarioBL CatUsuario
        {
            get
            {
                return _catUsuariolBL ?? (_catUsuariolBL = new CatUsuarioBL(transactionScope));
            }
        }

        private CBOBL _CBOBL;
        public CBOBL CBO
        {
            get
            {
                return _CBOBL ?? (_CBOBL = new CBOBL(transactionScope));
            }
        }

        private ComponenteBL _componenteBL;
        public ComponenteBL Componente
        {
            get
            {
                return _componenteBL ?? (_componenteBL = new ComponenteBL(transactionScope));
            }
        }

        private CRBL _CRBL;
        public CRBL CR
        {
            get
            {
                return _CRBL ?? (_CRBL = new CRBL(transactionScope));
            }
        }

        private DocenteBL _docenteBL;
        public DocenteBL Docente
        {
            get
            {
                return _docenteBL ?? (_docenteBL = new DocenteBL(transactionScope));
            }
        }



        private EmpresaBL _empresaBL;
        public EmpresaBL Empresa
        {
            get
            {
                return _empresaBL ?? (_empresaBL = new EmpresaBL(transactionScope));
            }
        }

        private FuncionalidadeBL _funcionalidadeBL;
        public FuncionalidadeBL Funcionalidade
        {
            get
            {
                return _funcionalidadeBL ?? (_funcionalidadeBL = new FuncionalidadeBL(transactionScope));
            }
        }

        //private SPERepository<Empresa> _empresaBL;
        //public SPERepository<Empresa> Empresa
        //{
        //    get
        //    {
        //        return _empresaBL ?? (_empresaBL = new SPERepository<Empresa>(transactionScope));
        //    }
        //}

        private LocalAmbienteBL _localAmbienteBL;
        public LocalAmbienteBL LocalAmbiente
        {
            get
            {
                return _localAmbienteBL ?? (_localAmbienteBL = new LocalAmbienteBL(transactionScope));
            }
        }

        private SPERepository<Material> _materialBL;
        public SPERepository<Material> Material
        {
            get
            {
                return _materialBL ?? (_materialBL = new SPERepository<Material>(transactionScope));
            }
        }

        private MatrizBL _matrizBL;
        public MatrizBL Matriz
        {
            get
            {
                return _matrizBL ?? (_matrizBL = new MatrizBL(transactionScope));
            }
        }

        private ModalidadeBL _modalidadeBL;
        public ModalidadeBL Modalidade
        {
            get
            {
                return _modalidadeBL ?? (_modalidadeBL = new ModalidadeBL(transactionScope));
            }
        }

        private ModuloBL _moduloBL;
        public ModuloBL Modulo
        {
            get
            {
                return _moduloBL ?? (_moduloBL = new ModuloBL(transactionScope));
            }
        }

        private RecursoBL _recursoBL;
        public RecursoBL Recurso
        {
            get
            {
                return _recursoBL ?? (_recursoBL = new RecursoBL(transactionScope));
            }
        }

        private SPERepository<RecursoAmbiente> _recursoAmbienteBL;
        public SPERepository<RecursoAmbiente> RecursoAmbiente
        {
            get
            {
                return _recursoAmbienteBL ?? (_recursoAmbienteBL = new SPERepository<RecursoAmbiente>(transactionScope));
            }
        }

        private SPERepository<Servico> _servicoBL;
        public SPERepository<Servico> Servico
        {
            get
            {
                return _servicoBL ?? (_servicoBL = new SPERepository<Servico>(transactionScope));
            }
        }

        private TipoAmbienteBL _tipoAmbienteBL;
        public TipoAmbienteBL TipoAmbiente
        {
            get
            {
                return _tipoAmbienteBL ?? (_tipoAmbienteBL = new TipoAmbienteBL(transactionScope));
            }
        }

        private TipoAtividadeBL _tipoAtividadeBL;
        public TipoAtividadeBL TipoAtividade
        {
            get
            {
                return _tipoAtividadeBL ?? (_tipoAtividadeBL = new TipoAtividadeBL(transactionScope));
            }
        }

        private TipoContratoBL _tipoContratoBL;
        public TipoContratoBL TipoContrato
        {
            get
            {
                return _tipoContratoBL ?? (_tipoContratoBL = new TipoContratoBL(transactionScope));
            }
        }

        private TipoRecursoBL _tipoRecursoBL;
        public TipoRecursoBL TipoRecurso
        {
            get
            {
                return _tipoRecursoBL ?? (_tipoRecursoBL = new TipoRecursoBL(transactionScope));
            }
        }

        private TurmaBL _turmaBL;
        public TurmaBL Turma
        {
            get
            {
                return _turmaBL ?? (_turmaBL = new TurmaBL(transactionScope));
            }
        }

        private TurnoBL _turnoBL;
        public TurnoBL Turno
        {
            get
            {
                return _turnoBL ?? (_turnoBL = new TurnoBL(transactionScope));
            }
        }

        private SPERepository<UnCurricular> _unCurricularBL;
        public SPERepository<UnCurricular> UnCurricular
        {
            get
            {
                return _unCurricularBL ?? (_unCurricularBL = new SPERepository<UnCurricular>(transactionScope));
            }
        }

        private UnidadeBL _unidadeBL;
        public UnidadeBL Unidade
        {
            get
            {
                return _unidadeBL ?? (_unidadeBL = new UnidadeBL(transactionScope));
            }
        }

        private UsuarioBL _usuarioBL;
        public UsuarioBL Usuario
        {
            get
            {
                return _usuarioBL ?? (_usuarioBL = new UsuarioBL(transactionScope));
            }
        }

        private PerfilBL _perfilBL;
        public PerfilBL Perfil
        {
            get
            {
                return _perfilBL ?? (_perfilBL = new PerfilBL(transactionScope));
            }
        }

        private CalendarioLegendaBL _calendarioLegendaBL;
        public CalendarioLegendaBL CalendarioLegenda
        {
            get
            {
                return _calendarioLegendaBL ?? (_calendarioLegendaBL = new CalendarioLegendaBL(transactionScope));
            }
        }

        private ReprovacaoTurmaBL _reprovacaoTurmaBL;
        public ReprovacaoTurmaBL ReprovacaoTurma
        {
            get
            {
                return _reprovacaoTurmaBL ?? (_reprovacaoTurmaBL = new ReprovacaoTurmaBL(transactionScope));
            }
        }

        private ReprovacaoMatrizBL _reprovacaoMatrizBL;
        public ReprovacaoMatrizBL ReprovacaoMatriz
        {
            get
            {
                return _reprovacaoMatrizBL ?? (_reprovacaoMatrizBL = new ReprovacaoMatrizBL(transactionScope));
            }
        }

        #endregion
    }
}