using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BurstWeapon : Weapon
{

    void Start()
    {
        base.SpawnPooledBullets();
    }

    //Burst Mode Shooting
    public override async void Shoot()
    {
        if (isShooting)
            return;

        isShooting = true;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            Bullet bullet = GetPooledBullet();
            bullet.gameObject.SetActive(true);
            bullet.Project(transform.up);
            await Task.Delay(ShotAsyncDelay);
        }

        isShooting = false;
    }
}
