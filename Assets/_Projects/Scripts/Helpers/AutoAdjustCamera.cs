using UnityEngine;

namespace Helpers
{
    public class AutoAdjustCamera : MonoBehaviour
    {
        [SerializeField] Camera _gameCamera;
        public static AutoAdjustCamera sharedInstance;

        public float sizeInMeters { get; private set; }
        public float gameWidth { get; private set; }
        public float gameHeight { get; private set; }

        public void Awake()
        {
            // DontDestroyOnLoad(gameObject);
            sharedInstance = this;
        }

        void Start()
        {
            sizeInMeters = 11;
            float ratio = (float)Screen.height / Screen.width;
            float orthoSize = 0;
            if (ratio < 1.6f)//ipad
            {
                sizeInMeters = 15f;
            }
            else if (ratio < 1.8f)
            {
                sizeInMeters = 12f;
            }
            else
            {
                sizeInMeters = 11f;
            }
            orthoSize = sizeInMeters * Screen.height / Screen.width * 0.5f;
            _gameCamera.orthographicSize = orthoSize;

            gameHeight = orthoSize * 2;
            gameWidth = gameHeight * Screen.width / Screen.height;
        }
    }
}