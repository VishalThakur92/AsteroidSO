using System.Collections;
using System.Collections.Generic;
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
    public void Shoot()
    {

        Bullet bullet = GetPooledBullet();
        bullet.gameObject.SetActive(true);
        bullet.Project(transform.up);
        //Bullet bullet1 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //Bullet bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //Bullet bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation);

        //bullet1.Project(transform.up);
        //bullet2.Project(bullet1.transform.right);
        //bullet3.Project(-bullet1.transform.right);
    }
}
