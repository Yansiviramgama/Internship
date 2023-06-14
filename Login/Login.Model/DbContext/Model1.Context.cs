﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Login.Model.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class YansiViramgama325Entities : DbContext
    {
        public YansiViramgama325Entities()
            : base("name=YansiViramgama325Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Student_User> Student_User { get; set; }
        public virtual DbSet<Stu_Country> Stu_Country { get; set; }
        public virtual DbSet<Stu_City> Stu_City { get; set; }
        public virtual DbSet<Stu_State> Stu_State { get; set; }
    
        public virtual int Sp_AddEdit_City(Nullable<int> iD, string cityName, Nullable<int> stateID, Nullable<int> countryID)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var cityNameParameter = cityName != null ?
                new ObjectParameter("CityName", cityName) :
                new ObjectParameter("CityName", typeof(string));
    
            var stateIDParameter = stateID.HasValue ?
                new ObjectParameter("stateID", stateID) :
                new ObjectParameter("stateID", typeof(int));
    
            var countryIDParameter = countryID.HasValue ?
                new ObjectParameter("CountryID", countryID) :
                new ObjectParameter("CountryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_AddEdit_City", iDParameter, cityNameParameter, stateIDParameter, countryIDParameter);
        }
    
        public virtual int Sp_AddEdit_Country(Nullable<int> iD, string countryName)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var countryNameParameter = countryName != null ?
                new ObjectParameter("CountryName", countryName) :
                new ObjectParameter("CountryName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_AddEdit_Country", iDParameter, countryNameParameter);
        }
    
        public virtual int Sp_AddEdit_State(Nullable<int> iD, string stateName, Nullable<int> countryID)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var stateNameParameter = stateName != null ?
                new ObjectParameter("StateName", stateName) :
                new ObjectParameter("StateName", typeof(string));
    
            var countryIDParameter = countryID.HasValue ?
                new ObjectParameter("CountryID", countryID) :
                new ObjectParameter("CountryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_AddEdit_State", iDParameter, stateNameParameter, countryIDParameter);
        }
    
        public virtual int Sp_Delete_City(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_Delete_City", iDParameter);
        }
    
        public virtual int Sp_Delete_Country(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_Delete_Country", iDParameter);
        }
    
        public virtual int Sp_Delete_State(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_Delete_State", iDParameter);
        }
    }
}
