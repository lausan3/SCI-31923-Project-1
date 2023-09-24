using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI EXPToMaxText;
    public TextMeshProUGUI hpBar;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Current Level: " + gm.level;
        EXPToMaxText.text = "To Next Level: " + gm.exp + "/" + gm.nextLevelThreshold;
    }
}
