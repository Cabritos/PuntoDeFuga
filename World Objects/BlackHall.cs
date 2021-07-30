using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHall : MonoBehaviour
{
    [SerializeField] private BlackHallPiece[] _blackHallPieces;
    
    void Awake()
    {
        if (_blackHallPieces.Length == 0) _blackHallPieces = FindObjectsOfType<BlackHallPiece>();
    }

    void Start()
    {
        foreach (var blackHallPiece in _blackHallPieces)
        {
            blackHallPiece.gameObject.SetActive(false);
        }
    }

    public void SetBlackHall()
    {
        GetComponent<AudioSource>().Play();

        foreach (var blackHallPiece in _blackHallPieces)
        {
            blackHallPiece.gameObject.SetActive(true);
        }
    }
}
