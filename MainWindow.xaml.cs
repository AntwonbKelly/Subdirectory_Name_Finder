using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Subdirectory_Name_Finder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String Location;
        String[] Directories;
        int counter; //Number of directories at selected Locaiton;
        int counter2; // Number of found directories based on entered string;
        ObservableCollection<String> DirectoryNameHolder = new ObservableCollection<string>(); //Holds directory Names
        ObservableCollection<String> FoundNames = new ObservableCollection<string>(); //Holds the names found in the lsit of directory names
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectDirButton_Click(object sender, RoutedEventArgs e)
        {
           
            counter = 0;
           
            if (Directories != null)
            {
                Array.Clear(Directories, 0, Directories.Length);
            }
            DirectoryNameHolder.Clear(); //Clears Directory Name holder once we search for a new directory

            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.MessageBox.Show(FBD.SelectedPath);


                Location = FBD.SelectedPath;

                Directories = System.IO.Directory.GetDirectories

                    (Location, "*", System.IO.SearchOption.AllDirectories); //Gets subdirectory names in directory

                int count = Directories.Count(); //Gets the amount of directories inside of the array

                if (count < 1) //If the array has 0 directories, than throw a this check
                {
                    System.Windows.MessageBox.Show("This directory has no directories to name!");
                    return;
                }
                DirNameTextBox.Clear(); //Clears the textbox
                DirCounter.Text = Convert.ToString(Directories.Count());
                FirstLbox.DataContext = Directories;
            }
        }

        private void DirectorieNamesToFind_Click(object sender, RoutedEventArgs e)
        {
            counter2 = 0;
            DirCounter2.Clear();

            if (Directories is null) //Checks to see if the user has searched for a directory yet
            {
                System.Windows.MessageBox.Show("You need to select a directory first!");
                return;
            }

            if(DirNameTextBox.Text == "") //Checks to see if the user has input any words to find
            {
                System.Windows.MessageBox.Show("You Need to Enter a Name to search for first!");
                return;
            }

            if (DirectoryNameHolder.Count <= 0) //If the DirectoryNameHolder has nothing in it, then we search and put names in it
            {
                foreach (String s in Directories)
                {
                    DirectoryNameHolder.Add(System.IO.Path.GetFileName(s)); //Gets subdirectory names and puts them in DirectoryName Holder
                }
            }
            FoundNames.Clear(); //Clears Found Names if the user searches the same directory again.

            foreach(string ss in DirectoryNameHolder)
            {
                if(ss.Contains(DirNameTextBox.Text))
                {
                    FoundNames.Add(ss); //Adds the found names that match the text the user has entered
                }
            }

            counter2 = FoundNames.Count();
            DirCounter2.Text = Convert.ToString(counter2);
            DirNameTextBox.Clear(); //Clears the textbox

            SecondLBox.DataContext = FoundNames; 
            
        }
    }
}
