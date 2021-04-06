using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ByeMouse
{
    public class ByeMouseController : MonoBehaviour
    {
        public static ByeMouseController Instance { get; private set; }

        public float HideAfterSeconds = 3f;
        public float ThresholdInPixels = 3f;

        private float _lastTime;
        private Vector3 _lastMousePos;

        private void Awake()
        {
            if (Instance != null)
            {
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this);
            Instance = this;
        }

        private void Start()
        {
            _lastTime = Time.timeSinceLevelLoad;
            _lastMousePos = Input.mousePosition;
        }

        private void Update()
        {
            var dx = Input.mousePosition - _lastMousePos;
            var move = (dx.sqrMagnitude > (ThresholdInPixels * ThresholdInPixels));
            _lastMousePos = Input.mousePosition;

            if (move)
                _lastTime = Time.timeSinceLevelLoad;

            Cursor.visible = (Time.timeSinceLevelLoad - _lastTime) < HideAfterSeconds;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}
