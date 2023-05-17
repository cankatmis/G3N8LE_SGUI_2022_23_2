using G3N8LE_ADT_2022_23_1.Models;
using G3N8LE_SGUI_2022_23_2.WpfClient.Clients;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace G3N8LE_SGUI_2022_23_2.WpfClient.ViewModels
{
    public class ReservationsWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Reservations> Reservations { get; set; }
        private Reservations _selectedReservation;

        public Reservations SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                SetProperty(ref _selectedReservation, value);
            }
        }
        private int _selectedReservationIndex;

        public int SelectedReservationIndex
        {
            get => _selectedReservationIndex;
            set
            {
                SetProperty(ref _selectedReservationIndex, value);
            }
        }
        public RelayCommand AddReservationCommand { get; set; }
        public RelayCommand EditReservationCommand { get; set; }
        public RelayCommand DeleteReservationCommand { get; set; }
        public ReservationsWindowViewModel()
        {
            Reservations = new ObservableCollection<Reservations>();

            _apiClient
                .GetAsync<List<Reservations>>("http://localhost:60617/reservations")
                .ContinueWith((reservations) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        reservations.Result.ForEach((reservation) =>
                        {
                            Reservations.Add(reservation);
                        });
                    });
                });

            AddReservationCommand = new RelayCommand(AddReservation);
            EditReservationCommand = new RelayCommand(EditReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation);

        }
        private void AddReservation()
        {

            Reservations n = new Reservations
            {
                TeacherId = SelectedReservation.TeacherId,
                StudentId = SelectedReservation.StudentId,
                DateTime = SelectedReservation.DateTime

            };

            _apiClient
                .PostAsync(n, "http://localhost:60617/reservations")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Reservations.Add(n);
                    });
                });
        }
        private void EditReservation()
        {
            _apiClient
                .PutAsync(SelectedReservation, "http://localhost:60617/reservations")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedReservationIndex;
                        Reservations a = SelectedReservation;
                        Reservations.Remove(SelectedReservation);
                        Reservations.Insert(i, a);
                    });
                });
        }
        private void DeleteReservation()
        {
            if (SelectedReservation != null)
            {
                _apiClient
                    .DeleteAsync(SelectedReservation.Id, "http://localhost:60617/reservations")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Reservations.Remove(SelectedReservation);
                        });
                    });
            }
            else
            {
                MessageBox.Show("No reservation selected.");
            }
        }

    }
}
