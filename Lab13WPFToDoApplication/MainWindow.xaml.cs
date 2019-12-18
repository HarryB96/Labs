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

namespace Lab13WPFToDoApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> items = new List<string>();
        List<Task> tasks = new List<Task>();
        Task task;
        List<Category> categories = new List<Category>();
        public MainWindow()
        {
            InitializeComponent();
            Initialise();
        }
        

        void Initialise()
        {
            using (var db = new TasksDBEntities())
            {
                tasks = db.Tasks.ToList();
                categories = db.Categories.ToList();
            }
            ListBoxTasks.ItemsSource = tasks;
            ListBoxTasks.DisplayMemberPath = "Description";
            ComboBoxCategory.ItemsSource = categories;
            ComboBoxCategory.DisplayMemberPath = "TaskCategory";
        }

        #region ListBoxTasks
        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            task = (Task)ListBoxTasks.SelectedItem;
            if (task != null)
            {
                TextBoxId.Text = task.TaskId.ToString();
                TextBoxDescription.Text = task.Description;
                TextBoxCategoryId.Text = task.CategoryId.ToString();
                ButtonEdit.IsEnabled = true;
                ButtonDelete.IsEnabled = true;
                if (task.CategoryId != null)
                {
                    ComboBoxCategory.SelectedIndex = (int)task.CategoryId - 1;
                }
                else
                {
                    ComboBoxCategory.SelectedItem = null;
                }
            }
        }

        private void ListBoxTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            task = (Task)ListBoxTasks.SelectedItem;
            if (task != null)
            {
                TextBoxId.Text = task.TaskId.ToString();
                TextBoxDescription.Text = task.Description;
                TextBoxCategoryId.Text = task.CategoryId.ToString();
                ButtonEdit.IsEnabled = true;
                if (task.CategoryId != null)
                {
                    ComboBoxCategory.SelectedIndex = (int)task.CategoryId - 1;
                }
                else
                {
                    ComboBoxCategory.SelectedItem = null;
                }
                TextBoxDescription.IsReadOnly = false;
                TextBoxCategoryId.IsReadOnly = false;
                ButtonEdit.Content = "Save";
                TextBoxCategoryId.Background = Brushes.White;
                TextBoxDescription.Background = Brushes.White;
            }
        }
        #endregion

        #region EditButton
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonEdit.Content.ToString() == "Edit")
            {
                TextBoxDescription.IsReadOnly = false;
                TextBoxCategoryId.IsReadOnly = false;
                ButtonEdit.Content = "Save";
                TextBoxCategoryId.Background = Brushes.White;
                TextBoxDescription.Background = Brushes.White;
            }
            else
            {
                using (var db = new TasksDBEntities())
                {
                    var taskToEdit = db.Tasks.Find(task.TaskId);
                    taskToEdit.Description = TextBoxDescription.Text;
                    int.TryParse(TextBoxCategoryId.Text, out int categoryid);
                    taskToEdit.CategoryId = categoryid;
                    if (task.CategoryId != null)
                    {
                        ComboBoxCategory.SelectedIndex = (int)taskToEdit.CategoryId - 1;
                    }
                    else
                    {
                        ComboBoxCategory.SelectedItem = null;
                    }
                    db.SaveChanges();
                    ListBoxTasks.ItemsSource = null;
                    tasks = db.Tasks.ToList();
                    ListBoxTasks.ItemsSource = tasks;
                }
                ButtonEdit.Content = "Edit";
                ButtonEdit.IsEnabled = false;
                TextBoxDescription.IsReadOnly = true;
                TextBoxCategoryId.IsReadOnly = true;
                var bc = new BrushConverter();
                TextBoxCategoryId.Background = (Brush)bc.ConvertFrom("#f4f4f4");
                TextBoxDescription.Background = (Brush)bc.ConvertFrom("#f4f4f4");
            }
        }
        #endregion

        #region AddButton
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAdd.Content.ToString() == "Add")
            {
                ButtonAdd.Content = "Confirm";
                TextBoxDescription.Background = Brushes.White;
                TextBoxDescription.IsReadOnly = false;
                TextBoxCategoryId.Background = Brushes.White;
                TextBoxCategoryId.IsReadOnly = false;
                TextBoxId.Text = "";
                TextBoxDescription.Text = "";
                TextBoxCategoryId.Text = "";
            }
            else
            {
                int.TryParse(TextBoxCategoryId.Text, out int categoryid);
                var taskToAdd = new Task()
                {
                    Description = TextBoxDescription.Text,
                    CategoryId = categoryid
                };
                using (var db = new TasksDBEntities())
                {
                    db.Tasks.Add(taskToAdd);
                    db.SaveChanges();
                    ListBoxTasks.ItemsSource = null;
                    tasks = db.Tasks.ToList();
                    ListBoxTasks.ItemsSource = tasks;
                }
                ButtonAdd.Content = "Add";
                TextBoxDescription.IsReadOnly = true;
                TextBoxCategoryId.IsReadOnly = true;
                var bc = new BrushConverter();
                TextBoxCategoryId.Background = (Brush)bc.ConvertFrom("#f4f4f4");
                TextBoxDescription.Background = (Brush)bc.ConvertFrom("#f4f4f4");
            }
        }
        #endregion

        #region DeleteButton
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonDelete.Content.ToString() == "Delete")
            {
                ButtonDelete.Content = "Confirm";
                var bc = new BrushConverter();
                TextBoxCategoryId.Background = (Brush)bc.ConvertFrom("#ff3333");
                TextBoxDescription.Background = (Brush)bc.ConvertFrom("#ff3333");
                TextBoxId.Background = (Brush)bc.ConvertFrom("#ff3333");
            }
            else
            {
                using (var db = new TasksDBEntities())
                {
                    var taskToDelete = db.Tasks.Find(task.TaskId);
                    db.Tasks.Remove(taskToDelete);
                    db.SaveChanges();
                    ListBoxTasks.ItemsSource = null;
                    tasks = db.Tasks.ToList();
                    ListBoxTasks.ItemsSource = tasks;
                }
                ButtonDelete.Content = "Delete";
                ButtonDelete.IsEnabled = false;
                TextBoxId.Text = "";
                TextBoxDescription.Text = "";
                TextBoxCategoryId.Text = "";
                var bc = new BrushConverter();
                TextBoxId.Background = (Brush)bc.ConvertFrom("#f4f4f4");
                TextBoxCategoryId.Background = (Brush)bc.ConvertFrom("#f4f4f4");
                TextBoxDescription.Background = (Brush)bc.ConvertFrom("#f4f4f4");
            }
        }
        #endregion
    }
}
