﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Squirrel;

namespace UpdateTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UpdateManager manager;
        
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            manager = await UpdateManager
                .GitHubUpdateManager(@"https://github.com/Fangmaci/UpdateTestApp");
            CurrentlyInstalledVersion.Text = manager.CurrentlyInstalledVersion().ToString();
        }

        private async void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            var updateInfo = await manager.CheckForUpdate();

            if(updateInfo.ReleasesToApply.Count >0) 
            {
                CheckForUpdatesButton.IsEnabled = true;
            }
            else
            {
                CheckForUpdatesButton.IsEnabled= false;
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await manager.UpdateApp();

            MessageBox.Show("Updated successfully");
        }
    }
}