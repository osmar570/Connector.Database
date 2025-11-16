using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace Connector.Database
{
    public class DbMysqlConnection : DbConnector
    {
        private readonly string _estabelecimento;
        private readonly DadosDeConexoes _dadosDeConexoes;

        public DbMysqlConnection(string estabelecimento)
        {
            _estabelecimento = estabelecimento;
        }
        public DbMysqlConnection(DadosDeConexoes dadosDeConexoes)
        {
            _dadosDeConexoes = dadosDeConexoes;
        }

        public override DbConnection GetDBConnection()
        {
            if (!string.IsNullOrEmpty(_estabelecimento))
            {
                if (!ConfigurationConnectionDatabase.Setting.ConnectionStrings.ContainsKey(_estabelecimento))
                    throw new InvalidOperationException($"String de conexão do banco de dados para o estabelecimento {_estabelecimento} não configurado no appsettings");

                var connectionString = ConfigurationConnectionDatabase.Setting.ConnectionStrings[_estabelecimento];

                if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

                return new MySqlConnection(connectionString);
            }
            else if(_dadosDeConexoes != null)
            {
                var connectionString = _dadosDeConexoes.StringConexao;
                return new MySqlConnection(connectionString);
            }
            throw new InvalidOperationException($"String de conexão do banco de dados para o estabelecimento não configurado no appsettings");
        }
    }
}
