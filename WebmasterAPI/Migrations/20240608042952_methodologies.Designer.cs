﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebmasterAPI.Shared.Persistence.Contexts;

#nullable disable

namespace WebmasterAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240608042952_methodologies")]
    partial class methodologies
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebmasterAPI.Authentication.Domain.Models.Developer", b =>
                {
                    b.Property<long>("developer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("completed_projects")
                        .HasColumnType("int");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("profile_img_url")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("specialties")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("developer_id");

                    b.HasIndex("user_id");

                    b.ToTable("Developers", (string)null);
                });

            modelBuilder.Entity("WebmasterAPI.Authentication.Domain.Models.Enterprise", b =>
                {
                    b.Property<long>("enterprise_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("RUC")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("enterprise_name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("profile_img_url")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("sector")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.Property<string>("website")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("enterprise_id");

                    b.HasIndex("user_id");

                    b.ToTable("Enterprises", (string)null);
                });

            modelBuilder.Entity("WebmasterAPI.Models.User", b =>
                {
                    b.Property<long>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("user_type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WebmasterAPI.Projects.Domain.Models.Deliverable", b =>
                {
                    b.Property<long>("deliverable_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<long>("developer_id")
                        .HasColumnType("bigint");

                    b.Property<string>("file")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<long>("projectID")
                        .HasColumnType("bigint");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("deliverable_id");

                    b.HasIndex("developer_id");

                    b.HasIndex("projectID");

                    b.ToTable("Deliverables", (string)null);
                });

            modelBuilder.Entity("WebmasterAPI.Projects.Domain.Models.Project", b =>
                {
                    b.Property<long>("projectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("budget")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("descriptionProject")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("developer_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("enterprise_id")
                        .HasColumnType("bigint");

                    b.Property<string>("frameworks")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("languages")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("methodologies")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("nameProject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("projectID");

                    b.HasIndex("enterprise_id");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("WebmasterAPI.Authentication.Domain.Models.Developer", b =>
                {
                    b.HasOne("WebmasterAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebmasterAPI.Authentication.Domain.Models.Enterprise", b =>
                {
                    b.HasOne("WebmasterAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebmasterAPI.Projects.Domain.Models.Deliverable", b =>
                {
                    b.HasOne("WebmasterAPI.Authentication.Domain.Models.Developer", "Developer")
                        .WithMany()
                        .HasForeignKey("developer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebmasterAPI.Projects.Domain.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("projectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("WebmasterAPI.Projects.Domain.Models.Project", b =>
                {
                    b.HasOne("WebmasterAPI.Authentication.Domain.Models.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("enterprise_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");
                });
#pragma warning restore 612, 618
        }
    }
}
