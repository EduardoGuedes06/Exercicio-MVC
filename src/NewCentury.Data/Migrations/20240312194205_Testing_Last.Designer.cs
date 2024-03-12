﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewCentury.Data.Context;

#nullable disable

namespace NewCentury.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20240312194205_Testing_Last")]
    partial class Testing_Last
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NewCentury.Domain.Models.HistoricoTentativas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("JogadorId")
                        .HasColumnType("char(36)");

                    b.Property<int>("NumeroTentativa")
                        .HasColumnType("int");

                    b.Property<int>("Resultado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JogadorId");

                    b.ToTable("HistoricoTentativas");
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Jogador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Jogador", (string)null);
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Partida", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Dificuldade")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("IdJogador")
                        .HasColumnType("char(36)");

                    b.Property<string>("QuemComeca")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Vencedor")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("numeroRodadas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdJogador");

                    b.ToTable("Partida", (string)null);
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Rodada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EscolhaJogador")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("EscolhaMaquina")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("PartidaId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Player")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Resultado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartidaId");

                    b.ToTable("Rodada", (string)null);
                });

            modelBuilder.Entity("NewCentury.Domain.Models.HistoricoTentativas", b =>
                {
                    b.HasOne("NewCentury.Domain.Models.Jogador", "Jogador")
                        .WithMany()
                        .HasForeignKey("JogadorId")
                        .IsRequired();

                    b.Navigation("Jogador");
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Partida", b =>
                {
                    b.HasOne("NewCentury.Domain.Models.Jogador", "Jogador")
                        .WithMany("Partidas")
                        .HasForeignKey("IdJogador")
                        .IsRequired();

                    b.Navigation("Jogador");
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Rodada", b =>
                {
                    b.HasOne("NewCentury.Domain.Models.Partida", "Partida")
                        .WithMany("Rodadas")
                        .HasForeignKey("PartidaId")
                        .IsRequired();

                    b.Navigation("Partida");
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Jogador", b =>
                {
                    b.Navigation("Partidas");
                });

            modelBuilder.Entity("NewCentury.Domain.Models.Partida", b =>
                {
                    b.Navigation("Rodadas");
                });
#pragma warning restore 612, 618
        }
    }
}
