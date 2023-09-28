using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    None,
    SELF_Aim,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTiIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class weaponsHandler : MonoBehaviour
{
    private Animator anim;

    public WeaponAim weaponAim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reloadSound;

    public WeaponFireType fireType;
    public WeaponBulletType bulletType;

    public GameObject attackPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTag.shootTrigger);
    }
    public void Aim(bool canaim)
    {
        anim.SetBool(AnimationTag.AimParam,canaim);
    }

    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }
    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
    void PlayShootSound()
    {
        shootSound.Play();
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }

    void TurnOnattackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffattackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
