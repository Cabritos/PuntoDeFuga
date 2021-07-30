using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlinkinText : MonoBehaviour
{
    [SerializeField] private GameObject _target = null;
    [SerializeField] private float _blinkRate = 0;

    void Start()
    {
        StartCoroutine(Blinking());
        Invoke("NextScene", 5f);
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            _target.gameObject.SetActive(true);
            yield return new WaitForSeconds(_blinkRate);
            _target.gameObject.SetActive(false);
            yield return new WaitForSeconds(_blinkRate/2);
        }
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
