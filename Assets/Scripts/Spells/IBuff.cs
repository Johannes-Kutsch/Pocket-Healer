using System.Collections;
using UnityEngine;

public interface IBuff {

    float GetDuration();
    Material GetMaterial();
    string GetRemainingTime();
    float DamageTaken(float amount);
    float FatalDamage(float amount);
    float HealingTaken(float amount);
    float GlobalHealingTaken(float amount);
    float GlobalDamageTaken(float amount);
    bool IsBuff();
    bool IsDispellable();
    void Destroy();
    void Reset();
}
