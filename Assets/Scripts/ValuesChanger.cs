using UnityEngine;

public class ValuesChanger : MonoBehaviour
{

    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;
    public void ChangeValue()
    {
        var value = Random.Range(minValue, maxValue);
        CardPool.ChangeCardValue(value);
    }
    
}
