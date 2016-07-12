namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity.Validation;
    using System.Web;
    using System.IO;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            PEDIDO = new HashSet<PEDIDO>();
        }

        [Key]
        public int IDUSUARIO { get; set; }

        [Required]
        public int IDTIPOUSUARIO { get; set; }

        [Required]
        [StringLength(30)]
        public string NOMBREUSU { get; set; }

        [Required]
        [StringLength(50)]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(20)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(20)]
        public string APELLIDOS { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(250)]
        public string FOTO { get; set; }

        [Required]
        [StringLength(1)]
        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO> PEDIDO { get; set; }

        public virtual TIPO_USUARIO TIPO_USUARIO { get; set; }

        public ResponseModel Acceder(string Email, string Password)
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_ventas())
                {
                    Password = HashHelper.MD5(Password);
                    var usuario = db.USUARIO.Where(x => x.EMAIL == Email)
                                            .Where(x => x.PASSWORD == Password)
                                            .SingleOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.IDUSUARIO.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Email o Password incorrecto...");
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return rm;
        }

        public List<USUARIO> Listar() //retornar es una colección
        {
            var usuarios = new List<USUARIO>();
            try
            {
                using (var db = new db_ventas())
                {
                    usuarios = db.USUARIO
                                .Include("TIPO_USUARIO")
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuarios;
        }

        //public USUARIO Obtener(string id) //retornar es un objeto
        //{
        //    var usuarios = new USUARIO();
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            usuarios = db.USUARIO.Include("PEDIDO")
        //                //.Include("PRODUCTO.NOMBRE")
        //                .Where(x => x.IDUSUARIO == id)
        //                .SingleOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return usuarios;
        //}

        public USUARIO Obtener(int id) //retornar es un objeto
        {
            var usuarios = new USUARIO();
            try
            {
                using (var db = new db_ventas())
                {
                    usuarios = db.USUARIO.Include("PEDIDO")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.IDUSUARIO == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuarios;
        }

        public USUARIO ObtenerLogin(int id)
        {
            var usuario = new USUARIO();

            try
            {
                using (var db = new db_ventas())
                {
                    usuario = db.USUARIO.Where(x => x.IDUSUARIO == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuario;
        }

        //public void Guardar()
        //{
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            if (this.IDUSUARIO == "")
        //            {
        //                db.Entry(this).State = EntityState.Modified;
        //            }
        //            else
        //            {
        //                db.Entry(this).State = EntityState.Added;
        //            }
        //            db.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public void Guardar()
        {
            try
            {
                using (var db = new db_ventas())
                {
                    if (this.IDUSUARIO != 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Eliminar()
        {
            try
            {
                using (var db = new db_ventas())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<USUARIO> Buscar(string criterio) //Buscar por nombre y estado
        {
            var usuarios = new List<USUARIO>();
            string estado = "";
            if (criterio == "Activo") estado = "A";
            if (criterio == "Inactivo") estado = "I";
            try
            {
                using (var db = new db_ventas())
                {
                    usuarios = db.USUARIO
                                .Where(x => x.NOMBRE.Contains(criterio) || x.ESTADO == estado)
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuarios;
        }

        public ResponseModel GuardarFoto(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_ventas())
                {
                    db.Configuration.ValidateOnSaveEnabled = false;

                    var eUsuario = db.Entry(this);
                    if (this.IDUSUARIO > 0)
                    { 
                        eUsuario.State = EntityState.Modified;
                        //Obviar campos o ignorar en la actualización
                        if (Foto != null)
                        {
                            String archivo = Path.GetFileName(Foto.FileName);//Path.GetExtension(Foto.FileName);
                        
                            //Nombre de imagen en forma aleatoria
                            //String archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);

                            //Colocar la ruta donde se grabará
                            Foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));

                            //enviar al modelo el nombre del archivo
                            this.FOTO = archivo;
                        }
                        else eUsuario.Property(x => x.FOTO).IsModified = false; // el campo no es obligatorio

                        if (this.NOMBREUSU == null) eUsuario.Property(x => x.NOMBREUSU).IsModified = false;

                        if (this.PASSWORD == null) eUsuario.Property(x => x.PASSWORD).IsModified = false;

                        db.SaveChanges();
                        rm.SetResponse(true);
                    }
                    else 
                    {
                        eUsuario.State = EntityState.Added;
                        //Obviar campos o ignorar en la actualización
                        if (Foto != null)
                        {
                            String archivo = Path.GetFileName(Foto.FileName);//Path.GetExtension(Foto.FileName);

                            //Nombre de imagen en forma aleatoria
                            //String archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);

                            //Colocar la ruta donde se grabará
                            Foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));

                            //enviar al modelo el nombre del archivo
                            this.FOTO = archivo;
                        }
                        else eUsuario.Property(x => x.FOTO).IsModified = false; // el campo no es obligatorio

                        //if (this.NOMBREUSU == null) eUsuario.Property(x => x.NOMBREUSU).IsModified = false;

                        //if (this.PASSWORD == null) eUsuario.Property(x => x.PASSWORD).IsModified = false;

                        db.SaveChanges();
                        rm.SetResponse(true);
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }

        public ResponseModel GuardarPassword(HttpPostedFileBase Pass)
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_ventas())
                {
                    db.Configuration.ValidateOnSaveEnabled = false;

                    var eUsuario = db.Entry(this);
                    eUsuario.State = EntityState.Modified;
                    //Obviar campos o ignorar en la actualización
                    if (Pass != null)
                    {
                        //String archivo = Path.GetFileName(Foto.FileName);//Path.GetExtension(Foto.FileName);

                        //Nombre de imagen en forma aleatoria
                        //String archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);

                        //Colocar la ruta donde se grabará
                        //Foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));

                        //enviar al modelo el nombre del archivo
                        //this.FOTO = archivo;
                    }
                    else eUsuario.Property(x => x.PASSWORD).IsModified = false; // el campo no es obligatorio

                    if (this.NOMBREUSU == null) eUsuario.Property(x => x.NOMBREUSU).IsModified = false;

                    if (this.FOTO == null) eUsuario.Property(x => x.FOTO).IsModified = false;

                    if (this.EMAIL == null) eUsuario.Property(x => x.EMAIL).IsModified = false;

                    db.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
    }
}
