using Microsoft.EntityFrameworkCore;
using Whisper.Data.Context;
using System.Threading.Tasks;


namespace Whisper.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(WhisperDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
