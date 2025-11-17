# Conector de Banco de Dados

Este projeto é uma biblioteca .NET que fornece uma maneira fácil de se conectar a diferentes tipos de bancos de dados usando um padrão de fábrica. Ele abstrai a complexidade de criar conexões de banco de dados específicas e fornece uma interface unificada para interagir com diferentes fontes de dados.

## Funcionalidades

- Suporte para múltiplos bancos de dados:
  - SQL Server
  - MySQL
  - PostgreSQL
  - Oracle
- Configuração fácil usando `appsettings.json`.
- Padrão de fábrica para criar conexões de banco de dados.
- Flexibilidade para criar conexões a partir da configuração ou de um objeto `DadosDeConexoes`.

## Configuração

Para configurar as conexões de banco de dados, você precisa adicionar as seguintes seções ao seu arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "CONNECTION_SQLSERVER": "Server=seu_servidor;Database=seu_banco;User Id=seu_usuario;Password=sua_senha;",
    "CONNECTION_MYSQL": "Server=seu_servidor;Database=seu_banco;Uid=seu_usuario;Pwd=sua_senha;",
    "CONNECTION_POSTGRESQL": "Host=seu_servidor;Database=seu_banco;Username=seu_usuario;Password=sua_senha;",
    "CONNECTION_ORACLE": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=seu_servidor)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=seu_servico)));User Id=seu_usuario;Password=sua_senha;"
  },
  "TipoBanco": {
    "CONNECTION": "SQL_SERVER"
  }
}
```

A seção `ConnectionStrings` contém as strings de conexão para cada banco de dados. A chave de cada string de conexão deve seguir o padrão `ESTABELECIMENTO_TIPOBANCO`.

A seção `TipoBanco` especifica o tipo de banco de dados a ser usado para cada estabelecimento. O valor pode ser `SQL_SERVER`, `MYSQL`, `POSTGRESQL`, ou `ORACLE`.

## Uso

Para usar o conector de banco de dados, você precisa primeiro configurar as conexões no seu arquivo `appsettings.json`. Em seguida, você pode usar a classe `ConnectorFactory` para criar uma conexão de banco de dados.

### Criando uma conexão a partir da configuração

Para criar uma conexão de banco de dados a partir da configuração, você pode usar o método `CreateConnection` da classe `ConnectorFactory`, passando o nome do estabelecimento como parâmetro:

```csharp
using Connector.Database;
using System.Data.Common;

// ...

DbConnection connection = ConnectorFactory.CreateConnection("CONNECTION");
```

### Criando uma conexão a partir de um objeto `DadosDeConexoes`

Você também pode criar uma conexão de banco de dados a partir de um objeto `DadosDeConexoes`. Isso é útil quando você precisa se conectar a um banco de dados que não está configurado no arquivo `appsettings.json`.

```csharp
using Connector.Database;
using System.Data.Common;

// ...

DadosDeConexoes dadosDeConexoes = new DadosDeConexoes("MINHA_CONEXAO_SQLSERVER", "Server=seu_servidor;Database=seu_banco;User Id=seu_usuario;Password=sua_senha;");
DbConnection connection = ConnectorFactory.CreateConnection(dadosDeConexoes);
```

## Exemplo

Aqui está um exemplo completo de como usar o conector de banco de dados para se conectar a um banco de dados SQL Server e executar uma consulta:

```csharp
using Connector.Database;
using System;
using System.Data.Common;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Crie uma conexão de banco de dados a partir da configuração
            using (DbConnection connection = ConnectorFactory.CreateConnection("CONNECTION"))
            {
                connection.Open();

                // Crie um comando
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM SuaTabela";

                    // Execute o comando
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Processe os resultados
                            Console.WriteLine(reader.GetString(0));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }
}
```
