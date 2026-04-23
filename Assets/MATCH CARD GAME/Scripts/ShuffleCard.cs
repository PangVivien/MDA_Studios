using UnityEngine;

public class ShuffleCard : MonoBehaviour
{
    public Transform[] cards; 

    private Vector3[] positions;

    void Awake()
    {
        positions = new Vector3[cards.Length];

        for (int i = 0; i < cards.Length; i++)
        {
            positions[i] = cards[i].position;
        }

        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            int rand = Random.Range(i, positions.Length);

            Vector3 temp = positions[i];
            positions[i] = positions[rand];
            positions[rand] = temp;
        }

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].position = positions[i];
        }
    }
}
