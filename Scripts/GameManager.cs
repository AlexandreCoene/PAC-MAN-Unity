using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText; // This variable holds the lives text
    [SerializeField] private Text usernameText; // This variable holds the username text

    private int ghostMultiplier = 1; // This variable keeps track of the ghost multiplier
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    private void Awake() // This method is called when the object is initialized
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject); // Destroy this object if an instance already exists
        }
        else
        {
            Instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading a new scene
        }
    }

    private void Start()
    {
        NewGame(); // Start a new game

        // Get the username from PlayerPrefs and set it in the UI
        string username = PlayerPrefs.GetString("Username");
        if (!string.IsNullOrEmpty(username))
        {
            usernameText.text = "Pseudo : " + username;
        }
        else
        {
            Debug.Log("Aucun pseudo trouvé.");
        }
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) // If the player has no lives left and presses any key
        {
            NewGame(); // Start a new game when the player presses any key
        }
    }

    private void NewGame() // This method starts a new game (initialisation)
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound() // This method starts a new round
    {
        gameOverText.enabled = false; // Disable the game over text

        foreach (Transform pellet in pellets) // Loop through all the pellets
        {
            pellet.gameObject.SetActive(true); // Activate the pellet
        }

        ResetState(); // Reset the state of the game
    }

    private void ResetState() // This method resets the state of the game
    {
        for (int i = 0; i < ghosts.Length; i++) // Loop through all the ghosts
        {
            ghosts[i].ResetState(); // Reset the state of the ghost
        }

        pacman.ResetState(); // Reset the state of pacman
    }

    private void GameOver()
    {
        gameOverText.enabled = true; // Affiche le texte de fin de jeu

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false); // Désactive les fantômes
        }

        pacman.gameObject.SetActive(false); // Désactive Pacman

        // Envoi du score à l'API
        string username = PlayerPrefs.GetString("Username");
        string date = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        StartCoroutine(SendScoreToAPI(username, score, date));
    }


    private void SetLives(int lives) // Cette fonction gère le nombre de vies
    {
        this.lives = lives; // Initialise le nombre de vies
        livesText.text = "x" + lives.ToString(); // Met a jour le nombre de vies (dans l'interface)
    }

    private void SetScore(int score) // This method sets the score
    {
        this.score = score; // Set the score
        scoreText.text = score.ToString().PadLeft(2, '0'); // Update the score text
    }

    public void PacmanEaten() // This method is called when pacman is eaten
    {
        pacman.DeathSequence(); // Play the death sequence

        SetLives(lives - 1); // Decrease the number of lives

        if (lives > 0) // If there are still lives left
        {
            Invoke(nameof(ResetState), 3f); // Reset the state after 3 seconds
        }
        else // If there are no lives left
        {
            GameOver(); // End the game
        }
    }

    public void GhostEaten(Ghost ghost) // This method is called when a ghost is eaten
    {
        int points = ghost.points * ghostMultiplier; // Calculate the points for eating the ghost
        SetScore(score + points); // Increase the score by the points

        ghostMultiplier++; // Increase the ghost multiplier
    }

    public void PelletEaten(Pellet pellet) // This method is called when a pellet is eaten
    {
        pellet.gameObject.SetActive(false); // Deactivate the pellet

        SetScore(score + pellet.points); // Increase the score by the pellet's point value

        if (!HasRemainingPellets()) // If there are no remaining pellets
        {
            pacman.gameObject.SetActive(false); // Deactivate pacman
            Invoke(nameof(NewRound), 3f); // Start a new round after 3 seconds
        }
    }

    public void PowerPelletEaten(PowerPellet pellet) // This method is called when a power pellet is eaten
    {
        for (int i = 0; i < ghosts.Length; i++) // Loop through all the ghosts
        {
            ghosts[i].frightened.Enable(pellet.duration); // Enable the frightened state for each ghost
        }

        PelletEaten(pellet); // Eat the pellet
        CancelInvoke(nameof(ResetGhostMultiplier)); // Cancel the reset of the ghost multiplier
        Invoke(nameof(ResetGhostMultiplier), pellet.duration); // Reset the ghost multiplier after the duration of the power pellet
    }

    private bool HasRemainingPellets() // This method checks if there are any remaining pellets
    {
        foreach (Transform pellet in pellets) // Loop through all the pellets
        {
            if (pellet.gameObject.activeSelf) // If the pellet is active
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier() // This method resets the ghost multiplier
    {
        ghostMultiplier = 1; // Reset le ghost multiplier
    }

    private IEnumerator SendScoreToAPI(string username, int score, string date)
    {
        // Créer un formulaire pour envoyer les données
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);
        form.AddField("date", date);

        // URL de l'API - assurez-vous que l'URL est complète et correcte
        string url = "https://iqrwfnkiuzoyiknupupi.supabase.co/rest/v1/partie"; // Assurez-vous que c'est le bon chemin

        // Créer une requête POST à l'API puis envoyer le formulaire
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            // Ajouter les en-têtes nécessaires
            www.SetRequestHeader("apikey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImlxcndmbmtpdXpveWlrbnVwdXBpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTYyODAzMjEsImV4cCI6MjAzMTg1NjMyMX0.ucbha2vJxRkyS8j5PCgC6RprRZewGH886mDxZTklaFU");

            // Optionnel : Définir le type de contenu explicitement si nécessaire
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            // Envoi de la requête et attend la réponse
            yield return www.SendWebRequest();

            // Vérification des erreurs
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                Debug.LogError("Response Code: " + www.responseCode);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("Response: " + www.downloadHandler.text);
            }
        }
    }





}

