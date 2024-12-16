using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    // Reference to the TextMeshPro component for displaying ammo count
    [SerializeField] TextMeshProUGUI ammoText;
    // Reference to the transform where weapons will be equipped
    [SerializeField] Transform weaponslot;

    // Current weapon being held by the player
    private Weapon currentWeapon;

    void Start()
    {
        // Get the default weapon from the weapon slot
        Weapon defaultWeapon = weaponslot.GetChild(0).GetComponent<Weapon>();

        // Retrieve the weapon data associated with the default weapon
        WeaponData defaultWeaponData = defaultWeapon.weaponData;

        // Set the current weapon to the default weapon and initialize it
        currentWeapon = defaultWeapon;
        currentWeapon.Initialize(defaultWeaponData, ammoText);
    }

    void Update()
    {
        // Call the Fire method on the current weapon
        if (Input.GetMouseButton(0) && currentWeapon != null)
            currentWeapon.Fire();
    }

    public void EquipWeapon(GameObject weaponPrefab, WeaponData weaponData)
    {
        // Destroy the current weapon instance if it exists
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        // Instantiate the new weapon prefab and set it as a child of the weapon slot
        GameObject newWeapon = Instantiate(weaponPrefab, weaponslot.transform);

        // Get the Weapon component from the newly instantiated weapon
        currentWeapon = newWeapon.GetComponent<Weapon>();

        // Initialize the new weapon if it exists
        if (currentWeapon != null)
            currentWeapon.Initialize(weaponData, ammoText);
    }
}
