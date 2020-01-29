using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace EmotionRecognition.Converters
{

    public class EmotionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var emotion = (Emotion)value;
            var sb = new StringBuilder();
            new Emotion().GetType().GetProperties().ToList().ForEach(p =>
                    sb.AppendLine($"{p.Name}: {Math.Round((double)p.GetValue(emotion), 2)}"));

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
         => throw new NotImplementedException();

    }
}
