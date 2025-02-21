using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apos_gestor_caja.Domain.Models;
using apos_gestor_caja.ApplicationLayer.Interfaces;
using apos_gestor_caja.Infrastructure.Repositories;

namespace apos_gestor_caja.ApplicationLayer.Services
{
    public class CajeroService : ICajeroService
    {
        private readonly CajeroRepository _cajeroRepository;

        public CajeroService()
        {
            _cajeroRepository = new CajeroRepository();
        }

        public async Task<bool> CrearCajeroAsync(Cajero cajero)
        {
            if (cajero == null)
                throw new ArgumentNullException(nameof(cajero));

            ValidarDatosCajero(cajero);

            // Verificar si el usuario ya existe
            if (await VerificarUsuarioExistenteAsync(cajero.Usuario))
                throw new Exception("El usuario ya existe en el sistema.");

            // Asignar barra automáticamente según el nivel
            cajero.Barra = DeterminarBarra(cajero.NivelAcceso);

            return await _cajeroRepository.CrearCajeroAsync(cajero);
        }

        public async Task<bool> VerificarUsuarioExistenteAsync(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentException("El usuario no puede estar vacío.");

            return await _cajeroRepository.VerificarUsuarioExistenteAsync(usuario);
        }

        public async Task<List<Cajero>> ObtenerCajerosAsync()
        {
            return await _cajeroRepository.ObtenerCajerosAsync();
        }

        public async Task<Cajero> ObtenerCajeroPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del cajero no es válido.");
            Console.WriteLine(id);
            return await _cajeroRepository.ObtenerCajeroPorIdAsync(id);
        }

        public async Task<bool> ActualizarCajeroAsync(Cajero cajero)
        {
            if (cajero == null)
                throw new ArgumentNullException(nameof(cajero));

            ValidarDatosCajero(cajero);

            // No validamos usuario existente en actualización porque podría ser el mismo
            cajero.Barra = DeterminarBarra(cajero.NivelAcceso);

            return await _cajeroRepository.ActualizarCajeroAsync(cajero);
        }

        public async Task<List<Cajero>> BuscarCajerosPorNombreAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return await ObtenerCajerosAsync();

            return await _cajeroRepository.BuscarCajerosPorNombreAsync(nombre);
        }

        private void ValidarDatosCajero(Cajero cajero)
        {
            if (string.IsNullOrWhiteSpace(cajero.Usuario))
                throw new ArgumentException("El usuario no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(cajero.Clave))
                throw new ArgumentException("La clave no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(cajero.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (cajero.NivelAcceso != 5 && cajero.NivelAcceso != 9)
                throw new ArgumentException("El nivel de acceso debe ser 5 o 9.");

            // Validar que la barra tenga al menos 4 caracteres
            if (cajero.Barra.ToString().Length < 4)
                throw new ArgumentException("La barra debe tener al menos 4 dígitos.");
        }

        private int DeterminarBarra(int nivelAcceso)
        {
            return nivelAcceso == 9 ? 1 : 2;
        }
    }
}