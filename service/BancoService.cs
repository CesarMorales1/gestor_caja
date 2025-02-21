using System;
using System.Threading.Tasks;
using apos_gestor_caja.Domain.Models;
using apos_gestor_caja.applicationLayer.interfaces;
using apos_gestor_caja.Infrastructure.Repositories;

namespace apos_gestor_caja.ApplicationCapa.Services
{
    public class BancoService : IBancoService
    {
        private readonly BancoRepository _bancoRepository;

        public BancoService(BancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository ?? throw new ArgumentNullException(nameof(bancoRepository));
        }
        public async Task AddBancoAsync(Banco banco)
        {
            if (banco == null)
                throw new ArgumentNullException(nameof(banco));
            if (string.IsNullOrWhiteSpace(banco.Nombre))
            {
                throw new ArgumentException("El nombre del banco no puede estar vacío.", nameof(banco.Nombre));
            }

            await _bancoRepository.AddBancoAsync(banco);
        }

        public async Task<Banco> GetBancoByIdAsync(int id)
        {
            var banco = await _bancoRepository.GetBancoByIdAsync(id);
            if (banco == null)
            {
                throw new Exception($"No se encontró el banco con el ID {id}."); // Consider using a custom NotFoundException
            }
            return banco;
        }
    }
}