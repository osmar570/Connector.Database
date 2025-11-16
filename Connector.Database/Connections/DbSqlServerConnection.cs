using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;

namespace Connector.Database
{
    public class DbSqlServerConnection : DbConnector
    {
        private readonly string _estabelecimento;
        private readonly DadosDeConexoes _dadosDeConexoes;

        public DbSqlServerConnection(string estabelecimento)
        {
            _estabelecimento = estabelecimento;
        }

        public DbSqlServerConnection(DadosDeConexoes dadosDeConexoes)
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

                return new SqlConnection(connectionString);
            }
            else if(_dadosDeConexoes != null)
            {
                var connectionString = _dadosDeConexoes.StringConexao;

                if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

                return new SqlConnection(connectionString);
            }
            throw new InvalidOperationException($"String de conexão do banco de dados para o estabelecimento não configurado no appsettings");
        }
    }
}
