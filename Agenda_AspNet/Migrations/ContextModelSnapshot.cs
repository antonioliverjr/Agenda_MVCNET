﻿// <auto-generated />
using System;
using Agenda_AspNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Agenda_AspNet.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Agenda_AspNet.Models.Categoria", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Agenda_AspNet.Models.Contato", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("categoria_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("data_criacao")
                        .HasColumnType("datetime");

                    b.Property<string>("descricao")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("foto")
                        .HasColumnType("text");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("sobrenome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("categoria_id");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("Agenda_AspNet.Models.Endereco", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("bairro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("cep")
                        .HasColumnType("int");

                    b.Property<string>("complemento")
                        .HasColumnType("text");

                    b.Property<int>("contato_id")
                        .HasColumnType("int");

                    b.Property<string>("localidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("logradouro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("nome_contato_id")
                        .HasColumnType("int");

                    b.Property<string>("numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("uf")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.HasKey("id");

                    b.HasIndex("nome_contato_id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Agenda_AspNet.Models.Contato", b =>
                {
                    b.HasOne("Agenda_AspNet.Models.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("categoria_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("Agenda_AspNet.Models.Endereco", b =>
                {
                    b.HasOne("Agenda_AspNet.Models.Contato", "contato")
                        .WithMany("enderecos")
                        .HasForeignKey("nome_contato_id");

                    b.Navigation("contato");
                });

            modelBuilder.Entity("Agenda_AspNet.Models.Contato", b =>
                {
                    b.Navigation("enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
