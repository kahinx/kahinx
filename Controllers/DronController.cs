using dron.Data.Repository;
using dron.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dron.Controllers
{
    [Route("api/dron")]
    [ApiController]
    public class DronController : ControllerBase
    {


        private readonly ILogger<DronController> _logger;
        private readonly IConsultaDronRepository _consultaDronRepository;

        public DronController(ILogger<DronController> logger, IConsultaDronRepository consultaDronRepository)
        {
            _logger = logger;
            _consultaDronRepository = consultaDronRepository;
        }


        [HttpPost]
        [Route("medicamento")]
        public async Task<IActionResult> Insertar_dron_medicamento(int idDron,int idMedicamento)
        {
            string output;
            try
            {
             
                output = "Medicamento Asignado correctamente!!!";



                return Ok(output);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al crear  DRON: {ex.Message}");
                return BadRequest(new { Code = "ERROR_CREAR_DRON", Message = ex.Message });
            }



        }


        [HttpPost]
        public async Task<IActionResult> Ingresar_medicamento(Medicamento medicamento)
        {
            string output;
            try
            {

             
                int result = await _consultaDronRepository.InsertarMedicamento(medicamento);
                if (result != 1)
                {
                    return BadRequest(new { status = "Error", Message = "Error al crear Dron." });

                }

                return Ok(output);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al crear  DRON: {ex.Message}");
                return BadRequest(new { Code = "ERROR_CREAR_DRON", Message = ex.Message });
            }



        }

        [HttpPost]
        public async Task<IActionResult> Insertar_Dron(RegistrarDronDto registrarDronDto)
        {
            string output;
            try
            {
                

                if (registrarDronDto.numeroserie.Length > 100)
                {
                     return BadRequest(new {status = "Error",  Message = "Numnero de serie muy Largo" });
                }


                if (registrarDronDto.peso > 500)
                {
                     return BadRequest(new {status = "Error", Message = "Excede el peso maximo" });
                }


                int result = await _consultaDronRepository.InsertarDron(registrarDronDto);
                if (result != 1)
                {
                    return BadRequest(new { status = "Error", Message = "Error al crear Dron." });

                }


                output = "Dron creado correctamente!!!";



                return Ok(output);
            }
            catch (Exception ex )
            {

                _logger.LogError($"Error al crear  DRON: {ex.Message}");
                return BadRequest(new { Code = "ERROR_CREAR_DRON", Message = ex.Message });
            }
           

           
        }

        [HttpPost]
        public async Task<IActionResult> Carga_Medicamento_Dron(MedicamentoDron medicamentoDron)
        {
            string output;
            try
            {
                


                int result = await _consultaDronRepository.Carga_Medicamento_Dron(MedicamentoDron);
                if (result != 1)
                {
                    return BadRequest(new { status = "Error", Message = "Error al crear Dron." });

                }


                output = "Dron creado correctamente!!!";



                return Ok(output);
            }
            catch (Exception ex )
            {

                _logger.LogError($"Error al crear  DRON: {ex.Message}");
                return BadRequest(new { Code = "ERROR_CREAR_DRON", Message = ex.Message });
            }
           

           
        }



        [HttpGet]
        [Route("{id}")]
        public async  Task<IActionResult> ConsultaDronById(string Id)
        {
            DronDto output;
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest(new { Code = "ERROR_ID_VACIO", Message = "El Id del Dron No puede ser vacío." });
            }

            try
            {
                output =await _consultaDronRepository.ListarDronById(int.Parse(Id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al consultar DRON: {ex.Message}");
                return BadRequest(new { Code = "ERROR_EN_CONSULTA", Message = ex.Message });
            }

           

            return Ok(output);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaDrones()
        {
            IEnumerable<DronDto> output;
           

            try
            {
                output = await _consultaDronRepository.ListarDrones();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al consultar DRON: {ex.Message}");
                return BadRequest(new { Code = "ERROR_EN_CONSULTA", Message = ex.Message });
            }

            if (output.Count() == 0)
            {
                _logger.LogWarning($"No Se Encontraron Datos Drones");
                return NotFound(new { Code = "DRON_NO_ENCONTRADO", Message = "Drones de Consulta No Encontrado" });
            }

            return Ok(output);
        }

    }
}
