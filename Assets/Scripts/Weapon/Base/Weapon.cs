using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected Bullet bulletPrefab;

    [SerializeField]
    protected Transform bulletsPoolParent;

    [SerializeField]
    protected List<Bullet> pooledBullets = new List<Bullet>();

    [SerializeField]
    protected int bulletPoolAmount = 20;

    //the weapon shall fire x nuumber of times bullet per Shoot()
    //To Shoot in burst mode set the value to be greater than 1
    [SerializeField]
    protected int bulletsPerShot = 1;


    [SerializeField]
    protected int ShotAsyncDelay = 150;

    protected bool isShooting = false;




    public virtual void SpawnPooledBullets() {
        for (int i = 0; i < bulletPoolAmount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletsPoolParent, false);
            bullet.gameObject.SetActive(false);
            pooledBullets.Add(bullet);
        }
    }


    public virtual Bullet GetPooledBullet() {

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

    private void OnDestroy()
    {
        pooledBullets.Clear();
    }


    //Shoot this weapon
    public virtual async void Shoot(){}
}
