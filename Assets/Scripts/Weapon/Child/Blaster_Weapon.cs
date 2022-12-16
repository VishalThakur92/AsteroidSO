using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Blaster_Weapon : Weapon
{

    void Start()
    {
        base.SpawnPooledBullets();
    }

    public override async void Shoot()
    {
        if (isShooting)
            return;

        isShooting = true;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            Bullet bullet = base.GetPooledBullet();
            bullet.gameObject.SetActive(true);
            bullet.Project(transform.up);
            await Task.Delay(burstAsyncDelay);
        }

        isShooting = false;
    }
}
