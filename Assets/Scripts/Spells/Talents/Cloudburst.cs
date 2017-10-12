using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Cloudburst : MonoBehaviour
{
    public static Cloudburst cloudburst;
    private Image cooldownOverlay;
    private List<IRaider> targetDict = new List<IRaider>();
    private float cooldown = 10f;
    public float cooldownTimer;
    private bool onCooldown = false;
    private float storedHealth = 0f;
    private float multiplyer = 1f;

    //ToDo Cloudburst should apply an invisible buff to track overheal

    void Awake()
    {
        if (cloudburst == null)
        {
            //DontDestroyOnLoad(gameObject);
            cloudburst = this;
        }
        else if (cloudburst != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cooldownOverlay = GetComponentInChildren<Image>();
        cooldownTimer = 0;
        cooldownOverlay.color = new Color32(160, 160, 160, 160);
    }

    void FixedUpdate()
    {
        if (cooldownTimer >= cooldown)
        {
            onCooldown = false;
            Cast();
        }
        else
        {
            cooldownTimer += 0.02f;
            cooldownOverlay.fillAmount = cooldownTimer / cooldown;
        }
    }

    public void AddOverheal(float amount)
    {
        storedHealth += amount;
    }
    public void Cast()
    {
        cooldownTimer = 0f;
        onCooldown = true;
        targetDict = RaiderDB.GetInstance().GetAllRaiders();
        float anzTargets = targetDict.Count;
        foreach (IRaider raider in targetDict) {
            raider.HealSimple(storedHealth*multiplyer/ anzTargets, true);
        }
        storedHealth = 0;
    }

    public void RemoveSpellFromButton()
    {
        GetComponent<MeshRenderer>().material = null;
        Destroy(this);
    }
}
