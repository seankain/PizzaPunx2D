using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Animation an;

    private void Start()
    {
        an = GetComponent<Animation>();
    }

    public void PlayAnim()
    {
        an.Play();
    }
}
