using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem muzzleFlash;

    public override void Fire()
    {
        if (!CanFire()) return;

        // Get the projectile from the pool
        GameObject projectile = ObjectPool.instance.GetObject("Projectile");

        // Set the position and rotation
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        // Play muzzle flash
        muzzleFlash.Play();

        // Reduce ammo and update UI
        currentAmmo--;
        UpdateAmmoUI();

        // Set the next fire time
        nextFireTime = Time.time + 1f / weaponData.fireRate;

        // Handle reloading
        if (currentAmmo <= 0)
        {
            StartCoroutine(ReloadWeapon());
        }
    }
}
