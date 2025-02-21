using MySql.Data.MySqlClient;
using apos_gestor_caja.Domain.Models;
using apos_gestor_caja.applicationLayer.interfaces;
using MyApp.Infrastructure.Database;
using System.Threading.Tasks;
using System;

namespace apos_gestor_caja.Infrastructure.Repositories
{
    public class BancoRepository : IBancoService
    {
        private readonly SqlHelper _sqlHelper;

        public BancoRepository()
        {
            _sqlHelper = new SqlHelper();
        }

        public async Task AddBancoAsync(Banco banco)
        {
            string query = "INSERT INTO apos01 (d2) VALUES (@nombre)";
            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", banco.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error al añadir banco: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<Banco> GetBancoByIdAsync(int id)
        {
            string query = "SELECT id, nombre FROM bancos WHERE id = @id";
            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Banco
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error al buscar banco: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}