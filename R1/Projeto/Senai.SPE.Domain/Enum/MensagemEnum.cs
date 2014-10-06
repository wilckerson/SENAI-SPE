using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.SPE.Domain.Enum
{
    public enum MensagemEnum
    {
        [Description("Erro. Por favor, tente mais tarde ou consulte o administrador do site.")]
        Index = 1,
        [Description("Erro ao tentar excluir item. Por favor, tente mais tarde ou consulte o administrador do site.")]
        Excluir = 2,
        [Description("Erro ao tentar editar item. Por favor, tente mais tarde ou consulte o administrador do site.")]
        Editar = 3,
        [Description("Erro ao tentar salvar item. Por favor, tente mais tarde ou consulte o administrador do site.")]
        Salvar = 4,
        [Description("Erro ao tentar detalhar item. Por favor, tente mais tarde ou consulte o administrador do site.")]
        Detalhar = 5

    }
}
