using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public UIManager manager;

    public House house;

    private void Start()
    {
        house = gameObject.GetComponent<House>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && house.entered == false) {
            house.entered = true;
            manager.OpenHouseInfo(house);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) {
            house.entered = false;
            // UIManager.CloseHouseInfo();
        }
    }
}
