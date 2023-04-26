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
    class ClassesWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Classes> Classes { get; set; }
        private Classes _selectedClass;

        public Classes SelectedClass
        {
            get => _selectedClass;
            set
            {
                SetProperty(ref _selectedClass, value);
            }
        }
        private int _selectedClassIndex;

        public int SelectedClassIndex
        {
            get => _selectedClassIndex;
            set
            {
                SetProperty(ref _selectedClassIndex, value);
            }
        }
        public RelayCommand AddClassCommand { get; set; }
        public RelayCommand EditClassCommand { get; set; }
        public RelayCommand DeleteClassCommand { get; set; }
        public ClassesWindowViewModel()
        {
            Classes = new ObservableCollection<Classes>();

            _apiClient
                .GetAsync<List<Classes>>("http://localhost:60617/classes")
                .ContinueWith((classes) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        classes.Result.ForEach((classs) =>
                        {
                            Classes.Add(classs);
                        });
                    });
                });

            AddClassCommand = new RelayCommand(AddClass);
            EditClassCommand = new RelayCommand(EditClass);
            DeleteClassCommand = new RelayCommand(DeleteClass);

        }
        private void AddClass()
        {
            Classes n = new Classes
            {
                Name = SelectedClass.Name,
                Price = SelectedClass.Price,             
                Grading = SelectedClass.Grading
            };

            _apiClient
                .PostAsync(n, "http://localhost:60617/classes")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Classes.Add(n);
                    });
                });

        }
        private void EditClass()
        {
            _apiClient
                .PutAsync(SelectedClass, "http://localhost:60617/classes")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedClassIndex;
                        Classes a = SelectedClass;
                        Classes.Remove(SelectedClass);
                        Classes.Insert(i, a);
                    });
                });
        }
        private void DeleteClass()
        {
            _apiClient
                .DeleteAsync(SelectedClass.Id, "http://localhost:60617/classes")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Classes.Remove(SelectedClass);
                    });
                });
        }
    }
}
