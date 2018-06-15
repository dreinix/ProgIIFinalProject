using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgIIFinalProject
{
    class Progamaciones
    {
        private object _materia;
        private String _estudiante;
        private object _horario;
        private String _trimestre;
        private String _aula;
        private String _profesor;
        private int _ID;

        public string materia
        {
            get { return _nombre; }
            set { _nombre = value; }
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
            set { _area = value; }
        }
        public string areaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }
        public void GenerarID()
        {
            string iD = "";
            Random r1 = new Random();
            iD = "" + r1.Next(0000, 10000);
            _ID = int.Parse(iD);
        }
        public void GenerarCodigo()
        {
            string prefix = _areaCode;
            string code = "";
            Random r1 = new Random();
            code = prefix + "" + r1.Next(0, 1000);
            _codigo = code;
        }
    }
}