using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

using MODEL;
using DAL.DBContext;


namespace BLL
{
    public static class ClientRepos
    {
        public static void Add(string _Name, string _Cpf, string _Email, string _CellphoneNumber, DateTime _RegistrationDate)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                Client client_ = new Client();
                client_.Name = _Name;
                client_.Cpf = _Cpf;
                client_.Email = _Email;
                client_.CellphoneNumber = _CellphoneNumber;
                client_.RegistrationDate = _RegistrationDate;
                dbContext.Clients.Add(client_);
                dbContext.SaveChanges();

            }
        }

        public static void Update(Client _client)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                var client_ = dbContext.Clients.Single(p => p.Cpf == _client.Cpf);
                client_.Name = _client.Name;
                client_.Cpf = _client.Cpf;
                client_.Email = _client.Email;
                client_.CellphoneNumber = _client.CellphoneNumber;
                dbContext.SaveChanges();
            }


        }

        public static void Delete(Client _client)
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                var client_ = dbContext.Clients.Single(P => P.Cpf == _client.Cpf);
                dbContext.Remove(client_);
                dbContext.SaveChanges();
            }
        }

        public static List<Client> GetAllClients()
        {
            using (var dbContext = new DatabaseMdfContext())
            {
                return dbContext.Clients.ToList();
            }
        }

        public static Client Get_Client_ByCpf(string _cpf)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var client_ = dbContext.Clients.Single(P => P.Cpf == _cpf);
                return client_;
            }

        }

        public static Client Get_Client_ByName(string _name)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var client_ = dbContext.Clients.Single(P => P.Name == _name);
                return client_;
            }

        }
        public static Client Get_Client_ByEmail(string _email)
        {

            using (var dbContext = new DatabaseMdfContext())
            {
                var client_ = dbContext.Clients.Single(P => P.Email == _email);
                return client_;
            }

        }
    }
}