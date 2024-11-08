using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        // cadastrar um novo usuario
        public async Task<int> CadastrarUsuarioDB(Usuario usuario)
        {
            using (var conn = Connection)
            {
                var sqlCadastrarUsuario = "INSERT INTO Usuarios ( Id, Nome, Email, Endereco) " +
                                        "VALUES (@Id, @Nome, @Email, @Endereco);" +
                                        "SELECT LAST_INSERT_ID();";

                return await conn.ExecuteScalarAsync<int>(sqlCadastrarUsuario, usuario);
            }
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarioDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Usuarios";
                return await conn.QueryAsync<Usuario>(sql);
            }
        }

        internal Task ListarUsuariosDB()
        {
            throw new NotImplementedException();
        }


    }
}
