using Microsoft.EntityFrameworkCore;

namespace Nest.BaseCore.Domain
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// 角色权限表
        /// </summary>
        public DbSet<tbRoleMenu> RoleMenu { get; set; }

        /// <summary>
        /// 角色权限表
        /// </summary>
        public DbSet<tbMenu> Menu { get; set; }

        ////对 DbContext 指定单数的表名来覆盖默认的表名
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>().ToTable("User");
        //}
    }
}
