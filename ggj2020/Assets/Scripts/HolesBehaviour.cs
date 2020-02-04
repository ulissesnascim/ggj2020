using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesBehaviour : MonoBehaviour
{
    [Header("Posiveis buracos a serem criados - NAO MUDAR ORDEM")]
    public List<Hole> Holes;

    [Header("Configuração do tempo dos buracos")]
    public int TimeToCreateFirstHole = 3;
    public int MinimumTimeToCreateNewHole = 2;
    public int MaximumTimeToCreateNewHole = 5;

    public List<Transform> _holePositions = new List<Transform>();
    public int[] currentHoleSizeCounts = new int[]{0,0,0};
    private int _numberActiveHoles = 0;

    public static HolesBehaviour instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (Transform child in transform)
        {
            _holePositions.Add(child);           
        }

        StartCoroutine(CreateHoleRoutine(TimeToCreateFirstHole));

    } 

    private IEnumerator CreateHoleRoutine(float time)
    {
        yield return new WaitForSeconds(time);

        int p = Random.Range(0, _holePositions.Count);
        int h = Random.Range(0, Holes.Count);
           
        if (_holePositions[p].childCount == 0)
        {
            CreateHole(h, _holePositions[p]);
        }
        else
        {
            StartCoroutine(CreateHoleRoutine(0f));
            yield break;
        }

        if (_numberActiveHoles < _holePositions.Count)
        {
            int t = Random.Range(MinimumTimeToCreateNewHole, MaximumTimeToCreateNewHole);
            StartCoroutine(CreateHoleRoutine(t));
        }
    }

    private void CreateHole(int holeIndex, Transform holePosition)
    {
        GameObject holeObject = Instantiate(Holes[holeIndex].gameObject, holePosition);

        AudioSource audioSource = holeObject.GetComponentInParent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

        Hole holeSize = holeObject.GetComponent<Hole>();
        
        _numberActiveHoles++;
        currentHoleSizeCounts[holeIndex] += 1;
    }

    public void ReplaceHole(Hole holeToReplace, int holeSizeIndex)
    {
        Transform holePosition = holeToReplace.transform.parent;

        Destroy(holeToReplace.gameObject);

        CreateHole(holeSizeIndex - 1, holePosition);

    }

    public void HoleDestroyed(int holeSizeIndex)
    {
        if (_numberActiveHoles > 0)
        {
            _numberActiveHoles--;
        }

        if (currentHoleSizeCounts[holeSizeIndex] > 0)
        {
            currentHoleSizeCounts[holeSizeIndex]--;
        }

    }

}
