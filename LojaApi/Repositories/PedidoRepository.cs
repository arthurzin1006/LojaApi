using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositories
{
    public class PedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);


        //listar pedidos por usuario
        public async Task<Pedido> BuscarPorUsuario(string usuario)
        {
            var sql = "SELECT * FROM Pedidos WHERE UsuarioId = @Usuario";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Pedido>(sql, new { Usuario = usuario });
            }
        }
        public async Task<IEnumerable<Pedido>> ListarHistoricoPedidosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Pedidos ped WHERE ped.DataPedido IS NOT NULL";

                return await conn.QueryAsync<Pedido>(sql);
            }
        }

    }
}
