using System;
using TMPro;
using UnityEngine;

namespace HlStudio
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private  TMP_Text _fpsText;
        [SerializeField] private TMP_Text _numberOfEntities;
        
        private float _deltaTime = 0.0f;

        private void Start()
        {
            _numberOfEntities.text = $"Entities: {PlayerPrefs.GetInt("NumberOfEntities")}";
        }

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            _fpsText.text = $"{fps:0.} FPS";
        }
    }
}