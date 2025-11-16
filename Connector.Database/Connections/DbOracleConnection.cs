using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;

namespace Connector.Database
{
    public class DbOracleConnection : DbConnector
    {
        private readonly string _estabelecimento;
        private readonly DadosDeConexoes _dadosDeConexao;

        public DbOracleConnection(string estabelecimento)
        {
            _estabelecimento = estabelecimento;
        }

        public DbOracleConnection(DadosDeConexoes dadosDeConexoes)
        {
            _dadosDeConexao = dadosDeConexoes;
        }
        public override DbConnection GetDBConnection()
        {
            if (!string.IsNullOrEmpty(_estabelecimento))
            {
                if (!ConfigurationConnectionDatabase.Setting.ConnectionStrings.ContainsKey(_estabelecimento))
                    throw new InvalidOperationException($"String de conexão do banco de dados para o estabelecimento {_estabelecimento} não configurado no appsettings");

                var connectionString = ConfigurationConnectionDatabase.Setting.ConnectionStrings[_estabelecimento];

                if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

                return new OracleConnection(connectionString);
            }
            else if( _dadosDeConexao != null)
            {
                var connectionString = _dadosDeConexao.StringConexao;
                return new OracleConnection(connectionString);
            }
            throw new InvalidOperationException($"String de conexão do banco de dados para o estabelecimento não configurado no appsettings");
        }
    }
}
