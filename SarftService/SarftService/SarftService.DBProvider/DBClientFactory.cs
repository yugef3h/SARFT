using Service.Data.Client;
using Service.Data.Manager;
using Service.Data.Manager.Client;
using SarftService.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SarftService.DBProvider
{
    public class DBClientFactory
    {
        static ClientFactory factory;

        /// <summary>
        /// 配置一个ClientFactory，并提供DBClient的获取
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DBClient GetDBClient(string connectionString)
        {
            if (factory == null)
            {
                ///配置字符串及持久框架
                DBManagerConfiguration config = new DBManagerConfiguration(InternalRepository.AdoMySql, connectionString);
                IDBManager dbManager = new DBManager(config);
                factory = new ClientFactory();
                ///添加自定义过滤器
                factory.AddConvention(new CanMapConvention());
                ///指定程序集，并生成数据架构
                factory.BuildFactory(dbManager, new Assembly[] { typeof(EntityBase).Assembly });
            }
            return factory.GetClient();
        }
    }

    /// <summary>
    /// 映射实体过滤方法
    /// </summary>
    public class CanMapConvention : ICanMapConvention
    {
        /// <summary>
        /// 继承于EntityBase类的才映射
        /// </summary>
        /// <param name="type">映射传入的方法</param>
        /// <returns></returns>
        public bool Apply(Type type)
        {
            return type.IsSubclassOf(typeof(EntityBase));
        }
    }

}
