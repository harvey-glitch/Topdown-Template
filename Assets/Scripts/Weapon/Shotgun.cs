using UnityEngine;

public class Shotgun : Weapon
{
    // Transform where the projectile are shot from
    [SerializeField] private Transform firePoint;
    // Reference to the muzzle flash particle
    [SerializeField] ParticleSystem muzzleFlash;

    public override void Fire()
    {
        if (!CanFire()) return;

        // Fire multiple pellets with some spread
        for (int i = 0; i < weaponData.pellets; i++)
        {
            // Random spread angle for each pellet
            float spread = Random.Range(-weaponData.spreadAngle, weaponData.spreadAngle);
            Quaternion spreadRotation = Quaternion.Euler(0, spread, 0);

            // Retrieve the projectile from the pool
            GameObject projectile = ObjectPool.instance.GetObject("Projectile");

            // Set each projectile position and rotation
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation * spreadRotation;
        }

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
