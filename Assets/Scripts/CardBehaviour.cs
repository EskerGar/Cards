using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text manaText;
    [Header("Card Settings")]
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int mana;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public int Mana
    {
        get => mana;
        set => mana = value;
    }

    public void RefreshUi()
    {
        healthText.text = "HP: " + Health;
        damageText.text = "DM: " + Damage;
        manaText.text = Mana.ToString();
    }

    public void CheckHealth()
    {
        if(Health > 0) return;
        CardPool.CheckCardFromHand(this, default);
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        transform.DOMoveY(.5f, .7f);
        yield return new WaitForSeconds(.71f);
        Destroy(gameObject);
    }
    

    private void Awake()
    {
        health = Random.Range(1, 9);
        damage = Random.Range(1, 9);
        mana = Random.Range(1, 9);
        RefreshUi();
    }
    
}
