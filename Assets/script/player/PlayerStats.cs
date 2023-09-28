using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStats, sataminaStats;

    public void DisplayHealthStats(float healhValue)
    {
        healhValue /= 100f;

        healthStats.fillAmount = healhValue;
    }

    public void DisplaySataminaStats(float sataminaValue)
    {
        sataminaValue /= 100f;

        sataminaStats.fillAmount = sataminaValue;
    }

}//class

