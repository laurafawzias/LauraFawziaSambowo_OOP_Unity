using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;

    void Start()
    {
        // Ambil Rigidbody2D yang ada pada Bullet
        rb = GetComponent<Rigidbody2D>();
        
        // Set velocity pada Rigidbody2D agar bullet bergerak
        rb.velocity = transform.up * bulletSpeed;
    }

    // Fungsi untuk memulai pergerakan Bullet jika diperlukan
    public void Launch()
    {
        if (rb != null)
        {
            // Pastikan Bullet bergerak dengan kecepatan yang sudah ditentukan
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    // Ketika Bullet bertabrakan dengan sesuatu
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Nonaktifkan Bullet ketika bertabrakan
        gameObject.SetActive(false);
    }

    // Jika Bullet keluar dari layar, nonaktifkan
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
