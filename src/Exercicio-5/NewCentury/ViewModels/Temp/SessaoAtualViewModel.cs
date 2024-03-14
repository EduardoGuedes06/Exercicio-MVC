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
        public string NomeJogador { get; set; }
        public bool MostrarModal { get; set; }
        public string PlayerInicial { get; set; }
        public int Rodadas { get; set; }
        public int RodadaAtual { get; set; }
        public int RodadaAntiga { get; set; }
        public int UltimaJogadaMaquina { get; set; }

        //Acertos
        public int QtdeMaquina { get; set; }
        public int QtdeJogador { get; set; }
        
        //Vencedor
        public string VencedorRodadaAtual { get; set; }

        //Finalização
        public string Dificuldade { get; set; }
        public int loop { get; set; }



    }
}
