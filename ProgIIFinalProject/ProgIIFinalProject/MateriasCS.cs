using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgIIFinalProject
{
    class MateriasCS
    {
        private String _nombre;
        private String _codigo;
        private int _ID;
        private String _area;


        public string nombre
        {
            get { return _nombre; }
        }
        public string codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string area
        {
            get { return _area; }
        }

        public MateriasCS(string nombre, string codigo, int ID, string area)
        {
            _nombre = nombre;
            _codigo = codigo;
            _ID = ID;
            _area = area;
        }

        public void GenerarID()
        {
            string iD = "";
            Random r1 = new Random();
            iD = "" + r1.Next(0000, 10000);
            _ID = int.Parse(iD);
        }
    }
}
