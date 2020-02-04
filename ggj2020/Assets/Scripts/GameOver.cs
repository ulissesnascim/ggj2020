using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityStandardAssets.Characters.FirstPerson;

public class GameOver : MonoBehaviour
{
    private float _timeSurvived;
    private bool _hasLost = false;

    public Image Background;
   
    public void GameOverEffect()
    {
        _hasLost = true;
        PlayerPrefs.SetFloat("timeSurvived", _timeSurvived);

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

    private void Update()
    {
        if (!_hasLost)
        {
            _timeSurvived += Time.deltaTime;
        }
    }
}
