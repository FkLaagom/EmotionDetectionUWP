using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmotionRecognition
{
        public class AzureFaceApi
        {
            private readonly string _subscriptionKey = Environment.GetEnvironmentVariable("AZURE_FACE_API_KEY");
            private readonly string _faceEndpoint = Environment.GetEnvironmentVariable("AZURE_FACE_API_ENDPOINT");

            // Add your Face endpoint to your environment variables.
            private readonly IFaceClient _client;

            public AzureFaceApi()
            {
            _client = new FaceClient(
            new ApiKeyServiceClientCredentials(_subscriptionKey),
            new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = _faceEndpoint
            };
        }

            public async Task<Emotion> UploadFaceAndGetEmotions(StorageFile file)
            {
                var faceAttributesToAnalyze = new FaceAttributeType[]
                {FaceAttributeType.Emotion};

                // Call the Face API
                try
                {
                    using (var stream = await file.OpenStreamForReadAsync())
                    {
                        // The second argument specifies not return the faceId, while
                        // the third argument specifies not to return face landmarks.
                        IList<DetectedFace> faceList = await _client.Face.DetectWithStreamAsync(stream, true, false, faceAttributesToAnalyze);
                        return faceList.FirstOrDefault().FaceAttributes.Emotion; ;
                    }
                }
                // Catch and display Face API errors.
                catch (APIErrorException f)
                {
                    Debug.WriteLine(f.Message);    
                }
                // Catch and display all other errors.
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                return null;
            }
        }
}
