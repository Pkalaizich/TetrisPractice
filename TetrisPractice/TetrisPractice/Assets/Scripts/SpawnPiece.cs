using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPiece : MonoBehaviour
{
    public GameObject[] Pieces;
    void Start()
    {
        newPiece();   
    }

    // Update is called once per frame
    public void newPiece()
    {
        Instantiate(Pieces[Random.Range(0, Pieces.Length)], transform.position, Quaternion.identity);
    }
}
