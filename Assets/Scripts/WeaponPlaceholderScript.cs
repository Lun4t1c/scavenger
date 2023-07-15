using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlaceholderScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static EventManager.GenericGameEvent OnReloadStart;
    public static EventManager.GenericGameEvent OnReloadStop;

    // Start is called before the first frame update
    void Start()
    {
        OnReloadStart += StartReloading;
        OnReloadStop += StopReloading;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartReloading() => animator.SetTrigger("ReloadStarted");
    void StopReloading() => animator.SetTrigger("ReloadStopped");
}
