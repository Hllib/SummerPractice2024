using System;
using BG_Games.Scripts.Buttons;
using UnityEngine;

namespace HlStudio
{
    public class SelectPresetButton: UIButton
    {
        [SerializeField] private SessionPreset _sessionPreset;
        private int _hashCode;
        
        public Action<int> Chosen;
        public SessionPreset Preset => _sessionPreset;
        
        protected override void Awake()
        {
            base.Awake();
            _hashCode = this.GetHashCode();
            
            AssignAction(SelectPreset);
        }

        private void SelectPreset()
        {
            PlayerPrefs.SetString(PrefsKeys.SessionPreset, _sessionPreset.name);
            SetAvailability(false);
            Chosen?.Invoke(_hashCode);
        }
    }
}