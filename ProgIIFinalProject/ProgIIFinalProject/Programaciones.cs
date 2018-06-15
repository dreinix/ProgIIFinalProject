using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgIIFinalProject
{
    class Programaciones
    {

        private MateriasCS _materia;
        private String _estudiante;
        private Horario _horario;
        private String _trimestre;
        private String _aula;
        private String _profesor;
        private int _ID;

        public MateriasCS materia
        {
            get { return _materia; }
            set { _materia = value; }
        }

        public Horario horario
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
