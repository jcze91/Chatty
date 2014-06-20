using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Microsoft.Practices.Unity;

namespace BackOffice.Utils
{
    /// <summary>
    /// Abstract class for providers
    /// </summary>
    /// <typeparam name="K">table id key type</typeparam>
    /// <typeparam name="E">dbo entity type</typeparam>
    /// <typeparam name="D">dao type</typeparam>
    /// <typeparam name="S">service type</typeparam>
    public abstract class BaseProvider<K, E, D, S>
        where K : struct, IEquatable<K>
        where E : BaseEntity<K>, new()
        where D : BaseDao<K, E>, new()
        where S : BaseService<K, E, D>, new()
    {

        protected S service { get { return Startup.container.Resolve<S>(); } }

        public BaseProvider()
        {
            Startup.container.Resolve<Runtime>().RegisterCommand(cmd_id, this);
            Startup.container.Resolve<Runtime>().RegisterCommand(cmd_insert, this);
            Startup.container.Resolve<Runtime>().RegisterCommand(cmd_update, this);
            Startup.container.Resolve<Runtime>().RegisterCommand(cmd_delete, this);
            Startup.container.Resolve<Runtime>().RegisterCommand(cmd_all, this);
        }

        protected abstract string getClass();
        protected abstract int GetFieldCount();
        protected abstract dynamic Insert(string[] args);
        protected abstract dynamic Update(string[] args);

        protected string cmd_id { get { return getClass() + "-id"; } }
        protected string cmd_insert { get { return getClass() + "-insert"; } }
        protected string cmd_update { get { return getClass() + "-update"; } }
        protected string cmd_delete { get { return getClass() + "-delete"; } }
        protected string cmd_all { get { return getClass() + "-all"; } }

        protected virtual dynamic Execute(string[] args)
        {
            /**
             * GET BY ID
             */
            if (args[0] == cmd_id && args.Length == 2)
                return service.GetById(convertToKey(args[1]));

            /**
             * INSERT
             */
            if (args[0] == cmd_insert && args.Length == GetFieldCount())
                return Insert(args);

            /**
             * UPDATE
             */
            if (args[0] == cmd_update && args.Length == GetFieldCount() + 1)
                return Insert(args);

            /**
             * DELETE
             */
            if (args[0] == cmd_delete && args.Length == 2)
                return service.Delete(convertToKey(args[1]));

            /**
             * GET ALL
             */
            if (args[0] == cmd_all && args.Length == 1)
                return service.GetAll();

            return null;
        }

        protected K convertToKey(string str)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<string, K>(new DefaultMapConfig()).Map(str);
        }
    }
}