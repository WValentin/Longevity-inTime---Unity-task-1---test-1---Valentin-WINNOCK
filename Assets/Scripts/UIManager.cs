using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject menuPanel;
    public GameObject menu;
    public GameObject avatarMenu;
    public GameObject housePanel;
    public Player player;

    private House _house;

    void Update()
    {
        GameUI.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Money: " + player.money;
    }

    public void OpenHouseInfo(House house)
    {
        GameUI.SetActive(false);
        _house = house;

        // Set the UI with the house's informations
        housePanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = house.houseName;
        housePanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = house.description;
        housePanel.transform.GetChild(5).gameObject.GetComponent<Text>().text = house.price.ToString();

        if (house.owned) {
            housePanel.GetComponent<Image>().color = new Color(111, 111, 111);
            housePanel.transform.GetChild(4).gameObject.SetActive(true);
        } else {
            if (player.money < house.price)
                housePanel.transform.GetChild(3).gameObject.SetActive(true);
            else
                housePanel.transform.GetChild(2).gameObject.SetActive(true);
        }
        housePanel.SetActive(true);
    }

    public void CloseHouseInfo()
    {
        housePanel.transform.GetChild(2).gameObject.SetActive(false);
        housePanel.transform.GetChild(3).gameObject.SetActive(false);
        housePanel.transform.GetChild(4).gameObject.SetActive(false);
        housePanel.GetComponent<Image>().color = new Color(0, 144, 255);
        housePanel.SetActive(false);
        GameUI.SetActive(true);
    }

    public void BuyHouse()
    {
        _house.owned = true;
        player.money -= _house.price;
        CloseHouseInfo();
    }

    public void SellHouse()
    {
        _house.owned = false;
        player.money += _house.price;
        CloseHouseInfo();
    }

    public void OpenMenu()
    {
        avatarMenu.SetActive(false);
        menu.SetActive(true);
        menuPanel.SetActive(true);
        GameUI.SetActive(false);
    }

    public void CloseMenu()
    {
        avatarMenu.SetActive(false);
        menu.SetActive(false);
        menuPanel.SetActive(false);
        GameUI.SetActive(true);
    }

    public void ReturnToMenu()
    {
        avatarMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void OpenAvatarMenu()
    {
        avatarMenu.SetActive(true);
        menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
