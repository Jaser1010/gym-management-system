using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
	public class MembershipRepository : GenericRepository<MemberShip>, IMembershipRepository
	{
		private readonly GymDbContext _dbContext;

		public MembershipRepository(GymDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool> predicate)
		{
			return _dbContext.memberShips.Include(X => X.Plan).Include(X => X.Member).Where(predicate).ToList();
		}
	}
}
