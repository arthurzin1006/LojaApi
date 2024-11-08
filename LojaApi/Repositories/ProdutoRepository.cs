using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositories
{
    public class ProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private readonly CarrinhoRepository _carrinhoRepository;

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Produto>> ListarProdutoDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos";
                return await conn.QueryAsync<Produto>(sql);
            }
        }

        internal Task ListarProdutosDB()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CadastrarProdutoDB(Produto produto)
        {
            using (var conn = Connection)
            {
                var sqlCadastrarProduto = "INSERT INTO Produtos ( Id, Nome, Descricao, Preco, QuantidadeEstoque) " +
                                        "VALUES (@Id, @Nome, @Descricao, @Preco, @QuantidadeEstoque);" +
                                        "SELECT LAST_INSERT_ID();";

                return await conn.ExecuteScalarAsync<int>(sqlCadastrarProduto, produto);
            }
        }

        // atualizar informações de um produto
        public async Task<int> AtualizarProdutoDB(int id, Produto produtoAtualizado)
        {
            using (var conn = Connection)
            {
                var sqlAtualizarProduto = "UPDATE Produtos SET Id = @Id,Nome = @Nome, Descricao = @Descricao, Preco = @Preco ,QuantidadeEstoque = @QuantidadeEstoque " +
                                        "WHERE Id = @Id";

                return await conn.ExecuteAsync(sqlAtualizarProduto, new { Id = id, produtoAtualizado.Nome, produtoAtualizado.Descricao, produtoAtualizado.Preco, produtoAtualizado.QuantidadeEstoque });
            }
        }

        // excluir um produto
        public async Task<bool> ExcluirProdutoDB(int id)
        {
      

            using (var conn = Connection)
            {
                var sqlExcluirProduto = "DELETE FROM Produtos WHERE Id = @Id";
                var rowsAffected = await conn.ExecuteAsync(sqlExcluirProduto, new { Id = id });
                return rowsAffected > 0;
            }
        }
        public async Task<bool> VerificarDisponibilidadeProduto(int produtoId)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT COUNT(*) FROM Produtos WHERE Id = @ProdutosId AND Disponivel = TRUE";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { ProdutoId = produtoId });

                return count > 0;
            }
        }

        public async Task<IEnumerable<Produto>> BuscarProdutos(string? nome = null, string? descricao = null, decimal? preco = null)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos WHERE 1=1";

                if (!string.IsNullOrEmpty(nome))
                {
                    sql += " AND Nome LIKE @Nome";
                }
                if (!string.IsNullOrEmpty(descricao))
                {
                    sql += " AND Descricao LIKE @Descricao";
                }
                if (preco.HasValue)
                {
                    sql += " AND Preco LIKE @Preco";
                }

                return (IEnumerable<Produto>)await conn.QueryAsync<Produto>(sql, new { Nome = $"%{nome}%", Descricao = $"%{descricao}%", Preco = preco });
            }
        }



    }
}
