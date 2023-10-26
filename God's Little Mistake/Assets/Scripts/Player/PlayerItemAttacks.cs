using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerItemAttacks : Singleton<PlayerItemAttacks>
{

    [Header("Ability")]
    bool isOnCoolDown;
    int abilitySlot;


    [Header("Animation")]
    bool chargeUpAnimationStart;

    [Header("Slug Legs")]
    bool slugLegsEquipped;
    public GameObject slugLeg_trail;
    public float timeBetweenTrail;
    public float destroyTime;
    bool spawnedTrail = false;

    [Header("Slug Eyes")]
    public bool slugEyesEquipped;
    bool slowProjectile;
    [SerializeField]
    public float slowDuration;
    [SerializeField]
    public float slowPercent; //decimal

    [Header("Basic Dash")]
    public Ease dashEase;
    bool dashing;
    float dashStartTime;
    public float dashDuration;
    public float dashPower;
    public GameObject dashParticles;

    [Header("Tripod")]
    bool tripodLegsEquipped;
    public int tripodCooldownTime;

    [Header("Human Fist")]
    public int humanFistCooldownTime;
    public float humanFistChargeUpTime;
    public float humanFistDamage;
    public bool gutpunch;

    [Header("Big Eyes")]
    Collider[] enemiesCharmed;
    public GameObject enemyLineStart;
    public int AOEradius;
    public LayerMask enemyMask;
    public float charmDuration;
    public int bigEyesCooldownTime;

    [Header("Hover Legs")]
    bool hovering;
    public float hoverLegsDuration;
    public int hoverLegsCooldownTime;

    [Header("Wolf Claw")]
    public bool wolfClawsEquipped;
    bool applyBleed;
    public float bleedDuration;
    public float bleedTickDmg;

    [Header("Pea Shooter")]
    public float peaShooerChargeUpTime;
    public int peaShooerCooldownTime;
    public float RPEAGexplosionRadius;
    public float RPEAGexplosionDmg;
    public GameObject bigPea;


    [Header("Ram Horns")]
    public int ramHornsCooldownTime;
    public float ramHornsStunDuration;
    public float ramHornsDamage;
    public bool ramming;
}


//    // Update is called once per frame
//    void Update()
//    {
//        #region Basic Dash
//        if (dashing)
//        {
//            dashParticles.SetActive(true);
//            print("Dash");
//            float timeSinceDash = Time.time - dashStartTime;

//            //turn off if u dont want trail render
//            //turn on trail render
//            _PC.GetComponent<TrailRenderer>().enabled = true;

//            if (timeSinceDash >= dashDuration)
//            {
//                dashing = false;
//            }
//            else
//            {
//                float knockbackProgress = timeSinceDash / dashDuration;

//                var dashDiresction = _PC.move * dashPower;
//                dashDiresction = new Vector3(dashDiresction.x, 0, dashDiresction.z);

//                //_PC.transform.DOMove(dashDiresction, 1).SetEase(dashEase);
//                _PC.controller.Move(dashDiresction * Time.deltaTime); //(WORKS BUT VERY SNAPPY)

//            }
//        }
//        else
//        {
//            //turn off if u dont want trail render
//            _PC.GetComponent<TrailRenderer>().enabled = false;
//            dashParticles.SetActive(false);
//        }

//        #endregion



//        #region Passive Abilities
//        //SNAIL LEGS
//        if (_PC.isMoving && slugLegsEquipped)
//        {
//            SlugLegs(timeBetweenTrail);
//        }

//        if(hovering)
//        {
//            //simple bobbing up and down motion
//        }

//        #endregion

//        #region Button Activated Abilities

//        //if (Input.GetKeyDown(KeyCode.Space))
//        //{
//        //    if (!isOnCoolDown)
//        //    {
//        //        for (int i = 0; i < _PC.playerInventory.Count; i++)
//        //        {
//        //            //check if primary active and has ability
//        //            if (_PC.playerInventory[i].active && _PC.playerInventory[i].hasActiveAbility)
//        //            {
//        //                abilitySlot = i;

//        //                //tripod dash
//        //                if (_PC.playerInventory[i].ID == 6)
//        //                {

//        //                    TripodLegs();

//        //                }

//        //                if (_PC.playerInventory[i].ID == 3)
//        //                {
//        //                    RamHorns();
//        //                }
                        
//        //                if (_PC.playerInventory[i].ID == 11)
//        //                {
//        //                    BigEyes();
//        //                }
                        
//        //                if (_PC.playerInventory[i].ID == 10)
//        //                {
//        //                    HoverLegs();
//        //                }
                        
//        //                if (_PC.playerInventory[i].ID == 8)
//        //                {
//        //                    _PC.enableMovement = false;
//        //                    ExecuteAfterSeconds(humanFistChargeUpTime, () => HumanFist());
//        //                }
                        
//        //                if (_PC.playerInventory[i].ID == 9)
//        //                {
//        //                    //start charge up animation
//        //                    if (!chargeUpAnimationStart)
//        //                    {
//        //                        chargeUpAnimationStart = true;
//        //                        _PC.itemsAnimForward[abilitySlot].SetBool("Charging", true);

//        //                        //if (_PC.itemsAnimBack[abilitySlot] != null) _PC.itemsAnimBack[abilitySlot].SetBool("Charging", true);

//        //                        //_PC.itemsAnimLeftSide[abilitySlot].SetBool("Charging", true);
//        //                        //_PC.itemsAnimRightSide[abilitySlot].SetBool("Charging", true);
//        //                    }

//        //                    _PC.enableMovement = false;
//        //                    ExecuteAfterSeconds(peaShooerChargeUpTime, () => PeaShooter());
//        //                }


//        //            }
//        //        }


//        //    }

//        //}


//        #endregion
//    }

//    void PeaShooter()
//    {
//        _PC.itemsAnimForward[abilitySlot].SetBool("Charging", false);

//        _PC.itemsAnimLeftSide[abilitySlot].SetBool("Charging", false);
//        _PC.itemsAnimRightSide[abilitySlot].SetBool("Charging", false);

//        chargeUpAnimationStart = false;


//        isOnCoolDown = true;
//        //turn on cooldown UI
//        ActivateCooldownUI(abilitySlot, peaShooerCooldownTime);

//        _PC.enableMovement = true;

//        //_PC.FireProjectile(bigPea, 300, 1, _PC.projectileRange);

//        ExecuteAfterSeconds(peaShooerCooldownTime, () => isOnCoolDown = false);

//    }

//    void HoverLegs()
//    {
//        print("HOVERING");

//        isOnCoolDown = true;
//        //turn on cooldown UI
//        ActivateCooldownUI(abilitySlot, hoverLegsCooldownTime);

//        hovering = true;
//        _PC.canFloat = true;
//        ExecuteAfterSeconds(hoverLegsDuration, () => ResetHover());
//        ExecuteAfterSeconds(hoverLegsCooldownTime, () => isOnCoolDown = false);

//    }

//    void ResetHover()
//    {
//        hovering = false;
//        _PC.canFloat = false;
//    }

//    void BigEyes()
//    {
//        isOnCoolDown = true;

//        //play Animation
//        _PC.itemsAnimForward[abilitySlot].SetTrigger("Attack");

//        if (_PC.itemsAnimBack[abilitySlot] != null) _PC.itemsAnimBack[abilitySlot].SetTrigger("Attack");

//        _PC.itemsAnimLeftSide[abilitySlot].SetTrigger("Attack");
//        _PC.itemsAnimRightSide[abilitySlot].SetTrigger("Attack");

//        //turn on cooldown UI
//        ActivateCooldownUI(abilitySlot, bigEyesCooldownTime);

//        enemiesCharmed = Physics.OverlapSphere(_PC.transform.position, AOEradius,enemyMask);
//        foreach (var collider in enemiesCharmed)
//        {
//            collider.gameObject.GetComponent<BaseEnemy>().enemyState = BaseEnemy.EnemyState.Charmed;
//        }

//        ExecuteAfterSeconds(charmDuration, () => TurnOffCharm());
        
//        ExecuteAfterSeconds(bigEyesCooldownTime, () => isOnCoolDown = false);
//    }

//    void TurnOffCharm()
//    {
//        foreach (var collider in enemiesCharmed)
//        {
//            collider.gameObject.GetComponent<BaseEnemy>().enemyState = BaseEnemy.EnemyState.Patrolling;
//        }
//    }

//    void HumanFist()
//    {
//        //turn off movement while winding up

//         _PC.enableMovement = true;

//        Dash(5, 0.3f);
//        gutpunch = true;

//        //Cooldown
//        isOnCoolDown = true;
//        //turn on cooldown UI
//        ActivateCooldownUI(abilitySlot, humanFistCooldownTime);
//        //OPTIONAL: Invunerable while dashing
//        _PC.immortal = true;
//        ExecuteAfterSeconds(0.3f, () => FistReset());
//        ExecuteAfterSeconds(humanFistCooldownTime, () => isOnCoolDown = false);

//    }

//    void FistReset()
//    {
//        _PC.immortal = false;
//        gutpunch = false;

//    }

//    void RamHorns()
//    {
//        print("RAMING");
//        Dash(5, 0.3f);
//        ramming = true;

//        //Cooldown
//        isOnCoolDown = true;
//        //turn on cooldown UI
//        ActivateCooldownUI(abilitySlot, ramHornsCooldownTime);

//        //OPTIONAL: Invunerable while dashing
//        _PC.immortal = true;
//        ExecuteAfterSeconds(0.3f, () => RamReset());
//        ExecuteAfterSeconds(ramHornsCooldownTime, () => isOnCoolDown = false);

//    }

//    void RamReset()
//    {
//        _PC.immortal = false;
//        ramming = false;
//    }

//    void TripodLegs()
//    {
//        Dash(5, 0.3f);



//        //CoolDown
//        isOnCoolDown = true;

//        //turn on cooldown UI
//        ActivateCooldownUI(abilitySlot, tripodCooldownTime);


//        //OPTIONAL: Invunerable while dashing
//        _PC.immortal = true;
//        ExecuteAfterSeconds(0.3f, () => _PC.immortal = false);

//        ExecuteAfterSeconds(tripodCooldownTime, () => isOnCoolDown = false);
//    }

//    /// <summary>
//    /// Dashes player in direction they are walking
//    /// </summary>
//    /// <param name="_dashPower"></param>
//    /// <param name="_dashDuration"></param>
//    public void Dash(float _dashPower, float _dashDuration)
//    {
//        dashPower = _dashPower;
//        dashDuration = _dashDuration;
//        dashStartTime = Time.time;
//        dashing = true;
//        print("dashing set true");

//        //start cooldown here
//    }

//    /// <summary>
//    /// Creates trial of toxic slime behind player
//    /// </summary>
//    void SlugLegs(float _timeBetweenTrail)
//    {
//        if(!spawnedTrail)
//        {
//            spawnedTrail = true;
//            GameObject trail = Instantiate(slugLeg_trail, _PC.transform.position, Quaternion.identity);
//            ExecuteAfterSeconds(_timeBetweenTrail, () => spawnedTrail = false);
//        }
//    }

//    public bool WolfClaw()
//    {
//        var r = Random.Range(0, 3);
//        if (r == 1) slowProjectile = true;
//        return slowProjectile;
//    }

//    public bool SlugEyes()
//    {
//        var r = Random.Range(0, 3);
//        if (r == 1) applyBleed = true;
//        return applyBleed;
//    }

//    private Tween TweenValue(float endValue, float time, float tweenValue)
//    {
//        var speedTween = DOTween.To(() => tweenValue, (x) => tweenValue = x, endValue, time);
//        return speedTween;
//    }

//    public void PassiveAbilityItemCheck()
//    {
//        slugLegsEquipped = false;
//        slugEyesEquipped = false;
//        tripodLegsEquipped = false;
//        wolfClawsEquipped = false;
//        ////check if slug legs are on player
//        //foreach (var item in _PC.playerInventory)
//        //{
//        //    if (item.ID == 15) slugLegsEquipped = true;
//        //    if (item.ID == 5) slugEyesEquipped = true;
//        //    if (item.ID == 7) wolfClawsEquipped = true;
//        //}

//    }

//    void ActivateCooldownUI(int _slot, int _cooldown)
//    {
//        switch(_slot)
//        {
//            case 0:

//                _HUD.isCooldown1 = true;
//                _HUD.SetCooldownSlo1(_cooldown);

//                break;
//            case 1:

//                _HUD.isCooldown2 = true;
//                _HUD.SetCooldownSlo2(_cooldown);

//                break;
//            case 2:
//                _HUD.isCooldown3 = true;
//                _HUD.SetCooldownSlo3(_cooldown);
//                break;
//            case 3:
//                _HUD.isCooldown4 = true;
//                _HUD.SetCooldownSlo4(_cooldown);
//                break;
//            case 4:

//                _HUD.isCooldown5 = true;
//                _HUD.SetCooldownSlo5(_cooldown);

//                break;
//            case 5:
//                _HUD.isCooldown6 = true;
//                _HUD.SetCooldownSlo6(_cooldown);
//                break;


//                //if(Input.GetKeyDown(KeyCode.E))
//                //{
//                //   
//                //}

//                //hit.GameObject.GetComponent<ScriptName>().FunctionName();
//        }
//    }

//}
