﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TherapEase.Context;

#nullable disable

namespace TherapEase.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20231225021431_FixColumnTypesAndLengthForText")]
    partial class FixColumnTypesAndLengthForText
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TherapEase.Data.Entities.Pacient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Pacients");
                });

            modelBuilder.Entity("TherapEase.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .HasColumnType("char(128)");

                    b.Property<string>("Salt")
                        .HasColumnType("char(128)");

                    b.HasKey("Id");

                    b.ToTable("Therapists");
                });
#pragma warning restore 612, 618
        }
    }
}
