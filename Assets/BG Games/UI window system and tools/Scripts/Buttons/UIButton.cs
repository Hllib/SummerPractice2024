using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BG_Games.Scripts.Buttons
{
    public class UIButton : Selectable, IPointerClickHandler
    {
        [field: Header("Button Settings")]
        [field: SerializeField] protected bool CanScale { get; private set; } = true;
        [field: SerializeField] protected float ScaleFactor { get; private set; } = 1.1f;

        private bool _wasTouched;
        
        protected event Action OnClick;
        
        private TMP_Text _buttonText;
        private Image _innerIcon;
        
        protected override void Awake()
        {
            base.Awake();
            
            _buttonText = GetComponentInChildren<TMP_Text>();
            _innerIcon = GetComponentInChildren<Image>();
        }
        
        public void SetAvailability(bool state)
        {
            interactable = state;
        }

        /// <summary>
        /// Sets gameobject active or inactive respectively 
        /// </summary>
        /// <param name="state">state of gameobject's activeSelf</param>
        public void SetVisibility(bool state)
        {
            gameObject.SetActive(state);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            if(!interactable) return;
            if (_wasTouched) return;

            if (CanScale)
            {
                transform.localScale /= ScaleFactor;
            }

            _wasTouched = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            
            if(!interactable) return;
            if (!_wasTouched) return;

            if (CanScale)
            {
                transform.localScale *= ScaleFactor;
            }

            _wasTouched = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!interactable) return;
            
            OnClick?.Invoke();
        }

        public void AssignAction(Action newAction)
        {
            OnClick = null;
            OnClick += newAction;
        }

        public void AssignText(string text)
        {
            if (_buttonText != null)
            {
                _buttonText.text = text;
            }
        }

        public void AssignInnerIcon(Sprite sprite)
        {
            if (_innerIcon != null)
            {
                _innerIcon.sprite = sprite;
            }
        }
    }
}