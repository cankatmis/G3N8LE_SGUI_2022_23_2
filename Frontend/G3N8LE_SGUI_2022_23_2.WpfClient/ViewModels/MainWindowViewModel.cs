using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_SGUI_2022_23_2.WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand ManageTeachersCommand { get; set; }
        public RelayCommand ManageStudentsCommand { get; set; }
        public RelayCommand ManageReservationsCommand { get; set; }
        public RelayCommand ManageClassesCommand { get; set; }

        public MainWindowViewModel()
        {
            ManageTeachersCommand = new RelayCommand(OpenTeachersWindow);
            ManageStudentsCommand = new RelayCommand(OpenStudentsWindow);
            ManageReservationsCommand = new RelayCommand(OpenReservationsWindow);
            ManageClassesCommand = new RelayCommand(OpenClassesWindow);
        }
        private void OpenClassesWindow()
        {
            new ClassesWindow().Show();
        }
        private void OpenTeachersWindow()
        {
            new TeachersWindow().Show();
        }
        private void OpenStudentsWindow()
        {
            new StudentsWindow().Show();
        }
        private void OpenReservationsWindow()
        {
            new ReservationsWindow().Show();
        }
    }
}
