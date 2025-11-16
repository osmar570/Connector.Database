using System;
using System.Data.Common;
using System.Linq;

namespace Connector.Database
{
    public static class ConnectorFactory
    {
        public static DbConnection CreateConnection(string estabelecimento = "ASPNET")
        {
            if (!ConfigurationConnectionDatabase.Setting.TipoBanco.ContainsKey(estabelecimento))
                throw new InvalidOperationException($"Tipo de banco de dados para o estabelecimento {estabelecimento} não configurado no appsettings");

            var tipoBanco = ConfigurationConnectionDatabase.Setting.TipoBanco[estabelecimento];
            DbConnector factory = (tipoBanco?.ToUpper()) switch
            {
                "ORACLE" => new DbOracleConnection(estabelecimento.ToUpper()),
                "SQL_SERVER" => new DbSqlServerConnection(estabelecimento.ToUpper()),
                "MYSQL" => new DbMysqlConnection(estabelecimento.ToUpper()),
                "POSTGRESQL" => new DbPostgreSqlConnection(estabelecimento.ToUpper()),
                _ => throw new InvalidOperationException($"Erro ao criar a connection string para o estabelecimento \"{estabelecimento}\""),
            };
            return factory.GetDBConnection();
        }

        public static DbConnection CreateConnection(DadosDeConexoes dadosDeConexoes)
        {

            var tipoBanco = dadosDeConexoes.TipoBanco;
            DbConnector factory = (tipoBanco?.ToUpper()) switch
            {
                "ORACLE" => new DbOracleConnection(dadosDeConexoes),
                "SQL_SERVER" => new DbSqlServerConnection(dadosDeConexoes),
                "MYSQL" => new DbMysqlConnection(dadosDeConexoes),
                "POSTGRESQL" => new DbPostgreSqlConnection(dadosDeConexoes),
                _ => throw new InvalidOperationException($"Erro ao criar a connection string para o estabelecimento \"{dadosDeConexoes.NomeString}\""),
            };
            return factory.GetDBConnection();
        }

        
    }
}
