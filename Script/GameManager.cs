using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text CurrentAmmoText;

    public Weapons Weapon;
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        CurrentAmmoText.text = Movement.Ammo.ToString();
        CurrentAmmoText.text = Movement.Ammo2.ToString();

        Weapon = Weapons.Weapon1;
        Weapon1.SetActive(true);
    }

    void Update()
    {

        CurrentAmmoText.text = Movement.Ammo.ToString();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Weapon = Weapons.Weapon1;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
            Weapon3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Weapon = Weapons.Weapon2;
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
            Weapon3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Weapon = Weapons.Weapon3;
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);
            Weapon3.SetActive(true);
        }
    }
}

public enum Weapons
{
    Weapon1,
    Weapon2,
    Weapon3
}