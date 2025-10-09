using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerViewModel? GetTrainerDetails(int TrainerId);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainer(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer);
        bool RemoveTrainer(int TrainerId);
    }
}
