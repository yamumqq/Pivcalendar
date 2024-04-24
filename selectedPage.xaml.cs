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

namespace Calendar
{
    /// <summary>
    /// Логика взаимодействия для selectedPage.xaml
    /// </summary>
    public partial class selectedPage : Page
    {
        DateTime date;
        DateNote dateNote = new DateNote()
        {
            notes = new List<Note>(),
        };

        bool flag = true;
        int exitcode = 0;
    

        public selectedPage(DateTime SelectedDate)
        {
            InitializeComponent();

            dateNote.notes.Add(new Note() { name = "Корона", iconPath = Environment.CurrentDirectory+ "\\Resources\\Corona1.png" });
            dateNote.notes.Add(new Note() { name = "Гараж", iconPath = Environment.CurrentDirectory + "\\Resources\\Garage.png" });
            dateNote.notes.Add(new Note() { name = "Эссе", iconPath = Environment.CurrentDirectory + "\\Resources\\Esse.png" });
            dateNote.notes.Add(new Note() { name = "Хайникен", iconPath = Environment.CurrentDirectory + "\\Resources\\Hai.png" });
            dateNote.notes.Add(new Note() { name = "Козел", iconPath = Environment.CurrentDirectory + "\\Resources\\Kozel.png" });

            date = SelectedDate;

            foreach (var day in DateNote.days)
            {
                if (day.day == SelectedDate)
                {
                    dateNote = day;
                    flag = false;
                    exitcode = DateNote.days.IndexOf(day);
                    break;
                }
            }

            GenerateItems();
        }

        private void GenerateItems()
        {
            itemsStack.Children.Clear();

            foreach (var note in dateNote.notes)
            {
                StackPanel itemStackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5),
                };

                CheckBox checkBox = new CheckBox() 
                { 
                    IsChecked = note.isSelected,
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Tag = note,
                };
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;

                BitmapImage bitmapImage = new BitmapImage(new Uri(note.iconPath, UriKind.Absolute));

                Image image = new Image()
                {
                    Source = bitmapImage,
                    Margin = new Thickness(5),
                    Stretch = Stretch.Fill,
                    MaxWidth = 100,
                    MaxHeight = 100,
                    Width = bitmapImage.PixelWidth,
                    Height = bitmapImage.PixelHeight,
                };

                TextBlock textBlock = new TextBlock()
                {
                    Text = note.name
                };

                itemStackPanel.Children.Add(checkBox);
                itemStackPanel.Children.Add(textBlock);
                itemStackPanel.Children.Add(image);
                
                itemsStack.Children.Add(itemStackPanel);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Content = (Application.Current.MainWindow as MainWindow).calendarPage;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag02 = false;

            foreach (var note in dateNote.notes)
            {
                if (note.isSelected)
                {
                    flag02 = true;                    
                }
            }

            if (flag02)
            {
                if(DateNote.days == null)
                {
                    DateNote.days = new List<DateNote>();                    
                }

                if(flag)
                {
                    dateNote.day = date;
                    DateNote.days.Add(dateNote);                    
                }
                else
                {
                    DateNote.days[exitcode] = dateNote;                    
                }
            }
            (Application.Current.MainWindow as MainWindow).calendarPage.createCards();
            (Application.Current.MainWindow as MainWindow).Content = (Application.Current.MainWindow as MainWindow).calendarPage;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            dateNote.notes[dateNote.notes.IndexOf((sender as CheckBox).Tag as Note)].isSelected = (sender as CheckBox).IsChecked ?? false;
            
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            
            dateNote.notes[dateNote.notes.IndexOf((sender as CheckBox).Tag as Note)].isSelected = (sender as CheckBox).IsChecked ?? false;
            
        }
    }
}
