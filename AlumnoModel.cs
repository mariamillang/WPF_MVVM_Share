using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UD05_AlumnosApp.Models
{
    public class AlumnoModel : INotifyPropertyChanged
    {
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _curso;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }
        public string Apellido
        {
            get { return _apellido; }
            set
            {
                _apellido = value;
                OnPropertyChanged(nameof(Apellido));
            }
        }
        public string Curso
        {
            get { return _curso; }
            set
            {
                _curso = value;
                OnPropertyChanged(nameof(Curso));
            }
        }

        public AlumnoModel(int id, string nombre, string apellido, string curso)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Curso = curso;
        }

        //Para las notificaciones de cambio en la vista.
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
