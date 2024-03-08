using NewCentury.Domain.Models.Enum;

namespace NewCentury.ViewModels.Temp
{
    public class SessaoAtualViewModel
    {
        public Guid partidaId {  get; set; }
        public int EscolhaJogador {  get; set; }
        public int EscolhaMaquina { get; set; }
        public string Player { get; set; }
        public Resultado Situacao { get; set; }

        //Marcadores
        public string Continuar { get; set; }

        public string Dificuldade { get; set; }

        public string PlayerInicial { get; set; }
        public int Rodadas { get; set; }
        public int RodadaAtual { get; set; }



    }
}
