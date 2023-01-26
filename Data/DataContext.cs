﻿using Microsoft.EntityFrameworkCore;

namespace GetPetAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {  }

        public DbSet<Pet> Pets { get; set; }
    }
}
