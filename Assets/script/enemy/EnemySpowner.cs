using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpowner : MonoBehaviour
{
    public static EnemySpowner instance;

    [SerializeField]
    private GameObject boarPrefeb, CannibalPrefeb;

    public Transform[] cannibalSpawnPoint, boarSpawnPoint;

    [SerializeField]
    private int cannibalEnemyCount, boarEnemyCount;

    private int initialCannibalCount, initialBoarCount;

    public float waitBeforeSpawnEnemyTime = 10f;


    private void Awake()
    {
        MakeInstance();
    }
  
    // Start is called before the first frame update
    void Start()
    {
        initialBoarCount = boarEnemyCount;
        initialCannibalCount = cannibalEnemyCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        SpawnCannibals();
        SpawnBoars();
    }

    void SpawnCannibals()
    {
        int index = 0;

        for(int i = 0; i < cannibalEnemyCount; i++)
        {
            if (index >= cannibalSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(CannibalPrefeb, cannibalSpawnPoint[index].position, Quaternion.identity);
            
            index++;

        }
        cannibalEnemyCount = 0;
    }
    void SpawnBoars()
    {
        int index = 0;

        for (int i = 0; i < boarEnemyCount; i++)
        {
            if (index >= boarSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(boarPrefeb, boarSpawnPoint[index].position, Quaternion.identity);

            index++;

        }
        boarEnemyCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawnEnemyTime);
        SpawnCannibals();
        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDie(bool cannibal)
    {
        if (cannibal)
        {
            cannibalEnemyCount++;

            if (cannibalEnemyCount > initialCannibalCount)
            {
                cannibalEnemyCount = initialCannibalCount;
            }
        }
        else
        {
            boarEnemyCount++;

            if (boarEnemyCount > initialBoarCount)
            {
                boarEnemyCount = initialBoarCount;
            }

        }
    }

    public void StopSpawing()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

}//class
