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
    /// Логика взаимодействия для PageExecutor.xaml
    /// </summary>
    public partial class PageExecutor : Page
    {
        public PageExecutor()
        {
            InitializeComponent();
            DGridExecutor.ItemsSource = ekzam_SokolovEntities.GetContext().Task.ToList();

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerHelp.MainFrame.GoBack();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ManagerHelp.MainFrame.Navigate(new PageAddExecutor((sender as Button).DataContext as Task));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame.Navigate(new PageAddExecutor(null));
            MessageBox.Show("В данный момент вы не можете добавить данные. " +
                "Данная функция находится в разработке!");
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var taskForRemoving = DGridExecutor.SelectedItems.Cast<Task>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {taskForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ekzam_SokolovEntities.GetContext().Task.RemoveRange(taskForRemoving);
                    ekzam_SokolovEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridExecutor.ItemsSource = ekzam_SokolovEntities.GetContext().Task.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ekzam_SokolovEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridExecutor.ItemsSource = ekzam_SokolovEntities.GetContext().Task.ToList();
        }
    }
}
