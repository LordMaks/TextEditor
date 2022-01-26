using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public bool SaveAssIsEnabled 
        {
            get { return _saveAssIsEnabled;  }
            set 
            {
                _saveAssIsEnabled = value; 
                OnPropertyChanged("SaveAssIsEnabled");
            }
        }
        private bool _saveAssIsEnabled;


        public bool SaveIsEnabled
        {
            get { return _saveIsEnabled; }
            set
            {
                _saveIsEnabled = value;
                OnPropertyChanged("SaveIsEnabled");
            }
        }
        private bool _saveIsEnabled;


        public string? TextBox
        {
            get { return _textBox; }
            set
            {
                _textBox = value;
                OnPropertyChanged("TextBox");
            }
        }
        private string? _textBox;

        public string? Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }
        private string? _path;

        public string? Dbug
        {
            get { return _dbug; }
            set
            {
                _dbug = value;
                OnPropertyChanged("Dbug");
            }
        }
        private string? _dbug;
    }


    public partial class MainWindow : Window
    {

        MainViewModel mvm;


        public MainWindow()
        {
            mvm = new MainViewModel();

            InitializeComponent();
            DataContext = mvm;
            mvm.SaveIsEnabled = false;
            mvm.SaveAssIsEnabled = false;
            mvm.Dbug = "Yes";
        }

        

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text != "" || mvm.Path != null || mvm.Path != "") 
            { 
                mvm.SaveIsEnabled = true;
                mvm.SaveAssIsEnabled = true;
            }
        }

        private void textBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            mvm.SaveIsEnabled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                mvm.Path = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    mvm.TextBox = reader.ReadToEnd();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(mvm.Path))
                File.WriteAllText(mvm.Path, mvm.TextBox);
        }

        private void SaveAss_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, mvm.TextBox);
        }
    }
}