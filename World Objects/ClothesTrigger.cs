using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _clothesPrefab = null;
    [SerializeField] private GameObject _bedroomObjects = null;

    [SerializeField] private Door[] _bedroomDoors = null;
    [SerializeField] private Drawer _bedroomDrawer = null;
    
    void OnTriggerEnter(Collider c)
    {
        if (!_clothesPrefab)
        {
            Debug.LogError("Clothes missing in Clothes Trigger");
            return;
        }
        else
        {
            var pos = new Vector3(0, 0, 0);
            Instantiate(_clothesPrefab, pos, _clothesPrefab.transform.rotation, _bedroomObjects.transform);

            foreach (var door in _bedroomDoors)
            {
                door.Close(false);
            }

            _bedroomDrawer.Close(false);

            GameManager.BedroomReset = true;

            Destroy(this);
        }
    }
}
