using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgIIFinalProject
{
    public class Programaciones
    {

        private string _materia;
        private string _estudiante;
        private Horario _horario;
        private String _trimestre="Enero/Marzo";
        private String _aula="GC 214";
        private String _profesor="XXXXXXX";
        private int _ID=00000;

        public string materia
        {
            get { return _materia; }
            set { _materia = value; }
        }

        public Horario Horario
        {
            get { return _horario; }
            set { _horario = value; }
        }


        public string estudiante
        {
            get { return _estudiante; }
            set { _estudiante = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string trimestre
        {
            get { return _trimestre; }
            set { _trimestre = value; }
        }
        public string aula
        {
            get { return _aula; }
            set { _aula = value; }
        }


        public string profesor
        {
            get { return _profesor; }
            set { _profesor = value; }
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
