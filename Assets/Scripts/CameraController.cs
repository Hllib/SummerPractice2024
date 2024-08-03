using System;
using System.Collections.Generic;
using UnityEngine;

namespace HlStudio
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<Transform> _cameraSpots;
        [SerializeField, Range(0.1f, 10f)] private float _blendSpeed; 
        
        private Transform _camTransform => _camera.transform;
        private int _currentCameraSpot;
        private bool _isBlending;

        private void Awake()
        {
            _camTransform.position = _cameraSpots[0].position;
        }

        [ContextMenu("Blend Camera")]
        public void BlendCamera()
        {
            if (!_isBlending)
            {
                BlendCameraToNextPosition();
            }
        }
        
        private int GetNextCameraSpot()
        {
            Debug.Log("Current camera index: " + _currentCameraSpot);
            return _currentCameraSpot >= _cameraSpots.Count ? _currentCameraSpot = 0 : ++_currentCameraSpot;
        }
        
        private async void BlendCameraToNextPosition()
        {
            using (CameraPositionBlender blender = new CameraPositionBlender())
            {
                try
                {
                    _isBlending = true;
                    await blender.BlendCameraPositionAsync(
                        _camTransform,
                        _cameraSpots[_currentCameraSpot],
                        _cameraSpots[GetNextCameraSpot()],
                        _blendSpeed);
                    _isBlending = false;
                }
                catch
                {
                    
                }
            }
        }
    }
}