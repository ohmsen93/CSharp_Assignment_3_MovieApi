// <auto-generated />
using System;
using CSharp_Assignment_3_MovieApi.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSharp_Assignment_3_MovieApi.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20230302105721_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CSharp_Assignment_3_MovieApi.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "The Boy Who Lived",
                            FirstName = "Harry",
                            Gender = "Male",
                            LastName = "Potter",
                            Picture = "https://example.com/harry_potter.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "The Brightest Witch of Her Age",
                            FirstName = "Hermione",
                            Gender = "Female",
                            LastName = "Granger",
                            Picture = "https://example.com/hermione_granger.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "The Boy Who Stayed",
                            FirstName = "Ron",
                            Gender = "Male",
                            LastName = "Weasley",
                            Picture = "https://example.com/ron_weasley.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Alias = "Iron Man",
                            FirstName = "Tony",
                            Gender = "Male",
                            LastName = "Stark",
                            Picture = "https://example.com/tony_stark.jpg"
                        },
                        new
                        {
                            Id = 5,
                            Alias = "Captain America",
                            FirstName = "Steve",
                            Gender = "Male",
                            LastName = "Rogers",
                            Picture = "https://example.com/steve_rogers.jpg"
                        },
                        new
                        {
                            Id = 6,
                            Alias = "Black Widow",
                            FirstName = "Natasha",
                            Gender = "Female",
                            LastName = "Romanoff",
                            Picture = "https://example.com/natasha_romanoff.jpg"
                        });
                });

            modelBuilder.Entity("CSharp_Assignment_3_MovieApi.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Franchises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A space opera media franchise created by George Lucas",
                            Name = "Star Wars"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A series of superhero films produced by Marvel Studios",
                            Name = "Marvel Cinematic Universe"
                        });
                });

            modelBuilder.Entity("CSharp_Assignment_3_MovieApi.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ReleaseYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Trailer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "George Lucas",
                            FranchiseId = 1,
                            Genre = "Science Fiction",
                            Picture = "https://www.example.com/star-wars-episode-4.jpg",
                            ReleaseYear = new DateTime(1977, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Star Wars: Episode IV - A New Hope",
                            Trailer = "https://www.example.com/star-wars-episode-4-trailer.mp4"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Anthony Russo, Joe Russo",
                            FranchiseId = 2,
                            Genre = "Superhero",
                            Picture = "https://www.example.com/avengers-endgame.jpg",
                            ReleaseYear = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Endgame",
                            Trailer = "https://www.example.com/avengers-endgame-trailer.mp4"
                        });
                });

            modelBuilder.Entity("Movie_Character", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("Movie_Character");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            CharacterId = 1
                        },
                        new
                        {
                            MovieId = 1,
                            CharacterId = 2
                        });
                });

            modelBuilder.Entity("CSharp_Assignment_3_MovieApi.Models.Movie", b =>
                {
                    b.HasOne("CSharp_Assignment_3_MovieApi.Models.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("Movie_Character", b =>
                {
                    b.HasOne("CSharp_Assignment_3_MovieApi.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSharp_Assignment_3_MovieApi.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CSharp_Assignment_3_MovieApi.Models.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
