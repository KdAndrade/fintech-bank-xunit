namespace FintechBank.App
{
    public class ContaCorrenteService
    {
        private const decimal SaldoIsencao = 5000.00m;
        private const decimal SaldoFaixaIntermediaria = 1000.00m;

        private const decimal TaxaFaixaIntermediaria = 12.50m;
        private const decimal TaxaFaixaBaixa = 25.00m;

        /// <summary>
        /// Calcula a taxa de manutenção da conta.
        /// - Saldo >= 5.000,00 OU cliente especial: isento (0.00)
        /// - Saldo entre 1.000,00 e 4.999,99 (não especial): 12.50
        /// - Saldo < 1.000,00 (não especial): 25.00
        /// </summary>
        public decimal CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)
        {
            if (saldo >= SaldoIsencao || clienteEspecial)
            {
                return 0.00m;
            }

            if (saldo >= SaldoFaixaIntermediaria)
            {
                return TaxaFaixaIntermediaria;
            }

            return TaxaFaixaBaixa;
        }

        /// <summary>
        /// Retorna true apenas se o valor do saque for menor ou igual
        /// à soma de saldo + limite do cheque especial.
        /// </summary>
        public bool PodeRealizarSaque(decimal saldo, decimal limiteChequeEspecial, decimal valorSaque)
        {
            return valorSaque <= saldo + limiteChequeEspecial;
        }
    }
}