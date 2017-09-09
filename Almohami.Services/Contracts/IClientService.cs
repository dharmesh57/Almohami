using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Almohami.Services.Contracts
{
   public interface IClientService
    {
       List<Client> GetAllActiveClient(ClientEntityModel model);

        void AddOrUpdateClient(ClientEntityModel cliententitymodel);

        ClientEntityModel GetClientById(Int64 clientid);

        //byte[] GetImage(Int64 id);
        void DeleteClient(int clientId);

        //FileStreamResult GetImages(Int64 id);
    }
}
