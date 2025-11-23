using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                var Repo = unitOfWork.GetRepository<Trainer>();

                if (IsEmailExists(createTrainer.email) || IsPhoneExists(createTrainer.phone)) return false;
                var Trainer = mapper.Map<CreateTrainerViewModel, Trainer>(createTrainer);


                Repo.Add(Trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers is null || !Trainers.Any()) return [];

            var mappedTrainers = mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerViewModel>>(Trainers);
            return mappedTrainers;
        }
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;

            var mappedTrainer = mapper.Map<Trainer, TrainerViewModel>(Trainer);
            return mappedTrainer;
        }
        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;

            var mappedTrainer = mapper.Map<Trainer, TrainerToUpdateViewModel>(Trainer);
            return mappedTrainer;



        }
        public bool RemoveTrainer(int trainerId)
        {
            var Repo = unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = Repo.GetById(trainerId);
            if (TrainerToRemove is null || HasActiveSessions(trainerId)) return false;
            Repo.Delete(TrainerToRemove);
            return unitOfWork.SaveChanges() > 0;

        }
        public bool UpdateTrainer(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer)
        {
            var emailExist = unitOfWork.GetRepository<Member>().GetAll(
                m => m.Email == UpdatedTrainer.email && m.Id != TrainerId);

            var PhoneExist = unitOfWork.GetRepository<Member>().GetAll(
                m => m.Phone == UpdatedTrainer.phone && m.Id != TrainerId);

            if (emailExist.Any() || PhoneExist.Any()) return false;

            var Repo = unitOfWork.GetRepository<Trainer>();
            var TrainerToUpdate = Repo.GetById(TrainerId);

            if (TrainerToUpdate is null) return false;

            mapper.Map(UpdatedTrainer, TrainerToUpdate);
            TrainerToUpdate.UpdatedAt = DateTime.Now;

            return unitOfWork.SaveChanges() > 0;
        }

        // finally


        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            var existing = unitOfWork.GetRepository<Trainer>().GetAll(
                m => m.Email == email).Any();
            return existing;
        }
        private bool IsPhoneExists(string phone)
        {
            var existing = unitOfWork.GetRepository<Trainer>().GetAll(
                m => m.Phone == phone).Any();
            return existing;
        }
        private bool HasActiveSessions(int Id)
        {
            var activeSessions = unitOfWork.GetRepository<Session>().GetAll(
               s => s.TrainerId == Id && s.StartDate > DateTime.Now).Any();
            return activeSessions;
        }

        
        #endregion
    }
}
