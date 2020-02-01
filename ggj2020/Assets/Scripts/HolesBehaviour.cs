using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesBehaviour : MonoBehaviour
{
    [Header("Posiveis buracos a serem criados")]
    public List<Hole> Holes;

    [Header("Configuração do tempo dos buracos")]
    public int TimeToCreateFirstHole = 3;
    public int MinimumTimeToCreateNewHole = 2;
    public int MaximumTimeToCreateNewHole = 5;

    private List<Transform> _holePositions = new List<Transform>();
    private int _numberActiveHoles = 0;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _holePositions.Add(child);           
        }

        StartCoroutine(CreateHole(TimeToCreateFirstHole));
    }  

    private IEnumerator CreateHole(float time)
    {
        yield return new WaitForSeconds(time);

        int p = Random.Range(0, _holePositions.Count);
        int h = Random.Range(0, Holes.Count);
           
        if (_holePositions[p].childCount == 0)
        {
            GameObject hole = Instantiate(Holes[h].gameObject, _holePositions[p]);
            _numberActiveHoles++;
        }
        else
        {
            StartCoroutine(CreateHole(0f));
            yield break;
        }

        if (_numberActiveHoles < _holePositions.Count)
        {
            int t = Random.Range(MinimumTimeToCreateNewHole, MaximumTimeToCreateNewHole);
            StartCoroutine(CreateHole(t));
        }
    }
}
