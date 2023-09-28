using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBow : MonoBehaviour
{
    public Rigidbody mybody;

    public float speed = 30f;

    public float deactiveTimer = 3f;
    public float damage = 100f;

    private void Awake()
    {
        mybody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactiveTimer);
    }

    public void Launch(Camera mainCamera)
    {
        mybody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + mybody.velocity);

    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.EnemyTag)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}
