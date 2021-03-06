// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationScheduling.Infrastructure;

namespace NotificationScheduling.Infrastructure.Migrations
{
    [DbContext(typeof(NotificationSchedulingContext))]
    partial class NotificationSchedulingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MarketId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyTypeId");

                    b.HasIndex("MarketId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.CompanyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CompanyTypes");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Market", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.ScheduleRequirements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AllowedCompanyTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MarketId")
                        .HasColumnType("int");

                    b.Property<int>("NotificationsCount")
                        .HasColumnType("int");

                    b.Property<string>("SendOnDays")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MarketId");

                    b.ToTable("ScheduleRequirements");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Company", b =>
                {
                    b.HasOne("NotificationScheduling.Domain.Entities.CompanyType", "CompanyType")
                        .WithMany("Companies")
                        .HasForeignKey("CompanyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationScheduling.Domain.Entities.Market", "Market")
                        .WithMany("Companies")
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyType");

                    b.Navigation("Market");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Notification", b =>
                {
                    b.HasOne("NotificationScheduling.Domain.Entities.Schedule", "Schedule")
                        .WithMany("Notifications")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("NotificationScheduling.Domain.Entities.Company", "Company")
                        .WithOne("Schedule")
                        .HasForeignKey("NotificationScheduling.Domain.Entities.Schedule", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.ScheduleRequirements", b =>
                {
                    b.HasOne("NotificationScheduling.Domain.Entities.Market", "Market")
                        .WithMany("ScheduleRequirements")
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Market");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Company", b =>
                {
                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.CompanyType", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Market", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("ScheduleRequirements");
                });

            modelBuilder.Entity("NotificationScheduling.Domain.Entities.Schedule", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
