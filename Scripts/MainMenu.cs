using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Méthode pour charger la scène "Register"
    public void Jouer()
    {
        SceneManager.LoadSceneAsync("Register");
    }

    // Nouvelle méthode pour ouvrir une URL
    public void OuvrirClassement()
    {
        // Remplacez l'URL par celle de votre classement
        Application.OpenURL("https://www.larousse.fr/dictionnaires/francais/test/77497");
    }
}
