using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Helpers;
using Shooping.Helpers;
using Proyecto_Final.Models;
using System;
using Vereyon.Web;
using static Shooping.Helpers.ModalHelper;

namespace Proyecto_Final.Controllers
{
    public class ClientController : Controller
    {
        private readonly DataContext _context;
        private readonly IClientHelper _clientHelper;
        private readonly IFlashMessage _flashMessage;

        public ClientController(DataContext context, IClientHelper clientHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _clientHelper = clientHelper;
            _flashMessage = flashMessage;
        }
        //Listar Clientes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<Client> clientModel = await _clientHelper.GetClientsAsync();
            return View(clientModel);
        }
        //Crear Cliente
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientModel)
        {
            //Validar el metodo
            if (ModelState.IsValid)
            {
                try
                {
                    Client newClient = await _clientHelper.AddClientAsync(clientModel);


                    //Guardar la información del cliente en la base de datos
                    _context.Add(newClient);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info("Registro creado.");

                    //Una vez guardada la información se redirige a la lista de servicios
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un cliente con el mismo nombre");
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
            return View(clientModel);
        }

        [Authorize(Roles = "Admin")]
        //Editar Cliente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clientes.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info("Registro actualizado.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un cliente con el mismo nombre");
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
            return View(client);
        }

        //Detalles de Servicio
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        //Eliminar Servicio
        [Authorize(Roles = "Admin")]
        [NoDirectAccess]
        public async Task<IActionResult> Delete(int? id)
        {
            Client client = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                _context.Clientes.Remove(client);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Registro borrado.");
            }
            catch
            {
                _flashMessage.Danger("No se puede borrar el cliente porque tiene registros relacionados.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
