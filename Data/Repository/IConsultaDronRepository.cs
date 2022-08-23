using dron.Models.Dto;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dron.Data.Repository
{
    public interface IConsultaDronRepository
    {
       Task<DronDto>  ListarDronById(int Id);
       Task<IEnumerable<DronDto>>  ListarDrones();


        Task<int> InsertarDron(RegistrarDronDto registrarDronDto);

        Task<int> InsertarMedicamento(Medicamento medicamento);
        
        Task<int> Carga_Medicamento_Dron(MedicamentoDron medicamentoDron );

         
    }
}
