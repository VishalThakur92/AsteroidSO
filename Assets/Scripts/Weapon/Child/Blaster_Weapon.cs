using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Blaster_Weapon : Weapon
{

    void Start()
    {
        base.SpawnPooledBullets();

        Shoot();
    }

    //Blaster mode Shooting
    public override async void Shoot()
    {
        if (isShooting)
            return;

        isShooting = true;

        while (true)
        {
            Bullet bullet = base.GetPooledBullet();
            if (bullet)
            {
                bullet.gameObject.SetActive(true);
                bullet.Project(transform.up);
            }
            await Task.Delay(ShotAsyncDelay);
        }
    }
}
