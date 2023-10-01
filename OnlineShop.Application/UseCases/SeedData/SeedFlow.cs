using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShop.Application.UseCase;
using OnlineShop.Business.Rule;
using OnlineShop.Business.GenerateVoucherCode;
using OnlineShop.Core.Schemas;
using OnlineShop.Core.Schemas.Base;
using OnlineShop.Services.Base;
using OnlineShop.Utils;
using static OnlineShop.Utils.CtrlUtil;
using OnlineShop.Business.GetDiscountPercentVoucherCode;

namespace OnlineShop.Application.UseCases.SyncAllPerm
{
    public class SeedFlow
    {
        private readonly IUnitOfWork uow;
        public SeedFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<Response> Seed(List<RouterPresenter> routers)
        {
            bool isSeeded = false;
            var executionStrategy = uow.CreateExecutionStrategy();

            await executionStrategy.Execute(async () =>
            {
                using IDbContextTransaction transaction = uow.BeginTransaction();
                try
                {
                    SeedGroup();
                    SeedUser();
                    SeedVoucher();
                    SeedProduct();
                    List<PermSchema> perms = CreatePerms(routers);
                    List<GroupSchema> groups = CreateGroupPerm(perms);
                    AddUserToGroup(groups);
                    isSeeded = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    isSeeded = false;
                    transaction.Rollback();
                    throw;
                }
            });
            Response response = new Response("success", isSeeded);
            return response;
        }

        private List<GroupSchema> CreateGroupPerm(List<PermSchema> perms)
        {
            List<GroupSchema> groups = uow.Groups.FindAll();
            List<GroupPerm> groupsPerms = new();
            foreach (var perm in perms)
            {
                foreach (var group in groups)
                {
                    if (group.ProfileType.Contains(perm.ProfileTypes) || perm.ProfileTypes == CtrlUtil.RoleType.PUBLIC_PROFILE)
                    {
                        GroupPerm gp = new GroupPerm();
                        gp.Perm = perm;
                        gp.Group = group;
                        groupsPerms.Add(gp);
                    }
                }
            }
            uow.GroupsPerms.Creates(groupsPerms);
            return groups;
        }

        private List<PermSchema> CreatePerms(List<RouterPresenter> routers)
        {
            List<PermSchema> perms = new List<PermSchema>();
            foreach (var router in routers)
            {
                PermSchema permSchema = new PermSchema();
                permSchema.Action = router.Template.Replace("api/", "");
                permSchema.Title = StrUtil.ConvertCamelToTitle(router.Name.Split('_')[0]);
                permSchema.ProfileTypes = "[" + router.Name.Split('_')[1] + "]";
                permSchema.Module = router.Module;
                perms.Add(permSchema);
            }
            perms = uow.Perms.Creates(perms);
            return perms;
        }

        private void AddUserToGroup(List<GroupSchema> groups)
        {
            List<UsersGroups> usersGroups = new List<UsersGroups>();
            List<UserSchema> users = uow.Users.FindAll();

            foreach (var user in users)
            {
                foreach (var group in groups)
                {
                    if (user.GroupIds.Contains(group.ProfileType))
                    {
                        UsersGroups ug = new UsersGroups();
                        ug.User = user;
                        ug.Group = group;
                        usersGroups.Add(ug);
                    }
                }
            }
            uow.UsersGroups.Creates(usersGroups);
        }

        private void SeedGroup()
        {
            GroupSchema admin = new GroupSchema { Id = 1, Title = "Admin", Description = "", ProfileType = RoleType.ADMIN_PROFILE };
            GroupSchema staff = new GroupSchema { Id = 2, Title = "Staff", Description = "", ProfileType = RoleType.STAFF_PROFILE };
            GroupSchema customer = new GroupSchema { Id = 3, Title = "Customer", Description = "", ProfileType = RoleType.CUSTOMER_PROFILE };

            List<GroupSchema> groups = new List<GroupSchema> { admin, staff, customer };
            uow.Groups.Creates(groups);
        }

        private void SeedUser()
        {
            string defaultPassword = JwtUtil.MD5Hash(UserRule.DEFAULT_PASSWORD);
            UserSchema userAdmin = new UserSchema { Id = 1, UserName = "admin", Password = defaultPassword, Email = "admin@gmail.com", GroupIds = RoleType.ADMIN_PROFILE };
            UserSchema userStaff = new UserSchema { Id = 2, UserName = "staff", Password = defaultPassword, Email = "staff@gmail.com", GroupIds = RoleType.STAFF_PROFILE };
            List<UserSchema> users = new List<UserSchema> { userAdmin, userStaff };
            uow.Users.Creates(users);
        }

        private void SeedProduct()
        {
            ProductSchema product1 = new ProductSchema { Name = "Product 1", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };
            ProductSchema product2 = new ProductSchema { Name = "Product 2", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };
            ProductSchema product3 = new ProductSchema { Name = "Product 3", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };
            ProductSchema product4 = new ProductSchema { Name = "Product 4", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };
            ProductSchema product5 = new ProductSchema { Name = "Product 5", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };
            ProductSchema product6 = new ProductSchema { Name = "Product 6", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };
            ProductSchema product7 = new ProductSchema { Name = "Product 7", Url = "url", Description = "Desc", Price = 25.5f, Currentcy = "vn", OriginLinkDetail = "link", Stock = 3 };

            List<ProductSchema> products = new List<ProductSchema> { product1, product2, product3, product4, product5, product6, product7 };

            uow.Products.Creates(products);
        }

        private void SeedVoucher()
        {
            DateTime dafaultExpiryDate = DateTime.UtcNow.AddDays(3);
            DateTime currentDate = DateTime.UtcNow;
            bool defaultActive = true;

            VoucherSchema BLACK_FRIDAY = new VoucherSchema { Code = VoucherRule.BLACK_FRIDAY, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.BLACK_FRIDAY), StartDate = currentDate, DiscountAmount = 0, EndDate = dafaultExpiryDate, Desc = "BLACK_FRIDAY", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema SPRING_SALE = new VoucherSchema { Code = VoucherRule.SPRING_SALE, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.SPRING_SALE), StartDate = currentDate, DiscountAmount = 0, EndDate = dafaultExpiryDate, Desc = "SPRING_SALE", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema VALENTINES_SALE = new VoucherSchema { Code = VoucherRule.VALENTINES_SALE, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.HALLOWEEN_SALE), StartDate = currentDate, DiscountAmount = 0, EndDate = dafaultExpiryDate, Desc = "VALENTINES_SALE", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema SUMMER_SALE = new VoucherSchema { Code = VoucherRule.SUMMER_SALE, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.SUMMER_SALE), StartDate = currentDate, DiscountAmount = 0, EndDate = dafaultExpiryDate, Desc = "SUMMER_SALE", Status = defaultActive, CurrentUsageCount = 0 };

            VoucherSchema CHRISTMAS_SALE = new VoucherSchema { Code = VoucherRule.CHRISTMAS_SALE, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.CHRISTMAS_SALE), StartDate = currentDate, DiscountAmount = 0, EndDate = dafaultExpiryDate, Desc = "CHRISTMAS_SALE", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema NEW_YEAR = new VoucherSchema { Code = VoucherRule.NEW_YEAR, StartDate = currentDate, DiscountAmount = 0, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.NEW_YEAR), EndDate = dafaultExpiryDate, Desc = "NEW_YEAR", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema FREE_SHIP = new VoucherSchema { Code = VoucherRule.FREE_SHIP, StartDate = currentDate, DiscountAmount = 0, DiscountPercent = 0, EndDate = dafaultExpiryDate, Desc = "FREE_SHIP", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema HALLOWEEN_SALE = new VoucherSchema { Code = VoucherRule.HALLOWEEN_SALE, StartDate = currentDate, DiscountAmount = 0, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.HALLOWEEN_SALE), EndDate = dafaultExpiryDate, Desc = "HALLOWEEN_SALE", Status = defaultActive, CurrentUsageCount = 0 };
            VoucherSchema WOMAN_DAY = new VoucherSchema { Code = VoucherRule.WOMAN_DAY, StartDate = currentDate, DiscountAmount = 0, DiscountPercent = GetDiscountPercentVoucherCode.getDiscountPercentVoucherCode(VoucherRule.WOMAN_DAY), EndDate = dafaultExpiryDate, Desc = "WOMAN_DAY", Status = defaultActive, CurrentUsageCount = 0 };

            List<VoucherSchema> vouchers = new List<VoucherSchema> { BLACK_FRIDAY, SPRING_SALE, VALENTINES_SALE, SUMMER_SALE, CHRISTMAS_SALE, NEW_YEAR, FREE_SHIP, HALLOWEEN_SALE, WOMAN_DAY };

            uow.Vouchers.Creates(vouchers);
        }
    }
}
