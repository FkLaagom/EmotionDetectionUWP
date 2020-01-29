using EmotionRecognition.Wiews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmotionRecognition
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (Navigation?.SelectedItem is NavigationViewItem item)
                switch (item.Tag)
                {
                    case "HomeView": ContentFrame.Navigate(typeof(HomeView)); break;
                    case "CameraView": ContentFrame.Navigate(typeof(LibraryView));break;
                    case "LibraryView": ContentFrame.Navigate(typeof(CameraView)); break;
                }
        }

        private void Navigation_Loaded(object sender, RoutedEventArgs e)
         => ContentFrame.Navigate(typeof(HomeView));
    }
}
