using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG_Games.Scripts.Buttons;
using UnityEngine;

namespace HlStudio
{
    public class CameraController : MonoBehaviour, IInitializable
    {
        public bool Initialized { get; set; }
        
        [SerializeField] private Camera _camera;
        [SerializeField] private List<Transform> _cameraSpots;
        [SerializeField, Range(0.1f, 10f)] private float _blendSpeed;

        [SerializeField] private UIButton _nextSpotButton;
        [SerializeField] private UIButton _previousSpotButton;

        private Transform _camTransform => _camera.transform;
        private int _currentCameraSpot;
        private bool _isBlending;

        private void BlendToNextCamera()
        {
            if (!_isBlending)
            {
                BlendCameraToPosition(GetNextCameraSpot);
            }
        }

        private void BlendToPreviousCamera()
        {
            if (!_isBlending)
            {
                BlendCameraToPosition(GetPreviousCameraSpot);
            }
        }

        private int GetNextCameraSpot()
        {
            return _currentCameraSpot >= _cameraSpots.Count - 1 ? _currentCameraSpot = 0 : ++_currentCameraSpot;
        }

        private int GetPreviousCameraSpot()
        {
            return _currentCameraSpot <= 0 ? (_currentCameraSpot = _cameraSpots.Count - 1) : --_currentCameraSpot;
        }

        private async void BlendCameraToPosition(Func<int> getCameraSpot)
        {
            using CameraPositionBlender blender = new CameraPositionBlender();

            _isBlending = true;

            await blender.BlendCameraPositionAsync(_camTransform, _cameraSpots[_currentCameraSpot],
                _cameraSpots[getCameraSpot.Invoke()], _blendSpeed);

            _isBlending = false;
        }

        public Task Init()
        {
            _camTransform.position = _cameraSpots[0].position;
            _camTransform.rotation = _cameraSpots[0].rotation;

            _nextSpotButton.AssignAction(BlendToNextCamera);
            _previousSpotButton.AssignAction(BlendToPreviousCamera);
            
            return Task.CompletedTask;
        }
    }
}