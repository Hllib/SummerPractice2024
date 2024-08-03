using System;
using System.Threading.Tasks;
using UnityEngine;

namespace HlStudio
{
    public class CameraPositionBlender: IDisposable
    {
        private bool _disposed = false;
        
        public async Task BlendCameraPositionAsync(Transform target, Transform startTransform, Transform endTransform, float duration)
        {
            if (startTransform == null || endTransform == null)
            {
                throw new ArgumentNullException("Transform parameters cannot be null.");
            }

            Vector3 startPos = startTransform.position;
            Vector3 endPos = endTransform.position;
            Quaternion startRot = startTransform.rotation;
            Quaternion endRot = endTransform.rotation;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                // Interpolate position and rotation
                target.position = Vector3.Lerp(startPos, endPos, t);
                target.rotation = Quaternion.Lerp(startRot, endRot, t);

                elapsedTime += Time.deltaTime;
                await Task.Yield(); // Yield control back to the main thread
            }

            // Ensure the final position and rotation are set
            target.position = endPos;
            target.rotation = endRot;
        }
        
        public void Dispose()
        {
            Disposes(true);
            GC.SuppressFinalize(this);
        }

        private void Disposes(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here if needed
                }

                // Dispose unmanaged resources here if needed

                _disposed = true;
            }
        }

        ~CameraPositionBlender()
        {
            Disposes(false);
        }
    }
}