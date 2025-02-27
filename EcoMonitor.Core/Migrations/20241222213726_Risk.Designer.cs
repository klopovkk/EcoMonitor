﻿// <auto-generated />
using EcoMonitor.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoMonitor.Core.Migrations
{
    [DbContext(typeof(EcoContext))]
    [Migration("20241222213726_Risk")]
    partial class Risk
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcoMonitor.Core.Models.Calculations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FactoryId")
                        .HasColumnType("int");

                    b.Property<float>("GeneralEmission")
                        .HasColumnType("real");

                    b.Property<int>("PollutionId")
                        .HasColumnType("int");

                    b.Property<float?>("Tax")
                        .HasColumnType("real");

                    b.Property<bool?>("isAir")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId");

                    b.HasIndex("PollutionId");

                    b.ToTable("Calculations");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Factories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Head")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Factories");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Pollutions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DangerRate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("EmissionStandart")
                        .HasColumnType("real");

                    b.Property<float>("FlowRate")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("TaxRateAw")
                        .HasColumnType("real");

                    b.Property<float?>("TaxRateP")
                        .HasColumnType("real");

                    b.Property<bool>("isAirPollution")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Pollutions");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.RadioCreation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("C1ns")
                        .HasColumnType("real");

                    b.Property<float?>("C1v")
                        .HasColumnType("real");

                    b.Property<float?>("C2ns")
                        .HasColumnType("real");

                    b.Property<float?>("C2v")
                        .HasColumnType("real");

                    b.Property<int>("FactoryId")
                        .HasColumnType("int");

                    b.Property<float?>("OnElectricity")
                        .HasColumnType("real");

                    b.Property<float?>("Tax")
                        .HasColumnType("real");

                    b.Property<float?>("V1ns")
                        .HasColumnType("real");

                    b.Property<float?>("V1v")
                        .HasColumnType("real");

                    b.Property<float?>("V2ns")
                        .HasColumnType("real");

                    b.Property<float?>("V2v")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId");

                    b.ToTable("Creations");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Risk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Conc")
                        .HasColumnType("real");

                    b.Property<float?>("Cr")
                        .HasColumnType("real");

                    b.Property<float?>("Date")
                        .HasColumnType("real");

                    b.Property<int>("FactoryId")
                        .HasColumnType("int");

                    b.Property<float?>("Hq")
                        .HasColumnType("real");

                    b.Property<float?>("Ladd")
                        .HasColumnType("real");

                    b.Property<float?>("OnElectricity")
                        .HasColumnType("real");

                    b.Property<int>("PollutionId")
                        .HasColumnType("int");

                    b.Property<float?>("Rfc")
                        .HasColumnType("real");

                    b.Property<float?>("Sf")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId");

                    b.HasIndex("PollutionId");

                    b.ToTable("Risk");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.TempStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FactoryId")
                        .HasColumnType("int");

                    b.Property<float>("N")
                        .HasColumnType("real");

                    b.Property<float>("T")
                        .HasColumnType("real");

                    b.Property<float>("Tax")
                        .HasColumnType("real");

                    b.Property<float>("V")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Calculations", b =>
                {
                    b.HasOne("EcoMonitor.Core.Models.Factories", "Factory")
                        .WithMany("Calculations")
                        .HasForeignKey("FactoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Core.Models.Pollutions", "Pollution")
                        .WithMany("Calculations")
                        .HasForeignKey("PollutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");

                    b.Navigation("Pollution");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.RadioCreation", b =>
                {
                    b.HasOne("EcoMonitor.Core.Models.Factories", "Factory")
                        .WithMany("Creations")
                        .HasForeignKey("FactoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Risk", b =>
                {
                    b.HasOne("EcoMonitor.Core.Models.Factories", "Factory")
                        .WithMany("Risks")
                        .HasForeignKey("FactoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Core.Models.Pollutions", "Pollution")
                        .WithMany("Risks")
                        .HasForeignKey("PollutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");

                    b.Navigation("Pollution");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.TempStorage", b =>
                {
                    b.HasOne("EcoMonitor.Core.Models.Factories", "Factory")
                        .WithMany("Storages")
                        .HasForeignKey("FactoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Factories", b =>
                {
                    b.Navigation("Calculations");

                    b.Navigation("Creations");

                    b.Navigation("Risks");

                    b.Navigation("Storages");
                });

            modelBuilder.Entity("EcoMonitor.Core.Models.Pollutions", b =>
                {
                    b.Navigation("Calculations");

                    b.Navigation("Risks");
                });
#pragma warning restore 612, 618
        }
    }
}
