using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using apos_gestor_caja.Domain.Models;
using MyApp.Infrastructure.Database;

namespace apos_gestor_caja.Infrastructure.Repositories
{
    public class CajeroRepository
    {
        private readonly SqlHelper _sqlHelper;

        public CajeroRepository()
        {
            _sqlHelper = new SqlHelper();
        }

        public async Task<bool> CrearCajeroAsync(Cajero cajero)
        {
            string query = @"INSERT INTO apos03 (d2, d3, d4, d5, d6) 
                           VALUES (@usuario, @clave, @barra, @nombre, @nivelAcceso)";

            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                Console.WriteLine("Conexión obtenida para CrearCajeroAsync"); // Debug log
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario", cajero.Usuario);
                    command.Parameters.AddWithValue("@clave", cajero.Clave);
                    command.Parameters.AddWithValue("@barra", cajero.Barra);
                    command.Parameters.AddWithValue("@nombre", cajero.Nombre);
                    command.Parameters.AddWithValue("@nivelAcceso", cajero.NivelAcceso);

                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cajero: {ex.Message}"); // Debug log
                throw new Exception($"Error al crear cajero: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    _sqlHelper.CerrarConexion(connection);
                    Console.WriteLine("Conexión cerrada en CrearCajeroAsync"); // Debug log
                }
            }
        }

        public async Task<bool> VerificarUsuarioExistenteAsync(string usuario)
        {
            string query = "SELECT COUNT(*) FROM apos03 WHERE d2 = @usuario";

            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                Console.WriteLine("Conexión obtenida para VerificarUsuarioExistenteAsync"); // Debug log
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario", usuario);
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar usuario: {ex.Message}"); // Debug log
                throw new Exception($"Error al verificar usuario: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    _sqlHelper.CerrarConexion(connection);

                    Console.WriteLine("Conexión cerrada en VerificarUsuarioExistenteAsync"); // Debug log
                }
            }
        }

        public async Task<List<Cajero>> ObtenerCajerosAsync()
        {
            Console.WriteLine("Iniciando ObtenerCajerosAsync"); // Debug log
            string query = @"SELECT 
                           d1 as Id, 
                           d2 as Usuario, 
                           d3 as Clave,
                           d4 as Barra,
                           d5 as Nombre, 
                           d6 as NivelAcceso 
                           FROM apos03 
                           ORDER BY d1 ASC";

            var cajeros = new List<Cajero>();
            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                Console.WriteLine("Conexión obtenida para ObtenerCajerosAsync"); // Debug log

                using (var command = new MySqlCommand(query, connection))
                {
                    Console.WriteLine("Ejecutando comando para obtener cajeros"); // Debug log
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        Console.WriteLine("DataReader obtenido, comenzando lectura"); // Debug log
                        while (await reader.ReadAsync())
                        {
                            var cajero = new Cajero
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Usuario = reader.GetString(reader.GetOrdinal("Usuario")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                NivelAcceso = reader.GetInt32(reader.GetOrdinal("NivelAcceso")),
                                Barra = reader.GetInt32(reader.GetOrdinal("Barra")),
                                Activo = true
                            };
                            Console.WriteLine($"Cajero leído - ID: {cajero.Id}, Usuario: {cajero.Usuario}"); // Debug log
                            cajeros.Add(cajero);
                        }
                    }
                    Console.WriteLine($"Lectura completada. Total cajeros: {cajeros.Count}"); // Debug log
                }
                return cajeros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerCajerosAsync: {ex.Message}"); // Debug log
                throw new Exception($"Error al obtener cajeros: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    _sqlHelper.CerrarConexion(connection);
                    Console.WriteLine("Conexión cerrada en ObtenerCajerosAsync"); // Debug log
                }
            }
        }

        public async Task<Cajero> ObtenerCajeroPorIdAsync(int id)
        {
            Console.WriteLine($"Iniciando ObtenerCajeroPorIdAsync para ID: {id}"); // Debug log
            string query = @"SELECT 
                           d1 as Id, 
                           d2 as Usuario, 
                           d3 as Clave,
                           d4 as Barra,
                           d5 as Nombre, 
                           d6 as NivelAcceso 
                           FROM apos03 
                           WHERE d1 = @id";

            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                Console.WriteLine("Conexión obtenida para ObtenerCajeroPorIdAsync"); // Debug log

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    Console.WriteLine("Ejecutando comando para obtener cajero por ID"); // Debug log

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var cajero = new Cajero
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Usuario = reader.GetString(reader.GetOrdinal("Usuario")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                NivelAcceso = reader.GetInt32(reader.GetOrdinal("NivelAcceso")),
                                Barra = reader.GetInt32(reader.GetOrdinal("Barra")),
                                Activo = true
                            };
                            Console.WriteLine($"Cajero encontrado - ID: {cajero.Id}, Usuario: {cajero.Usuario}"); // Debug log
                            return cajero;
                        }
                        Console.WriteLine($"No se encontró cajero con ID: {id}"); // Debug log
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerCajeroPorIdAsync: {ex.Message}"); // Debug log
                throw new Exception($"Error al obtener cajero por ID: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    _sqlHelper.CerrarConexion(connection);

                    Console.WriteLine("Conexión cerrada en ObtenerCajeroPorIdAsync"); // Debug log
                }
            }
        }

        public async Task<bool> ActualizarCajeroAsync(Cajero cajero)
        {
            string query = @"UPDATE apos03 
                           SET d2 = @usuario, 
                               d4 = @barra,
                               d5 = @nombre, 
                               d6 = @nivelAcceso
                           WHERE d1 = @id";

            if (!string.IsNullOrWhiteSpace(cajero.Clave))
            {
                query = @"UPDATE apos03 
                         SET d2 = @usuario, 
                             d3 = @clave,
                             d4 = @barra,
                             d5 = @nombre, 
                             d6 = @nivelAcceso
                         WHERE d1 = @id";
            }

            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                Console.WriteLine("Conexión obtenida para ActualizarCajeroAsync"); // Debug log
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cajero.Id);
                    command.Parameters.AddWithValue("@usuario", cajero.Usuario);
                    command.Parameters.AddWithValue("@barra", cajero.Barra);
                    command.Parameters.AddWithValue("@nombre", cajero.Nombre);
                    command.Parameters.AddWithValue("@nivelAcceso", cajero.NivelAcceso);

                    if (!string.IsNullOrWhiteSpace(cajero.Clave))
                    {
                        command.Parameters.AddWithValue("@clave", cajero.Clave);
                    }

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cajero: {ex.Message}"); // Debug log
                throw new Exception($"Error al actualizar cajero: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    _sqlHelper.CerrarConexion(connection);

                    Console.WriteLine("Conexión cerrada en ActualizarCajeroAsync"); // Debug log
                }
            }
        }

        public async Task<List<Cajero>> BuscarCajerosPorNombreAsync(string nombre)
        {
            string query = @"SELECT 
                           d1 as Id, 
                           d2 as Usuario, 
                           d3 as Clave,
                           d4 as Barra,
                           d5 as Nombre, 
                           d6 as NivelAcceso 
                           FROM apos03 WHERE d5 LIKE @nombre";

            var cajeros = new List<Cajero>();
            MySqlConnection connection = null;
            try
            {
                connection = _sqlHelper.ObtenerConexion();
                Console.WriteLine("Conexión obtenida para BuscarCajerosPorNombreAsync"); // Debug log
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", $"%{nombre}%");
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cajeros.Add(new Cajero
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Usuario = reader["Usuario"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                Barra = Convert.ToInt32(reader["Barra"]),
                                Nombre = reader["Nombre"].ToString(),
                                NivelAcceso = Convert.ToInt32(reader["NivelAcceso"]),
                                Activo = true
                            });
                        }
                    }
                }
                return cajeros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar cajeros: {ex.Message}"); // Debug log
                throw new Exception($"Error al buscar cajeros: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    _sqlHelper.CerrarConexion(connection);
                    Console.WriteLine("Conexión cerrada en BuscarCajerosPorNombreAsync"); // Debug log
                }
            }
        }
    }
}