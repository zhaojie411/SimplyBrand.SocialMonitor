using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.DB
{
    public class DBProviderFactory
    {
        public static IDBProvider GetDBProvider(DBType type)
        {
            IDBProvider dbProvider = null;
            switch (type)
            {
                case DBType.SqlServer:
                    dbProvider = new SQLServerProvider();
                    break;
                case DBType.Hadoop:
                    dbProvider = new HadoopProvider();
                    break;
            }
            return dbProvider;
        }
    }
}
