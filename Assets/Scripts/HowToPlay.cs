using UnityEngine;
using TMPro;

public class HowToPlay : MonoBehaviour
{
    public TMP_Text controlsText; 

    private bool isVisible = false; // Track visibility state

    public void ToggleVisibility()
    {
        isVisible = !isVisible; // Toggle the state
        controlsText.gameObject.SetActive(isVisible); // Show or hide the text
    }
}
