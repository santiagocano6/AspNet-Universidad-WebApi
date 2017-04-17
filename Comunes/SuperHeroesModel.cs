using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Comunes
{
    public class SuperHeroesModel
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string SuperPoder { get; set; }

        public string Sexo { get; set; }
    }
}
