﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedditFlairs.Core.Data;

namespace RedditFlairs.Core.Migrations
{
    [DbContext(typeof(FlairDbContext))]
    [Migration("20190828173928_SubRedditRequiresUpdate")]
    partial class SubRedditRequiresUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RedditFlairs.Core.Entities.LeaguePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QueueType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int>("SummonerId");

                    b.Property<string>("Tier")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("SummonerId");

                    b.ToTable("LeaguePositions");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.RankWeight", b =>
                {
                    b.Property<string>("RankName")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Weight");

                    b.HasKey("RankName");

                    b.ToTable("RankWeights");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.SubReddit", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("CssPattern")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasDefaultValue("");

                    b.Property<string>("FlairPattern")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasDefaultValue("");

                    b.Property<string>("QueueTypes")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasDefaultValue("");

                    b.Property<bool?>("RequiresUpdate");

                    b.HasKey("Name");

                    b.ToTable("SubReddits");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.Summoner", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("PUUID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTimeOffset?>("RankUpdatedAt");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("SummonerId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("SummonerName");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Summoners");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.SummonerValidation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("AttemptedAt");

                    b.Property<int>("Attempts");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("SummonerValidations");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.TierWeight", b =>
                {
                    b.Property<string>("TierName")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Weight");

                    b.HasKey("TierName");

                    b.ToTable("TierWeights");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("FlairsUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.UserFlair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CssText")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasDefaultValue("");

                    b.Property<bool>("NeedToSend");

                    b.Property<string>("SubRedditName");

                    b.Property<string>("Text")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasDefaultValue("");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SubRedditName");

                    b.HasIndex("UserId");

                    b.ToTable("UserFlairs");
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.LeaguePosition", b =>
                {
                    b.HasOne("RedditFlairs.Core.Entities.Summoner", "Summoner")
                        .WithMany("LeaguePositions")
                        .HasForeignKey("SummonerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.Summoner", b =>
                {
                    b.HasOne("RedditFlairs.Core.Entities.SummonerValidation", "Validation")
                        .WithOne()
                        .HasForeignKey("RedditFlairs.Core.Entities.Summoner", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RedditFlairs.Core.Entities.User", "User")
                        .WithMany("Summoners")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RedditFlairs.Core.Entities.UserFlair", b =>
                {
                    b.HasOne("RedditFlairs.Core.Entities.SubReddit", "SubReddit")
                        .WithMany()
                        .HasForeignKey("SubRedditName");

                    b.HasOne("RedditFlairs.Core.Entities.User", "User")
                        .WithMany("Flairs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
