using System.Data.Common;

namespace Connector.Database
{
    public abstract class DbConnector
    {
        public abstract DbConnection GetDBConnection();
    }
}
