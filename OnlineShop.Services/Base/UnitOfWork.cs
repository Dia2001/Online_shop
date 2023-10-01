using OnlineShop.Core;
using Microsoft.EntityFrameworkCore.Storage;

namespace OnlineShop.Services.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUser Users { get; }
        IPerm Perms { get; }
        IProduct Products { get; }
        IGroup Groups { get; }
        IGroupPerm GroupsPerms { get; }
        IUserGroup UsersGroups { get; }
        ICustomer Customers { get; }
        ICategory Categories { get; }
        ICustomerGroup CustomersGroups { get; }
        IVoucher Vouchers { get; }
        IShipment Shipments { get; }
        IProductOrder ProductOrder { get; }
        IAddress Address {get;}


        int SaveChanges();
        Task SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dbContext;

        public IUser Users { get; }
        public IPerm Perms { get; }
        public IGroup Groups { get; }
        public IGroupPerm GroupsPerms { get; }
        public IUserGroup UsersGroups { get; }
        public IProduct Products { get; }
        public ICustomer Customers { get; }
        public ICategory Categories { get; }
        public ICustomerGroup CustomersGroups { get; }
        public IVoucher Vouchers { get; }
        public IShipment Shipments { get; }
        public IProductOrder ProductOrder { get; }
        public IAddress Address {get;}


        public UnitOfWork(DataContext _dataContext)
        {
            dbContext = _dataContext;
            Users = new UserService(dbContext);
            Perms = new PermService(dbContext);
            Groups = new GroupService(dbContext);
            GroupsPerms = new GroupPermService(dbContext);
            UsersGroups = new UserGroupService(dbContext);
            Products = new ProductService(dbContext);
            Customers = new CustomerService(dbContext);
            Categories = new CategoryService(dbContext);
            CustomersGroups = new CustomerGroupService(dbContext);
            Vouchers = new VoucherService(dbContext);
            Shipments = new ShipmentService(dbContext);
            ProductOrder = new ProductOrderService(dbContext);
            Address = new AddressService(dbContext);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return dbContext.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
    }
}
