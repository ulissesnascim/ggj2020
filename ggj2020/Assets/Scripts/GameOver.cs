using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{   
    public Image Background;
   
    public void GameOverEffect()
    {
        Background.DOColor(Color.black, 2.5f).OnComplete(() => 
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            levelManager.GoToScene("GameOver");
        });
    }
}
