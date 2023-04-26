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
    public class StudentsWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Students> Students { get; set; }
        public IList<KeyValuePair<int, int>> BestStudent { get; set; }
        public IList<KeyValuePair<int, int>> WorstStudent { get; set; }
        private Students _selectedStudent;

        public Students SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                SetProperty(ref _selectedStudent, value);
            }
        }
        private int _selectedStudentIndex;

        public int SelectedStudentIndex
        {
            get => _selectedStudentIndex;
            set
            {
                SetProperty(ref _selectedStudentIndex, value);
            }
        }
        public RelayCommand AddStudentCommand { get; set; }
        public RelayCommand EditStudentCommand { get; set; }
        public RelayCommand DeleteStudentCommand { get; set; }
        public RelayCommand BestStudentCommand { get; set; }
        public RelayCommand WorstStudentCommand { get; set; }
        public StudentsWindowViewModel()
        {
            Students = new ObservableCollection<Students>();
            BestStudent = new List<KeyValuePair<int, int>>();
            WorstStudent = new List<KeyValuePair<int, int>>();

            _apiClient
                .GetAsync<List<Students>>("http://localhost:60617/students")
                .ContinueWith((students) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        students.Result.ForEach((student) =>
                        {
                            Students.Add(student);
                        });
                    });
                });
            _apiClient
               .GetAsync<List<KeyValuePair<int, int>>>("http://localhost:60617/Noncrudfan/BestStudents")
               .ContinueWith((Beststudent) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       Beststudent.Result.ForEach((Beststud) =>
                       {
                           BestStudent.Add(Beststud);
                       });
                   });
               });
            _apiClient
              .GetAsync<List<KeyValuePair<int, int>>>("http://localhost:60617/Noncrudfan/WorstStudents")
              .ContinueWith((Worststudent) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      Worststudent.Result.ForEach((Worststud) =>
                      {
                          WorstStudent.Add(Worststud);
                      });
                  });
              });

            AddStudentCommand = new RelayCommand(AddStudent);
            EditStudentCommand = new RelayCommand(EditStudent);
            DeleteStudentCommand = new RelayCommand(DeleteStudent);
            BestStudentCommand = new RelayCommand(BestStudentMethod);
            WorstStudentCommand = new RelayCommand(WorstStudentMethod);

        }

        #region CRUD
        private void AddStudent()
        {
            Students n = new Students
            {
                Name = SelectedStudent.Name,
                City = SelectedStudent.City,
                Email = SelectedStudent.Email,
                PhoneNumber = SelectedStudent.PhoneNumber

            };

            _apiClient
                .PostAsync(n, "http://localhost:60617/students")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Students.Add(n);
                    });
                });

        }
        private void EditStudent()
        {
            _apiClient
                .PutAsync(SelectedStudent, "http://localhost:60617/students")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedStudentIndex;
                        Students a = SelectedStudent;
                        Students.Remove(SelectedStudent);
                        Students.Insert(i, a);
                    });
                });
        }
        private void DeleteStudent()
        {
            _apiClient
                .DeleteAsync(SelectedStudent.Id, "http://localhost:60617/students")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Students.Remove(SelectedStudent);
                    });
                });
        }
        #endregion

        #region NON-CRUD
        private void BestStudentMethod()
        {
            new BestStudentWindow().Show();
        }
        private void WorstStudentMethod()
        {
            new WorstStudentWindow().Show();
        }
        #endregion
    }
}
