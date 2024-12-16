using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Weapon : MonoBehaviour
{
    // Holds the weapon's data
    [SerializeField] public WeaponData weaponData;
    // UI text to display ammo count
    [SerializeField] protected TextMeshProUGUI ammoText;
    // Tracks the current ammo count
    [HideInInspector] protected int currentAmmo;

    // Tracks the next time the weapon can fire
    protected float nextFireTime;

    public void Initialize(WeaponData data, TextMeshProUGUI ammoUI)
    {
        // Initialize data for weapon and UI for ammo display
        weaponData = data;
        ammoText = ammoUI;

        // Initialize the current ammo and update the ammo display UI
        currentAmmo = weaponData.maxAmmo;
        UpdateAmmoUI();
    }

    // Abstract method for firing projectiles; to be implemented by derived classes
    public abstract void Fire();

    protected bool CanFire()
    {
        // Determine if the weapon can fire: must be time for the next shot and have ammo
        return Time.time >= nextFireTime && currentAmmo > 0;
    }

    protected void UpdateAmmoUI()
    {
        // Update the ammo text in the UI to reflect the current ammo count
        ammoText.text = currentAmmo.ToString();
    }

    protected IEnumerator ReloadWeapon()
    {
        // Track the time elapsed during the reload process
        float timeElapsed = 0f;

        while (timeElapsed <= weaponData.reloadSpeed)
        {
            ammoText.text = "...";
            timeElapsed += Time.deltaTime;

            // Wait for the next frame before continuing the loop
            yield return null;
        }

        // Reset the current ammo
        currentAmmo = weaponData.maxAmmo;

        // Update the ammo display in the UI
        UpdateAmmoUI();
    }
}
