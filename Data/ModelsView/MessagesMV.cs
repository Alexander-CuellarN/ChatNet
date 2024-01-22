using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsView
{
    public class MessagesMV
    {
        public int MensajeId { get; set; }
        public string Contenido { get; set; }
        public string UserNick { get; set; }
    }
}
