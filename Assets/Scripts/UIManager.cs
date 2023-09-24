using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI EXPToMaxText;
    public TextMeshProUGUI hpText;
    private GameManager gm;
    private PlayerController pc;

    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "Current HP: " + pc.health;
        levelText.text = "Current Level: " + gm.level;
        EXPToMaxText.text = "To Next Level: " + gm.exp + "/" + gm.nextLevelThreshold;
    }
}
