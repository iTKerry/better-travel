﻿// <auto-generated />
using System;
using BetterTravel.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BetterTravel.DataAccess.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200703084626_ChatSettingsCascadeDelete")]
    partial class ChatSettingsCascadeDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BetterTravel.Domain.Entities.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ChatID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChatId")
                        .HasColumnName("TelegramChatID")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Chat","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.ChatCountrySubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ChatCountrySubscriptionID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<int?>("SettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("SettingsId");

                    b.ToTable("ChatCountrySubscription","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.ChatDepartureSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ChatDepartureSubscriptionID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartureId")
                        .HasColumnType("int");

                    b.Property<int?>("SettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartureId");

                    b.HasIndex("SettingsId");

                    b.ToTable("ChatDepartureSubscription","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.ChatSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ChatSettingsID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("bit");

                    b.Property<int>("SettingsOfChatId")
                        .HasColumnName("SettingsOfChatID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SettingsOfChatId")
                        .IsUnique();

                    b.ToTable("ChatSettings");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.DepartureLocation", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("DepartureLocationID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DepartureLocation","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.HotTour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("HotTourID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartureLocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DepartureLocationId");

                    b.ToTable("HotTour","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.HotelCategory", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("HotelCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HotelCategory","dbo");
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.Chat", b =>
                {
                    b.OwnsOne("BetterTravel.Domain.ValueObjects.ChatInfo", "Info", b1 =>
                        {
                            b1.Property<int>("ChatId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Description")
                                .HasColumnName("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Title")
                                .HasColumnName("Title")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnName("Type")
                                .HasColumnType("int");

                            b1.HasKey("ChatId");

                            b1.ToTable("Chat");

                            b1.WithOwner()
                                .HasForeignKey("ChatId");
                        });
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.ChatCountrySubscription", b =>
                {
                    b.HasOne("BetterTravel.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("BetterTravel.Domain.Entities.ChatSettings", "Settings")
                        .WithMany("CountrySubscriptions")
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.ChatDepartureSubscription", b =>
                {
                    b.HasOne("BetterTravel.Domain.Entities.DepartureLocation", "Departure")
                        .WithMany()
                        .HasForeignKey("DepartureId");

                    b.HasOne("BetterTravel.Domain.Entities.ChatSettings", "Settings")
                        .WithMany("DepartureSubscriptions")
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.ChatSettings", b =>
                {
                    b.HasOne("BetterTravel.Domain.Entities.Chat", "Chat")
                        .WithOne("Settings")
                        .HasForeignKey("BetterTravel.Domain.Entities.ChatSettings", "SettingsOfChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BetterTravel.Domain.Entities.HotTour", b =>
                {
                    b.HasOne("BetterTravel.Domain.Entities.HotelCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("BetterTravel.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("BetterTravel.Domain.Entities.DepartureLocation", "DepartureLocation")
                        .WithMany()
                        .HasForeignKey("DepartureLocationId");

                    b.OwnsOne("BetterTravel.Domain.ValueObjects.Duration", "Duration", b1 =>
                        {
                            b1.Property<int>("HotTourId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Count")
                                .HasColumnName("DurationCount")
                                .HasColumnType("int");

                            b1.Property<int>("Type")
                                .HasColumnName("DurationType")
                                .HasColumnType("int");

                            b1.HasKey("HotTourId");

                            b1.ToTable("HotTour");

                            b1.WithOwner()
                                .HasForeignKey("HotTourId");
                        });

                    b.OwnsOne("BetterTravel.Domain.ValueObjects.HotTourInfo", "Info", b1 =>
                        {
                            b1.Property<int>("HotTourId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("DepartureDate")
                                .HasColumnName("DepartureDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("DetailsUri")
                                .HasColumnName("DetailsLink")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ImageUri")
                                .HasColumnName("ImageLink")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnName("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("HotTourId");

                            b1.ToTable("HotTour");

                            b1.WithOwner()
                                .HasForeignKey("HotTourId");
                        });

                    b.OwnsOne("BetterTravel.Domain.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<int>("HotTourId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Amount")
                                .HasColumnName("PriceAmount")
                                .HasColumnType("int");

                            b1.Property<int>("Type")
                                .HasColumnName("PriceType")
                                .HasColumnType("int");

                            b1.HasKey("HotTourId");

                            b1.ToTable("HotTour");

                            b1.WithOwner()
                                .HasForeignKey("HotTourId");
                        });

                    b.OwnsOne("BetterTravel.Domain.ValueObjects.Resort", "Resort", b1 =>
                        {
                            b1.Property<int>("HotTourId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Name")
                                .HasColumnName("ResortName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("HotTourId");

                            b1.ToTable("HotTour");

                            b1.WithOwner()
                                .HasForeignKey("HotTourId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
