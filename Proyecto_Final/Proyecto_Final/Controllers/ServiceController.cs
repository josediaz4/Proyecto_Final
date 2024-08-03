using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Helpers;
using Shooping.Helpers;
using Vereyon.Web;
using static Shooping.Helpers.ModalHelper;

namespace Proyecto_Final.Controllers
{
    
    public class ServiceController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IFlashMessage _flashMessage;

        public ServiceController(DataContext context, IBlobHelper blobHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _blobHelper = blobHelper;
            _flashMessage = flashMessage;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<Service> serviceModel = await GetServices();
            return View(serviceModel);
        }

        public async Task<List<Service>> GetServices()
        {
            return await _context.Servicios.ToListAsync();
        }

        [Authorize(Roles = "Admin")]
        ////Crear Servicio
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service model)
        {
            //Validar el metodo
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                try
                {
                    //Verificar si se ha agregado una imagen
                    if (model.ImageFile != null)
                    {
                        //Guardamos la imagen en el contendedor de Blob Storage
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "service");

                        model.ImageId = imageId;
                        model.State = true;
                    }
                    //Guardar la información del servicio en la base de datos
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info("Registro creado.");
                    //Una vez guardada la información se redirige a la lista de servicios
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe un servicio con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(model);
        }

        ////[Authorize(Roles = "Admin")]
        //////Eliminar Categoria
        ////public async Task<IActionResult> Delete(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var category = await _context.Servicios
        ////        .FirstOrDefaultAsync(m => m.Id == id);
        ////    if (category == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return View(category);
        ////}

        ////[Authorize(Roles = "Admin")]
        ////// POST: Countries/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> DeleteConfirmed(int id)
        ////{
        ////    var category = await _context.Servicios.FindAsync(id);
        ////    if (category != null)
        ////    {
        ////        _context.Servicios.Remove(category);
        ////    }

        ////    await _context.SaveChangesAsync();
        ////    return RedirectToAction(nameof(Index));
        ////}

        [Authorize(Roles = "Admin")]
        //Editar Servicio
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Servicios.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Guid imageId = service.ImageId;
                try
                {
                    //Verificar si se ha agregado una imagen
                    if (service.ImageFile != null)
                    {
                        //Guardamos la imagen en el contendedor de Blob Storage
                        imageId = await _blobHelper.UploadBlobAsync(service.ImageFile, "service");

                        service.ImageId = imageId;
                        service.State = true;
                    }
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    //_flashMessage.Info("Registro actualizado.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe un servicio con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(service);
        }

        //Detalles de Servicio
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Servicios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        //Eliminar Servicio
        [NoDirectAccess]
        public async Task<IActionResult> Delete(int? id)
        {
            Service service = await _context.Servicios.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                _context.Servicios.Remove(service);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Registro borrado.");
            }
            catch
            {
                _flashMessage.Danger("No se puede borrar la categoría porque tiene registros relacionados.");
            }

            return RedirectToAction(nameof(Index));
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Service());
            }
            else
            {
                Service service = await _context.Servicios.FindAsync(id);
                if (service == null)
                {
                    return NotFound();
                }

                return View(service);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Service model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                try
                {
                    if (id == 0) //Insert
                    {
                        //Verificar si se ha agregado una imagen
                        if (model.ImageFile != null)
                        {
                            //Guardamos la imagen en el contendedor de Blob Storage
                            imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "service");

                            model.ImageId = imageId;
                            model.State = true;
                        }
                        //Guardar la información del servicio en la base de datos
                        _context.Add(model);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Registro creado.");
                    }
                    else //Update
                    {
                        _context.Update(model);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Registro actualizado.");
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe un servicio con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                    return View(model);
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                    return View(model);
                }

                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", _context.Servicios.ToList()) });

            }

            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddOrEdit", model) });
        }

    }
}
