using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmotionRecognition.Wiews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LibraryView : Page
    {
        public EmotionModel EmotionModel {
            get { return (EmotionModel)GetValue(EmotionModelProperty); }
            set { SetValue(EmotionModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmotionModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmotionModelProperty =
            DependencyProperty.Register("EmotionModel", typeof(EmotionModel), typeof(LibraryView), null);



        public LibraryView()
        {
            this.InitializeComponent();
        }

        private async void Button_OpenFile(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile photo = await picker.PickSingleFileAsync();
            if (photo != null)
            {
                var api = new AzureFaceApi();
                var emotion = await api.UploadFaceAndGetEmotions(photo);
                if (emotion != null)
                {
                    EmotionModel = new EmotionModel(emotion, photo.Path);
                }
            }
        }

        private async void Button_OpenCamera(object sender, RoutedEventArgs e)
        {
            var capture = new CameraCaptureUI();
            capture.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            capture.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                var api = new AzureFaceApi();
                Emotion emotion = await api.UploadFaceAndGetEmotions(photo);
                if (emotion != null)
                {
                    var emo = new EmotionModel(emotion, photo.Path);
                    EmotionModel = emo;
                    CameraView.AddEmotionModelToView(EmotionModel);
                }
            }
        }

    }
}
