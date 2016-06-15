namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("CATEGORIA")]
    public partial class CATEGORIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORIA()
        {
            PRODUCTO = new HashSet<PRODUCTO>();
        }

        [Key]
        public int IDCATEGORIA { get; set; }

        [Required]
        [StringLength(20)]
        public string NOMBRE { get; set; }
        
        [Column(TypeName = "text")]
        public string DESCRIPCION { get; set; }

        [Required]
        [StringLength(1)]
        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }

        public List<CATEGORIA> Listar() //retornar es una colecci�n
        {
            var categorias = new List<CATEGORIA>();
            try
            {
                using (var db = new db_ventas())
                {
                    categorias = db.CATEGORIA.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return categorias;
        }

        //public CATEGORIA Obtener(int id) //retornar es un objeto
        //{
        //    var categorias = new CATEGORIA();
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            categorias = db.CATEGORIA.Where ( x => x.IDCATEGORIA == id) .SingleOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return categorias;
        //}

        public CATEGORIA Obtener(int id) //retornar es un objeto
        {
            var categorias = new CATEGORIA();
            try
            {
                using (var db = new db_ventas())
                {
                    categorias = db.CATEGORIA.Include("PRODUCTO")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.IDCATEGORIA == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return categorias;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new db_ventas())
                {
                    if (this.IDCATEGORIA > 0)
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

        //public List<CATEGORIA> Buscar(string criterio) //Buscar solo por nombre
        //{
        //    var categorias = new List<CATEGORIA>();
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            categorias = db.CATEGORIA
        //                        .Where(x => x.NOMBRE.Contains(criterio))
        //                        .ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return categorias;
        //}

        public List<CATEGORIA> Buscar(string criterio) //Buscar por nombre y estado
        {
            var categorias = new List<CATEGORIA>();
            string estado = "";
            if (criterio == "Activo") estado = "A";
            if (criterio == "Inactivo") estado = "I";
            try
            {
                using (var db = new db_ventas())
                {
                    categorias = db.CATEGORIA
                                .Where(x => x.NOMBRE.Contains(criterio) || x.ESTADO == estado)
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return categorias;
        }
    }
}