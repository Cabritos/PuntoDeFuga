using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperClue : MonoBehaviour
{
    [SerializeField] private GameObject _canvasContainer = null;

    [SerializeField] private GameObject _pieceLeft = null;
    [SerializeField] private GameObject _pieceRight = null;
    [SerializeField] private GameObject _pieceBottom = null;

    [SerializeField] private GameObject _tableContainer = null;

    [SerializeField] private GameObject _tablePieceLeft = null;
    [SerializeField] private GameObject _tablePieceRight = null;
    [SerializeField] private GameObject _tablePieceBottom = null;

    public static int Count { get; private set; } = 0;

    void Start()
    {
        _pieceLeft.SetActive(false);
        _pieceRight.SetActive(false);
        _pieceBottom.SetActive(false);

        _tableContainer.SetActive(false);

        _tablePieceLeft.SetActive(false);
        _tablePieceRight.SetActive(false);
        _tablePieceBottom.SetActive(false);
    }
    
    public IEnumerator DisplayPieces()
    {
        _canvasContainer.SetActive(true);
        yield return StartCoroutine(WaitForAction());
        _canvasContainer.SetActive(false);
    }

    public IEnumerator GetLeftPiece()
    {
        _pieceLeft.SetActive(true);
        _tableContainer.SetActive(true);
        _tablePieceLeft.SetActive(true);
        Count++;
        yield return StartCoroutine(DisplayPieces());
    }

    public IEnumerator GetRightPiece()
    {
        _pieceRight.SetActive(true);
        _tableContainer.SetActive(true);
        _tablePieceRight.SetActive(true);
        Count++;
        yield return StartCoroutine(DisplayPieces());
    }

    public IEnumerator GetBottomPiece()
    {
        _pieceBottom.SetActive(true);
        _tableContainer.SetActive(true);
        _tablePieceBottom.SetActive(true);
        Count++;
        yield return StartCoroutine(DisplayPieces());
    }


    private IEnumerator WaitForAction()
    {
        bool done = false;
        while (!done)
        {
            if (InputManager.Action() || InputManager.Cancel())
            {
                done = true;
            }
            yield return null;
        }
    }
}
