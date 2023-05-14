using System;
using System.Collections.Generic;
using Grace.DependencyInjection;
using Inventory_server.Commands.DeliveryCompany;
using Inventory_server.Commands.ProductType;
using Inventory_server.Commands.RawMaterialProducer;
using Inventory_server.Commands.RawMaterialType;
using Inventory_server.Commands.User;
using Inventory_server.Commands.Warehouse;
using InventoryServer;
using InventoryServer.Commands;
using InventoryServer.Commands.DeliveryCompany;
using InventoryServer.Commands.ProductType;
using InventoryServer.Commands.RawMaterialProducer;
using InventoryServer.Commands.RawMaterialType;
using InventoryServer.Commands.Warehouse;
using InventoryServer.Context.Providers;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Context.Providers.RawMaterialTypes;
using InventoryServer.Context.Providers.Users;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Services.Crypt;
using InventoryServer.Services.JwtToken;

namespace Inventory_server
{
    public class Locator : ILocatorService
    {
        private readonly DependencyInjectionContainer _container;
        private static readonly Lazy<Locator> Lazy = new(() => new Locator());

        public static Locator Current => Lazy.Value;

        private Locator()
        {
            _container = new DependencyInjectionContainer();
            _container.Configure(RegisterDependencies);
        }

        private static void RegisterDependencies(IExportRegistrationBlock registration)
        {
            RegisterSingleton<IServer, Server>(registration);
            RegisterSingleton<ICommand, LoginCommand>(registration);
            RegisterSingleton<ICommand, RegisterCommand>(registration);

            RegisterSingleton<ICryptService, CryptSHA256Service>(registration);
            RegisterSingleton<IJwtTokenService>(registration,
                () => new JwtTokenService(ConfigurationTokens.JwtIssuer, ConfigurationTokens.JwtSecretKey));

            #region Providers

            RegisterSingleton<IDeliveryCompanyProvider, DeliveryCompanyProvider>(registration);
            RegisterSingleton<IProductTypeProvider, ProductTypeProvider>(registration);
            RegisterSingleton<IRawMaterialProducerProvider, RawMaterialProducerProvider>(registration);
            RegisterSingleton<IRawMaterialTypeProvider, RawMaterialTypeProvider>(registration);
            RegisterSingleton<IUserProvider, UserProvider>(registration);
            RegisterSingleton<IWarehouseProvider, WarehouseProvider>(registration);

            #endregion

            #region DeliveryCompanyCommands
            RegisterSingleton<ICommand, CreateDeliveryCompany>(registration);
            RegisterSingleton<ICommand, DeleteDeliveryCompany>(registration);
            RegisterSingleton<ICommand, GetAllDeliveryCompany>(registration);
            RegisterSingleton<ICommand, GetOneDeliveryCompany>(registration);
            RegisterSingleton<ICommand, UpdateDeliveryCompany>(registration);
            #endregion

            #region ProductTypeCommands

            RegisterSingleton<ICommand, CreateProductType>(registration);
            RegisterSingleton<ICommand, DeleteProductType>(registration);
            RegisterSingleton<ICommand, GetAllProductType>(registration);
            RegisterSingleton<ICommand, GetOneProductType>(registration);
            RegisterSingleton<ICommand, UpdateProductType>(registration);

            #endregion

            #region RawMaterialProducerCommands 

            RegisterSingleton<ICommand, CreateRawMaterialProducer>(registration);
            RegisterSingleton<ICommand, DeleteRawMaterialProducer>(registration);
            RegisterSingleton<ICommand, GetAllRawMaterialProducer>(registration);
            RegisterSingleton<ICommand, GetOneRawMaterialProducer>(registration);
            RegisterSingleton<ICommand, UpdateRawMaterialProducer>(registration);

            #endregion

            #region RawMaterialTypeCommands

            RegisterSingleton<ICommand, CreateRawMaterialType>(registration);
            RegisterSingleton<ICommand, DeleteRawMaterialType>(registration);
            RegisterSingleton<ICommand, GetAllRawMaterialType>(registration);
            RegisterSingleton<ICommand, GetOneRawMaterialType>(registration);
            RegisterSingleton<ICommand, UpdateRawMaterialType>(registration);

            #endregion

            #region WarehouseCommands

            RegisterSingleton<ICommand, CreateWarehouse>(registration);
            RegisterSingleton<ICommand, DeleteWarehouse>(registration);
            RegisterSingleton<ICommand, GetAllWarehouse>(registration);
            RegisterSingleton<ICommand, GetOneWarehouse>(registration);
            RegisterSingleton<ICommand, UpdateWarehouse>(registration);

            #endregion

        }

        private static void RegisterSingleton<TFrom, TTo>(IExportRegistrationBlock registrationBlock) where TTo : TFrom
        {
            registrationBlock.Export<TTo>().As<TFrom>().Lifestyle.Singleton();
        }

        private static void RegisterSingleton<TFrom>(IExportRegistrationBlock registrationBlock, Func<TFrom> create)
        {
            registrationBlock.ExportFactory(create).As<TFrom>().Lifestyle.Singleton();
        }

        public object GetService(Type serviceType)
        {
            return _container.Locate(serviceType);
        }
        public bool CanLocate(Type type, ActivationStrategyFilter consider = null, object key = null)
        {
            return _container.CanLocate(type, consider, key);
        }
        public object Locate(Type type)
        {
            return _container.Locate(type);
        }
        public object LocateOrDefault(Type type, object defaultValue)
        {
            return _container.LocateOrDefault(type, defaultValue);
        }
        public T Locate<T>()
        {
            return _container.Locate<T>();
        }
        public T LocateOrDefault<T>(T defaultValue = default)
        {
            return _container.LocateOrDefault(defaultValue);
        }
        public List<object> LocateAll(Type type, object extraData = null, ActivationStrategyFilter consider = null, IComparer<object> comparer = null)
        {
            return _container.LocateAll(type, extraData, consider, comparer);
        }
        public List<T> LocateAll<T>(Type type = null, object extraData = null, ActivationStrategyFilter consider = null, IComparer<T> comparer = null)
        {
            return _container.LocateAll(type, extraData, consider, comparer);
        }
        public bool TryLocate<T>(out T value, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
        {
            return _container.TryLocate(out value, extraData, consider, withKey, isDynamic);
        }
        public bool TryLocate(Type type, out object value, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
        {
            return _container.TryLocate(type, out value, extraData, consider, withKey, isDynamic);
        }
        public object LocateByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
        {
            return _container.LocateByName(name, extraData, consider);
        }
        public List<object> LocateAllByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
        {
            return _container.LocateAllByName(name, extraData, consider);
        }
        public bool TryLocateByName(string name, out object value, object extraData = null, ActivationStrategyFilter consider = null)
        {
            return _container.TryLocateByName(name, out value, extraData, consider);
        }
        // ReSharper disable MethodOverloadWithOptionalParameter
        public object Locate(Type type, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
        {
            return _container.Locate(type, extraData, consider, withKey, isDynamic);
        }
        public T Locate<T>(object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
        {
            return _container.Locate<T>(extraData, consider, withKey, isDynamic);
        }
        // ReSharper restore MethodOverloadWithOptionalParameter
    }
}
