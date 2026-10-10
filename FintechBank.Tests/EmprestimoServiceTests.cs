using FintechBank.App;

namespace FintechBank.Tests
{
  public class EmprestimoServiceTests
  {
    [Theory]
    [InlineData(10000.00, 10, 800, 1050.00)]
    [InlineData(10000.00, 10, 600, 1120.00)]
    [InlineData(10000.00, 10, 400, 1200.00)]
    public void CalcularValorParcela_DeveAplicarJurosConformeScore(
        double valorTotal,
        int parcelas,
        int scoreCredito,
        double valorEsperado)
    {
      var service = new EmprestimoService();

      decimal resultado = service.CalcularValorParcela(
          (decimal)valorTotal,
          parcelas,
          scoreCredito);

      Assert.Equal((decimal)valorEsperado, resultado);
    }

    [Theory]
    [InlineData(5000.00, 1500.00, true)]
    [InlineData(5000.00, 1600.00, false)]
    public void AprovarEmprestimo_DeveValidarLimiteDeComprometimentoDaRenda(
        double rendaMensal,
        double valorParcela,
        bool resultadoEsperado)
    {
      var service = new EmprestimoService();

      bool resultado = service.AprovarEmprestimo(
          (decimal)rendaMensal,
          (decimal)valorParcela);

      Assert.Equal(resultadoEsperado, resultado);
    }
  }
}