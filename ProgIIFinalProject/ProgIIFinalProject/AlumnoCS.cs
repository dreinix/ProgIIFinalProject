using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Data.Sql;

namespace ProgIIFinalProject
{
    class AlumnoCS
    {
        
        private String _nombre;
        private String _apellido;
        private int _ID;
        private String _identificadorPersonal;
        private String _estado;
        private bool _extrangero = true;
        private String _carrera;
        private string _fechaNacimiento;
        

        public string nombre
        {
            get { return _nombre; }
        }
        public string apellido
        {
            get { return _apellido; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string identificadorPersonal
        {
            get { return _identificadorPersonal; }
        }
        public string estado
        {
            get { return _estado; }
        }
        public string carrera
        {
            get { return _carrera; }
        }
        public bool extrangero
        {
            get { return _extrangero; }
        }
 
        public string fechaNacimiento
        {
            get { return _fechaNacimiento; }

        }
        
        public AlumnoCS()
        {
        }

        public AlumnoCS(string nombre, string apellido, int ID, string identificadorPersonal, string estado, bool extrangero, string carrera, string fechaNacimiento)
        {
            _nombre = nombre;
            _apellido = apellido;
            _ID = ID;
            _identificadorPersonal = identificadorPersonal;
            _estado = estado;
            _extrangero = extrangero;
            _carrera = carrera;
            _fechaNacimiento = fechaNacimiento;
        }

        public void GenerarID()
        {
            string yearIdentiffier = DateTime.Today.Year.ToString(), currentId = yearIdentiffier[2] + "0" + yearIdentiffier[3];
            string iD = "";
            Random r1 = new Random();
            iD = currentId + "" + r1.Next(1001, 10000);
            _ID = int.Parse(iD);
        }
    }
}
