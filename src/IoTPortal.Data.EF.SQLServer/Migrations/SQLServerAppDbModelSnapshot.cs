﻿// <auto-generated />
using System;
using IoTPortal.Data.EF.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IoTPortal.Data.EF.SQLServer.Migrations
{
    [DbContext(typeof(SQLServerAppDb))]
    partial class SQLServerAppDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.DeviceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.ToTable("Devices", (string)null);
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.MeasurementGroupEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Measurements")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("MeasurementGroups", (string)null);
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.MeasurementTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Variable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("MeasurementTypes", (string)null);
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.UserDeviceRoleEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("DeviceRole")
                        .HasColumnType("int");

                    b.HasKey("UserId", "DeviceId");

                    b.HasIndex("DeviceId");

                    b.ToTable("UserDeviceRoles", (string)null);
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.MeasurementGroupEntity", b =>
                {
                    b.HasOne("IoTPortal.Data.EF.Entities.DeviceEntity", "Device")
                        .WithMany("MeasurementGroups")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.MeasurementTypeEntity", b =>
                {
                    b.HasOne("IoTPortal.Data.EF.Entities.DeviceEntity", "Device")
                        .WithMany("MeasurementTypes")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.UserDeviceRoleEntity", b =>
                {
                    b.HasOne("IoTPortal.Data.EF.Entities.DeviceEntity", "Device")
                        .WithMany("UserDeviceRoles")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTPortal.Data.EF.Entities.DeviceEntity", b =>
                {
                    b.Navigation("MeasurementGroups");

                    b.Navigation("MeasurementTypes");

                    b.Navigation("UserDeviceRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
