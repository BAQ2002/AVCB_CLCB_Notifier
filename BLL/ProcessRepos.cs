using DAL.DBContext;
using MODEL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProcessRepos
    {
        public static void Add(string _Type, string _ProcessNumber, string EstablishmentFantasyName, string _ClientName, DateTime _IssuanceDate, DateTime _ExpirationDate, string _Status, string _Extra)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                DocProcess process_ = new DocProcess();
                process_.Type = _Type;
                process_.ProcessNumber = _ProcessNumber;
                process_.EstablishmentFantasyName = EstablishmentFantasyName;
                process_.ClientName = _ClientName;
                process_.IssuanceDate = _IssuanceDate;
                process_.ExpirationDate = _ExpirationDate;
                process_.Status = _Status;
                process_.Extra = _Extra;

                dbContext.Add(process_);
                dbContext.SaveChanges();
            }
        }

        public static void Update(DocProcess _process)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                var process_ = dbContext.DocProcesses.Single(p => p.ProcessNumber == _process.ProcessNumber);
                process_.Type = _process.Type;
                process_.ProcessNumber = _process.ProcessNumber;
                process_.EstablishmentFantasyName = _process.EstablishmentFantasyName;
                process_.ClientName = _process.ClientName;
                process_.IssuanceDate = _process.IssuanceDate;
                process_.ExpirationDate = _process.ExpirationDate;
                process_.Status = _process.Status;
                process_.Extra = _process.Extra;

                dbContext.SaveChanges();
            }


        }

        public static void Delete(DocProcess _process)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                var process_ = dbContext.DocProcesses.Single(p => p.ProcessNumber == _process.ProcessNumber);
                dbContext.Remove(process_);
                dbContext.SaveChanges();
            }
        }

        public static List<DocProcess> GetAllProcesses()
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                return dbContext.DocProcesses.ToList();
            }
        }

        public static DocProcess Get_Process_ByProcessNumber(DocProcess _process)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var process_ = dbContext.DocProcesses.Single(p => p.ProcessNumber == _process.ProcessNumber);
                return process_;
            }

        }

        public static DocProcess Get_Process_ByFantasyName(DocProcess _process)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var process_ = dbContext.DocProcesses.Single(p => p.ProcessNumber == _process.ProcessNumber);
                return process_;
            }
        }
        public static DocProcess Get_Process_ByClientName(DocProcess _process)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var process_ = dbContext.DocProcesses.Single(p => p.ProcessNumber == _process.ProcessNumber);
                return process_;
            }
        }
    }
}
