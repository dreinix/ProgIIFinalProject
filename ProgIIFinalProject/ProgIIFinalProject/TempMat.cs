using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgIIFinalProject
{
    public class TempMat
    {
        private string _area;
        private string _codigo;
        private String _nombre;

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public TempMat()
        {
        }

        public TempMat(string area, string codigo, string nombre)
        {
            _area = area;
            _codigo = codigo;
            _nombre = nombre;
        }
    }
}
