﻿// <auto-generated />
using System;
using BetterMomshWebAPI.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetterMomshWebAPI.Migrations
{
    [DbContext(typeof(API_DataContext))]
    partial class API_DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.BabyBook", b =>
                {
                    b.Property<long>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("BookId"));

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("BookId");

                    b.HasIndex("user_id");

                    b.ToTable("BabyBook");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Journal", b =>
                {
                    b.Property<long>("journalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("journalId"));

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Entry_Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("JournalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PhotoData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("journalEntry")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<long>("weekId")
                        .HasColumnType("bigint");

                    b.HasKey("journalId");

                    b.HasIndex("BookId");

                    b.HasIndex("user_id");

                    b.HasIndex("weekId");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Month", b =>
                {
                    b.Property<long>("MonthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("MonthId"));

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<string>("Months")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TrimesterId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("MonthId");

                    b.HasIndex("BookId");

                    b.HasIndex("TrimesterId");

                    b.HasIndex("user_id");

                    b.ToTable("Months");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("RefreshTokens")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TokenCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("TokenExpired")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("user_id")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.TokenBlacklist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TokenBlacklist");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Trimester", b =>
                {
                    b.Property<long>("TrimesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("TrimesterId"));

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<string>("Trimesters")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("TrimesterId");

                    b.HasIndex("BookId");

                    b.HasIndex("user_id");

                    b.ToTable("Trimester");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.UserCredential", b =>
                {
                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("user_id");

                    b.ToTable("user_credential");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.UserInformation", b =>
                {
                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("ContactNumber")
                        .HasColumnType("numeric(12,2)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("RelationshipStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Religion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("user_id");

                    b.ToTable("user_profile");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Week", b =>
                {
                    b.Property<long>("weekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("weekId"));

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<long>("MonthId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<string>("week_number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("weekId");

                    b.HasIndex("BookId");

                    b.HasIndex("MonthId");

                    b.HasIndex("user_id");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.BabyBook", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "UserCred")
                        .WithMany("BabyBooks")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCred");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Journal", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.BabyBook", "babyBook")
                        .WithMany("Journals")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "user")
                        .WithMany("Journal")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.Week", "week")
                        .WithMany("journal")
                        .HasForeignKey("weekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("babyBook");

                    b.Navigation("user");

                    b.Navigation("week");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Month", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.BabyBook", "babyBook")
                        .WithMany("Month")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.Trimester", "trim")
                        .WithMany("mon")
                        .HasForeignKey("TrimesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "user")
                        .WithMany("Month")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("babyBook");

                    b.Navigation("trim");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.RefreshToken", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "userCred")
                        .WithOne("RefreshTokens")
                        .HasForeignKey("BetterMomshWebAPI.EFCore.RefreshToken", "user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userCred");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Trimester", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.BabyBook", "babyBook")
                        .WithMany("Trimesters")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "user")
                        .WithMany("Trimester")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("babyBook");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.UserInformation", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "userCred")
                        .WithOne("UserInfo")
                        .HasForeignKey("BetterMomshWebAPI.EFCore.UserInformation", "user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userCred");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Week", b =>
                {
                    b.HasOne("BetterMomshWebAPI.EFCore.BabyBook", "babyBook")
                        .WithMany("Week")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.Month", "mon")
                        .WithMany("weeks")
                        .HasForeignKey("MonthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BetterMomshWebAPI.EFCore.UserCredential", "user")
                        .WithMany("Week")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("babyBook");

                    b.Navigation("mon");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.BabyBook", b =>
                {
                    b.Navigation("Journals");

                    b.Navigation("Month");

                    b.Navigation("Trimesters");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Month", b =>
                {
                    b.Navigation("weeks");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Trimester", b =>
                {
                    b.Navigation("mon");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.UserCredential", b =>
                {
                    b.Navigation("BabyBooks");

                    b.Navigation("Journal");

                    b.Navigation("Month");

                    b.Navigation("RefreshTokens")
                        .IsRequired();

                    b.Navigation("Trimester");

                    b.Navigation("UserInfo")
                        .IsRequired();

                    b.Navigation("Week");
                });

            modelBuilder.Entity("BetterMomshWebAPI.EFCore.Week", b =>
                {
                    b.Navigation("journal");
                });
#pragma warning restore 612, 618
        }
    }
}
