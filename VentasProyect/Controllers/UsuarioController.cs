using System.Web.Mvc;
using VentasProyect.Models.usuario;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            Session["SessionStatus"] = true;

            var usuarios = _usuarioRepository.GetUsuarios();
            return View(usuarios);
        }

        public ActionResult Create()
        {
            Session["SessionStatus"] = true;
            return View();
        }

        [HttpPost]
        public ActionResult Create(usuario usuario)
        {


            if (ModelState.IsValid)
            {


                // Lógica para guardar el nuevo usuario en la base de datos
                _usuarioRepository.CreateUsuario(usuario);

                // Redirecciona al usuario a alguna página de confirmación o a la lista de usuarios
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Edit(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

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

        public ActionResult Details(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

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