using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using SIT.Data;

namespace SoftUniIssueTracker.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160326160850_defaultMigration")]
    partial class defaultMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("SIT.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("IssueId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssigneeId");

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("DueDate");

                    b.Property<string>("IssueKey")
                        .IsRequired();

                    b.Property<int>("PriorityId");

                    b.Property<int>("ProjectId");

                    b.Property<int>("StatusId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.IssueLabel", b =>
                {
                    b.Property<int>("IssueId");

                    b.Property<int>("LabelId");

                    b.Property<int>("Id");

                    b.HasKey("IssueId", "LabelId");
                });

            modelBuilder.Entity("SIT.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("LeadId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ProjectKey")
                        .IsRequired();

                    b.Property<int>("TransitionSchemeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.ProjectLabel", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("LabelId");

                    b.Property<int>("Id");

                    b.HasKey("ProjectId", "LabelId");
                });

            modelBuilder.Entity("SIT.Models.ProjectPriority", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("PriorityId");

                    b.Property<int>("Id");

                    b.HasKey("ProjectId", "PriorityId");
                });

            modelBuilder.Entity("SIT.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.StatusTransition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChildStatusId");

                    b.Property<int>("ParentStatusId");

                    b.Property<int>("TransitionSchemeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.TransitionScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SIT.Models.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SIT.Models.Comment", b =>
                {
                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("SIT.Models.Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");
                });

            modelBuilder.Entity("SIT.Models.Issue", b =>
                {
                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("SIT.Models.Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("SIT.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("SIT.Models.Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("SIT.Models.IssueLabel", b =>
                {
                    b.HasOne("SIT.Models.Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");

                    b.HasOne("SIT.Models.Label")
                        .WithMany()
                        .HasForeignKey("LabelId");
                });

            modelBuilder.Entity("SIT.Models.Project", b =>
                {
                    b.HasOne("SIT.Models.User")
                        .WithMany()
                        .HasForeignKey("LeadId");

                    b.HasOne("SIT.Models.TransitionScheme")
                        .WithMany()
                        .HasForeignKey("TransitionSchemeId");
                });

            modelBuilder.Entity("SIT.Models.ProjectLabel", b =>
                {
                    b.HasOne("SIT.Models.Label")
                        .WithMany()
                        .HasForeignKey("LabelId");

                    b.HasOne("SIT.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("SIT.Models.ProjectPriority", b =>
                {
                    b.HasOne("SIT.Models.Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("SIT.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("SIT.Models.StatusTransition", b =>
                {
                    b.HasOne("SIT.Models.Status")
                        .WithMany()
                        .HasForeignKey("ChildStatusId");

                    b.HasOne("SIT.Models.Status")
                        .WithMany()
                        .HasForeignKey("ParentStatusId");

                    b.HasOne("SIT.Models.TransitionScheme")
                        .WithMany()
                        .HasForeignKey("TransitionSchemeId");
                });
        }
    }
}
