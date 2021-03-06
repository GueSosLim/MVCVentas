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

        //public virtual PRODUCTO PRODUCTOS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }

        public List<CATEGORIA> Listar() //retornar es una colecci�n
        {
            var categorias = new List<CATEGORIA>();
            try
            {
                using (var db = new db_ventas())
                {
                    categorias = db.CATEGORIA
                                    .Include("PRODUCTO")
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        //public List<CATEGORIA> Consulta() //retornar es una colecci�n
        //{
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            var categorias1 = db.CATEGORIA.GroupJoin(db.PRODUCTO,
        //                p => p.IDCATEGORIA,
        //                c => c.IDCATEGORIA,
        //                (pro, cat) => new
        //                {
        //                    IDCATEGORIA,
        //                    cate = cat.NOMBRE,
        //                    total = pro.PRODUCTO.Count()
        //                });

        //            var categorias2 = from c in db.CATEGORIA
        //                             join p in db.PRODUCTO
        //                             on c.IDCATEGORIA equals p.IDCATEGORIA into g
        //                             select new
        //                             {
        //                                 IDCATEGORIA = c.IDCATEGORIA,
        //                                 NOMBRE = c.NOMBRE,
        //                                 total = g.Count()
        //                             };

        //            var categorias3 = db.CATEGORIA
        //                                .Include(x => x.PRODUCTO)
        //                                .GroupBy(g => new { g.IDCATEGORIA, g.NOMBRE })
        //                                .Select(new CATEGORIA { IDCATEGORIA = g.key.IDCATEGORIA });
        //            //NOMBRE = g.key.IDCATEGORIA})
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return categorias2;
        //}

        //public List<CATEGORIA> Consulta() //retornar es una colecci�n
        //{
        //    var categorias3 = new List<CATEGORIA>();
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            //var categorias1 = db.CATEGORIA.GroupJoin(db.PRODUCTO,
        //            //    p => p.IDCATEGORIA,
        //            //    c => c.IDCATEGORIA,
        //            //    (pro, cat) => new
        //            //    {
        //            //        IDCATEGORIA,
        //            //        cate = cat.NOMBRE,
        //            //        total = pro.PRODUCTO.Count()
        //            //    });

        //            //var categorias2 = from c in db.CATEGORIA
        //            //                  join p in db.PRODUCTO
        //            //                  on c.IDCATEGORIA equals p.IDCATEGORIA into g
        //            //                  select new
        //            //                  {
        //            //                      IDCATEGORIA = c.IDCATEGORIA,
        //            //                      NOMBRE = c.NOMBRE,
        //            //                      total = g.Count()
        //            //                  }
        //            //                  ;

        //            categorias3 = db.CATEGORIA
        //                                .Include(x => x.PRODUCTO)
        //                                .GroupBy(g => new { g.IDCATEGORIA, g.NOMBRE })
        //                                .Select(g => new CATEGORIA { IDCATEGORIA = g.Key.IDCATEGORIA,
        //                                    NOMBRE = g.Key.NOMBRE
        //                                })
        //                                .ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return categorias3;
        //}

        public string[,] Consulta()
        {
            string[,] categoria;
            try
            {
                using (var db = new db_ventas())
                {
                    var cat = (from c in db.CATEGORIA
                               join p in db.PRODUCTO
                               on c.IDCATEGORIA equals p.IDCATEGORIA into g
                               select new
                               {
                                   IDCATEGORIA = c.IDCATEGORIA,
                                   NOMBRE = c.NOMBRE,
                                   TOTAL = g.Count()
                               }).ToList();
                    categoria = new string[cat.Count(), 3];
                    int count = 0;
                    foreach (var c in cat)
                    {
                        categoria[count, 0] = Convert.ToString(c.IDCATEGORIA);
                        categoria[count, 1] = Convert.ToString(c.NOMBRE);
                        categoria[count, 2] = Convert.ToString(c.TOTAL);
                        count++;
                    }
                }
                return categoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public CATEGORIA Consultar(int IDCATEGORIA, string NOMBRE, int TOTAL) //retornar es una colecci�n
        //{
        //    var categorias2 = new CATEGORIA();
        //    try
        //    {
        //        using (var db = new db_ventas())
        //        {
        //            categorias2 = (from c in db.CATEGORIA
        //                               join p in db.PRODUCTO
        //                               on c.IDCATEGORIA equals p.IDCATEGORIA into g
        //                               from p in g.DefaultIfEmpty()
        //                               select new
        //                               {
        //                                   IDCATEGORIA = c.IDCATEGORIA,
        //                                   NOMBRE = c.NOMBRE,
        //                                   TOTAL = g.Count()
        //                               }).ToListAsync;

        //            var categorias4 = (from C in db.CATEGORIA
        //                              join P in db.PRODUCTO on C.IDCATEGORIA equals P.IDCATEGORIA into P_join
        //                              from P in P_join.DefaultIfEmpty()
        //                              group new { C, P } by new
        //                              {
        //                                  C.IDCATEGORIA,
        //                                  C.NOMBRE
        //                              } into g
        //                              select new
        //                              {
        //                                  IDCATEGORIA = (int)g.Key.IDCATEGORIA,
        //                                  g.Key.NOMBRE,
        //                                  TOTAL = g.Count(p => p.P.IDCATEGORIA != null)
        //                              }).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return categorias2;
        //}
    }
}