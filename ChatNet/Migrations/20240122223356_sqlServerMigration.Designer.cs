﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatNet.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20240122223356_sqlServerMigration")]
    partial class sqlServerMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Mensaje", b =>
                {
                    b.Property<int>("MensajeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MensajeId"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("SalaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("MensajeId");

                    b.HasIndex("SalaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("Data.Models.Sala", b =>
                {
                    b.Property<int>("SalaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaID"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("LimiteUsuarios")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("SalaID");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("Data.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioID"));

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Data.Models.Mensaje", b =>
                {
                    b.HasOne("Data.Models.Sala", "salaNavigation")
                        .WithMany("mensajes")
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Usuario", "UsuarioNavigation")
                        .WithMany("Mensajes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioNavigation");

                    b.Navigation("salaNavigation");
                });

            modelBuilder.Entity("Data.Models.Sala", b =>
                {
                    b.Navigation("mensajes");
                });

            modelBuilder.Entity("Data.Models.Usuario", b =>
                {
                    b.Navigation("Mensajes");
                });
#pragma warning restore 612, 618
        }
    }
}
