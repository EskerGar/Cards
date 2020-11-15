using UnityEngine;
using Random = UnityEngine.Random;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform tempCard;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
        var amount = Random.Range(4, 6);
        for(int i = 0; i < amount; i++)
            CardPool.AddCard(CreateCard(hand, tempCard, canvas));
    }
    
    private CardBehaviour CreateCard(Transform parent, Transform tempCardTransform, Canvas canvases)
    {
        var card = Instantiate(cardPrefab).GetComponent<CardBehaviour>();
        card.GetComponent<CardMove>().Initialize(tempCardTransform, canvases);
        card.transform.SetParent(parent);
        card.transform.localScale = new Vector3(50f, 50f, 1f);
        return card;
    }
}
