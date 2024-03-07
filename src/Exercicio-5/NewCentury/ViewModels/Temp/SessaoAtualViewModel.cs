using NewCentury.Domain.Models.Enum;

namespace NewCentury.ViewModels.Temp
{
    public class SessaoAtualViewModel
    {
        public Guid partidaId {  get; set; }
        public int rodadaAtual { get; set; }
        public int EscolhaJogador {  get; set; }
        public int EscolhaMaquina { get; set; }
        public string Player { get; set; }
        public Resultado Situacao { get; set; }

        //Indicadores
        public string Continuiar { get; set; }



    }
}
