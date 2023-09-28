using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimationController enemyAnim;
    private EnemyController enemyController;
    private NavMeshAgent navMesh;

    private PlayerStats playerStats;

    public float health=100f;

    public bool isPlayer, isBoar, isCanniball;

    private bool isdead;

    private EnemyAudio enemyAudio;

    private void Awake()
    {
        if (isBoar || isCanniball)
        {
            enemyAnim = GetComponent<EnemyAnimationController>();
            enemyController = GetComponent<EnemyController>();
            navMesh = GetComponent<NavMeshAgent>();
            //get enemy audio

            enemyAudio = GetComponentInChildren<EnemyAudio>();

        }
        if (isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if (isdead)
            return;

        health -= damage;
        

        if (isPlayer)
        {
            playerStats.DisplayHealthStats(health);
        }
        if(isBoar|| isCanniball)
        {
            if (enemyController.Enemy_State == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
            }
        }

        if (health <= 0f)
        {
            PlayerDied();

            isdead = true;
        }
        
    }//apply damage

    void PlayerDied()
    {
        if (isCanniball)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            

            enemyController.enabled = false;
            navMesh.enabled = false;
            enemyAnim.enabled = false;

            //startcoroutine
            StartCoroutine(DeadSound());

            // EnemManager to Spawn More enemy

            EnemySpowner.instance.EnemyDie(true);

        }//canniboll died

        if (isBoar)
        {
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
            enemyController.enabled = false;

            enemyAnim.Dead();

            //startcoroutine
            StartCoroutine(DeadSound());

            // EnemManager to Spawn More enemy

            EnemySpowner.instance.EnemyDie(false);


        }//boar died

        if (isPlayer)
        {
            GameObject[] enmies = GameObject.FindGameObjectsWithTag(Tags.EnemyTag);

            for (int i = 0; i < enmies.Length; i++)
            {
                enmies[i].GetComponent<EnemyController>().enabled = false;
            }
            //call the enemy manager to stop spawing enemy

            EnemySpowner.instance.StopSpawing();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<weaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);


        }//player died

        if (tag == Tags.PlayerTag)
        {
            Invoke("RestartGame", 5f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    }//function died

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDieSound();
    }

}//class
