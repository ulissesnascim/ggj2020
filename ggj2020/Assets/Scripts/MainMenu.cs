using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform playButton = null;
    private float randomMoveSpeedPlay;

    [SerializeField] private RectTransform quitButton = null;
    private float randomMoveSpeedQuit;

    [SerializeField] private float minMoveSpeed = 0;
    [SerializeField] private float maxMoveSpeed = 0;

    private void Start()
    {
        UpdatePlaySpeed();
        UpdateQuitSpeed();
    }

    public void FixedUpdate()
    {
        MoveRight(playButton, randomMoveSpeedPlay);
        MoveRight(quitButton, randomMoveSpeedQuit);
    
    }

    private void MoveRight(RectTransform buttonRect, float moveSpeed)
    {
        Vector3 initialPosition = buttonRect.position;
        buttonRect.position = new Vector3(initialPosition.x + moveSpeed / 100f, initialPosition.y, initialPosition.z);

    }

    private void UpdatePlaySpeed()
    {
        randomMoveSpeedPlay = Random.Range(minMoveSpeed, maxMoveSpeed);

    }
    
    private void UpdateQuitSpeed()
    {
        randomMoveSpeedQuit = Random.Range(minMoveSpeed, maxMoveSpeed);

    }


    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);

    }

    public void Quit()
    {
        Application.Quit();
        Debug.LogWarning("Application quit");

    }
}
