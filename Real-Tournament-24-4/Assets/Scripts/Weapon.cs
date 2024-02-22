using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int ammo;
    public int maxAmmo = 10;
    public int invAmmo;

    public int pelletcount;
    public float pelletSpread;
    public float pelletVScale;

    public bool isReloading;
    public bool isAutomatic;

    public bool isShotgun;

    public float fireInterval = 0.1f;
    public float fireCooldown;
    public float reloadTime = 2;

    void Update()
    {
        // manual shooting
        if (!isAutomatic && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        // automatic shooting
        if (isAutomatic && Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        fireCooldown -= Time.deltaTime;
    }

     void Shoot()
    {
        var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (isReloading) return;
        if (ammo <= 0)
        {
            Reload();
            return;
        }
        if(fireCooldown > 0) return;

        ammo--;
        fireCooldown = fireInterval;
        if (isShotgun)
        {
            for (int i = 0; i <= pelletcount; i++)
            {
                pos.x += Random.Range(-pelletSpread,pelletSpread) * pelletVScale;
                pos.y += Random.Range(-pelletSpread,pelletSpread) / pelletVScale;
                if (pos.y<0.2)
                {
                    pos.y += 0.2f;
                }
                
                
                Instantiate(bulletPrefab, pos,transform.rotation);
            }
        }
        else
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    async void Reload()
    {

        if (ammo == maxAmmo) return;
        if(isReloading) return;
        if (invAmmo <= 0) return;
        isReloading = true;

        print ("Reloading...");
        await new WaitForSeconds(reloadTime);
        print("Reloaded!");

        isReloading = false;
        invAmmo -= maxAmmo - ammo;
        ammo = maxAmmo;
    }
}