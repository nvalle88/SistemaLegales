using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Extensores;
using SistemasLegales.Models.Utiles;
using SistemasLegales.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Controllers
{
    [Authorize]
    public class RequisitoController : Controller
    {
        private readonly SistemasLegalesContext db;
        public IConfigurationRoot Configuration { get; }
        private readonly IEmailSender emailSender;
        private readonly IUploadFileService uploadFileService;
        private readonly UserManager<ApplicationUser> userManager;
        public RequisitoController(UserManager<ApplicationUser> userManager, SistemasLegalesContext context, IEmailSender emailSender, IUploadFileService uploadFileService)
        {
            this.userManager = userManager;
            db = context;
            this.emailSender = emailSender;
            this.uploadFileService = uploadFileService;
        }

        private async Task<List<Requisito>> ListarRequisitos()
        {
            return await db.Requisito
                .Where(x=> x.Finalizado==false)
                    .Include(c => c.Documento)
                    .ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                    .Include(c => c.Documento)
                    .Include(c => c.Ciudad)
                    .Include(c => c.Proceso)
                    .Include(c=> c.Proyecto)
                    .OrderBy(c => c.IdDocumento)
                    .ThenBy(c=> c.Documento.IdRequisitoLegal)
                    .ThenBy(c=> c.Documento.RequisitoLegal.IdOrganismoControl)
                    .ThenBy(c => c.IdCiudad)
                    .ThenBy(c => c.IdProceso)
                    .ThenBy(c=>c.Status)
                    .ThenBy(c=> c.IdProyecto).OrderBy(x=>x.FechaCaducidad).ToListAsync();
        }

        private async Task<List<Requisito>> ListarRequisitosFinalizado()
        {
            return await db.Requisito
                .Where(x => x.Finalizado == true)
                    .Include(c => c.Documento)
                    .ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                    .Include(c => c.Documento)
                    .Include(c => c.Ciudad)
                    .Include(c => c.Proceso)
                    .Include(c => c.Proyecto)
                    .OrderBy(c => c.IdDocumento)
                    .ThenBy(c => c.Documento.IdRequisitoLegal)
                    .ThenBy(c => c.Documento.RequisitoLegal.IdOrganismoControl)
                    .ThenBy(c => c.IdCiudad)
                    .ThenBy(c => c.IdProceso)
                    .ThenBy(c => c.IdProyecto).OrderBy(x => x.FechaCaducidad).ToListAsync();
        }

        private async Task<List<Accion>> ListarAcciones(int IdRequisito)
        {
            return await db.Accion.Where(c => c.IdRequisito == IdRequisito).ToListAsync();                                     
        }

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> ListadoResultFinalizado(Requisito requisito)
        {
            var listaRequisitos = new List<Requisito>();
            try
            {
                var lista = await ListarRequisitosFinalizado();

                if (requisito?.Documento?.RequisitoLegal?.IdOrganismoControl != -1)
                    lista = lista.Where(c => c.Documento.RequisitoLegal.IdOrganismoControl == requisito.Documento.RequisitoLegal.IdOrganismoControl).ToList();

                if (requisito.IdActorResponsableGestSeg != -1)
                    lista = lista.Where(c => c.IdActorResponsableGestSeg == requisito.IdActorResponsableGestSeg).ToList();

                if (requisito.IdProyecto != -1)
                    lista = lista.Where(c => c.IdProyecto == requisito.IdProyecto).ToList();

                if (requisito.Anno != null)
                    lista = lista.Where(c => c.FechaCumplimiento?.Year == requisito.Anno).ToList();

                return PartialView("_ListadoFinalizado", lista);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> GestionarFinalizado(int? id)
        {
            try
            {
                ViewBag.accion ="Editar";
                if (id != null)
                {
                    var requisito = await db.Requisito.Include(c => c.DocumentoRequisito).Include(x => x.Accion).Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl).FirstOrDefaultAsync(c => c.IdRequisito == id);
                    if (requisito == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
                    ViewData["Ciudad"] = new SelectList(await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync(), "IdCiudad", "Nombre");
                    ViewData["Proceso"] = new SelectList(await db.Proceso.OrderBy(c => c.Nombre).ToListAsync(), "IdProceso", "Nombre");
                    ViewData["Proyecto"] = new SelectList(await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync(), "IdProyecto", "Nombre");
                    ViewData["Actor"] = new SelectList(await db.Actor.OrderBy(c => c.Nombres).ToListAsync(), "IdActor", "Nombres");
                    ViewData["Status"] = new SelectList(await db.Status.ToListAsync(), "IdStatus", "Nombre");
                    ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre", requisito.Documento.RequisitoLegal.IdOrganismoControl);
                    ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal(requisito?.Documento?.RequisitoLegal?.IdOrganismoControl ?? -1);
                    ViewData["Documento"] = await ObtenerSelectListDocumento(requisito?.Documento?.IdRequisitoLegal ?? -1);
                    requisito.IdStatusAnterior = requisito.IdStatus;
                    return View(requisito);
                }
                return this.Redireccionar("Requisito","IndexFinalizado",$"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> GestionarFinalizado(Requisito requisito, IFormFile file)
        {
            try
            {
                Requisito miRequisito = new Requisito();
                ViewBag.accion = requisito.IdRequisito == 0 ? "Crear" : "Editar"; var tt = Request.Form;
                ModelState.Remove("Documento.Nombre");
                ModelState.Remove("Documento.Tipo");
                ModelState.Remove("Documento.Cantidad");
                ModelState.Remove("Documento.RequisitoLegal.Nombre");
                if (ModelState.IsValid)
                {
                        miRequisito = await db.Requisito.FirstOrDefaultAsync(c => c.IdRequisito == requisito.IdRequisito);
                        miRequisito.IdDocumento = requisito.IdDocumento;
                        miRequisito.IdCiudad = requisito.IdCiudad;
                        miRequisito.Criticidad = requisito.Criticidad;
                        miRequisito.IdProceso = requisito.IdProceso;
                        miRequisito.IdProyecto = requisito.IdProyecto;
                        miRequisito.IdActorDuennoProceso = requisito.IdActorDuennoProceso;
                        miRequisito.IdActorResponsableGestSeg = requisito.IdActorResponsableGestSeg;
                        miRequisito.IdActorCustodioDocumento = requisito.IdActorCustodioDocumento;
                        miRequisito.FechaCumplimiento = requisito.FechaCumplimiento;
                        miRequisito.FechaCaducidad = requisito.FechaCaducidad;
                        miRequisito.IdStatus = requisito.IdStatus;
                        miRequisito.DuracionTramite = requisito.DuracionTramite;
                        miRequisito.DiasNotificacion = requisito.DiasNotificacion;
                        miRequisito.EmailNotificacion1 = requisito.EmailNotificacion1;
                        miRequisito.EmailNotificacion2 = requisito.EmailNotificacion2;
                        miRequisito.Observaciones = requisito.Observaciones;
                    await db.SaveChangesAsync();

                    var responseFile = true;
                    if (file != null)
                    {
                        byte[] data;
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);

                        if (data.Length > 0)
                        {
                            var activoFijoDocumentoTransfer = new DocumentoRequisitoTransfer { Nombre = file.FileName, Fichero = data, IdRequisito = miRequisito.IdRequisito };
                            responseFile = await uploadFileService.UploadFiles(activoFijoDocumentoTransfer);
                        }
                    }
                    await miRequisito.EnviarEmailNotificaionFinalizadoModificado(User.Identity.Name,userManager, emailSender, db);
                    if (responseFile)
                    {
                       
                        return this.Redireccionar(  $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}", "IndexFinalizado");
                    }
                    return this.Redireccionar( $"{Mensaje.Aviso}|{Mensaje.ErrorUploadFiles}", "IndexFinalizado");
                }
                return this.VistaError(requisito, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.Excepcion}");
            }
        }

        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> IndexFinalizado()
        {
            var lista = new List<Requisito>();
            try
            {
                lista =await ListarRequisitosFinalizado();
                var listadoOrganismoControl = await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync();
                listadoOrganismoControl.Insert(0, new OrganismoControl { IdOrganismoControl = -1, Nombre = "Todos" });
                ViewData["OrganismoControl"] = new SelectList(listadoOrganismoControl, "IdOrganismoControl", "Nombre");

                var listadoActores = await db.Actor.OrderBy(c => c.Nombres).ToListAsync();
                listadoActores.Insert(0, new Actor { IdActor = -1, Nombres = "Todos" });
                ViewData["Actor"] = new SelectList(listadoActores, "IdActor", "Nombres");

                var listadoProyectos = await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync();
                listadoProyectos.Insert(0, new Proyecto { IdProyecto = -1, Nombre = "Todos" });
                ViewData["Proyecto"] = new SelectList(listadoProyectos, "IdProyecto", "Nombre");


            }
            catch (Exception)
            {
                TempData["Mensaje"] = $"{Mensaje.Error}|{Mensaje.ErrorListado}";
            }
            return View(lista);
        }




        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> Index()
        {
            var lista = new List<Requisito>();
            try
            {
                var listadoOrganismoControl = await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync();
                listadoOrganismoControl.Insert(0, new OrganismoControl { IdOrganismoControl = -1, Nombre = "Todos" });
                ViewData["OrganismoControl"] = new SelectList(listadoOrganismoControl, "IdOrganismoControl", "Nombre");

                var listadoActores = await db.Actor.OrderBy(c => c.Nombres).ToListAsync();
                listadoActores.Insert(0, new Actor { IdActor = -1, Nombres = "Todos" });
                ViewData["Actor"] = new SelectList(listadoActores, "IdActor", "Nombres");

                var ListaStatus = await db.Status.ToListAsync();
                ListaStatus.Insert(0, new Status { IdStatus = -1, Nombre = "Todos" });
                ViewData["Status"] = new SelectList(ListaStatus, "IdStatus", "Nombre");

                var listadoProyectos = await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync();
                listadoProyectos.Insert(0, new Proyecto { IdProyecto = -1, Nombre = "Todos" });
                ViewData["Proyecto"] = new SelectList(listadoProyectos, "IdProyecto", "Nombre");


            }
            catch (Exception)
            {
                TempData["Mensaje"] = $"{Mensaje.Error}|{Mensaje.ErrorListado}";
            }
            return View(lista);
        }


        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> EditarFinalizado(int? id)
        {
            try
            {
                var accion = await db.Accion.Where(x => x.IdAccion == id).Select(x => new Accion { Detalle = x.Detalle, Fecha = x.Fecha, IdRequisito = x.IdRequisito, IdAccion = x.IdAccion }).FirstOrDefaultAsync();
                if (accion != null)
                {
                    return View(accion);
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> EditarFinalizado(Accion accion)
        {
            try
            {
                var accionActualizar = await db.Accion.Where(x => x.IdAccion == accion.IdAccion).FirstOrDefaultAsync();
                if (accionActualizar != null)
                {
                    accionActualizar.Fecha = accion.Fecha;
                    accionActualizar.Detalle = accion.Detalle;
                    await db.SaveChangesAsync();

                    TempData["Mensaje"] = $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}";
                    return RedirectToAction("DetallesFinalizado", new { id = accionActualizar.IdRequisito });
                }
                return this.Redireccionar("Requisito","IndexFinalizado",$"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar("Requisito","IndexFinalizado",$"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> Editar(int? id)
        {
            try
            {
                var accion =await db.Accion.Where(x => x.IdAccion == id).Select(x => new Accion { Detalle = x.Detalle, Fecha = x.Fecha, IdRequisito = x.IdRequisito, IdAccion = x.IdAccion }).FirstOrDefaultAsync();
                if (accion!=null)
                {
                    return View(accion);
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Editar(Accion accion)
        {
            try
            {
                var accionActualizar = await db.Accion.Where(x => x.IdAccion == accion.IdAccion).FirstOrDefaultAsync();
                if (accionActualizar!=null)
                {
                    accionActualizar.Fecha = accion.Fecha;
                    accionActualizar.Detalle = accion.Detalle;
                    await db.SaveChangesAsync();

                    TempData["Mensaje"] = $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}";
                    return RedirectToAction("Detalles", new { id = accionActualizar.IdRequisito });
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> Finalizar(Requisito requisito, IFormFile file)
        {
            var requisitoFinalizar = await db.Requisito.Where(x => x.IdRequisito == requisito.IdRequisito).FirstOrDefaultAsync();
            if (requisitoFinalizar != null)
            {
                requisitoFinalizar.Finalizado = true;
                await db.SaveChangesAsync();

                 var FechaCumplimiento = new DateTime?();
                var FechaCaducidad = new DateTime?();
                
                var documento = await db.Documento.Where(x => x.IdDocumento == requisitoFinalizar.IdDocumento).FirstOrDefaultAsync();

                switch (documento.Tipo)
                {
                    case 1:
                        {
                           FechaCumplimiento= requisitoFinalizar.FechaCumplimiento?.AddDays((int)documento.Cantidad);
                            FechaCaducidad= requisitoFinalizar.FechaCaducidad?.AddDays((int)documento.Cantidad);
                            break;
                        }
                    case 2:
                        {
                            FechaCumplimiento = requisitoFinalizar.FechaCumplimiento?.AddMonths((int)documento.Cantidad);
                            FechaCaducidad = requisitoFinalizar.FechaCaducidad?.AddMonths(Convert.ToInt32(documento.Cantidad));
                            break;
                        }
                    case 3:
                        {
                            FechaCumplimiento = requisitoFinalizar.FechaCumplimiento?.AddYears((int)documento.Cantidad);
                            FechaCaducidad = requisitoFinalizar.FechaCaducidad?.AddYears((int)documento.Cantidad);
                            break;
                        }
                }

                Requisito miRequisito = new Requisito();
                miRequisito = new Requisito
                {
                    IdDocumento = requisitoFinalizar.IdDocumento,
                    IdCiudad = requisitoFinalizar.IdCiudad,
                    IdProceso = requisitoFinalizar.IdProceso,
                    IdProyecto = requisitoFinalizar.IdProyecto,
                    IdActorDuennoProceso = requisitoFinalizar.IdActorDuennoProceso,
                    IdActorResponsableGestSeg = requisitoFinalizar.IdActorResponsableGestSeg,
                    IdActorCustodioDocumento = requisitoFinalizar.IdActorCustodioDocumento,
                    FechaCumplimiento = FechaCumplimiento,
                    FechaCaducidad = FechaCaducidad,
                    IdStatus = EstadoRequisito.Iniciado,
                    DuracionTramite = requisitoFinalizar.DuracionTramite,
                    DiasNotificacion = requisitoFinalizar.DiasNotificacion,
                    EmailNotificacion1 = requisitoFinalizar.EmailNotificacion1,
                    EmailNotificacion2 = requisitoFinalizar.EmailNotificacion2,
                    Observaciones = requisitoFinalizar.Observaciones,
                    NotificacionEnviada = false,
                    Finalizado = false,
                };

                await db.AddAsync(miRequisito);
                await db.SaveChangesAsync();

                await requisitoFinalizar.EnviarEmailNotificaionRequisitoFinalizado(emailSender, db);
                await miRequisito.EnviarEmailNotificaionRequisitoCreacionAutomatica(userManager,emailSender, db);

                return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
            }
            return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
        }

        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Gestionar(int? id)
        {
            try
            {
                ViewBag.accion = id == null ? "Crear" : "Editar";
                ViewData["Ciudad"] = new SelectList(await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync(), "IdCiudad", "Nombre");
                ViewData["Proceso"] = new SelectList(await db.Proceso.OrderBy(c => c.Nombre).ToListAsync(), "IdProceso", "Nombre");
                ViewData["Proyecto"] = new SelectList(await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync(), "IdProyecto", "Nombre");
                ViewData["Actor"] = new SelectList(await db.Actor.OrderBy(c => c.Nombres).ToListAsync(), "IdActor", "Nombres");
                ViewData["Status"] = new SelectList(await db.Status.ToListAsync(), "IdStatus", "Nombre");


                if (id != null)
                {
                    var requisito = await db.Requisito.Include(c=> c.DocumentoRequisito).Include(x=>x.Accion).Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl).FirstOrDefaultAsync(c => c.IdRequisito == id);
                    if (requisito == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    if (requisito.Finalizado==true)
                    {
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.RequisitoFinalizado}");
                    }


                    ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre", requisito.Documento.RequisitoLegal.IdOrganismoControl);
                    ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal(requisito?.Documento?.RequisitoLegal?.IdOrganismoControl ?? -1);
                    ViewData["Documento"] = await ObtenerSelectListDocumento(requisito?.Documento?.IdRequisitoLegal ?? -1);
                    requisito.IdStatusAnterior = requisito.IdStatus;
                    return View(requisito);
                }
                ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal((ViewData["OrganismoControl"] as SelectList).FirstOrDefault() != null ? int.Parse((ViewData["OrganismoControl"] as SelectList).FirstOrDefault().Value) : -1);
                ViewData["Documento"] = await ObtenerSelectListDocumento((ViewData["RequisitoLegal"] as SelectList).FirstOrDefault() != null ? int.Parse((ViewData["RequisitoLegal"] as SelectList).FirstOrDefault().Value) : -1);
                var requisitoNew = new Requisito { IdRequisito=0,IdStatusAnterior=-1,Accion=new List<Accion>()};
                return View(requisitoNew);
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Gestionar(Requisito requisito, IFormFile file)
        {
            try
            {

                if (requisito.IdStatus==EstadoRequisito.Terminado && file==null)
                {
                    ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                    ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal(requisito?.Documento?.RequisitoLegal?.IdOrganismoControl ?? -1);
                    ViewData["Documento"] = await ObtenerSelectListDocumento(requisito?.Documento?.IdRequisitoLegal ?? -1);
                    ViewData["Ciudad"] = new SelectList(await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync(), "IdCiudad", "Nombre");
                    ViewData["Proceso"] = new SelectList(await db.Proceso.OrderBy(c => c.Nombre).ToListAsync(), "IdProceso", "Nombre");
                    ViewData["Proyecto"] = new SelectList(await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync(), "IdProyecto", "Nombre");
                    ViewData["Actor"] = new SelectList(await db.Actor.OrderBy(c => c.Nombres).ToListAsync(), "IdActor", "Nombres");
                    ViewData["Status"] = new SelectList(await db.Status.ToListAsync(), "IdStatus", "Nombre");
                    var requisitoSalida = await db.Requisito.Include(c => c.DocumentoRequisito).Include(x => x.Accion).Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl).FirstOrDefaultAsync(c => c.IdRequisito == requisito.IdRequisito);
                    var acciones =await db.Accion.Where(x => x.IdRequisito == requisito.IdRequisito).ToListAsync();
                    requisitoSalida.Accion = acciones;
                    return this.VistaError(requisitoSalida, $"{Mensaje.Error}|{Mensaje.CargarArchivoEstadoTerminado}");
                }

                

                Requisito miRequisito = new Requisito();
                ViewBag.accion = requisito.IdRequisito == 0 ? "Crear" : "Editar";var tt = Request.Form;
                ModelState.Remove("Documento.Nombre");
                ModelState.Remove("Documento.Tipo");
                ModelState.Remove("Documento.Cantidad");
                ModelState.Remove("Documento.RequisitoLegal.Nombre");
                if (ModelState.IsValid)
                {
                    var nuevoRegistro = false;

                    if (requisito.IdRequisito == 0)
                    {

                         miRequisito = new Requisito
                        {
                            IdDocumento = requisito.IdDocumento,
                            IdCiudad = requisito.IdCiudad,
                            Criticidad= requisito.Criticidad,
                            IdProceso = requisito.IdProceso,
                            IdProyecto = requisito.IdProyecto,
                            IdActorDuennoProceso = requisito.IdActorDuennoProceso,
                            IdActorResponsableGestSeg = requisito.IdActorResponsableGestSeg,
                            IdActorCustodioDocumento = requisito.IdActorCustodioDocumento,
                            FechaCumplimiento = requisito.FechaCumplimiento,
                            FechaCaducidad = requisito.FechaCaducidad,
                            IdStatus = requisito.IdStatus,
                            DuracionTramite = requisito.DuracionTramite,
                            DiasNotificacion = requisito.DiasNotificacion,
                            EmailNotificacion1 = requisito.EmailNotificacion1,
                            EmailNotificacion2 = requisito.EmailNotificacion2,
                            Observaciones = requisito.Observaciones,
                            NotificacionEnviada = false
                        };

                        db.Add(miRequisito);
                        nuevoRegistro = true;
                        
                    }
                    else
                    {
                        miRequisito = await db.Requisito.FirstOrDefaultAsync(c => c.IdRequisito == requisito.IdRequisito);
                        miRequisito.IdDocumento = requisito.IdDocumento;
                        miRequisito.IdCiudad = requisito.IdCiudad;
                        miRequisito.Criticidad = requisito.Criticidad;
                        miRequisito.IdProceso = requisito.IdProceso;
                        miRequisito.IdProyecto = requisito.IdProyecto;
                        miRequisito.IdActorDuennoProceso = requisito.IdActorDuennoProceso;
                        miRequisito.IdActorResponsableGestSeg = requisito.IdActorResponsableGestSeg;
                        miRequisito.IdActorCustodioDocumento = requisito.IdActorCustodioDocumento;
                        miRequisito.FechaCumplimiento = requisito.FechaCumplimiento;
                        miRequisito.FechaCaducidad = requisito.FechaCaducidad;
                        miRequisito.IdStatus = requisito.IdStatus;
                        miRequisito.DuracionTramite = requisito.DuracionTramite;
                        miRequisito.DiasNotificacion = requisito.DiasNotificacion;
                        miRequisito.EmailNotificacion1 = requisito.EmailNotificacion1;
                        miRequisito.EmailNotificacion2 = requisito.EmailNotificacion2;
                        miRequisito.Observaciones = requisito.Observaciones;
                    }
                    await db.SaveChangesAsync();

                    var responseFile = true;
                    if (file != null)
                    {
                        byte[] data;
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);

                        if (data.Length > 0)
                        {
                            var activoFijoDocumentoTransfer = new DocumentoRequisitoTransfer { Nombre = file.FileName, Fichero = data, IdRequisito = miRequisito.IdRequisito };
                            responseFile = await uploadFileService.UploadFiles(activoFijoDocumentoTransfer);
                        }
                    }


                    if (requisito.IdStatus != requisito.IdStatusAnterior && requisito.IdStatusAnterior==EstadoRequisito.Terminado)
                    {
                        var url = "";
                        if (requisito.IdRequisito == 0)
                        {
                            url = $"{this.Request.Scheme}://{this.Request.Host}/{Mensaje.CarpertaHost}{this.Request.Path}/{miRequisito.IdRequisito}";
                        }
                        else
                        {
                            url = $"{this.Request.Scheme}://{this.Request.Host}/{Mensaje.CarpertaHost}{this.Request.Path}";
                        }
                        await miRequisito.EnviarEmailNotificaionNoFinalizado( url, emailSender, db);
                    }

                    //if (requisito.IdStatus == EstadoRequisito.Terminado)
                    //{
                    //    var url = "";
                    //    if (requisito.IdRequisito == 0)
                    //    {
                    //         url = $"{this.Request.Scheme}://{this.Request.Host}/{Mensaje.CarpertaHost}{this.Request.Path}/{miRequisito.IdRequisito}";
                    //    }
                    //    else
                    //    {
                    //        url = $"{this.Request.Scheme}://{this.Request.Host}/{Mensaje.CarpertaHost}{this.Request.Path}";
                    //    }
                    //    await miRequisito.EnviarEmailNotificaionRequisitoTerminado( userManager, url,emailSender, db);
                    //}

                    if (nuevoRegistro)
                    {
                        var url = "";
                        if (requisito.IdRequisito == 0)
                        {
                            url = $"{this.Request.Scheme}://{this.Request.Host}/{Mensaje.CarpertaHost}{this.Request.Path}/{miRequisito.IdRequisito}";
                        }
                        else
                        {
                            url = $"{this.Request.Scheme}://{this.Request.Host}/{Mensaje.CarpertaHost}{this.Request.Path}";
                        }
                        await miRequisito.EnviarEmailNotificaionRequisitoCreacion(url, emailSender, db);
                    }

                    await miRequisito.EnviarEmailNotificaion(userManager,emailSender, db);
                    return this.Redireccionar(responseFile ? $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}" : $"{Mensaje.Aviso}|{Mensaje.ErrorUploadFiles}", "Gestionar",new { id= miRequisito.IdRequisito } );

                   // return this.Redireccionar(,responseFile ? $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}" : $"{Mensaje.Aviso}|{Mensaje.ErrorUploadFiles}");
                }
                ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal(requisito?.Documento?.RequisitoLegal?.IdOrganismoControl ?? -1);
                ViewData["Documento"] = await ObtenerSelectListDocumento(requisito?.Documento?.IdRequisitoLegal ?? -1);
                ViewData["Ciudad"] = new SelectList(await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync(), "IdCiudad", "Nombre");
                ViewData["Proceso"] = new SelectList(await db.Proceso.OrderBy(c => c.Nombre).ToListAsync(), "IdProceso", "Nombre");
                ViewData["Proyecto"] = new SelectList(await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync(), "IdProyecto", "Nombre");
                ViewData["Actor"] = new SelectList(await db.Actor.OrderBy(c => c.Nombres).ToListAsync(), "IdActor", "Nombres");
                ViewData["Status"] = new SelectList(await db.Status.ToListAsync(), "IdStatus", "Nombre");
                return this.VistaError(requisito, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.Excepcion}");
            }
        }

        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> Detalles(int? id)
        {
            try
            {
                if (id != null)
                {
                    var requisito = await db.Requisito
                        .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                            .Include(c => c.Documento)
                            .Include(c => c.Ciudad)
                            .Include(c => c.Proceso)
                            .Include(c=> c.Proyecto)
                            .Include(c => c.ActorDuennoProceso)
                            .Include(c => c.ActorResponsableGestSeg)
                            .Include(c => c.ActorCustodioDocumento)
                            .Include(c => c.Status)
                            .Include(c=> c.DocumentoRequisito)
                            .Include(c=> c.Accion)
                        .FirstOrDefaultAsync(c => c.IdRequisito == id);
                    if (requisito == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(requisito);
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> DetallesFinalizado(int? id)
        {
            try
            {
                if (id != null)
                {
                    var requisito = await db.Requisito
                        .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                            .Include(c => c.Documento)
                            .Include(c => c.Ciudad)
                            .Include(c => c.Proceso)
                            .Include(c => c.Proyecto)
                            .Include(c => c.ActorDuennoProceso)
                            .Include(c => c.ActorResponsableGestSeg)
                            .Include(c => c.ActorCustodioDocumento)
                            .Include(c => c.Status)
                            .Include(c => c.DocumentoRequisito)
                            .Include(c => c.Accion)
                        .FirstOrDefaultAsync(c => c.IdRequisito == id);
                    if (requisito == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(requisito);
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var requisito = await db.Requisito.FirstOrDefaultAsync(m => m.IdRequisito == id);
                if (requisito != null)
                {
                    db.Requisito.Remove(requisito);
                    await db.SaveChangesAsync();
                    return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.BorradoNoSatisfactorio}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> CambiarHabilitar(int idRequisito)
        {
            try
            {
                var requisito = await db.Requisito.FirstOrDefaultAsync(m => m.IdRequisito == idRequisito);
                if (requisito != null)
                {
                    switch (requisito.Habilitado)
                    {
                        case true : requisito.Habilitado = false;
                                    break;
                         case false: requisito.Habilitado = true;
                            break;
                    }
                    
                    await db.SaveChangesAsync();
                    return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.BorradoNoSatisfactorio}");
            }
        }

       

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> ListadoResult(Requisito requisito)
        {
            var listaRequisitos = new List<Requisito>();
            try
            {
                var lista = await ListarRequisitos();

                if (requisito?.Documento?.RequisitoLegal?.IdOrganismoControl != -1)
                    lista = lista.Where(c => c.Documento.RequisitoLegal.IdOrganismoControl == requisito.Documento.RequisitoLegal.IdOrganismoControl).ToList();

                if (requisito.IdActorResponsableGestSeg != -1)
                    lista = lista.Where(c => c.IdActorResponsableGestSeg == requisito.IdActorResponsableGestSeg).ToList();

                if (requisito.IdProyecto != -1)
                    lista = lista.Where(c => c.IdProyecto == requisito.IdProyecto).ToList();


                if (requisito.IdStatus != -1)
                    lista = lista.Where(c => c.IdStatus == requisito.IdStatus).ToList();

                if (requisito.Anno != null)
                    lista = lista.Where(c => c.FechaCumplimiento?.Year == requisito.Anno).ToList();

                foreach (var item in lista)
                {
                    int semaforo = item.ObtenerSemaforo();
                    var validarSemaforo = false;

                    if (requisito.SemaforoVerde)
                    {
                        if (semaforo == 1)
                            validarSemaforo = true;
                    }
                    if (requisito.SemaforoAmarillo)
                    {
                        if (semaforo == 2)
                            validarSemaforo = true;
                    }
                    if (requisito.SemaforoRojo)
                    {
                        if (semaforo == 3)
                            validarSemaforo = true;
                    }

                    if (validarSemaforo)
                        listaRequisitos.Add(item);
                }
                return PartialView("_Listado", listaRequisitos);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        [Authorize(Policy = "Gestion")]
        public async Task<JsonResult> InsertarAcciones(string observaciones, int idRequisito, DateTime fechaAccion)
        {
            try
            {
                var accion = new Accion {Detalle = observaciones, IdRequisito = idRequisito, Fecha = fechaAccion };
                await db.Accion.AddAsync(accion);
                await db.SaveChangesAsync();

                var listaAcciones = await db.Accion.Where(x => x.IdRequisito == idRequisito).Select(x => new Accion { IdAccion = x.IdAccion, Detalle = x.Detalle, Fecha = x.Fecha }).ToListAsync();

                return Json(listaAcciones);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Authorize(Policy = "Administracion")]
        public async Task<JsonResult> EliminarAccion(int idAccion,int idRequisito)
        {

            var listaAcciones = new List<Accion>();
            try
            {
                var accion = await db.Accion.Where(x => x.IdAccion == idAccion).FirstOrDefaultAsync();
                db.Remove(accion);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                listaAcciones = await db.Accion.Where(x => x.IdRequisito == idRequisito).Select(x => new Accion { IdAccion = x.IdAccion, Detalle = x.Detalle, Fecha = x.Fecha }).ToListAsync();
                return Json(listaAcciones);
            }
            listaAcciones = await db.Accion.Where(x => x.IdRequisito == idRequisito).Select(x => new Accion { IdAccion = x.IdAccion, Detalle = x.Detalle, Fecha = x.Fecha }).ToListAsync();

            return Json(listaAcciones);
        }



        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> DescargarArchivo(int id)
        {
            try
            {
                var documentoRequisitoTransfer = await uploadFileService.GetFileDocumentoRequisito(id);
                return File(documentoRequisitoTransfer.Fichero, MimeTypes.GetMimeType(documentoRequisitoTransfer.Nombre), documentoRequisitoTransfer.Nombre);
            }
            catch (Exception)
            {
                return StatusCode(400, "El archivo solicitado no est� disponible, por favor comun�quese con el administrador para obtener  m�s informaci�n.");
               
            }
        }

        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> EliminarArchivo(int id)
        {
            try
            {
                var requisitoDocumeto =await db.DocumentoRequisito.Where(x => x.IdDocumentoRequisito == id).FirstOrDefaultAsync();
                var URL = requisitoDocumeto.Url;
                var Eliminado= uploadFileService.DeleteFile(URL);
                var idRequisitoActual = requisitoDocumeto.IdRequisito;
                db.DocumentoRequisito.Remove(requisitoDocumeto);
                await db.SaveChangesAsync();
                return this.Redireccionar( $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}", "Gestionar", new { id = idRequisitoActual });
            }
            catch (Exception)
            {
                return StatusCode(400, "El archivo solicitado no est� disponible, por favor comun�quese con el administrador para obtener  m�s informaci�n.");

            }
        }

        #region AJAX_RequisitoLegal
        public async Task<SelectList> ObtenerSelectListRequisitoLegal(int idOrganismoControl)
        {
            try
            {
                var listaRequisitoLegal = idOrganismoControl != -1 ? await db.RequisitoLegal.Where(c => c.IdOrganismoControl == idOrganismoControl).Select(y=>new RequisitoLegal
                { IdRequisitoLegal=y.IdRequisitoLegal,Nombre=y.Nombre.Length>100? y.Nombre.Substring(0,100).ToString()+" ...":y.Nombre}).ToListAsync() : new List<RequisitoLegal>();
                return new SelectList(listaRequisitoLegal, "IdRequisitoLegal", "Nombre");
            }
            catch (Exception)
            {
                return new SelectList(new List<RequisitoLegal>());
            }
        }

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> RequisitoLegal_SelectResult(int idOrganismoControl)
        {
            ViewBag.RequisitoLegal = await ObtenerSelectListRequisitoLegal(idOrganismoControl);
            return PartialView("_RequisitoLegalSelect", new Requisito());
        }

        [HttpPost]
        [Authorize(Policy = "Administracion")]
        public async Task<IActionResult> RequisitoLegal_SelectResult_Finalizar(int idOrganismoControl)
        {
            ViewBag.RequisitoLegal = await ObtenerSelectListRequisitoLegal(idOrganismoControl);
            return PartialView("_RequisitoLegalSelect", new Requisito());
        }
        #endregion

        #region AJAX_Documento
        public async Task<SelectList> ObtenerSelectListDocumento(int idRequisitoLegal)
        {
            try
            {
                var listaDocumento = idRequisitoLegal != -1 ? await db.Documento.Where(c=> c.IdRequisitoLegal == idRequisitoLegal)
                                                                      .Select(y=> new Documento {IdDocumento=y.IdDocumento,Nombre=y.Nombre.Length>100 ? y.Nombre.Substring(0,100).ToString()+" ...":y.Nombre })  
                                                                      .ToListAsync() : new List<Documento>();
                return new SelectList(listaDocumento, "IdDocumento", "Nombre");
            }
            catch (Exception)
            {
                return new SelectList(new List<Documento>());
            }
        }

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> Documento_SelectResult(int idRequisitoLegal)
        {
            ViewBag.Documento = await ObtenerSelectListDocumento(idRequisitoLegal);
            return PartialView("_DocumentoSelect", new Requisito());
        }
        #endregion
    }
}