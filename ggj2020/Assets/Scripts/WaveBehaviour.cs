using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    public IEnumerator WaitAndStartAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GetComponent<Animator>().SetTrigger("Start");
    }
}
