
using NetFramework.Correios;
using NetFramework.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetFramework.Controllers
{
    public partial class HomeController : Controller
    {
        public async Task<ActionResult> LocalizarAsync(Cep model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index));

            using (var correios = new Correios.AtendeClienteClient())
            {
                try
                {
                    var consulta = await correios.consultaCEPAsync(model.Codigo.Replace("-", String.Empty));
                    var resultado = consulta.@return;
                    //resultado.unidadesPostagem

                    if (resultado != null)
                    {
                        var endereco = new Endereco()
                        {
                            Descricao = resultado.end,
                            Complemento = resultado.complemento2,
                            Bairro = resultado.bairro,
                            Cidade = resultado.cidade,
                            UF = resultado.uf
                        };

                        return PartialView("_Endereco", endereco);
                    }
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("CEP", ex.Message);
                    return View(nameof(Index));
                    throw;
                }
            }
            return View(nameof(Index));
        }

        public ActionResult Localizar(Cep model)
        {
            //if (!ModelState.IsValid)
            //    return View(nameof(Index));

            using (var correios = new Correios.AtendeClienteClient())
            {
                try
                {
                    var consulta = correios.consultaCEP(model.Codigo.Replace("-", String.Empty));

                    if (consulta != null)
                    {
                        var endereco = new Endereco()
                        {
                            Descricao = consulta.end,
                            Complemento = consulta.complemento2,
                            Bairro = consulta.bairro,
                            Cidade = consulta.cidade,
                            UF = consulta.uf
                        };

                        return PartialView("_Endereco", endereco);
                    }
                    return View(nameof(Error));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("CEP", ex.Message);
                    return View(nameof(Error));
                    throw;
                }
            }
        }
    }
}