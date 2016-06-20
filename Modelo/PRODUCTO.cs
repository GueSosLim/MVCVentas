namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    [Table("PRODUCTO")]
    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            DETALLE_PEDIDO = new HashSet<DETALLE_PEDIDO>();
        }

        [Key]
        public int IDPRODUCTO { get; set; }

        [Required]
        public int IDCATEGORIA { get; set; }

        [Required]
        [StringLength(20)]
        public string NOMBRE { get; set; }

        [StringLength(50)]
        public string DESCRIPCION { get; set; }

        [Required]
        public int PRECIO { get; set; }

        [Required]
        public int STOCK { get; set; }

        [Required]
        [StringLength(1)]
        public string PORTADA { get; set; }

        public int IMPORTANCIA { get; set; }

        [StringLength(50)]
        public string IMAGEN { get; set; }

        [Required]
        [StringLength(1)]
        public string ESTADO { get; set; }

        public virtual CATEGORIA CATEGORIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_PEDIDO> DETALLE_PEDIDO { get; set; }

        public List<PRODUCTO> Listar() //retornar es una colección
        {
            var productos = new List<PRODUCTO>();
            try
            {
                using (var db = new db_ventas())
                {
                    productos = db.PRODUCTO
                                    .Include("CATEGORIA")
                                    .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return productos;
        }

        public PRODUCTO Obtener(int id) //retornar es un objeto
        {
            var productos = new PRODUCTO();
            try
            {
                using (var db = new db_ventas())
                {
                    productos = db.PRODUCTO.Include("DETALLE_PEDIDO")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.IDPRODUCTO == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return productos;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new db_ventas())
                {
                    if (this.IDPRODUCTO > 0)
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

        public List<PRODUCTO> Buscar(string criterio) //Buscar por nombre y estado
        {
            var productos = new List<PRODUCTO>();
            string estado = "";
            if (criterio == "Activo") estado = "A";
            if (criterio == "Inactivo") estado = "I";
            try
            {
                using (var db = new db_ventas())
                {
                    productos = db.PRODUCTO
                                .Where(x => x.NOMBRE.Contains(criterio) || x.ESTADO == estado)
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return productos;
        }
    }
}
