
using NewCentury.Domain.Notificacoes;
using System.Collections.Generic;

namespace NewCentury.Domain.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}