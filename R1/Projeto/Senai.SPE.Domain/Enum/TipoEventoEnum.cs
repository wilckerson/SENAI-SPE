using System.ComponentModel;

namespace Senai.SPE.Domain.Enum
{
    public enum TipoEventoEnum
    {
        [Description("Ferias")]
        Ferias = 0,
        [Description("Licença Médica")]
        Licenca = 1,
        [Description("Evento Extra-Classe")]
        Evento = 2,
        [Description("Outros")]
        Outros = 3
    }
}
