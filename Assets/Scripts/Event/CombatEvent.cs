using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent : Event
{
    public Owner attacker;
    public Owner defender;

    public float attackerHullDamage;
    public float attackerShieldDamage;

    public float defenderHullDamage;
    public float defenderShieldDamage;

    public bool isAttackerAlive = true;
    public bool isDefenderAlive = true;

    //public string eventType = "Combat";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <param name="attackerHullDamage"></param>
    /// <param name="attackerShieldDamage"></param>
    /// <param name="defenderHullDamage"></param>
    /// <param name="defenderShieldDamage"></param>
    /// <param name="isAttackerAlive"></param>
    /// <param name="isDefenderAlive"></param>

    //public CombatEvent(Owner attacker, Owner defender, float attackerHullDamage, float attackerShieldDamage, float defenderHullDamage, float defenderShieldDamage, bool isAttackerAlive, bool isDefenderAlive)
    //{
    //    this.attacker = attacker;
    //    this.defender = defender;
    //    this.attackerHullDamage = attackerHullDamage;
    //    this.attackerShieldDamage = attackerShieldDamage;
    //    this.defenderHullDamage = defenderHullDamage;
    //    this.defenderShieldDamage = defenderShieldDamage;
    //    this.isAttackerAlive = isAttackerAlive;
    //    this.isDefenderAlive = isDefenderAlive;
    //}

    public CombatEvent()
    {
        eventType = "Combat";
        //return this;
    }



    public override string ToString()
    {
        string firstLine = SetOwnerColor(attacker, attacker.ToString()) + " " + "attacks" + " " + SetOwnerColor(defender, defender.ToString()) + ". ";
        string secondLine = SetOwnerColor(attacker, attacker.ToString()) + " deals " + SetStringColor(attackerHullDamage.ToString(), Color.red) + "Hull+" + SetStringColor(attackerShieldDamage.ToString(), Color.yellow) + "Shield. ";
        string thirdLine = SetOwnerColor(defender, defender.ToString()) + " deals " + SetStringColor(defenderHullDamage.ToString(), Color.red) + "Hull+" + SetStringColor(defenderShieldDamage.ToString(), Color.yellow) + "Shield. ";
        string forthLine = "";
        if(!isAttackerAlive && !isDefenderAlive)
        {
            forthLine = "Both side eliminated";
        }
        else if(!isDefenderAlive)
        {
            forthLine = SetOwnerColor(defender, defender.ToString()) + " is eliminated";
        }
        else if (!isAttackerAlive)
        {
            forthLine = SetOwnerColor(attacker, attacker.ToString()) + " is eliminated";
        }

        string message = firstLine + secondLine + thirdLine + forthLine;
        Debug.Log(message);
        return message;
    }

    public override string ToSimpleString()
    {

        return base.ToSimpleString();
    }

    public string SetOwnerColor( Owner owner , string s)
    {
        string newS;
        if(owner == Owner.PLAYER)
        {
            newS = SetStringColor(s, Color.green);
        }
        else if (owner == Owner.ENEMY) {
            newS = SetStringColor(s, Color.red);
        }
        else{
            newS = SetStringColor(s, Color.grey);
        }

        return newS;
    }

    //public string SetStringColor(string s, Color color )
    //{
    //    return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + s + "</color>";
    //}
}
