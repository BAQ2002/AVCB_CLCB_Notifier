using DAL.DBContext;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class EstablishmentRepos
    {
        public static void Add(string _SocialReason, string _FantasyName, string _Cnpj, string _Address, string _ClientName)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                Establishment establishment_ = new Establishment();
                establishment_.SocialReason = _SocialReason;
                establishment_.FantasyName = _FantasyName;
                establishment_.Cnpj = _Cnpj;
                establishment_.Address = _Address;
                establishment_.ClientName = _ClientName;
                dbContext.Add(establishment_);
                dbContext.SaveChanges();
            }
        }

        public static void Update(Establishment _establishment)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                var establishment_ = dbContext.Establishments.Single(p => p.Cnpj == _establishment.Cnpj);
                establishment_.SocialReason = _establishment.SocialReason;
                establishment_.FantasyName = _establishment.FantasyName;
                establishment_.Cnpj = _establishment.Cnpj;
                establishment_.Address = _establishment.Address;
                establishment_.ClientName = _establishment.ClientName;
                dbContext.SaveChanges();
            }
        }

        public static void Delete(Establishment _establishment)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                var establishment_ = dbContext.Establishments.Single(p => p.Cnpj == _establishment.Cnpj);
                dbContext.Remove(establishment_);
                dbContext.SaveChanges();
            }
        }

        public static List<Establishment> GetAllEstablishments()
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                return dbContext.Establishments.ToList();
            }
        }

        public static Establishment Get_Establishment_ByCnpj(string _cnpj)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var establishment_ = dbContext.Establishments.Single(p => p.Cnpj == _cnpj);
                return establishment_;
            }
        }

        public static Establishment Get_Establishment_ByFantasyName(string _fantasyName)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var establishment_ = dbContext.Establishments.Single(p => p.FantasyName == _fantasyName);
                return establishment_;
            }
        }

        public static Establishment Get_Establishment_ByClient(string _clientName)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var establishment_ = dbContext.Establishments.Single(p => p.ClientName == _clientName);
                return establishment_;
            }
        }
    }
}
