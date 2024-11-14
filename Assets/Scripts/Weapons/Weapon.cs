using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    public Transform parentTransform;
    private bool isEquipped = false;

    void Start()
    {
        if (bullet == null)
        {
            Debug.LogError("Bullet is not assigned!", this);
        }
        if (bulletSpawnPoint == null)
        {
            Debug.LogError("Bullet Spawn Point is not assigned!", this);
        }

        // Inisialisasi Object Pooling
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGet,
            OnRelease,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    void Update()
    {
        // Pastikan hanya menembak jika weapon sudah di-equip
        if (!isEquipped) return;

        // Timer untuk menembak setiap interval
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f; // Reset timer
        }
    }

    private Bullet CreateBullet()
    {
        // Membuat bullet baru jika Object Pool membutuhkan
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        newBullet.gameObject.SetActive(false);
        return newBullet;
    }

    private void OnGet(Bullet bullet)
    {
        // Aktifkan bullet dan mulai gerakkan
        bullet.gameObject.SetActive(true);
        bullet.Launch();
    }

    private void OnRelease(Bullet bullet)
    {
        // Nonaktifkan bullet ketika dilepaskan
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        // Hancurkan bullet ketika tidak digunakan
        Destroy(bullet.gameObject);
    }

    private void Shoot()
    {
        // Ambil bullet dari object pool dan tembak
        Bullet newBullet = bulletPool.Get();
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    public void Equip()
    {
        isEquipped = true; // Menandakan weapon sudah di-equip
        gameObject.SetActive(true); // Menyalakan weapon jika di-equip
    }

    public void Unequip()
    {
        isEquipped = false; // Menandakan weapon sudah di-un-equip
        gameObject.SetActive(false); // Menonaktifkan weapon jika tidak di-equip
    }
}
