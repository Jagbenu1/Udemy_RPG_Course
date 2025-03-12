using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float coolDown;
    protected float coolDownTimer;


    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

   public virtual bool CanUseSkill()
    {
        if(coolDownTimer <= 0)
        {
            UseSKill();
            coolDownTimer = coolDown;
            return true;
        }

        Debug.Log("Skill is on cooldown");
        return false;
    }

    public virtual void UseSKill()
    {
        // do some skill specific things
    }
}
