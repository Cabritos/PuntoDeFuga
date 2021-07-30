using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BananaRoomHouseToggler : MonoBehaviour
{
    [SerializeField] private GameObject[] _houseObjects = null;
    [SerializeField] private GameObject _doorway = null;
    [SerializeField] private MultiuseRoomDoor _door = null;
    [SerializeField] private GameObject _bananaRoom = null;

    private bool _unused = true;
    private GameObject _player;

    void Awake()
    {
        _player = GameManager.Player;
    }

    void OnTriggerEnter(Collider c)
    {
        if (_unused)
        {
            foreach (var thing in _houseObjects)
            {
                thing.SetActive(false);
            }

            _unused = false;

            _door.Close(false);
        }
    }

    public void HouseRestore()
    {
        StartCoroutine(Restore());
    }

    private IEnumerator Restore()
    {
        yield return new WaitForSeconds(2f);

        Destroy(_bananaRoom);
        _door.Finished();

        foreach (var thing in _houseObjects)
        {
            thing.SetActive(true);
        }

        _doorway.SetActive(true);

        FindObjectOfType<Thunders>().BananaThunder(2);

        yield return new WaitForSeconds(1f);

        FindObjectOfType<BlackScreen>().TurnBlackOff();
        
        _player.transform.position = new Vector3(9, 0.4f, 8.5f);
    }
}
