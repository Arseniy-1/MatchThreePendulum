using UnityEngine;
using System;

namespace Code
{
    public class InputService : IInputService
    {
        public event Action OnClick;
    
        private bool _isEnabled = true;
        private bool _ignoreUI = false;
    
        public void Enable() => _isEnabled = true;
        public void Disable() => _isEnabled = false;
    
        public void Update()
        {
            if (!_isEnabled) return;
        
            if (IsClick())
            {
                OnClick?.Invoke();
            }
        }
    
        private bool IsClick()
        {
            // Проверяем клик мышью
            if (Input.GetMouseButtonDown(0))
            {
                return _ignoreUI || !IsOverUI();
            }
        
            // Проверяем тач на мобильных
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return _ignoreUI || !IsOverUI();
            }
        
            return false;
        }
    
        private bool IsOverUI()
        {
            return UnityEngine.EventSystems.EventSystem.current != null && 
                   UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }
    }
}