using System;
using System.Collections.Generic;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.IServices
{
    public interface ITrainingService
    {
        IEnumerable<RecordForTraining> GetAllRecords();
        IEnumerable<RecordForTraining> GetRecordsByTrainerid(int trainerId);
        IEnumerable<RecordForTraining> GetRecordsByClientId(int clientId);
        bool AddRecord(Client client, User trainer, DateTime time);
        int GetQuntityOfClientForTrainerCurrentDay(DateTime day, int trainerId);
        int GetQuntityOfClientForTrainerCurrentWeek(DateTime day, int trainerId);
        void ChangeTrainingStatus();
    }
}
