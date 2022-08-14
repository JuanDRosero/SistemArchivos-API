using Microsoft.AspNetCore.Mvc;
using SistemArchivos_API.Model;

namespace SistemArchivos_API.Controllers
{
    [ApiController]
    [Route("api/v1/ETX2")]
    public class APIController : ControllerBase
    {
        private SuperBloque super;

        [HttpGet]
        [Route("/")]
        public ActionResult Iniciar(int cantidadBloques, int cantidadInodos, int tamañoBloque)
        {
            if (cantidadBloques<1 || cantidadInodos<1 || tamañoBloque<1)
            {
                return BadRequest("Valores invalidos");
            }
            try
            {
                super = new SuperBloque(cantidadBloques, cantidadInodos, tamañoBloque);
                return Ok("El objeto se ha creado Correctamente");
            }
            catch(Exception E)
            {
                return BadRequest("Error: "+ E.Message);
            }
        }
        [HttpGet]
        [Route("/Bloques")]
        public IActionResult GetBloques()
        {
            if (super==null || super.TablaBloques==null)
            {
                return NotFound("El superBloque no se encuentra o la tabla de bloques no se encuentra");
            }
            return Ok(super.GetBloques());
        }
        [HttpGet]
        [Route("/Inodos")]
        public IActionResult GetInodos()
        {
            if (super == null || super.TablaINodos == null)
            {
                return NotFound("El superBloque no se encuentra o la tabla de bloques no se encuentra");
            }
            return Ok(super.GetNodos());
        }

        [HttpPost]
        [Route("/Archivo")]
        public IActionResult CrearArchivo(Archivo archivo, int padre)
        {
            try
            {
                if (padre < -1 || padre >= super.CantidadInodos)
                {
                    return NotFound("El valor del padre no se encuentra: " + padre);
                }
                var correcto = super.CrearArchivo(archivo, padre);
                return Ok(correcto);
            } catch(DirectoryNotFoundException nfe)
            {
                return BadRequest(nfe.Message);
            }catch(NullReferenceException nrf)
            {
                return BadRequest("Ha ocurrudo un error: " + nrf.Message);
            }
        }
        [HttpPost]
        [Route("/Carpeta")]
        public IActionResult CrearCarpeta(string nombre, int padre)
        {
            try
            {
                if (padre < -1 || padre >= super.CantidadInodos)
                {
                    return NotFound("El valor del padre no se encuentra: " + padre);
                }
                var correcto = super.CrearCarpeta(padre, nombre);
                return Ok(correcto);
            }
            catch (DirectoryNotFoundException nfe)
            {
                return BadRequest(nfe.Message);
            }
            catch (NullReferenceException nrf)
            {
                return BadRequest("Ha ocurrudo un error: " + nrf.Message);
            }
        }
        //Hacer las de borrar

    }
}
