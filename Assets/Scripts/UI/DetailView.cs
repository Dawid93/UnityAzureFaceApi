using System;
using FacialExpression.AzureFaceApi;
using FacialExpression.AzureFaceApi.Models;
using FacialExpression.Helpers;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public class DetailView : MonoBehaviour
    {
        private event Action<bool> OnDataAvailableChange; 
        private bool IsFaceDataAvailable
        {
            get => _isFaceDataAvailable;
            set
            {
                _isFaceDataAvailable = value;
                OnDataAvailableChange?.Invoke(_isFaceDataAvailable);
            }
        }

        [SerializeField] private Button infoButton;
        [SerializeField] private ControlPanel controlPanel;
        [SerializeField] private EmotionDetail emotionDetail;
        [SerializeField] private RawImage image;
        [SerializeField] private ImageAnalyzer analyzer;
        [SerializeField] private RawImageSettings rawImageSettings;
        [SerializeField] private AspectRatioFitter aspectRatioFitter;
        [SerializeField] private DancerController dancer;
        
        private Emotion _emotion;
        
        private bool _isFaceDataAvailable;
        private bool _isInfoVisible;
        private Image _infoButtonImage;
        
        public void ShowView(string filePath, Texture2D texture2D)
        {
            OnDataAvailableChange += OnOnDataAvailableChange;
            infoButton.interactable = false;
            
            analyzer.MakeAnalysisRequest(texture2D.EncodeToPNG(), OnResponse);
            
            controlPanel.gameObject.SetActive(false);
            
            _isInfoVisible = false;
            image.texture = texture2D;
            emotionDetail.gameObject.SetActive(_isInfoVisible);
            
            aspectRatioFitter.aspectRatio = rawImageSettings.AspectRatio;
            image.rectTransform.localScale = new Vector3(1f, rawImageSettings.ScaleY, 1f);
            image.rectTransform.localEulerAngles = new Vector3(0, 0, rawImageSettings.Orientation);
            ChangeInfoButtonColor(Color.white);
        }

        private void OnOnDataAvailableChange(bool available)
        {
            infoButton.interactable = available;
            if (available && _emotion.happiness > .5)
            {
                dancer.gameObject.SetActive(true);
                dancer.StartDance();
            }
        }

        public void CloseView()
        {
            OnDataAvailableChange -= OnOnDataAvailableChange;
            controlPanel.gameObject.SetActive(true);
            dancer.StopDance();
            dancer.gameObject.SetActive(false);
            image.texture = rawImageSettings.DefaultTexture;
            gameObject.SetActive(false);
        }

        public void ShowInfo()
        {
            _isInfoVisible = !_isInfoVisible;
            emotionDetail.UpdateEmotions(_emotion);
            emotionDetail.gameObject.SetActive(_isInfoVisible);
        }

        private void OnResponse(Response response)
        {
            if (!string.IsNullOrEmpty(response.Error))
            {
                _emotion = null;
                IsFaceDataAvailable = false;
                ChangeInfoButtonColor(Color.red);
                return;
            }
            
            var faceDatas = JsonConvert.DeserializeObject<FaceData[]>(response.Data);
            if (faceDatas != null && faceDatas.Length > 0)
            {
                _emotion = faceDatas[0].faceAttributes.emotion;
                ChangeInfoButtonColor(Color.green);
                IsFaceDataAvailable = true;
            }
            else
            {
                _emotion = null;
                ChangeInfoButtonColor(Color.red);
                IsFaceDataAvailable = false;
            }
        }

        private void ChangeInfoButtonColor(Color color)
        {
            if (_infoButtonImage == null)
                _infoButtonImage = infoButton.GetComponent<Image>();

            _infoButtonImage.color = color;
        }
    }
}