using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeUIManager : MonoBehaviour
{
    public GameObject lifeImagePrefab; // Prefab for each life icon
    public Transform lifePanel;        // Panel in the Canvas where life icons will be displayed
    private List<GameObject> lifeImages = new List<GameObject>(); // List to store the life icons

    public void UpdateLivesUI(int lives)
    {
        // Clear existing life icons
        foreach (GameObject img in lifeImages)
        {
            Destroy(img); // Destroy each life icon GameObject
        }
        lifeImages.Clear(); // Clear the list of life icons

        // Instantiate a new life icon for each life
        for (int i = 0; i < lives; i++)
        {
            GameObject lifeImage = Instantiate(lifeImagePrefab, lifePanel); // Create a new life icon and set it as a child of lifePanel
            lifeImages.Add(lifeImage); // Add the newly created icon to the list
        }
    }
}
