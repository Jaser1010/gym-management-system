using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
	public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
	{
		private readonly GymDbContext _dbContext;

		public BookingRepository(GymDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public IEnumerable<MemberSession> GetBySessionId(int sessionId)
		{
			return _dbContext.MemberSessions.Include(X => X.Member)
									  .Where(X => X.SessionId == sessionId).ToList();
		}

	}
}
