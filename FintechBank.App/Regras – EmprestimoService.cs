using System;

namespace FintechBank.App
{
    public class EmprestimoService
    {
        private const int ScoreAlto = 700;
        private const int ScoreMedio = 500;

        private const decimal FatorJurosScoreAlto = 1.05m;
        private const decimal FatorJurosScoreMedio = 1.12m;
        private const decimal FatorJurosScoreBaixo = 1.20m;

        private const decimal PercentualMaximoComprometimento = 0.30m;

        /// <summary>
        /// Calcula o valor de cada parcela aplicando juros totais conforme o score:
        /// - Score >= 700: 5% (1.05)
        /// - Score entre 500 e 699: 12% (1.12)
        /// - Score < 500: 20% (1.20)
        /// </summary>
        public decimal CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)
        {
            if (parcelas <= 0)
            {
                throw new ArgumentException("O número de parcelas deve ser maior que zero.", nameof(parcelas));
            }

            decimal fatorJuros;

            if (scoreCredito >= ScoreAlto)
            {
                fatorJuros = FatorJurosScoreAlto;
            }
            else if (scoreCredito >= ScoreMedio)
            {
                fatorJuros = FatorJurosScoreMedio;
            }
            else
            {
                fatorJuros = FatorJurosScoreBaixo;
            }

            decimal valorComJuros = valorTotal * fatorJuros;

            return valorComJuros / parcelas;
        }

        /// <summary>
        /// Aprova o empréstimo apenas se a parcela comprometer no máximo
        /// 30% da renda mensal (valorParcela <= rendaMensal * 0.30).
        /// </summary>
        public bool AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)
        {
            return valorParcela <= rendaMensal * PercentualMaximoComprometimento;
        }
    }
}