using BG_Games.Scripts.Base;
using BG_Games.Scripts.Base.Enums;
using UnityEngine;

namespace BG_Games.Scripts.Buttons
{
    public class ShowWindowButton : UIButton
    {
        [Header("Show window button")]
        [SerializeField] private UIWindowType _windowType;

        private void Start()
        {
            AssignAction(ShowWindow);
        }

        private async void ShowWindow()
        {
            var window = OverlayUI.Instance.LoadWindow(_windowType);
            await window.Show(SiblingType.Last);
        }
    }
}