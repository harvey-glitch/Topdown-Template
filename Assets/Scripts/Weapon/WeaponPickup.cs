using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // Prefab of the weapon to be picked up
    [SerializeField] GameObject weaponPrefab;
    // Data associated with the weapon
    [SerializeField] WeaponData weaponData;

    void OnTriggerEnter(Collider other)
    {
        WeaponManager weaponManager = other.GetComponent<WeaponManager>();

        // Check if the WeaponManager component exists on the GameObject we collide with
        if (weaponManager != null)
        {
            // Equip the weapon and assign the weapon data
            weaponManager.EquipWeapon(weaponPrefab, weaponData);

            // Optional: Destroy the pickup object after it has been used
            //Destroy(gameObject);
        }
    }
}