// <auto-generated />
using System;
using AlugaCarros.Locacoes.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlugaCarros.Locacoes.Infra.Migrations
{
    [DbContext(typeof(LocacoesDbContext))]
    [Migration("20220606111354_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AlugaCarros.Locacoes.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AgencyCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ClientDocument")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReservationCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("SecurityDepositValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("VehicleGroupCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehiclePlate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Locations", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
