using Almohami.Data.AlmohamiModel;
using Almohami.Data.UnitOfWork;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Almohami.Services.Services
{
    public class ClientService : IClientService
    {
        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ClientService()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>



        public List<Client> GetAllActiveClient(ClientEntityModel model)
        {
            return _unitOfWork.Repository<Client>().Table().Where(m=>m.ClientDelete==false&&m.ClientStatus==true).ToList();
        }

        //public byte[] GetImage(Int64 id)
        //{
        //    byte[] imageBytes = (from a in _unitOfWork.Repository<Client>().Table()
        //                         where a.ClientID == id
        //                         select a.ClientAvtar).SingleOrDefault();

        //    return imageBytes.ToArray();

         
        //}

        //public FileStreamResult GetImages(Int64 id)
        //{
            
        //    var image = (from a in _unitOfWork.Repository<Client>().Table()
        //                 where a.ClientID == id
        //                 select a.ClientAvtar).SingleOrDefault();

        //    var stream = new MemoryStream(image.ToArray());
        //    return new FileStreamResult(stream, "image/jpeg");
        //}

        public ClientEntityModel GetClientById(Int64 clientid)
        {

            return (from a in _unitOfWork.Repository<Client>().Table()

                    where a.ClientID == clientid && a.ClientDelete == false
                    select new ClientEntityModel
                    {
                        ClientID=a.ClientID,
                        ClientName=a.ClientName,
                        ClientName2 = a.ClientName2,
                        ClientCivilId = a.ClientCivilId,
                        ClientCompany = a.ClientCompany,
                        ClientNotes = a.ClientNotes,
                        ClientAvtar = a.ClientAvtar,
                        ClientEmailId = a.ClientEmailId,
                        ClientMobileNo = a.ClientMobileNo,
                        ClientOfficeNo =a.ClientOfficeNo,                       
                        ClientFaxNo = a.ClientFaxNo,
                        ClientAddress = a.ClientAddress,
                        ClientWebsite = a.ClientWebsite,
                        ClientDelete = a.ClientDelete,
                      
                    }).FirstOrDefault();
        }

        public void AddOrUpdateClient(ClientEntityModel cliententitymodel)
        {

            if (cliententitymodel.ClientID > 0)
            {
                // update
                var clientdata = _unitOfWork.Repository<Client>().Table().FirstOrDefault(c => c.ClientID == cliententitymodel.ClientID);

                if (clientdata == null)
                {
                    throw new Exception("Client not found");
                }
                clientdata.ClientName = cliententitymodel.ClientName;
                clientdata.ClientName2 = cliententitymodel.ClientName2;
                clientdata.ClientCivilId = cliententitymodel.ClientCivilId;
                clientdata.ClientCompany = cliententitymodel.ClientCompany;
                clientdata.ClientNotes = cliententitymodel.ClientNotes;
                clientdata.ClientAvtar = cliententitymodel.ClientAvtar;
                clientdata.ClientEmailId = cliententitymodel.ClientEmailId;
                clientdata.ClientMobileNo = cliententitymodel.ClientMobileNo;
                clientdata.ClientOfficeNo = cliententitymodel.ClientOfficeNo;
                clientdata.ClientFaxNo = cliententitymodel.ClientFaxNo;
                clientdata.ClientAddress = cliententitymodel.ClientAddress;
                clientdata.ClientWebsite = cliententitymodel.ClientWebsite;
                clientdata.ClientStatus = cliententitymodel.ClientStatus;
                clientdata.ClientDelete = cliententitymodel.ClientDelete;                            
                clientdata.ClientLastModifiedDate = DateTime.Now;
                clientdata.ClientLastModifiedBy = 1;               

                _unitOfWork.Repository<Client>().Update(clientdata);
                _unitOfWork.Save();
            }
            else
            {
                //insert
                Mapper.CreateMap<ClientEntityModel, Client>();
                var ClientDetails = Mapper.Map<ClientEntityModel, Client>(cliententitymodel);

                ClientDetails.ClientName = cliententitymodel.ClientName;
                ClientDetails.ClientName2 = cliententitymodel.ClientName2;
                ClientDetails.ClientCivilId = cliententitymodel.ClientCivilId;
                ClientDetails.ClientCompany = cliententitymodel.ClientCompany;
                ClientDetails.ClientNotes = cliententitymodel.ClientNotes;
                ClientDetails.ClientAvtar = cliententitymodel.ClientAvtar;
                ClientDetails.ClientEmailId = cliententitymodel.ClientEmailId;
                ClientDetails.ClientMobileNo = cliententitymodel.ClientMobileNo;
                ClientDetails.ClientOfficeNo = cliententitymodel.ClientOfficeNo;
                ClientDetails.ClientFaxNo = cliententitymodel.ClientFaxNo;
                ClientDetails.ClientAddress = cliententitymodel.ClientAddress;
                ClientDetails.ClientWebsite = cliententitymodel.ClientWebsite;
                ClientDetails.ClientStatus = cliententitymodel.ClientStatus;
                ClientDetails.ClientDelete = cliententitymodel.ClientDelete;                            
                ClientDetails.ClientLastModifiedDate = DateTime.Now;
                ClientDetails.ClientLastModifiedBy = 1;       
                ClientDetails.ClientCreatedBy = 1;
                ClientDetails.ClientCreatedDate = DateTime.Now;
                _unitOfWork.Repository<Client>().Add(ClientDetails);
                _unitOfWork.Save();

            }

        }
        public void DeleteClient(int clientId)
        {
            var client = _unitOfWork.Repository<Client>().Table().FirstOrDefault(c => c.ClientID == clientId);


            if (client == null)
            {
                throw new Exception("role not found");
            }

            client.ClientDelete = true;
            client.ClientStatus = false;
            _unitOfWork.Repository<Client>().Update(client);
            _unitOfWork.Save();
        }
        #endregion


    }
}
