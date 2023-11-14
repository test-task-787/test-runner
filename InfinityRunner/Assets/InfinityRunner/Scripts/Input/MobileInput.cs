using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InfinityRunner.Scripts.Input
{
    /// <summary>
    /// Implementation of input for mobile control.
    /// Works throug Image overlay, that handle touches instead of UnityEngine.Input.GetPoint().
    /// </summary>
    public class MobileInput : InputModule
    {
        private static Image _inputHandler = null;
        
        public MobileInput()
        {
            InputFactoryMethod();
            ProcessInput(_inputHandler);
        }
        
        /// <summary>
        /// Implements singleton Input trigger. Create and configure canvas
        /// </summary>
        private void InputFactoryMethod()
        {
            if (_inputHandler != null) return;
            var handler = new GameObject("InputTouches");
            GameObject.DontDestroyOnLoad(handler);

            var canvas = handler.AddComponent<Canvas>();
            handler.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            var imageGo = new GameObject();
            var image = imageGo.AddComponent<Image>();
            image.transform.SetParent(handler.transform);
            image.color = new Color(0f, 0f, 0f, 0f);
            
            var canvasRectTrasnsform = handler.GetComponent<RectTransform>();
            var rectTransform = imageGo.GetComponent<RectTransform>();
            
            canvasRectTrasnsform.anchorMin = Vector2.zero;
            canvasRectTrasnsform.anchorMax = Vector2.one;
            canvasRectTrasnsform.sizeDelta = Vector2.zero;
            canvasRectTrasnsform.anchoredPosition = Vector2.zero;
            
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;

            _inputHandler = image;
        }


        private void ProcessInput(Image image)
        {
            PointerEventData _lastPointerData = null;
            
            bool isClicked = false;
            image.OnPointerDownAsObservable().Subscribe(x =>
            {
                _lastPointerData = x;
                _isJumpClick = true;
                isClicked = true;
            });

                
            image.OnPointerUpAsObservable().Subscribe(x =>
            {
                _lastPointerData = null;
                _isJumpClick = false;
                _isJumpPressed = false;
                _down = false;
                _up  = false;
                isClicked = false;
            });

            Observable.EveryLateUpdate().Where(_ => isClicked).Subscribe(_ =>
            {
                _isJumpClick = false;
                // Debug.Log(_lastPointerData.position);
                // Debug.Log(_lastPointerData.pointerId);
                _isJumpPressed = true;
                _down = _lastPointerData.position.y < Screen.height * 0.5f;
                _up = _lastPointerData.position.y > Screen.height * 0.5f;
            });
        }
        
        private bool _isJumpClick = false;
        private bool _isJumpPressed = false;
        private bool _down = false;
        private bool _up = false;
        
        public override bool IsJumpClick => _isJumpClick;
        public override bool IsJumpPressed => _isJumpPressed;
        public override bool Down => _down;
        public override bool Up => _up;
    }
}