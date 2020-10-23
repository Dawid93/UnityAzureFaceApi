using System.Collections.Generic;

namespace FacialExpression.AzureFaceApi.Models
{
    public class FaceRectangle    {
        public int top { get; set; } 
        public int left { get; set; } 
        public int width { get; set; } 
        public int height { get; set; } 
    }

    public class Emotion    {
        public double anger { get; set; } 
        public double contempt { get; set; } 
        public double disgust { get; set; } 
        public double fear { get; set; } 
        public double happiness { get; set; } 
        public double neutral { get; set; } 
        public double sadness { get; set; } 
        public double surprise { get; set; } 
    }

    public class FaceAttributes    {
        public Emotion emotion { get; set; } 
    }

    public class FaceData    {
        public FaceRectangle faceRectangle { get; set; } 
        public FaceAttributes faceAttributes { get; set; } 
    }


}