using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityStandardAssets.Characters.FirstPerson;

public class GameOver : MonoBehaviour
{   
    public Image Background;
   
    public void GameOverEffect()
    {
        Background.DOColor(Color.black, 2.5f).OnComplete(() => 
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            FirstPersonController[] fps = FindObjectsOfType<FirstPersonController>();

            for (int i = 0; i < fps.Length; i++)
            {
                fps[i].CanMove = false;
                fps[i].m_MouseLook.SetCursorLock(false);
            }

            levelManager.GoToScene("GameOver");
        });
    }
}
