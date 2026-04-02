using UnityEngine;

public class EditMode : MonoBehaviour
{
    public GameObject editButton;
    public GameObject doneButton;
    public GameObject cancelButton;
    public GameObject layoutOption;

    public GameObject subText;
    public GameObject instructionText;

    private void OnEnable()
    {
        editButton.SetActive(true);
        doneButton.SetActive(true);
        cancelButton.SetActive(false);

        layoutOption.SetActive(true);

        subText.SetActive(true);
        instructionText.SetActive(false);
    }

    public void EnableDrag()
    {
        doneButton.SetActive(false);
        editButton.SetActive(false);
        cancelButton.SetActive(true);

        layoutOption.SetActive(false);

        subText.SetActive(false);
        instructionText.SetActive(true);
    }

    public void DisableDrag()
    {
        doneButton.SetActive(true);
        editButton.SetActive(true);
        cancelButton.SetActive(false);

        layoutOption.SetActive(true);

        subText.SetActive(true);
        instructionText.SetActive(false);
    }
}
