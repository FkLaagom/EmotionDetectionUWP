using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmotionRecognition.Wiews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CameraView : Page
    {
        private bool _hasLoadedSampleData = false;
        private static ObservableCollection<EmotionModel> _emotions = new ObservableCollection<EmotionModel>();
        public ObservableCollection<EmotionModel> Emotions { get => _emotions; set => _emotions = value; }

        public CameraView()
        {
            this.InitializeComponent();
            this.DataContext = Emotions;
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            if (!_hasLoadedSampleData)
            {
                var emotions = await EmotionModel.GetSampleDataAsync();
                foreach (var item in emotions)
                    Emotions.Add(item);
                _hasLoadedSampleData = true;
            }
        }
        public static void AddEmotionModelToView(EmotionModel emotionModel) => _emotions.Add(emotionModel);
    }
}
