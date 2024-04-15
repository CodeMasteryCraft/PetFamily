﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Infrastructure;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    [DbContext(typeof(PetFamilyDbContext))]
    partial class PetFamilyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Domain.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AnimalAttitude")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("animal_attitude");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("breed");

                    b.Property<bool>("Castration")
                        .HasColumnType("boolean")
                        .HasColumnName("castration");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Health")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("health");

                    b.Property<int?>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<bool>("OnTreatment")
                        .HasColumnType("boolean")
                        .HasColumnName("on_treatment");

                    b.Property<bool>("OnlyOneInFamily")
                        .HasColumnType("boolean")
                        .HasColumnName("only_one_in_family");

                    b.Property<string>("PeopleAttitude")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("people_attitude");

                    b.Property<bool>("Vaccine")
                        .HasColumnType("boolean")
                        .HasColumnName("vaccine");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.Domain.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Building")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("building");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Index")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("index");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ContactPhoneNumber", "PetFamily.Domain.Entities.Pet.ContactPhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("contact_phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Place", "PetFamily.Domain.Entities.Pet.Place#Place", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("place");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("VolunteerPhoneNumber", "PetFamily.Domain.Entities.Pet.VolunteerPhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("volunteer_phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Weight", "PetFamily.Domain.Entities.Pet.Weight#Weight", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Grams")
                                .HasColumnType("integer")
                                .HasColumnName("weight");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Entities.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_photo");

                    b.HasIndex("PetId")
                        .HasDatabaseName("ix_photo_pet_id");

                    b.ToTable("photo", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Entities.Vaccination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Applied")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("applied");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_vaccination");

                    b.HasIndex("PetId")
                        .HasDatabaseName("ix_vaccination_pet_id");

                    b.ToTable("vaccination", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Entities.Photo", b =>
                {
                    b.HasOne("PetFamily.Domain.Entities.Pet", null)
                        .WithMany("Photos")
                        .HasForeignKey("PetId")
                        .HasConstraintName("fk_photo_pets_pet_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Entities.Vaccination", b =>
                {
                    b.HasOne("PetFamily.Domain.Entities.Pet", null)
                        .WithMany("Vaccinations")
                        .HasForeignKey("PetId")
                        .HasConstraintName("fk_vaccination_pets_pet_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Entities.Pet", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("Vaccinations");
                });
#pragma warning restore 612, 618
        }
    }
}
