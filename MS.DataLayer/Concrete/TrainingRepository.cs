using System;
using System.Collections.Generic;
using System.Linq;
using MS.Common.Enums;
using MS.Common.Services;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.DataLayer.Concrete
{
    public class TrainingRepository : ITrainingRepository
    {

        private readonly ManagmentSystemContext _context;
        public TrainingRepository(ManagmentSystemContext context)
        {
            _context = context;
        }
        public bool AddRecord(Client client, User trainer, DateTime time)
        {
            var record = new RecordForTraining { Client = client, Trainer = trainer, TrainingDate = time, StatusId = (int)ETrainingRecordStatus.New };
            _context.RecordsForTraining.Add(record);
            _context.SaveChanges();
            return true;

        }

        public void ChangeTrainingStatus()
        {
            var time = DateTime.Now.AddHours(-1);
            var expireTrainings = _context.RecordsForTraining.Where(x => x.TrainingDate < time && x.StatusId!= (int)ETrainingRecordStatus.Closed).ToList();
            foreach (var training in expireTrainings)
            {
                training.StatusId = (int)ETrainingRecordStatus.Closed;
                _context.SaveChanges();
            }
        }
        public void ChangeTrainingStatus(int trainingId,int statusId)
        {
            var record = _context.RecordsForTraining.FirstOrDefault(x => x.Id == trainingId);
            if (record != null)
            {
                record.StatusId = statusId;
                _context.SaveChanges();
            }
        }

        public IEnumerable<RecordForTraining> GetAllRecords()
        {
            return _context.RecordsForTraining.OrderByDescending(x=>x.TrainingDate).ToList();
        }

        public int GetQuntityOfClientForTrainerCurrentDay(DateTime day, int trainerId)
        {
            return GetRecordsByTrainerid(trainerId).Count(x => x.TrainingDate.Day == day.Day && x.TrainingDate.Month == day.Month && x.TrainingDate.Year == day.Year);
        }

        public int GetQuantityOfClientForTrainerSpecificHour(DateTime time, int trainerId)
        {
            return GetRecordsByTrainerid(trainerId).Count(x => x.TrainingDate.Day == time.Day && x.TrainingDate.Hour == time.Hour && x.TrainingDate.Month == time.Month && x.TrainingDate.Year == time.Year);
        }

        public int GetQuntityOfClientForTrainerCurrentWeek(DateTime day, int trainerId)
        {
            DateTime firstDayOfWeek = DateTime.Now.FirstDayOfWeek();
            DateTime lastDayOfWeek = DateTime.Now.LastDayOfWeek();
            return GetRecordsByTrainerid(trainerId).Count(x => x.TrainingDate >= firstDayOfWeek && x.TrainingDate <= lastDayOfWeek);
        }

        public IEnumerable<RecordForTraining> GetRecordsByClientId(int clientId)
        {
            return GetAllRecords().Where(x => x.Client.Id == clientId).ToList();
        }

        public IEnumerable<RecordForTraining> GetRecordsByTrainerid(int trainerId)
        {
            return GetAllRecords().Where(x => x.Trainer.Id == trainerId).ToList();
        }

        public int GetActiveSubscriptionsByClient(int clientId)
        {
            return GetRecordsByClientId(clientId).Where(x => x.StatusId == (int)ETrainingRecordStatus.New || x.StatusId == (int)ETrainingRecordStatus.Payed).Count();
        }
        public List<RecordForTraining> FilterTrainingRecords(string filter,int trainerId)
        {
            return GetRecordsByTrainerid(trainerId).
                Where(x=>x.Client.Name.ToLower().Contains(filter.ToLower()) || x.Client.Surname.ToLower().Contains(filter.ToLower()) || x.Client.PhoneNumber.ToLower().Contains(filter.ToLower()) || x.Client.Email.ToLower().Contains(filter.ToLower())).ToList();
        }

        public bool CancelTrainingRecord(int recordId)
        {
            RecordForTraining record = _context.RecordsForTraining.FirstOrDefault(x => x.Id == recordId);
            if (record != null)
            {
                record.StatusId = (int)ETrainingRecordStatus.Closed;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
