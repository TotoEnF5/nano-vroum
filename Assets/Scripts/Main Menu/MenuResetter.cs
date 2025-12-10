using UnityEngine;
using UnityEngine.EventSystems; // N'oubliez pas l'espace de nom !
using UnityEngine.UI;

public class MenuResetter: MonoBehaviour
{
    public Button firstSelectedButton; // Glissez-déposez le bouton 'Play' ici

    void OnEnable()
    {
        // 1. Désélectionner tout, au cas où un élément était déjà sélectionné
        EventSystem.current.SetSelectedGameObject(null);

        // 2. Sélectionner le nouveau bouton
        EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
    }
}