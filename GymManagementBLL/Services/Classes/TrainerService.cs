using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                var emailExists = unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email == createTrainer.email).Any();
                var PhoneExists = unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == createTrainer.phone).Any();
                if (PhoneExists || emailExists) return false;
                var Trainer = new Trainer()
                {
                    Name = createTrainer.name,
                    Email = createTrainer.email,
                    Phone = createTrainer.phone,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Gender = createTrainer.Gender,
                    Address = new Address
                    {
                        BuildingNumber = createTrainer.BuildingNumber,
                        Street = createTrainer.Street,
                        City = createTrainer.City
                    },
                    Specialties = createTrainer.Specialties
                };
                unitOfWork.GetRepository<Trainer>().Add(Trainer);
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
            if (Trainers is null || !Trainers.Any()) return Enumerable.Empty<TrainerViewModel>();
            var memberViewModels = Trainers.Select(m => new TrainerViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString(),
                Specialties = m.Specialties.ToString()

            }).ToList();
            return memberViewModels;
        }

        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null) return null;
            var ViewModel = new TrainerViewModel
            {
                Name = Trainer.Name,
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                Gender = Trainer.Gender.ToString(),
                DateOfBirth = Trainer.DateOfBirth.ToShortDateString(),
                Address = $"{Trainer.Address.BuildingNumber}- {Trainer.Address.Street}- {Trainer.Address.City}",
                Specialties = Trainer.Specialties.ToString()

            };
            return ViewModel;
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null) return null;
            return new TrainerToUpdateViewModel
            {
                Name = Trainer.Name,
                email = Trainer.Email,
                phone = Trainer.Phone,
                BuildingNumber = Trainer.Address.BuildingNumber,
                Street = Trainer.Address.Street,
                City = Trainer.Address.City,
                Specialties = Trainer.Specialties
            };
        }

        public bool RemoveTrainer(int TrainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null) return false;
            var HasActiveTrainerSessions = unitOfWork.GetRepository<Session>()
                .GetAll(ms => ms.Id == TrainerId && ms.StartDate > DateTime.Now).Any();
            if (HasActiveTrainerSessions) return false;
            try
            { 
                unitOfWork.GetRepository<Trainer>().Delete(Trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTrainer(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer)
        {
            try
            {
                var EmailExists = unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email == UpdatedTrainer.email).Any();
                var phoneExists = unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == UpdatedTrainer.phone).Any();
                if (EmailExists || phoneExists) return false;
                var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                if (Trainer is null) return false;
                Trainer.Email = UpdatedTrainer.email;
                Trainer.Phone = UpdatedTrainer.phone;
                Trainer.Address.BuildingNumber = UpdatedTrainer.BuildingNumber;
                Trainer.Address.Street = UpdatedTrainer.Street;
                Trainer.Address.City = UpdatedTrainer.City;
                Trainer.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Trainer>().Update(Trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // finally
    }
}
