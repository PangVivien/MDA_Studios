using UnityEngine;

public class EditMode : MonoBehaviour
{
    public GameObject editButton;
    public GameObject doneButton;
    public GameObject cancelButton;
    public GameObject editFunction;

    public GameObject subText;
    public GameObject instructionText;

    private void OnEnable()
    {
        editButton.SetActive(true);
        doneButton.SetActive(true);
        cancelButton.SetActive(false);

        editFunction.SetActive(false);

        subText.SetActive(true);
        instructionText.SetActive(false);
    }

    public void EnableEdit()
    {
        doneButton.SetActive(false);
        editButton.SetActive(false);
        cancelButton.SetActive(true);

        editFunction.SetActive(true);

        subText.SetActive(false);
        instructionText.SetActive(true);
    }

    public void DisableEdit()
    {
        doneButton.SetActive(true);
        editButton.SetActive(true);
        cancelButton.SetActive(false);

        editFunction.SetActive(false);

        subText.SetActive(true);
        instructionText.SetActive(false);
    }
}
