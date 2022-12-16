using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NormalWeapon : Weapon
{

    void Start()
    {
        //Instantiate and Cache bullet
        //All bulletrs instantitated will be added to the Bullets Pool
        for (int i = 0; i < bulletPoolAmount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletsPoolParent, false);
            bullet.gameObject.SetActive(false);
            pooledBullets.Add(bullet);
        }
    }

    public override Bullet GetPooledBullet()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].gameObject.activeInHierarchy)
            {
                pooledBullets[i].transform.localPosition = Vector3.zero;
                return pooledBullets[i];
            }
        }
        return null;
    }

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
            await Task.Delay(burstAsyncDelay);
        }

        isShooting = false;
    }
}
