using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend_MyWay.ApplicationData;

public partial class MyWayDbContext : DbContext
{
    public MyWayDbContext()
    {
    }

    public MyWayDbContext(DbContextOptions<MyWayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FavoriteVisitsView> FavoriteVisitsViews { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<TrendingVisitsView> TrendingVisitsViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<VisitsReview> VisitsReviews { get; set; }

    public virtual DbSet<VisitsRoute> VisitsRoutes { get; set; }

    public virtual DbSet<VisitsView> VisitsViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IgorPc\\SQLEXPRESS; Database=MyWayDb; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Categories)
                .HasForeignKey(d => d.SubcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_Subcategories");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Users");

            entity.HasOne(d => d.Visit).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Visits");
        });

        modelBuilder.Entity<FavoriteVisitsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FavoriteVisitsView");

            entity.Property(e => e.Cost)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("cost");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.DescriptionShort)
                .HasMaxLength(150)
                .HasColumnName("description_short");
            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(1, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TrendingVisitsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TrendingVisitsView");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("cost");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.DescriptionShort)
                .HasMaxLength(150)
                .HasColumnName("description_short");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(1, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("cost");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.Description)
                .HasMaxLength(1500)
                .HasColumnName("description");
            entity.Property(e => e.DescriptionShort)
                .HasMaxLength(150)
                .HasColumnName("description_short");
            entity.Property(e => e.DistanceKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("distance_km");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(1, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.TimeHours)
                .HasPrecision(0)
                .HasColumnName("time_hours");

            entity.HasOne(d => d.Category).WithMany(p => p.Visits)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visits_Categories");

            entity.HasOne(d => d.Country).WithMany(p => p.Visits)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visits_Countries");

            entity.HasOne(d => d.Level).WithMany(p => p.Visits)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visits_Levels");
        });

        modelBuilder.Entity<VisitsReview>(entity =>
        {
            entity.HasKey(e => e.VisitReviewId);

            entity.Property(e => e.VisitReviewId).HasColumnName("visit_review_id");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Review).WithMany(p => p.VisitsReviews)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitsReviews_Reviews");

            entity.HasOne(d => d.Visit).WithMany(p => p.VisitsReviews)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitsReviews_Visits");
        });

        modelBuilder.Entity<VisitsRoute>(entity =>
        {
            entity.HasKey(e => e.VisitRouteId);

            entity.ToTable("VisitsRoute");

            entity.Property(e => e.VisitRouteId).HasColumnName("visit_route_id");
            entity.Property(e => e.RoutePath).HasColumnName("route_path");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Visit).WithMany(p => p.VisitsRoutes)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitsRoute_Visits");
        });

        modelBuilder.Entity<VisitsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VisitsView");

            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.Description)
                .HasMaxLength(1500)
                .HasColumnName("description");
            entity.Property(e => e.DistanceKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("distance_km");
            entity.Property(e => e.LevelName)
                .HasMaxLength(50)
                .HasColumnName("level_name");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(1, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.TimeHours)
                .HasPrecision(0)
                .HasColumnName("time_hours");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
