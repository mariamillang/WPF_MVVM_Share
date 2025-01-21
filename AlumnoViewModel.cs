using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using UD05_AlumnosApp.Models;
using UD05_AlumnosApp.Servicios;

namespace UD05_AlumnosApp.ViewModels
{
    public class AlumnoViewModel : INotifyPropertyChanged
    {

        //Para las notificaciones de cambio en la vista.
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private readonly AlumnoService _alumnoService;

        public ObservableCollection<AlumnoModel> Alumnos { get; set; }

        //Comandos
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand EditCommand { get; }

        public RelayCommand CancelCommand { get; }

        #region Propiedades nuevo alumno

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
               OnPropertyChanged(nameof(Nombre));
            }
        }

        private string _apellido;
        public string Apellido
        {
            get => _apellido;
            set
            {
                _apellido = value;
                OnPropertyChanged(nameof(Apellido));
            }
        }

        private string _curso;
        public string Curso
        {
            get => _curso;
            set
            {
                _curso = value;
                OnPropertyChanged(nameof(Curso));
            }
        }
        #endregion

        #region Propiedad alumno seleccionado
        private AlumnoModel _selectedAlumno;
        public AlumnoModel SelectedAlumno
        {
            get { return _selectedAlumno; }
            set
            {
                _selectedAlumno = value;
                OnPropertyChanged(nameof(SelectedAlumno));

                if(_selectedAlumno != null)
                {
                    Nombre = _selectedAlumno.Nombre;
                    Apellido = _selectedAlumno.Apellido;
                    Curso = _selectedAlumno.Curso;

                }
            }
        }
        #endregion
        public AlumnoViewModel()
        {
            //Iniciamos una instancia de mi clase de servicios para poder acceder a sus métodos
            _alumnoService = new AlumnoService();
            //Iniciamos mi listado de alumnos
            Alumnos = new ObservableCollection<AlumnoModel>();

            AddCommand = new RelayCommand(
              execute: _ => AddAlumno(),
              canExecute: _ => checkAlumno() && SelectedAlumno == null
            );

            DeleteCommand = new RelayCommand(
                execute: _ => DeleteAlumno(),
                canExecute : _ => SelectedAlumno !=null                
            );

            EditCommand = new RelayCommand(
              execute: _ => EditAlumno(),
              canExecute: _ => SelectedAlumno != null
            );

            CancelCommand = new RelayCommand(
                execute: _ => LimpiarFormulario(),
                canExecute => SelectedAlumno != null
            );

            LoadData();
        }

        #region CRUD ALUMNO
        private void LoadData()
        {
            Alumnos = _alumnoService.GetAllAlumnos();
        }

        private void AddAlumno()
        {
            int idAlum = Alumnos.Count + 1;
            //AlumnoModel alum = new AlumnoModel("Víctor", "Carmona", "1 DAM");
            AlumnoModel alum = new AlumnoModel(idAlum,Nombre, Apellido, Curso);
            _alumnoService.AddAlumno(alum);
            LimpiarFormulario();

        }
        private void DeleteAlumno() {
            var confirmacion = MessageBox.Show("¿Desea eliminar el alumno?",
               "Alerta", MessageBoxButton.OKCancel);
            if (confirmacion == MessageBoxResult.OK)
            {
                _alumnoService.RemoveAlumno(SelectedAlumno);
                LimpiarFormulario();
            }
            
        }

        private void EditAlumno()
        {
            if (SelectedAlumno != null)
            {
                SelectedAlumno.Nombre = Nombre;
                SelectedAlumno.Apellido = Apellido;
                SelectedAlumno.Curso = Curso;

                _alumnoService.UpdateAlumno(SelectedAlumno);

                LimpiarFormulario();
            }
        }

        #endregion
        private bool checkAlumno()
        {
            bool check = false;
            if(Nombre!=null && Apellido != null && Curso != null)
            {
               check = true;
            }
            return check;
        }

        private void LimpiarFormulario()
        {
            Nombre = null;
            Apellido = null;
            Curso = null;
            SelectedAlumno = null;
        }

    }
}
