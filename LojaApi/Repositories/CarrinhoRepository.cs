using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositories
{
    public class CarrinhoRepository
    {
        private readonly string _connectionString;

        public CarrinhoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private readonly PedidoRepository _pedidoRepository;

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Carrinho>> ListarCarrinhoDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Carrinho";
                return await conn.QueryAsync<Carrinho>(sql);
            }
        }

        internal Task ListarCarrinhosDB()
        {
            throw new NotImplementedException();
        }


        public async Task<int> CadastrarCarrinhoDB(Carrinho carrinho)
        {
            using (var conn = Connection)
            {
                var sqlCadastrarCarrinho = "INSERT INTO Carrinho ( Id, UsuarioId, ProdutoId, Quantidade) " +
                                        "VALUES (@Id, @UsuarioId, @ProdutoId, @Quantidade);" +
                                        "SELECT LAST_INSERT_ID();";

                return await conn.ExecuteScalarAsync<int>(sqlCadastrarCarrinho, carrinho);

            }
        }

        // excluir do carrinho
        public async Task<bool> ExcluirCarrinhoDB(int id)
        {
            using (var conn = Connection)
            {
                var sqlExcluirCarrinho = "DELETE FROM Carrinho WHERE Id = @Id";
                var rowsAffected = await conn.ExecuteAsync(sqlExcluirCarrinho, new { Id = id });
                return rowsAffected > 0;
            }
        }

        internal Task<bool> ProdutoCarrinho(int id)
        {
            throw new NotImplementedException();
        }
    }
}
