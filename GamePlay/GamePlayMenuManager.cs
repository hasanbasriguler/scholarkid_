using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayMenuManager : MonoBehaviour
{
    int sceneindex;


    private void Start()
    {
        sceneindex= SceneManager.GetActiveScene().buildIndex;
    }


    public void BackStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void StartFruitLevel()
    {
        SceneManager.LoadScene("FruitVegetable");
    }

    public void StartAnimalLevel()
    {
        SceneManager.LoadScene("Animals");
    }

    public void StartGeometryLevel()
    {
        SceneManager.LoadScene("Geometry");
    }

    public void StartColorsLevel()
    {
        SceneManager.LoadScene("Colors");
    }

    public void StartOrgansLevel()
    {
        SceneManager.LoadScene("Organs");
    }

    public void BackToCategories()
    {
        SceneManager.LoadScene("Categories");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Categories");
        }
    }

}
