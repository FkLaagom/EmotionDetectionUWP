using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;


namespace EmotionRecognition
{
    public class EmotionModel
    {
        public StorageFile Photo { get; set; }
        public string Url { get; set; }
        public Emotion Emotion { get; set; }
        public EmotionModel(Emotion emotion, StorageFile photo)
        {
            Photo = photo;
            Emotion = emotion;
        }
        public EmotionModel(Emotion emotion, string url)
        {
            Url = url;
            Emotion = emotion;
        }

        public static async Task<List<EmotionModel>> GetSampleDataAsync()
        {
            var emotions = new List<EmotionModel>();
            var api = new AzureFaceApi();

            var emos = new List<Task<Emotion>>();
            var urls = new List<string>();
            for (int i = 1; i <= 11; i++)
            {
                string url = $"ms-appx:///Assets/FaceApiImages/{i}.jpg";
                var sourceFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(url));
                urls.Add(url);
                emos.Add(api.UploadFaceAndGetEmotions(sourceFile));
            }

            var tasks = emos.ToArray();
            var emos2 = await Task.WhenAll(tasks);
            for (int i = 0; i < emos2.Length; i++)
                if (emos2[i] != null)
                    emotions.Add(new EmotionModel(emos2[i], urls[i]));

            return emotions;
        }

        public static async Task<ObservableCollection<EmotionModel>> GetDummyDataAsync()
        {
            var emotions = new ObservableCollection<EmotionModel>();
            for (int i = 1; i <= 11; i++)
            {
                var r = new Random();
                var emotion = new Emotion(r.NextDouble(), r.NextDouble(), r.NextDouble(), r.NextDouble(), r.NextDouble(), r.NextDouble(), r.NextDouble(), r.NextDouble());
                string url = $"ms-appx:///Assets/FaceApiImages/{i}.jpg";
                if (emotion != null)
                    emotions.Add(new EmotionModel(emotion, url));
                await Task.Delay(0);
            }

            return emotions;
        }


    }
}
