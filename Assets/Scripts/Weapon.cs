using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Transform bulletsPoolParent;

    [SerializeField]
    List<Bullet> pooledBullets = new List<Bullet>();

    [SerializeField]
    int bulletPoolAmount = 20;

    //the weapon shall fire x nuumber of times bullet per Shoot()
    //To Shoot in burst mode set the value to be greater than 1
    [SerializeField]
    int bulletsPerShot = 1;

    bool isShooting = false;

    private void Start()
    {

        //Instantiate and Cache bullet
        //All bulletrs instantitated will be added to the Bullets Pool
        for (int i = 0; i < bulletPoolAmount; i++) {
            Bullet bullet = Instantiate(bulletPrefab , bulletsPoolParent , false);
            bullet.gameObject.SetActive(false);
            pooledBullets.Add(bullet);
        }
    }

    public Bullet GetPooledBullet() {

        for (int i = 0; i<pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].gameObject.activeInHierarchy) {
                pooledBullets[i].transform.localPosition = Vector3.zero;
                return pooledBullets[i];
            }
        }
        return null;
    }


    //Shoot this weapon
    public async void Shoot()
    {
        if (isShooting)
            return;

        isShooting = true;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            Bullet bullet = GetPooledBullet();
            bullet.gameObject.SetActive(true);
            bullet.Project(transform.up);
            await Task.Delay(150);
        }

        isShooting = false;
    }
}
