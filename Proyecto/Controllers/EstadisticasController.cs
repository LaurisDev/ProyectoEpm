using ConexionBDConsultas;
using System.Web.Mvc;

public class EstadisticasController : Controller
{
    //case 4
    public ActionResult PromedioConsumoEnergia()
    {
        Estadisticas estadisticas = new Estadisticas();
        decimal promedioGeneralConsumoEnergia = estadisticas.CalcularPromedioConsumoEnergiaGeneral();

        ViewBag.PromedioGeneralConsumoEnergia = promedioGeneralConsumoEnergia;

        return View();
    }

    // case 5

    public ActionResult CalcularTotalDescuentos()
    {

        double totalDescuentos = Estadisticas.CalcularTotalDescuentos();
        ViewBag.PromedioGeneralConsumoEnergia = totalDescuentos;
        return View();
    }

    // case 6
    public ActionResult CalcularTotalExcesoAgua()
    {
        Estadisticas estadisticas = new Estadisticas();
        double totalExcesoAgua = Estadisticas.CalcularTotalExcesoAgua();
        ViewBag.ExcesoAgua = totalExcesoAgua;
        return View();
    }
    

   

    //case 10
    public ActionResult TotalclientesConConsumoMayorPromedio()
    {
        Estadisticas estadisticas = new Estadisticas();
        decimal totalclientesConConsumoMayorPromedio = estadisticas.CalcularTotalClientesConConsumoMayorPromedio();

        ViewBag.ClientesConConsumoMayorPromedio = totalclientesConConsumoMayorPromedio;

        return View();
    }

    // case 11
    public ActionResult TotalPagadoEnergia()
    {
        Estadisticas estadisticas = new Estadisticas();
        decimal totalPagadoEnergia = estadisticas.CalcularTotalPagadoEnergiaGeneral();

        ViewBag.TotalPagadoEnergia = totalPagadoEnergia;

        return View();
    }

    //case 12
    public ActionResult TotalPagadoAgua()
    {
        Estadisticas estadisticas = new Estadisticas();
        decimal totalPagadoAgua = estadisticas.CalcularTotalPagadoAguaGeneral();

        ViewBag.TotalPagadoAgua = totalPagadoAgua;

        return View();
    }


    public ActionResult ResumenEstadisticas()
    {
        Estadisticas estadisticas = new Estadisticas();
        decimal promedioGeneralConsumoEnergia = estadisticas.CalcularPromedioConsumoEnergiaGeneral();
        decimal totalPagadoEnergia = estadisticas.CalcularTotalPagadoEnergiaGeneral();
        decimal totalPagadoAgua = estadisticas.CalcularTotalPagadoAguaGeneral();
        decimal totalclientesConConsumoMayorPromedio = estadisticas.CalcularTotalClientesConConsumoMayorPromedio();
        double totalExcesoAgua = Estadisticas.CalcularTotalExcesoAgua();
        double totalDescuentos = Estadisticas.CalcularTotalDescuentos();


        ViewBag.PromedioGeneralConsumoEnergia = promedioGeneralConsumoEnergia;
        ViewBag.TotalPagadoEnergia = totalPagadoEnergia;
        ViewBag.TotalPagadoAgua = totalPagadoAgua;
        ViewBag.TotalclientesConConsumoMayorPromedio = totalclientesConConsumoMayorPromedio;
        ViewBag.ExcesoAgua = totalExcesoAgua;
        ViewBag.PromedioGeneralConsumoEnergia = totalDescuentos;

        return View();
    }

   
}
