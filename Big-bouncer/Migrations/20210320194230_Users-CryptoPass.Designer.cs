// <auto-generated />
using System;
using Big_bouncer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Big_bouncer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210320194230_Users-CryptoPass")]
    partial class UsersCryptoPass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Big_bouncer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Password = "$2a$11$Ubk7KDbbdOPZAFx7Yznkq.2DyMtL5j73kMlCu.Sq.qqnG0kTJJQou",
                            Username = "User1"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Password = "$2a$11$q4siDfQvWubZbKJ2QTdCOu2vx1pZAN.HlI2Yqy4/CExExxTW5a2l6",
                            Username = "User2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
