using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;


namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int sessionId);
        bool CreateSession(SessionViewModel CreatedSession);
        UpdateSessionViewModel? GetSessionToUpdate(int SessionId);
        bool UpdateSession(UpdateSessionViewModel UpdatedSession, int sessionId);
        bool RemoveSession(int sessionId);
    }
}
