using System.Collections.Generic;

namespace Connector.Database
{
    internal class DataBase
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        public Dictionary<string, string> TipoBanco { get; set; }
    }

    internal static class ConfigurationConnectionDatabase
    {
        public static DataBase Setting { get; set; } = new DataBase();
    }

    public class DadosDeConexoes
    {
        public DadosDeConexoes(string nomeString, string stringConexao)
        {
            NomeString = nomeString;
            StringConexao = stringConexao;

        }
        public string? NomeString { get; set; }
        public string? StringConexao { get; set; }
        public string? TipoBanco
        {
            get
            {
                if (NomeString.ToUpper().Contains("SQLSERVER") )
                {

                    return "SQL_SERVER";
                }
                else if (NomeString.ToUpper().Contains("ORACLE") )
                {
                    return "ORACLE";
                }
                else if (NomeString.ToUpper().Contains("MYSQL"))
                {
                    return "MYSQL";
                }
                else if (NomeString.ToUpper().Contains("POSTGRESS") || NomeString.ToUpper().Contains("POSTGRESQL") )
                {
                    return "POSTGRESQL";
                }
                else return null;
            }
        }

    }
}
