using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.16");

        modelBuilder.Entity("User", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
            b.Property<string>("Username").IsRequired().HasColumnType("TEXT");
            b.Property<string>("Email").HasColumnType("TEXT");
            b.Property<DateTime>("CreatedUtc").HasColumnType("TEXT");
            b.HasKey("Id");
            b.ToTable("Users");
        });

        modelBuilder.Entity("Device", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
            b.Property<string>("Name").IsRequired().HasColumnType("TEXT");
            b.Property<string>("SerialNumber").HasColumnType("TEXT");
            b.Property<int>("UserId").HasColumnType("INTEGER");
            b.Property<DateTime>("CreatedUtc").HasColumnType("TEXT");
            b.HasKey("Id");
            b.HasIndex("UserId");
            b.ToTable("Devices");
        });

        modelBuilder.Entity("Device", b =>
        {
            b.HasOne("User")
                .WithMany("Devices")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });
    }
}
