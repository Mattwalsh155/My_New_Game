using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int totalWeapons = 1;
    public int currentWeaponIndex;
    private float timer = 0f;

    [SerializeField] private GameObject[] guns;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject currentGun;

    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        ResetWeapon();

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        CheckGunTimer();
    }

    public GameObject GetCurrentGun()
    {
        return currentGun;
    }

    public void PickUpWeapon(GameObject weaponPickedUp)
    {
        switch (weaponPickedUp.tag)
        {
            case Constants.shotgunTag:
                ResetWeapon();
                guns[1].SetActive(true);
                currentGun = guns[1];
                currentWeaponIndex = 1;
                timer = 0;
                break;
            case Constants.minigunTag:
                ResetWeapon();
                guns[2].SetActive(true);
                currentGun = guns[2];
                currentWeaponIndex = 2;
                timer = 0;
                break;
            case Constants.bazookaTag:
                ResetWeapon();
                guns[3].SetActive(true);
                currentGun = guns[3];
                currentWeaponIndex = 3;
                timer = 0;
                break;
            default:
                ResetWeapon();
                guns[0].SetActive(true);
                currentGun = guns[0];
                currentWeaponIndex = 0;
                break;
        }
    }

    private void ResetWeapon()
    {
        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }
    }

    private void CheckGunTimer()
    {
        if (timer >= 5)
        {
            PickUpWeapon(gameObject);
            timer = 0;
        }
    }
}
