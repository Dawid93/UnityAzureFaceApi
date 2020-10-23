using FacialExpression.AzureFaceApi.Models;
using TMPro;
using UnityEngine;

namespace FacialExpression.UI
{
    public class EmotionDetail : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI anger;
        [SerializeField] private TextMeshProUGUI contempt;
        [SerializeField] private TextMeshProUGUI disgust;
        [SerializeField] private TextMeshProUGUI fear;
        [SerializeField] private TextMeshProUGUI happiness;
        [SerializeField] private TextMeshProUGUI neutral;
        [SerializeField] private TextMeshProUGUI sadness;
        [SerializeField] private TextMeshProUGUI surprise;
        
        public void UpdateEmotions(Emotion emotion)
        {
            if(emotion == null)
                emotion = new Emotion();
            
            anger.SetText($"Anger: {emotion.anger}");
            contempt.SetText($"Contempt: {emotion.contempt}");
            disgust.SetText($"Disgust: {emotion.disgust}");
            fear.SetText($"Fear: {emotion.fear}");
            happiness.SetText($"Happiness: {emotion.happiness}");
            neutral.SetText($"Neutral: {emotion.neutral}");
            sadness.SetText($"Sadness: {emotion.sadness}");
            surprise.SetText($"Surprise: {emotion.surprise}");
        }
    }
}
