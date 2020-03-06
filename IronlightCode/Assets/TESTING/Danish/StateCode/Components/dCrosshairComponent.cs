using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Danish.Components
{
    public class dCrosshairComponent
    {
        GameObject canvas = null;
        Transform camera = null;
        Transform muzzle = null;
        RectTransform rectTransform = null;
        Ray ray;

        public void Init(GameObject _panel, Transform _cameraHolder, Transform _muzzleRef)
        {
            canvas = _panel;

            rectTransform = canvas.GetComponent<RectTransform>();

            camera = _cameraHolder;

            muzzle = _muzzleRef;
        }

        public void Tick()
        {

        }

        public Vector3 GetFiringDirection()
        {
            ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

            Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);

            return ray.direction;
        }
    }
}