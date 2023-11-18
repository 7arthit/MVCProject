using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Data
{
    public class AppDBContext : DbContext
    {
        //สร้าง คอนสครัคเตอร์
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } //สร้างเก็บฐานข้อมูล
    }
}
