using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
	public interface IBookingRepository : IGenericRepository<MemberSession>
	{

		// Get All Available Session For Manage Member Session
		// (Upcoming => For New Booking )
		// (Ongoing => For Attending ) 

		// Get Session By Id With Data Of Member [Id - Name - Is Attended Flag]
		IEnumerable<MemberSession> GetBySessionId(int sessionId);


		// Add Member Session Using Add Of Generic Repository 
		// Cancel  Member Session Using Remove Of Generic Repository 
		// Is  Member Attended Session Using Update Of Generic Repository 
	}
}
