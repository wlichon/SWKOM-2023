﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NPaperless.Services.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NPaperless.Services.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231112172458_newCorrespondent")]
    partial class newCorrespondent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NPaperless.Services.Models.Correspondent", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<long>("document_count")
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "document_count");

                    b.Property<bool>("is_insensitive")
                        .HasColumnType("boolean")
                        .HasAnnotation("Relational:JsonPropertyName", "is_insensitive");

                    b.Property<DateTime>("last_correspondence")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "last_correspondence");

                    b.Property<string>("match")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "match");

                    b.Property<long>("matching_algorithm")
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "matching_algorithm");

                    b.Property<string>("name")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("slug")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "slug");

                    b.HasKey("id");

                    b.ToTable("Correspondents");
                });
#pragma warning restore 612, 618
        }
    }
}
