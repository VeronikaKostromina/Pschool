using Microsoft.EntityFrameworkCore;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private GenericRepository<Student> studentRepository;
        private GenericRepository<Parent> parentRepository;

        public UnitOfWork(DbContext dbContext)
        {
            context = dbContext;
        }

        public GenericRepository<Student> StudentRepository
        {
            get
            {
                this.studentRepository ??= new GenericRepository<Student>(context);
                return studentRepository;
            }
        }

        public GenericRepository<Parent> ParentRepository
        {
            get
            {
                this.parentRepository ??= new GenericRepository<Parent>(context);
                return parentRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
