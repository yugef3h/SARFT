using NRails.Configuration;
using Service.Data.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SarftService.ServiceCommon
{
    public class DBClinetHelp
    {
        public static DBClient Client()
        {
            DBClient client = ServiceContainer.ServiceHostContainer.Current.Session["tran"] as DBClient;
            if (client == null)
                throw new Exception("事务未开启");
            return client;
        }
    }
}
