﻿// <auto-generated />
using System;
using GoFast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoFast.Infrastructure.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20230602231758_teste_doc_motorista")]
    partial class teste_doc_motorista
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoFast.Domain.Entities.BlobStorage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdAzure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("base64")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BlobStorage");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Carro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AnoFabricacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Carro.DocumentoCarro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.HasIndex("Carro.DocumentoCarro");

                    b.ToTable("Carros");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Documento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Documento.Blob")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expedicao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("TipoDocumento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Documento.Blob");

                    b.ToTable("Documentos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Documento");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Motorista", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("Motorista.Carro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Motorista.DocumentoMotorista")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Motorista.Endereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Motorista.Carro");

                    b.HasIndex("Motorista.DocumentoMotorista");

                    b.HasIndex("Motorista.Endereco");

                    b.ToTable("Motorista");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("LoginUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.DocumentoCarro", b =>
                {
                    b.HasBaseType("GoFast.Domain.Entities.Documento");

                    b.Property<DateTime>("Renovacao")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("DocumentoCarro");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.DocumentoMotorista", b =>
                {
                    b.HasBaseType("GoFast.Domain.Entities.Documento");

                    b.HasDiscriminator().HasValue("DocumentoMotorista");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Carro", b =>
                {
                    b.HasOne("GoFast.Domain.Entities.DocumentoCarro", "DocumentoCarro")
                        .WithMany()
                        .HasForeignKey("Carro.DocumentoCarro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentoCarro");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Documento", b =>
                {
                    b.HasOne("GoFast.Domain.Entities.BlobStorage", "Blob")
                        .WithMany()
                        .HasForeignKey("Documento.Blob")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blob");
                });

            modelBuilder.Entity("GoFast.Domain.Entities.Motorista", b =>
                {
                    b.HasOne("GoFast.Domain.Entities.Carro", "Carro")
                        .WithMany()
                        .HasForeignKey("Motorista.Carro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoFast.Domain.Entities.DocumentoMotorista", "DocumentoMotorista")
                        .WithMany()
                        .HasForeignKey("Motorista.DocumentoMotorista")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoFast.Domain.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("Motorista.Endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carro");

                    b.Navigation("DocumentoMotorista");

                    b.Navigation("Endereco");
                });
#pragma warning restore 612, 618
        }
    }
}
