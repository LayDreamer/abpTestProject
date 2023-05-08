using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using EasyAbp.PrivateMessaging.EntityFrameworkCore;
using EasyAbp.Abp.Trees.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using LocalTest.Books;
using LocalTest.Families;
using LocalTest.FamilyTrees;
using LocalTest.FamilyLibs;
using LocalTest.MaterialSpecificationList;
using LocalTest.FactoryList;

namespace LocalTest.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class LocalTestDbContext :
    AbpDbContext<LocalTestDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Book> Books { get; set; }

    public DbSet<Family> Families { get; set; }

    public DbSet<FamilyTree> Trees { get; set; }

    public DbSet<FamilyLib> FamilyLibs { get; set; }

    public DbSet<Project> Projects { get; set; }
    public DbSet<MaterialSpecification> Materials { get; set; }

    public DbSet<MaterialSpecificationDetail> MaterialDetails { get; set; }

    public DbSet<RequisitionList> RequisitionList { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public LocalTestDbContext(DbContextOptions<LocalTestDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        #region 默认配置
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        #endregion

        #region 新增模块
        builder.ConfigurePrivateMessaging();
        builder.ConfigureBlobStoring();
        #endregion
        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(LocalTestConsts.DbTablePrefix + "YourEntities", LocalTestConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Book>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "Books",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        builder.Entity<Family>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "Families",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.ProcductName).IsRequired().HasMaxLength(128);
            b.Property(x => x.FileName).IsRequired().HasMaxLength(128);
        });

        builder.Entity<FamilyTree>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "FamilyTree",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.DisplayName).IsRequired().HasMaxLength(128);
            //b.Property(x => x.BlobName).IsRequired().HasMaxLength(128);
        }
        );

        builder.Entity<FamilyLib>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "FamilyLibs",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.DisplayName).IsRequired().HasMaxLength(128);
            //b.Property(x => x.FilePath).IsRequired().HasMaxLength(128);
        }
        );

        builder.Entity<Project>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "Projects",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Code).IsRequired().HasMaxLength(128);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            //b.Property(x => x.FilePath).IsRequired().HasMaxLength(128);
        }
        );

        builder.Entity<MaterialSpecification>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "MaterialSpecificationList",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.ProjectCode).IsRequired().HasMaxLength(128);
            b.Property(x => x.ProjectName).IsRequired().HasMaxLength(128);
            //b.Property(x => x.FilePath).IsRequired().HasMaxLength(128);
        }
        );

        builder.Entity<MaterialSpecificationDetail>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "MaterialSpecificationDetails",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.ProjectName).IsRequired().HasMaxLength(128);
            //b.Property(x => x.FilePath).IsRequired().HasMaxLength(128);
        });


        builder.Entity<RequisitionList>(b =>
        {
            b.ToTable(LocalTestConsts.DbTablePrefix + "RequisitionList",
                LocalTestConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });
    }
}
