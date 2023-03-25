using MS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.DataLayer.Abstract
{
    public interface ITrainingRepository
    {
        IEnumerable<RecordForTraining> GetAllRecords();
        IEnumerable<RecordForTraining> GetRecordsByTrainerid(int trainerId);
        IEnumerable<RecordForTraining> GetRecordsByClientId(int clientId);
        bool AddRecord(Client client, User trainer, DateTime time);
        int GetQuntityOfClientForTrainerCurrentDay(DateTime day, int trainerId);
        int GetQuntityOfClientForTrainerCurrentWeek(DateTime day, int trainerId);
        void ChangeTrainingStatus();
        int GetQuantityOfClientForTrainerSpecificHour(DateTime time, int trainerId);
        int GetActiveSubscriptionsByClient(int clientId);
        void ChangeTrainingStatus(int trainingId, int statusId);
        List<RecordForTraining> FilterTrainingRecords(string filter, int trainerId);
        bool CancelTrainingRecord(int recordId);
    }
}
