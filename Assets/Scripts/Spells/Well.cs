using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Well : MonoBehaviour {
    private IRaider target;
    public Image cooldownOverlay;
    private List<IRaider> raiderDict = new List<IRaider>();
    private float cooldown = 1f;
    public float cooldownTimer;
    private float cooldownMax;
    public bool onCooldown = false;
    private float healAmount = 15f;

    void Start()
    {
        cooldownOverlay = GetComponentInChildren<Image>();
        cooldownTimer = cooldown;
    }

    void FixedUpdate()
    {
        if (cooldownTimer >= cooldownMax)
        {
            onCooldown = false;
            cooldownOverlay.color = new Color32(160, 160, 160, 0);
            Cast();
        }
        else
        {
            cooldownTimer += 0.02f;
            cooldownOverlay.fillAmount = cooldownTimer / cooldownMax;
        }
    }

    public void Cast()
    {
        cooldownTimer = 0f;
        cooldownMax = cooldown;
        onCooldown = true;
        cooldownOverlay.color = new Color32(160, 160, 160, 160);
        raiderDict = RaiderDB.GetInstance().GetAllRaiderSortedByHealth();
        target = raiderDict.First();
        target.Heal(healAmount);
    }

    public void RemoveSpellFromButton()
    {
        GetComponent<MeshRenderer>().material = null;
        Destroy(this);
    }
}
