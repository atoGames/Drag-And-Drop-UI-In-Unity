using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GenerateCards : MonoBehaviour
{
    // Card prefab
    public GameObject m_CardPrefab;
    // Player cards transform
    public Transform m_PlayerCardsTransform;
    // Ability icons list
    public List<Sprite> m_AbilityIconsList = new List<Sprite>();

    void Start()
    {
        // Loop  
        for (int i = 0; i < m_PlayerCardsTransform.childCount; i++)
        {
            // Take ref/instance of card we spawn
            var _Card = Instantiate(m_CardPrefab, m_PlayerCardsTransform.GetChild(i));
            // Change card/text number 
            _Card.transform.Find("Card Amount").GetComponent<TextMeshProUGUI>().text = UnityEngine.Random.Range(0, 10).ToString();
            // Get icon image
            var _Icon = Array.Find(_Card.GetComponentsInChildren<Transform>(), n => n.name == "Ability icon").GetComponent<Image>();
            // Set new icon to image
            _Icon.sprite = m_AbilityIconsList[UnityEngine.Random.Range(0, m_AbilityIconsList.Count)];
        }
    }

}
