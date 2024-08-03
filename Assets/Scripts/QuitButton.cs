using BG_Games.Scripts.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HlStudio
{
    public class QuitButton : UIButton
    {
        protected override void Awake()
        {
            base.Awake();
            AssignAction(QuitToMenu);
        }

        private void QuitToMenu()
        {
            SceneManager.LoadScene("SessionMenu");
        }
    }
}