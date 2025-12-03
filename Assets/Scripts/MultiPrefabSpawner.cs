using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPrefabSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] prefabs;
    private PlayerInputManager manager;

    private void Start()
    {
        manager = GetComponent<PlayerInputManager>();
    }

    private int playerCount = 0;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        // Nous sommes déjà dans le processus d'instanciation. Le Manager a créé
        // un GameObject basé sur le 'Dummy' que nous lui avons donné.

        // 1. Détermine le Prefab à utiliser
        if (playerCount >= prefabs.Length)
        {
            Debug.LogWarning("Tous les emplacements de joueurs sont pris.");
            Destroy(playerInput.gameObject); // Détruit l'objet en trop
            return;
        }

        GameObject prefabToUse = prefabs[playerCount];

        // 2. Transférer le contrôle à un NOUVEL objet (le vrai Prefab)

        // Transfère la manette (device) et l'index au nouveau prefab.
        // Utilisation de la surcharge de PlayerInput.Instantiate pour assigner directement 
        // les propriétés de l'objet temporaire 'playerInput'.
        PlayerInput newPlayerInput = PlayerInput.Instantiate(
            prefabToUse,
            playerIndex: playerInput.playerIndex, // Utilise l'index que le Manager a assigné
            controlScheme: playerInput.currentControlScheme,
            pairWithDevice: playerInput.devices.ToArray()[playerCount]
        );

        // 3. DÉTRUIRE l'objet temporaire seulement après l'instanciation réussie
        // Cela brise la boucle en assurant que la manette est prise avant que le GameObject ne disparaisse.
        Destroy(playerInput.gameObject);

        playerCount++;
        Debug.Log($"Joueur {playerCount} rejoint avec le Prefab : {prefabToUse.name}");
    }
}
