using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public ApplicationDbContext(DbContextOptions options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookList> BookLists { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            builder.Entity<BookList>().HasData(new BookList { Id = 1 });

            builder.Entity<Author>().HasData(new Author
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
            },
            new Author
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe"
            });


            builder.Entity<Book>().HasData(new Book
            {
                Title = "Dragons",
                AuthorId = 1,
                Description = "Book about dragons",
                PageCount = 222,
                Id = 1,
                Status = BookStatus.Wanted,
                BookListId = 1
            },
            new Book
            {
                Title = "Wizards",
                AuthorId = 2,
                Description = "Book about wizards",
                PageCount = 343,
                Id = 2,
                Status = BookStatus.Owned,
                BookListId = 1
            });
        }
    }
}
