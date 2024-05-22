using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // M�thode pour charger la sc�ne "Register"
    public void Jouer()
    {
        SceneManager.LoadSceneAsync("Register");
    }

    // Nouvelle m�thode pour ouvrir une URL
    public void OuvrirClassement()
    {
        // Remplacez l'URL par celle de votre classement
        Application.OpenURL("https://www.larousse.fr/dictionnaires/francais/test/77497");
    }
}
