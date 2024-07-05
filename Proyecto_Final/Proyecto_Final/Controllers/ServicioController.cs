using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Helpers;

namespace Proyecto_Final.Controllers
{
    public class ServicioController : Controller
    {
        private readonly DataContext _context;
        private readonly IHelpersBlob _helpersBlob;

        public ServicioController(DataContext context, IHelpersBlob helpersBlob)
        {
            _context = context;
            _helpersBlob = helpersBlob;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> serviceModel = await GetServices();
            return View(serviceModel);
        }

        public async Task<List<Service>> GetServices()
        {
            return await _context.Servicios.ToListAsync();
        }

        //Crear Servicio
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
                try
                {
                    //Verificar si se ha agregado una imagen
                    if (model.ImageFile!=null)
                    {
                        //Guardamos la imagen en el contendedor de Blob Storage
                        await _helpersBlob.UploadBlobAsync(model.ImageFile, "servicios");
                        model.UrlImage = model.ImageFile.FileName;
                        model.Estado = true;
                    }
                    //Guardar la información del servicio en la base de datos
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    //Una vez guardada la información se redirige a la lista de servicios
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoria con el mismo nombre");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            //Recuperamos la información del servicio
            var service = _context.Servicios.FirstOrDefault(s => s.Id == id);

            //Verificamos si existe el servicio en la base de datos
            if (service is null)
            {
                return NotFound();
            }
            try
            {
                //Eliminamos la imagen de Azure Blob Storage
                _helpersBlob.DeleteBlob(service.UrlImage, "servicios");
                //Eliminar el servicio de la base de datos
                _context.Servicios.Remove(service);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }


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
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una servicio con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
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
    }
}
