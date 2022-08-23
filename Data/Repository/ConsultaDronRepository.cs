using Dapper;
using dron.Models.Dto;
using Dron.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dron.Data.Repository
{
    public class ConsultaDronRepository : IConsultaDronRepository
    {


        private IConnectionFactory _connectionFactory;
        private readonly ILogger<ConsultaDronRepository> _logger;

        public ConsultaDronRepository(ILogger<ConsultaDronRepository> logger, IConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }


        public async Task<int>  InsertarDron(RegistrarDronDto registrarDronDto)
        {
            _logger.LogInformation($"Insertando  Dron ");
            using (var con = _connectionFactory.CreateConnection())
            {
                _logger.LogInformation($"Ejecutando Query");


                string sql = @"INSERT INTO [dbo].[dron]
                           ([numeroserie]
                           ,[IdModelo]
                           ,[peso]
                           ,[porcentaje_bateria]
                           ,[Idestado])
                     VALUES
                           (@numeroserie
                           ,@IdModelo
                           ,@peso
                           ,@porcentaje_bateria
                           ,@Idestado)";

                       await con.ExecuteAsync(sql, new { 
                                    registrarDronDto.numeroserie
                                   ,registrarDronDto.IdModelo
                                   ,registrarDronDto.peso
                                   ,registrarDronDto.porcentaje_bateria
                                   ,registrarDronDto.Idestado}).ConfigureAwait(false);

                return 1;
            }



        }


        public async Task<int>  Carga_Medicamento_Dron(MedicamentoDron medicamentoDron)
        {
            _logger.LogInformation($"Insertando  Carga_Medicamento_Dron ");
            using (var con = _connectionFactory.CreateConnection())
            {
                _logger.LogInformation($"Ejecutando Query");


                string sql = @"INSERT INTO [dbo].[dron_medicamento]
                           ([iddron]
                           ,[idmedicamento]
                           
                     VALUES
                           (@iddron
                           ,@idmedicamento)";

                       await con.ExecuteAsync(sql, new { 
                                    medicamentoDron.iddron
                                   ,medicamentoDron.idmedicamento}).ConfigureAwait(false);

                return 1;
            }



        }

        public async Task<int>  InsertarMedicamento(Medicamento medicamento)
        {
            _logger.LogInformation($"Insertando  Medicamento ");
            using (var con = _connectionFactory.CreateConnection())
            {
                _logger.LogInformation($"Ejecutando Query");


                string sql = @"INSERT INTO [dbo].[medicamentos]
                            ([nombre]
                            ,[peso]
                            ,[codigo]
                            ,[imagen])
                     VALUES
                           (@nombre 
                            ,@peso
                            ,@codigo
                            ,@imagen)";

                       await con.ExecuteAsync(sql, new { 
                                    medicamento.nombre
                                   ,medicamento.peso
                                   ,medicamento.codigo
                                   ,medicamento.imagen}).ConfigureAwait(false);

                return 1;
            }



        }

        public async Task<DronDto> ListarDronById(int Id)
        {
            _logger.LogInformation($"Consultando Informacion del Dron {Id}");
            using (var con =  _connectionFactory.CreateConnection())
            {
                _logger.LogInformation($"Ejecutando Query");


                string sql= @"Select d.Id,numeroserie,peso,porcentaje_bateria,m.Nombre modelo, e.nombre estado
                            from dron d
                            ,modelo m
                            , estado e
                                where d.IdModelo = m.Id
                            and e.Id = d.Idestado
                            and d.id = @id";

                DronDto output = await con.QueryFirstOrDefaultAsync<DronDto>(sql, new { Id }).ConfigureAwait(false);
                                
                return output;
            }
        }

        public async Task<IEnumerable<DronDto>> ListarDrones()
        {
            _logger.LogInformation($"Consultando Informacion del Drones");
            using (var con = _connectionFactory.CreateConnection())
            {
                string sql = @"Select d.Id,numeroserie,peso,porcentaje_bateria,m.Nombre modelo, e.nombre estado
                            from dron d
                            ,modelo m
                            , estado e
                                where d.IdModelo = m.Id
                            and e.Id = d.Idestado";
                
                _logger.LogInformation($"Ejecutando Query");
                IEnumerable<DronDto> output = await con.QueryAsync<DronDto>(sql).ConfigureAwait(false);
                return output;
            }
        }
    }
}
