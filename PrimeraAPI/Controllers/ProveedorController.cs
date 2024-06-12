using Microsoft.AspNetCore.Mvc;

namespace PrimeraAPI.Controllers
{
    [ApiController] 
    [Route("proveedores")] //https://localhost:7212/proveedor
    public class ProveedorController : ControllerBase
    {

        public List<Proveedor> Proveedores { get; set; }

        public ProveedorController()
        {
            Proveedores = new List<Proveedor>() 
            {
                new Proveedor() { CodProveedor = "123", Nombre ="Fernando"},
                new Proveedor() { CodProveedor = "345", Nombre ="Alejandro"}

            };
        }

        [HttpGet] //https://localhost:7212/proveedores
        public IActionResult GetProveedores() 
        {
            bool flagValidacion = false;

            if (!flagValidacion) return BadRequest(new { Error = "Error en alguna validacion, averigualo..."});  //400 BadRequest
            return Ok(Proveedores); //200

        }

        [HttpGet] //https://localhost:7212/proveedores/firstProveedor
        [Route("firstProveedor")]
        public IActionResult GetFirstProveedor() 
        {
            var result = Proveedores.FirstOrDefault();
            return Ok(result);
        
        }

        [HttpGet]   //https://localhost:7212/proveedores/123 => Parametros en la ruta
        [Route("{codProveedor}")]
        public IActionResult GetProveedorById(  [FromRoute]  string codProveedor ) //Model Binding 
        {
            var match = Proveedores.FirstOrDefault(w=> w.CodProveedor == codProveedor );
            if (match == null) return NotFound("No se encontro a ese proveedor");
            return Ok(match);
        }

        [HttpGet]   //https://localhost:7212/proveedores/proveedor?codProveedor=123&nombre=fernando => Query params
        [Route("proveedor")]
        public IActionResult GetProveedorById2( [FromQuery] string codProveedor, [FromQuery] string nombre) 
        {
            var match = Proveedores.FirstOrDefault(w => w.CodProveedor == codProveedor && w.Nombre == nombre);
            if (match == null) return NotFound("No se encontro a ese proveedor");
            return Ok(match);
        }

        [HttpGet]   
        [Route("proveedor2")]
        public IActionResult GetProveedorById3([FromQuery] GetProveedorRequest request )
        {
            var match = Proveedores.FirstOrDefault(w => w.CodProveedor == request.CodProveedor && w.Nombre == request.Nombre);
            if (match == null) return NotFound("No se encontro a ese proveedor");
            return Ok(match);
        }

    }


    public class GetProveedorRequest 
    {
        public string CodProveedor { get; set; }
        public string Nombre { get; set; }

    }

    public class Proveedor 
    {

        public string Nombre { get; set; }
        public string CodProveedor { get; set; }
    }

}
