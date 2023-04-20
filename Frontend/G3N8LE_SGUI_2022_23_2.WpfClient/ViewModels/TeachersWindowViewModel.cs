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
    class TeachersWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Teachers> Teachers { get; set; }
        public IList<KeyValuePair<string, int>> TotalTeachersEarnings { get; set; }
        public IList<KeyValuePair<string, int>> MostPaidTeacher { get; set; }
        public IList<KeyValuePair<string, int>> LessPaidTeacher { get; set; }

        private Teachers _selectedTeacher;

        public Teachers SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                SetProperty(ref _selectedTeacher, value);
            }
        }
        private int _selectedTeacherIndex;

        public int SelectedTeacherIndex
        {
            get => _selectedTeacherIndex;
            set
            {
                SetProperty(ref _selectedTeacherIndex, value);
            }
        }
        public RelayCommand AddTeacherCommand { get; set; }
        public RelayCommand EditTeacherCommand { get; set; }
        public RelayCommand DeleteTeacherCommand { get; set; }
        public RelayCommand TeachersEarningCommand { get; set; }
        public RelayCommand MostPaidTeacherCommand { get; set; }
        public RelayCommand LessPaidTeacherCommand { get; set; }
        public TeachersWindowViewModel()
        {
            Teachers = new ObservableCollection<Teachers>();
            TotalTeachersEarnings = new List<KeyValuePair<string, int>>();
            MostPaidTeacher = new List<KeyValuePair<string, int>>();
            LessPaidTeacher = new List<KeyValuePair<string, int>>();


            _apiClient
                .GetAsync<List<Teachers>>("http://localhost:37793/teachers")
                .ContinueWith((teachers) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        teachers.Result.ForEach((teacher) =>
                        {
                            Teachers.Add(teacher);
                        });
                    });
                });

            _apiClient
               .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:37793/Noncrudteacher/TeachersEarnings")
               .ContinueWith((teachersEar) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                      teachersEar.Result.ForEach((teacherea) =>
                       {
                           TotalTeachersEarnings.Add(teacherea);
                       });
                   });
               });
            _apiClient
               .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:37793/Noncrudteacher/Mostpaidteacher")
               .ContinueWith((MostTeach) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       MostTeach.Result.ForEach((mostteach) =>
                       {
                           MostPaidTeacher.Add(mostteach);
                       });
                   });
               });
            _apiClient
              .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:37793/Noncrudteacher/Lesspaidteacher")
              .ContinueWith((MostTeach) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      MostTeach.Result.ForEach((mostteacher) =>
                      {
                          LessPaidTeacher.Add(mostteacher);
                      });
                  });
              });


            AddTeacherCommand = new RelayCommand(AddTeacher);
            EditTeacherCommand = new RelayCommand(EditTeacher);
            DeleteTeacherCommand = new RelayCommand(DeleteTeacher);
            TeachersEarningCommand = new RelayCommand(TeachersEarningCalculation);
            MostPaidTeacherCommand = new RelayCommand(MostPaidTeacherCalculation);
            LessPaidTeacherCommand = new RelayCommand(LessPaidTeacherCalculation);


        }


        #region CRUD
        private void AddTeacher()
        {
            Teachers n = new Teachers
            {
                Name = SelectedTeacher.Name,
                Price = _selectedTeacher.Price,
                Branch = SelectedTeacher.Branch,
                Duration = SelectedTeacher.Duration

            };

            _apiClient
                .PostAsync(n, "http://localhost:37793/teachers")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Teachers.Add(n);
                    });
                });
        }

        private void EditTeacher()
        {
            _apiClient
                .PutAsync(SelectedTeacher, "http://localhost:37793/teachers")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedTeacherIndex;
                        Teachers a = SelectedTeacher;
                        Teachers.Remove(SelectedTeacher);
                        Teachers.Insert(i, a);
                    });
                });
        }
        private void DeleteTeacher()
        {
            _apiClient
                .DeleteAsync(SelectedTeacher.Id, "http://localhost:37793/teachers")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Teachers.Remove(SelectedTeacher);
                    });
                });
        }
        #endregion

        #region NON-CRUD
        private void TeachersEarningCalculation()
        {
            new TeachersEarningWindow().Show();
        }
        private void MostPaidTeacherCalculation()
        {

            new MostPaidTeacherWindow().Show();
        }
        private void LessPaidTeacherCalculation()
        {

            new LessPaidTeacherWindow().Show();
        }
        #endregion

    }
}
