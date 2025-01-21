using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UD05_AlumnosApp.Models;

namespace UD05_AlumnosApp.Servicios
{
    public class AlumnoService
    {
        private ObservableCollection<AlumnoModel> _alumnoList { get; set; }

        //MÉTODO PARA OBTENER EL LISTADO DE ALUMNOS QUE YA TENGO DEFINIDO
        public ObservableCollection<AlumnoModel> GetAllAlumnos()
        {
            _alumnoList = new ObservableCollection<AlumnoModel>();

            _alumnoList.Add(new AlumnoModel(1,"María", "Millán", "2 DAM"));
            _alumnoList.Add(new AlumnoModel(2,"Pedro", "Caro", "2 DAM"));
            _alumnoList.Add(new AlumnoModel(3,"Ana", "Limón", "1 DAM"));
            _alumnoList.Add(new AlumnoModel(4, "Mónica", "Valero", "1 DAM"));

            return _alumnoList;
        }

        public void AddAlumno(AlumnoModel AlumnoModel)
        {
            _alumnoList.Add(AlumnoModel);

        }

        public void RemoveAlumno(AlumnoModel AlumnoModel)
        {
            _alumnoList.Remove(AlumnoModel);

        }

        public void UpdateAlumno(AlumnoModel updatedAlumno)
        {
            // Buscar por Id en la lista
            var existing = _alumnoList.FirstOrDefault(a => a.Id == updatedAlumno.Id);
            if (existing != null)
            {
                existing.Nombre = updatedAlumno.Nombre;
                existing.Apellido = updatedAlumno.Apellido;
                existing.Curso = updatedAlumno.Curso;
            
            }
        }
    }
}
