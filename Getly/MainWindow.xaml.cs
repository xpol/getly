using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Getly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Task> _items;
        public MainWindow()
        {
            InitializeComponent();

            _items = new ObservableCollection<Task>
            {
                new Task() {Name = "John Doe", Speed = 42, Progress = 0.5f},
                new Task() {Name = "Jane Doe", Speed = 39, Progress = 0.0f},
                new Task() {Name = "Sammy Doe", Speed = 7, Progress = 1.0f}
            };
            Tasks.ItemsSource = _items;
        }

        private void AddDummyTask(object sender, MouseButtonEventArgs e)
        {
            _items.Add(new Task() { Name = "Sammy Doe", Speed = 7, Progress = 1.0f });
        }
    }

    public class Task
    {
        public string Name { get; set; }

        public float Speed { get; set; }

        public float Progress { get; set; }
    }
}
