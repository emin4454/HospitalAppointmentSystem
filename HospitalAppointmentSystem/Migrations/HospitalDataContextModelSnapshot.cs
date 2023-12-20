﻿// <auto-generated />
using System;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HospitalAppointmentSystem.Migrations
{
    [DbContext(typeof(HospitalDataContext))]
    partial class HospitalDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Appointment", b =>
                {
                    b.Property<int>("appointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("appointmentId"));

                    b.Property<DateTime>("appointmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("appointmentTime")
                        .HasColumnType("interval");

                    b.Property<int>("doctorId")
                        .HasColumnType("integer");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("appointmentId");

                    b.HasIndex("doctorId");

                    b.HasIndex("userId");

                    b.ToTable("appointments", (string)null);
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Branch", b =>
                {
                    b.Property<int>("branchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("branchId"));

                    b.Property<string>("branchName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("branchId");

                    b.ToTable("branches", (string)null);
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Doctor", b =>
                {
                    b.Property<int>("doctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("doctorId"));

                    b.Property<int>("branchId")
                        .HasColumnType("integer");

                    b.Property<string>("doctorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("policlinicId")
                        .HasColumnType("integer");

                    b.Property<int>("worktimeLength")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("worktimeStart")
                        .HasColumnType("interval");

                    b.HasKey("doctorId");

                    b.HasIndex("branchId");

                    b.HasIndex("policlinicId");

                    b.ToTable("doctors", (string)null);
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Policlinic", b =>
                {
                    b.Property<int>("policlinicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("policlinicId"));

                    b.Property<string>("policlinicName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("policlinicId");

                    b.ToTable("policlinics", (string)null);
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("userId"));

                    b.Property<int>("isAdmin")
                        .HasColumnType("integer");

                    b.Property<string>("userMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("userPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("userId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Appointment", b =>
                {
                    b.HasOne("HospitalAppointmentSystem.Models.Doctor", "doctor")
                        .WithMany("appointments")
                        .HasForeignKey("doctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalAppointmentSystem.Models.User", "user")
                        .WithMany("appointments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("user");
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Doctor", b =>
                {
                    b.HasOne("HospitalAppointmentSystem.Models.Branch", "branch")
                        .WithMany("doctors")
                        .HasForeignKey("branchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalAppointmentSystem.Models.Policlinic", "policlinic")
                        .WithMany("doctors")
                        .HasForeignKey("policlinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("branch");

                    b.Navigation("policlinic");
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Branch", b =>
                {
                    b.Navigation("doctors");
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Doctor", b =>
                {
                    b.Navigation("appointments");
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.Policlinic", b =>
                {
                    b.Navigation("doctors");
                });

            modelBuilder.Entity("HospitalAppointmentSystem.Models.User", b =>
                {
                    b.Navigation("appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
