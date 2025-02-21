using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Infrastructure.Database;
using apos_gestor_caja.Domain.Models;

namespace apos_gestor_caja.applicationLayer.interfaces
{
    //Agregar distintas funciones de bancos
    public interface IBancoService
    {
        Task AddBancoAsync(Banco banco);
        Task<Banco> GetBancoByIdAsync(int id);
    }
}