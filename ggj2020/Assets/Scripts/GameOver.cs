using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{

    public Text GameOverText;
    public Image Background;
    public Button PlayButton;
    
    public void GameOverEffect()
    {
        Background.DOColor(Color.black, 2.5f).OnComplete(() => 
        {
            GameOverText.enabled = true;
            PlayButton.enabled = true;
        });
    }
}
