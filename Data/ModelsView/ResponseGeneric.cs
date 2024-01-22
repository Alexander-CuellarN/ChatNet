using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsView
{
    public class ResponseGeneric<T>
    {
        public string Message { get; set; }
        public ICollection<T> Data{ get; set; }
    }
}
