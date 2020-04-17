using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

//Klasa odpowiada za generowanie nowych fal przeciwników
public class EnemyWaves : MonoBehaviour
{
    public Text waveNumberText;
    int nextWaveNumber;
    public GameObject waveMenuUI;
    bool displayUI = true;

    void Start()
    {
        nextWaveNumber = 1;
        StartCoroutine(NextWaveSplash());
    }


    void Update()
    {
        if (CheckAllWavesDestroyed())
        {
            displayUI = true;
            SpawnNewWaves();
        }
    }

    //Sprawdzenie czy wszystkie rzędy przeciwników zostały zniszczone
    bool CheckAllWavesDestroyed()
    {
        List<bool> destroyedWavesList = new List<bool>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.CompareTag("Wave"))
            {
                destroyedWavesList.Add(gameObject.transform.GetChild(i).gameObject.activeSelf);
            }
        }
        if (destroyedWavesList.All(x => x == false))
        {
            return true;
        }
        return false;
    }

    //Stworzenie nowej fali przeciwników
    void SpawnNewWaves()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.CompareTag("Wave"))
            {
                gameObject.transform.GetChild(i).gameObject.transform.position = new Vector3(-7f, gameObject.transform.GetChild(i).gameObject.transform.position.y);
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    //Wyświetlenie informacji o nowej fali
    IEnumerator NextWaveSplash()
    {
        while (true)
        {
            if (displayUI)
            {
                Time.timeScale = 0.1f;
                waveNumberText.text = nextWaveNumber.ToString();
                waveMenuUI.SetActive(true);
                nextWaveNumber++;
                displayUI = false;
                yield return new WaitForSeconds(0.2f);
                waveMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
            yield return null;
        }
    }
}
