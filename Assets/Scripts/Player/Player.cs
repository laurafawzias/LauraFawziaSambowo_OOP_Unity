using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    PlayerMovement playerMovement;
    Animator animator;
    private bool hasWeapon;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

        void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

        void FixedUpdate()
    {
        playerMovement.Move();
        // playerMovement.RestrictMovement();
    }

    void LateUpdate() {
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }

    public bool HasWeapon()
    {
        return hasWeapon;
    }

    public void EquipWeapon()
    {
        hasWeapon = true;
    }

    public void UnequipWeapon()
    {
        hasWeapon = false;
    }
}
