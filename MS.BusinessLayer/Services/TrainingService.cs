using System;
using System.Collections.Generic;
using MS.BusinessLayer.IServices;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public IEnumerable<RecordForTraining> GetAllRecords()
        {
            return _trainingRepository.GetAllRecords();
        }

        public IEnumerable<RecordForTraining> GetRecordsByTrainerid(int trainerId)
        {
            return _trainingRepository.GetRecordsByTrainerid(trainerId);
        }

        public IEnumerable<RecordForTraining> GetRecordsByClientId(int clientId)
        {
            return _trainingRepository.GetRecordsByClientId(clientId);
        }

        public bool AddRecord(Client client, User trainer, DateTime time)
        {
            return _trainingRepository.AddRecord(client, trainer, time);
        }

        public int GetQuntityOfClientForTrainerCurrentDay(DateTime day, int trainerId)
        {
            return _trainingRepository.GetQuntityOfClientForTrainerCurrentDay(day, trainerId);
        }

        public int GetQuntityOfClientForTrainerCurrentWeek(DateTime day, int trainerId)
        {
            return _trainingRepository.GetQuntityOfClientForTrainerCurrentWeek(day, trainerId);
        }
        public void ChangeTrainingStatus()
        {
            _trainingRepository.ChangeTrainingStatus();
        }
    }
}
