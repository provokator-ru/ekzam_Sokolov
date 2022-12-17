using ekzam_Sokolov.AppDataFile;
using ekzam_Sokolov.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task = ekzam_Sokolov.AppDataFile.Task;

namespace ekzam_Sokolov.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddExecutor.xaml
    /// </summary>
    public partial class PageAddExecutor : Page
    {
        private Task _currentTask = new Task();
        public PageAddExecutor(Task selectedTask)
        {
            InitializeComponent();

            if (selectedTask != null)
                _currentTask = selectedTask;

            DataContext = _currentTask;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentTask.ID == 0)
                ekzam_SokolovEntities.GetContext().Task.Add(_currentTask);
            try
            {
                ekzam_SokolovEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                ManagerHelp.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerHelp.MainFrame.GoBack();
        }
    }
}
