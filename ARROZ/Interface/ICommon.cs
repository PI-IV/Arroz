using ARROZ.Models;
namespace ARROZ.Interface
{
    public interface ICommon
    {
        List<Common> GetPrecoDolar();
        List<Common> GetSalarioMinimo();
        List<Common> GetPrecoArroz();
        List<Common> GetConsumoMedio();
    }
}
