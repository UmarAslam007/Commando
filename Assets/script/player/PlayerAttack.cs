using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private weaponManager weaponManager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20;
    [SerializeField]
    private Animator ZoomCameraAnim;
    private bool zoomed;

    private bool IsAming;

    [SerializeField]
    private GameObject Arrow_prefab, Spear_prefab;

    [SerializeField]
    private Transform arrowBowStartingPosition;

    private Camera mainCam;

    private GameObject crosshair;

    private void Awake()
    {
        weaponManager = GetComponent<weaponManager>();
      //  ZoomCameraAnim = transform.Find(Tags.LookRoot)
      //      .transform.Find(Tags.zoomCamera).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CrossHair);
        mainCam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Weaponshoot();
        ZoomInOut();
    }

    void Weaponshoot()
    {
        //if its assult rifile
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTiIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > fireRate)
            {
                nextTimeToFire = Time.time / fireRate;
                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                //shootBullet();
                BulletFired();
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Axe
                if (weaponManager.GetCurrentSelectedWeapon().tag == Tags.AxeTag)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                }
                //Shoot
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    //shootBullet();
                    BulletFired();
                }
                else
                {//Arrow or Bow
                    if (IsAming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                        if(weaponManager.GetCurrentSelectedWeapon().bulletType
                            == WeaponBulletType.ARROW)
                        {
                            //throw arrow
                            ThrowArrowSpear(true);
                        }
                        else if(weaponManager.GetCurrentSelectedWeapon().bulletType
                            == WeaponBulletType.SPEAR)
                        {
                            //throw spear
                            ThrowArrowSpear(false);
                        }
                    }
                }
            }
        }
    }
    void ZoomInOut()
    {
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ZoomCameraAnim.Play(AnimationTag.zoomInAnim);
                crosshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                ZoomCameraAnim.Play(AnimationTag.zoomOutAnim);
                crosshair.SetActive(true);
            }

        }
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_Aim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                IsAming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                IsAming = false;
            }
        }
    }

    void ThrowArrowSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(Arrow_prefab);
            arrow.transform.position = arrowBowStartingPosition.position;

            arrow.GetComponent<ArrowBow>().Launch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(Spear_prefab);
            spear.transform.position = arrowBowStartingPosition.position;

            spear.GetComponent<ArrowBow>().Launch(mainCam);

        }

    }
    void BulletFired()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position,mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.EnemyTag)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }

}//class

