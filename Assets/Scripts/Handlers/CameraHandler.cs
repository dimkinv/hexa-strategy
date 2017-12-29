using System;
using UnityEngine;

namespace Handlers
{
    public class CameraHandler : MonoBehaviour
    {
        private Vector3 _oldCameraPosition;
        public event EventHandler<CameraMovedEventArgs> OnCameraMoved;

        private void Update()
        {
            if (this._oldCameraPosition == Camera.main.transform.position ||
                this.OnCameraMoved == null) return;
            
            this.OnCameraMoved(this,
                new CameraMovedEventArgs(this._oldCameraPosition, Camera.main.transform.position));
            this._oldCameraPosition = Camera.main.transform.position;
        }
    }

    public class CameraMovedEventArgs : EventArgs
    {
        public Vector3 OldPosition;
        public Vector3 NewPosition;

        public CameraMovedEventArgs(Vector3 oldPosition, Vector3 newPosition)
        {
            this.OldPosition = oldPosition;
            this.NewPosition = newPosition;
        }
    }
}