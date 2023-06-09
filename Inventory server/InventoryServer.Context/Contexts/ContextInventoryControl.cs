﻿using System.Data.Entity;
using InventoryServer.Context.SqlConnectSettings;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Contexts
{
	public class ContextInventoryControl : DbContext
	{
		public ContextInventoryControl() : base(InventoryControlConnectSettings.SqlConnectionIntegratedSecurity)
		{
		}

		public DbSet<DeliveryCompany> DeliveryCompanies { get; set; }
		public DbSet<HistoryOfProductSold> HistoryOfProductsSolids { get; set; }
		public DbSet<HistoryDefectiveProduct> HistoryDefectiveProducts { get; set; }
		public DbSet<HistoryDefectiveRavMaterial> HistoryDefectiveRavMaterials { get; set; }
		public DbSet<PercentageOfRawMaterial> PercentOfRawMaterials { get; set; }
		public DbSet<ProductInOnePackage> ProductInOnePackages { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<RawMaterialInOnePackage> RawMaterialInOnePackages { get; set; }
		public DbSet<RawMaterialProducer> RawMaterialsProducers { get; set; }
		public DbSet<RawMaterialType> RawMaterialsTypes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Warehouse> Warehouses { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			#region DeliveryCompany

			modelBuilder.Entity<DeliveryCompany>()
				.Property(a => a.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<DeliveryCompany>()
				.HasIndex(a => a.Name)
				.IsClustered(false)
				.IsUnique(true);

			#endregion

			#region HistoryOfProductSolid

			modelBuilder.Entity<HistoryOfProductSold>()
				.Property(a => a.DateOfManufacture)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<HistoryOfProductSold>()
				.Property(a => a.Suitability)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<HistoryOfProductSold>()
				.Property(a => a.Price)
				.HasColumnType("money")
				.IsRequired();

			modelBuilder.Entity<HistoryOfProductSold>()
				.HasRequired(de => de.ProductType)
				.WithMany(d => d.HistoryOfProductSolids)
				.HasForeignKey(de => de.ProductTypeId)
				.WillCascadeOnDelete(true);

			#endregion

			#region HistoryDefectiveProduct

			modelBuilder.Entity<HistoryDefectiveProduct>()
				.HasRequired(de => de.ProductType)
				.WithMany(d => d.HistoryMarriageProducts)
				.HasForeignKey(de => de.ProductTypeId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<HistoryDefectiveProduct>()
				.Property(a => a.CostPrice)
				.HasColumnType("money")
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveProduct>()
				.Property(a => a.DateOfAssignment)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveProduct>()
				.Property(a => a.Weight)
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveProduct>()
				.Property(a => a.Note)
				.HasMaxLength(500)
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveProduct>()
				.HasIndex(ph => ph.Note)
				.IsClustered(false)
				.IsUnique(false);

			#endregion

			#region HistoryDefectiveRavMaterial

			modelBuilder.Entity<HistoryDefectiveRavMaterial>()
				.HasRequired(de => de.RawMaterialType)
				.WithMany(d => d.HistoryMarriageRavMaterials)
				.HasForeignKey(de => de.RawMaterialTypeId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<HistoryDefectiveRavMaterial>()
				.Property(a => a.CostPrice)
				.HasColumnType("money")
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveRavMaterial>()
				.Property(a => a.DateOfAssignment)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveRavMaterial>()
				.Property(a => a.Weight)
				.IsRequired();

			modelBuilder.Entity<HistoryDefectiveRavMaterial>()
				.Property(a => a.Note)
				.HasMaxLength(500)
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.HasIndex(ph => ph.Note)
				.IsClustered(false)
				.IsUnique(false);

			#endregion

			#region PercentageOfRawMaterial

			modelBuilder.Entity<PercentageOfRawMaterial>()
				.Property(a => a.Percent)
				.IsRequired();

			modelBuilder.Entity<PercentageOfRawMaterial>()
				.HasRequired(de => de.ProductType)
				.WithMany(d => d.PercentageOfRawMaterials)
				.HasForeignKey(de => de.ProductTypeId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<PercentageOfRawMaterial>()
				.HasRequired(de => de.RawMaterialType)
				.WithMany(d => d.PercentOfRawMaterials)
				.HasForeignKey(de => de.RawMaterialTypeId)
				.WillCascadeOnDelete(true);

			#endregion

			#region ProductInOnePackage

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.DateOfManufacture)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.SellBy)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.Suitability)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.LaboratoryAnalysis)
				.HasMaxLength(200);

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.Note)
				.HasMaxLength(500)
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.HasIndex(ph => ph.Note)
				.IsClustered(false)
				.IsUnique(false);

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.Price)
				.HasColumnType("money")
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.Property(a => a.Weight)
				.IsRequired();

			modelBuilder.Entity<ProductInOnePackage>()
				.HasRequired(de => de.ProductType)
				.WithMany(d => d.ProductInOnePackage)
				.HasForeignKey(de => de.ProductTypeId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<ProductInOnePackage>()
				.HasRequired(de => de.Warehouse)
				.WithMany(d => d.ProductInOnePackage)
				.HasForeignKey(de => de.WarehouseId)
				.WillCascadeOnDelete(true);

			#endregion

			#region ProductType

			modelBuilder.Entity<ProductType>()
				.Property(a => a.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<ProductType>()
				.HasIndex(ph => ph.Name)
				.IsClustered(false)
				.IsUnique(false);

			#endregion

			#region RawMaterialInOnePackage

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.DateOfManufacture)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.SellBy)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.Suitability)
				.HasColumnType("date")
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.Note)
				.HasMaxLength(500)
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.HasIndex(ph => ph.Note)
				.IsClustered(false)
				.IsUnique(false);

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.Price)
				.HasColumnType("money")
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.CostOfDelivery)
				.HasColumnType("money")
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.Weight)
				.IsRequired();

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.Property(a => a.LaboratoryAnalysis)
				.HasMaxLength(200);

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.HasRequired(de => de.DeliveryCompany)
				.WithMany(d => d.RawMaterialInOnePackage)
				.HasForeignKey(de => de.DeliveryCompanyId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.HasRequired(de => de.RawMaterialProducer)
				.WithMany(d => d.RawMaterialInOnePackage)
				.HasForeignKey(de => de.RawMaterialProducerId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.HasRequired(de => de.RawMaterialType)
				.WithMany(d => d.RawMaterialInOnePackage)
				.HasForeignKey(de => de.RawMaterialTypeId)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<RawMaterialInOnePackage>()
				.HasRequired(de => de.Warehouse)
				.WithMany(d => d.RawMaterialInOnePackage)
				.HasForeignKey(de => de.WarehouseId)
				.WillCascadeOnDelete(true);

			#endregion

			#region RawMaterialProducer

			modelBuilder.Entity<RawMaterialProducer>()
				.Property(a => a.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<RawMaterialProducer>()
				.HasIndex(a => a.Name)
				.IsClustered(false)
				.IsUnique(true);

			#endregion

			#region RawMaterialType

			modelBuilder.Entity<RawMaterialType>()
				.Property(a => a.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<RawMaterialType>()
				.HasIndex(a => a.Name)
				.IsClustered(false)
				.IsUnique(true);

			#endregion

			#region User

			modelBuilder.Entity<User>()
				.Property(a => a.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<User>()
				.HasIndex(a => a.Name)
				.IsClustered(false)
				.IsUnique(true);

			modelBuilder.Entity<User>()
				.Property(a => a.PasswordHash)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<User>()
				.HasIndex(a => a.PasswordHash)
				.IsClustered(false)
				.IsUnique(false);

			modelBuilder.Entity<User>()
				.Property(a => a.UserRole)
				.IsRequired();

			#endregion

			#region Warehouse

			modelBuilder.Entity<Warehouse>()
				.Property(a => a.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<Warehouse>()
				.HasIndex(a => a.Name)
				.IsClustered(false)
				.IsUnique(true);

			#endregion
		}
	}
}