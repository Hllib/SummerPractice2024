using System;
using System.Globalization;
using BG_Games.Scripts.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HlStudio
{
    public class SessionController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberOfEntitiesText;
        [SerializeField] private Slider _slider;
        [SerializeField] private UIButton _playButton;

        private int _currentSliderValue;
        
        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            _playButton.AssignAction(StartSession);
        }

        private void StartSession()
        {
            PlayerPrefs.SetInt("NumberOfEntities", _currentSliderValue);
            SceneManager.LoadScene("Demo");
        }
        
        private void OnSliderValueChanged(float value)
        {
            int amount = Convert.ToInt32(value);
            _currentSliderValue = amount;
            _numberOfEntitiesText.text = amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}