using System;
using System.Collections.Generic;
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
            _playButton.AssignAction(StartSession);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            
            _currentSliderValue = (int)_slider.value;
        }
        
        private void StartSession()
        {
            PlayerPrefs.SetInt(PrefsKeys.NumberOfEntities, _currentSliderValue);
            SceneManager.LoadScene("Demo");
        }

        private void OnSliderValueChanged(float value)
        {
            int amount = Convert.ToInt32(value);
            _currentSliderValue = amount;

            _numberOfEntitiesText.text = amount == 1000
                ? $"Number of entities: {amount.ToString(CultureInfo.InvariantCulture)} <br> (recommended)"
                : $"Number of entities: {amount.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}