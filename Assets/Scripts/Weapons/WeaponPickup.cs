using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    private void Awake()
    {
        weapon = Instantiate(weaponHolder);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            Weapon currentWeapon = collision.gameObject.GetComponentInChildren<Weapon>();
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            weapon.transform.SetParent(collision.gameObject.transform, false);
            weapon.transform.localPosition = new Vector3(0, 0, 1);
            TurnVisual(true);

            if (player != null)
            {
                player.EquipWeapon();
            }
        }
    }

    private void TurnVisual(bool on)
    {
        TurnVisual(on, weapon);
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        weapon.GetComponent<SpriteRenderer>().enabled = on;
        weapon.GetComponent<Weapon>().enabled = on;
        weapon.GetComponent<Animator>().enabled = on;   
    }

}
