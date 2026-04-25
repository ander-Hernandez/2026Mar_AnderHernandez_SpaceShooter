using TMPro;
using UnityEngine;

public class PointObjectBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(m_TextMeshProUGUI == null)
            m_TextMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    
    void Update()
    {
        
    }

    public void InitializePointObject() { 
        
    }
}
