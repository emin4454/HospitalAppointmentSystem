﻿using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class HospitalDataContext : IdentityDbContext<User>
    {
        public HospitalDataContext(DbContextOptions<HospitalDataContext> options) :
            base(options)
        {
        }

        public DbSet<Appointment> appointments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<Policlinic> policlinics { get; set; }
    }
}
