﻿using System.Web.Mvc;
using VentasProyect.Models.usuario;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public EncryptRepository _encryptRepository = new EncryptRepository();

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [FiltroSeguridadController]
        // GET: Usuario
        public ActionResult Index()
        {
            Session["SessionStatus"] = true;

            var usuarios = _usuarioRepository.GetUsuarios();
            return View(usuarios);
        }

        [FiltroSeguridadController]
        public ActionResult Create()
        {
            Session["SessionStatus"] = true;
            return View();
        }

        [FiltroSeguridadController]
        [HttpPost]
        public ActionResult Create(usuario usuario)
        {


            if (ModelState.IsValid)
            {
                //Encriptar contraseña
                //usuario.usu_contrasenia = _encryptRepository.EncryptPassword(usuario.usu_contrasenia);

                // Lógica para guardar el nuevo usuario en la base de datos
                _usuarioRepository.CreateUsuario(usuario);

                // Redirecciona al usuario a alguna página de confirmación o a la lista de usuarios
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [FiltroSeguridadController]
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        [FiltroSeguridadController]
        [HttpPost]
        public ActionResult Edit(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.UpdateUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [FiltroSeguridadController]
        public ActionResult Details(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        [FiltroSeguridadController]
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _usuarioRepository.DeleteUsuario(id);
            return RedirectToAction("Index");
        }
    }
}