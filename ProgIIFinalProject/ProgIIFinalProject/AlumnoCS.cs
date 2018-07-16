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
    public class AlumnoCS
    {
        
        private String _nombre;
        private String _apellido;
        private int _ID;
        private String _identificadorPersonal;
        private String _estado;
        private bool _nacionalidad = true;
        private String _carrera;
        private string _fechaNacimiento;
        

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string IdentificadorPersonal
        {
            get { return _identificadorPersonal; }
            set { _identificadorPersonal = value; }

        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public string carrera
        {
            get { return _carrera; }
            set { _carrera = value; }
        }
        public bool Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }
 
        public string FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }

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
            _nacionalidad = extrangero;
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
