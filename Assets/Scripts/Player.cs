using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour, IDamagable
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private float interactionRange = 3f;

    public GameObject WeaponPlaceholderObject;
    public GameObject[] WeaponsReel;
    public GameObject InitialWeaponPrefab;
    public GameObject Flashlight;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    private bool IsFocusedOnInteractable = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        WeaponsReel = new GameObject[System.Enum.GetValues(typeof(GameEnums.WeaponsEnum)).Length];
        WeaponsReel[(int)GameEnums.WeaponsEnum.Pistol] = Instantiate(InitialWeaponPrefab, WeaponPlaceholderObject.transform);
        WeaponsReel[(int)GameEnums.WeaponsEnum.Pistol].transform.localPosition = Vector3.zero;
        SwitchToWeapon(GameEnums.WeaponsEnum.Pistol);

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventManager.OnHealthUpdate?.Invoke(CurrentHealth.ToString());
    }

    void Update()
    {
        FppMovement();
        CheckInput();
        WeaponSwitchInput();
        CheckInteractableFocus();
    }

    private void WeaponSwitchInput()
    {
        // TODO try and make this more modular
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToWeapon(GameEnums.WeaponsEnum.Pistol);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToWeapon(GameEnums.WeaponsEnum.AssaultRifle);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchToWeapon(GameEnums.WeaponsEnum.Shotgun);
    }

    private void SwitchToWeapon(GameEnums.WeaponsEnum weapon)
    {
        if (WeaponsReel[(int)weapon] == null) return;

        DeactivateAllWeapons();

        WeaponsReel[(int)weapon]?.SetActive(true);
    }

    private void FppMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void TakeCollectible(CollectibleBase collectible)
    {
        switch (collectible)
        {
            case WeaponCollectible weaponCollectible:
                TakeWeaponCollectible(collectible as WeaponCollectible);
                break;
        }
    }

    public void TakeWeaponCollectible(WeaponCollectible weaponCollectible)
    {
        // TODO possibly simplify this switch
        switch (weaponCollectible.WeaponObject.GetComponent<WeaponBase>())
        {
            case PistolScript pistolScript:
                WeaponsReel[(int)GameEnums.WeaponsEnum.Pistol].GetComponent<WeaponBase>().AddAmmo(pistolScript.MagCapacity);
                break;

            case AssaultRifleScript assaultRifleScript:
                if (WeaponsReel[(int)GameEnums.WeaponsEnum.AssaultRifle] == null)
                {
                    DeactivateAllWeapons();
                    WeaponsReel[(int)GameEnums.WeaponsEnum.AssaultRifle] = Instantiate(weaponCollectible.WeaponPrefabHandle, WeaponPlaceholderObject.transform);
                    WeaponsReel[(int)GameEnums.WeaponsEnum.AssaultRifle].transform.localPosition = Vector3.zero;
                    SwitchToWeapon(GameEnums.WeaponsEnum.AssaultRifle);
                }
                else
                {
                    WeaponsReel[(int)GameEnums.WeaponsEnum.AssaultRifle]
                        .GetComponent<WeaponBase>()
                        .AddAmmo(assaultRifleScript.MagCapacity);
                }
                break;

            case ShotgunScript shotgunScript:
                if (WeaponsReel[(int)GameEnums.WeaponsEnum.Shotgun] == null)
                {
                    DeactivateAllWeapons();
                    WeaponsReel[(int)GameEnums.WeaponsEnum.Shotgun] = Instantiate(weaponCollectible.WeaponPrefabHandle, WeaponPlaceholderObject.transform);
                    WeaponsReel[(int)GameEnums.WeaponsEnum.Shotgun].transform.localPosition = Vector3.zero;
                    SwitchToWeapon(GameEnums.WeaponsEnum.Shotgun);
                }
                else
                {
                    WeaponsReel[(int)GameEnums.WeaponsEnum.Shotgun]
                        .GetComponent<WeaponBase>()
                        .AddAmmo(shotgunScript.MagCapacity);
                }
                break;
        }
    }

    public void ApplyDamage(int amount)
    {
        CurrentHealth -= amount;
        EventManager.OnHealthUpdate?.Invoke(CurrentHealth.ToString());
    }

    #region Helpers
    private void DeactivateAllWeapons()
    {
        foreach (GameObject weaponObject in WeaponsReel)
            weaponObject?.SetActive(false);
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteractions();

        if (Input.GetKeyDown(KeyCode.F))
            Flashlight.SetActive(!Flashlight.activeSelf);
    }

    private void CheckInteractions()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
            hit.collider.GetComponent<IInteractable>()?.Interact();
    }

    private void CheckInteractableFocus()
    {
        RaycastHit hit;

        if (!IsFocusedOnInteractable && Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            if (hit.collider.GetComponent<IInteractable>() == null) return;
            EventManager.OnInteractableFocus?.Invoke();
            IsFocusedOnInteractable = !IsFocusedOnInteractable;
        }

        if (IsFocusedOnInteractable && !Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            EventManager.OnInteractableUnfocus?.Invoke();
            IsFocusedOnInteractable = !IsFocusedOnInteractable;
        }
    }
    #endregion
}