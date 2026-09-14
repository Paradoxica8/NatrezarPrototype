using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSwitch : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private PlayerAttack daggerAttack;
    [SerializeField] private PlayerBow bowAttack;

    [Header("Switch")]
    [SerializeField] private float switchCooldown = 3f;

    private float switchCooldownTimer;

    private void Start()
    {
        daggerAttack.enabled = true;
        bowAttack.enabled = false;
    }
    
    private void Update()
    {
        if (switchCooldownTimer > 0f)
        {
            switchCooldownTimer -= Time.deltaTime;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SwitchToDagger();
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SwitchToBow();
        }
    }

    private void SwitchToDagger()
    {
        if (switchCooldownTimer > 0f)
            return;

        daggerAttack.enabled = true;
        bowAttack.enabled = false;

        switchCooldownTimer = switchCooldown;

        Debug.Log("Weapon switched: Daggers");
    }

    private void SwitchToBow()
    {
        if (switchCooldownTimer > 0f)
            return;

        daggerAttack.enabled = false;
        bowAttack.enabled = true;

        switchCooldownTimer = switchCooldown;

        Debug.Log("Weapon switched: Bow");
    }
}