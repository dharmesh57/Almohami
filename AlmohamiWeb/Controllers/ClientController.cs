using Almohami.Core.Helper;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmohamiWeb.Controllers
{
    public class ClientController : AlmohamiController
    {
          #region Private Variables
        private readonly IClientService _ClientServices;       
        #endregion

        #region Controller
        public ClientController()
        {
            _ClientServices = new ClientService();
            
        }
        #endregion

        // GET: Client
        public ActionResult List()
        {
            ClientEntityModel cliententitymodel = new ClientEntityModel();
            cliententitymodel.ClientStatus = true;
            cliententitymodel.ClientList = _ClientServices.GetAllActiveClient(cliententitymodel);
            return View(cliententitymodel);
            
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                ClientEntityModel cliententitymodel = new ClientEntityModel();
                cliententitymodel = _ClientServices.GetClientById(id);
               
                if (cliententitymodel != null)
                {
                    return View(cliententitymodel);
                }
                return View(cliententitymodel);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


       

        // GET: Client/Create
        public ActionResult Create()
        {
            try
            {
                ClientEntityModel cliententitymodel = new ClientEntityModel();
                return View(cliententitymodel);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(ClientEntityModel cliententitymodel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //cliententitymodel.ar = 2;
                    cliententitymodel.filesize = 10;
                    string us = UploadUserFile(file, cliententitymodel);
                    if (us != null)
                    {
                        //ViewBag.ResultMessage = cliententitymodel.getseterror;
                        ModelState.AddModelError("", cliententitymodel.getseterror);
                        return View();
                    }

                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Content/Img/clientimg/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);

                    //save new record in database

                    cliententitymodel.ClientAvtar = ImageName;

                    cliententitymodel.ClientStatus = true;
                    cliententitymodel.ClientDelete = false;
                   _ClientServices.AddOrUpdateClient(cliententitymodel);
                    return RedirectToAction("List", "Client");
                }
                return View(cliententitymodel);

            }
            catch
            {
                return View();
            }
        }

       

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ClientEntityModel cliententitymodel = new ClientEntityModel();
                //Int64 id = Convert.ToInt64(Helpers.base64Decode(CaseId));
                cliententitymodel = _ClientServices.GetClientById(id);
               
                if (cliententitymodel != null)
                {
                    return View(cliententitymodel);
                }
                return View(cliententitymodel);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(Int64 id, ClientEntityModel cliententitymodel, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                   if (ModelState.IsValid)
                    {
                        cliententitymodel.filesize = 10;
                        string us = UploadUserFile(file, cliententitymodel);
                        if (us != null)
                        {
                            //ViewBag.ResultMessage = cliententitymodel.getseterror;
                            ModelState.AddModelError("", cliententitymodel.getseterror);
                            return View();
                        }

                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/Content/Img/clientimg/" + ImageName);

                        // save image in folder
                        file.SaveAs(physicalPath);

                        //save new record in database

                        cliententitymodel.ClientAvtar = ImageName;
                       
                        cliententitymodel.ClientStatus = true;
                        cliententitymodel.ClientDelete = false;
                        _ClientServices.AddOrUpdateClient(cliententitymodel);
                        return RedirectToAction("List", "Client");
                    }
                    return View(cliententitymodel);

                }
                catch
                {
                    return View();
                }
               
            }
        [HttpPost]
        public ActionResult SaveAndNew(ClientEntityModel cliententitymodel, HttpPostedFileBase[] file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    cliententitymodel.ClientStatus = true;
                    cliententitymodel.ClientDelete = false;
                    _ClientServices.AddOrUpdateClient(cliententitymodel);
                    return RedirectToAction("List", "Client");
                }
                return View(cliententitymodel);

            }
            catch
            {
                return View();
            }
        }

          

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ClientEntityModel cliententitymodel)
        {
            try
            {
                cliententitymodel = new ClientEntityModel();
                _ClientServices.DeleteClient(id);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
              
            }
            catch
            {
                return View();
            }
        }

     

        public string UploadUserFile(HttpPostedFileBase file, ClientEntityModel clientEntityModel)
        {
            try
            {
                // supported extensions
                // you can add any of extension,if you want pdf file validation then add .pdf in 
                // variable supportedTypes.

                var supportedTypes = new[] { "jpg", "jpeg", "png" };

                // following will fetch the extension of posted file.

                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                // Image datatype is included in System.Drawing librery.will get the image properties 
                //  like height, width.

                Image fp = System.Drawing.Image.FromStream(file.InputStream);

                //variable will get the ratio of image 
                // (600 x 400),ratio will be 1.5 

                //decimal fu = ((decimal)fp.Width / fp.Height);

                if (file.ContentLength > (clientEntityModel.filesize * 1024))
                {
                    clientEntityModel.getseterror = "filesize will be upto " + clientEntityModel.filesize + "KB";
                    return clientEntityModel.getseterror;
                }
                else if (!supportedTypes.Contains(fileExt))
                {
                    clientEntityModel.getseterror = "file extension is not valid";
                    return clientEntityModel.getseterror;
                }
                //else if (fu != clientEntityModel.ar)
                //{
                //    clientEntityModel.getseterror = "file should be in mentioned aspect ratio";
                //    return clientEntityModel.getseterror;
                //}
                else
                {
                    clientEntityModel.getseterror = null;
                    return clientEntityModel.getseterror;
                }
            }
            catch (Exception ex)
            {
                clientEntityModel.getseterror = ex.Message;
                return clientEntityModel.getseterror;
            }
        }
    }
}
