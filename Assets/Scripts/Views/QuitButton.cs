using BG_Games.Scripts.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HlStudio
{
    public class QuitButton : UIButton
    {
        [SerializeField] private bool _quitApp;

        protected override void Awake()
        {
            base.Awake();
            AssignAction(Quit);
        }

        private void Quit()
        {
            if (_quitApp)
                Application.Quit();
            else
                SceneManager.LoadScene("SessionMenu");
        }
    }
}