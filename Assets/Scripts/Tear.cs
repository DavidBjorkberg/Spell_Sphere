using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    public float baseSpeed;
    public float explodePower;
    protected List<int> hitIndices = new List<int>();
    internal List<TearEffect> tearEffects = new List<TearEffect>();
    public LayerMask enemyLayer;
    internal Vector3 direction;
    protected virtual void Update()
    {
        Move();
    }
    virtual public void Move()
    {
        transform.position += direction * baseSpeed * Time.deltaTime;
        for (int i = 0; i < tearEffects.Count; i++)
        {
            tearEffects[i].OnMove(this);
        }
    }
    virtual public void Initialize(Vector3 direction, List<TearEffect> newTearEffects)
    {
        tearEffects.Clear(); 
        for (int i = 0; i < newTearEffects.Count; i++)
        {
            if (newTearEffects[i].ToString() == " (Explode)")
            {
                tearEffects.Add(ScriptableObject.CreateInstance("Explode") as TearEffect);
            }
            else if (newTearEffects[i].ToString() == " (RotatingTear)")
            {
                tearEffects.Add(ScriptableObject.CreateInstance("RotatingTear") as TearEffect);
            }
            else if (newTearEffects[i].ToString() == " (DoubleShot)")
            {
                tearEffects.Add(ScriptableObject.CreateInstance("DoubleShot") as TearEffect);
            }
            else if(newTearEffects[i].ToString() == " (Boomerang)")
            {
                tearEffects.Add(ScriptableObject.CreateInstance("Boomerang") as TearEffect);
            }
            else if(newTearEffects[i].ToString() == " (MachineGun)")
            {
                tearEffects.Add(ScriptableObject.CreateInstance("MachineGun") as TearEffect);
                GameManager.Instance.player.playerCombat.shootCooldown = 0.1f;
            }
        }

        this.direction = direction;
        transform.LookAt(transform.position + direction);
        for (int i = 0; i < tearEffects.Count; i++)
        {
            tearEffects[i].OnInitialize(this);
        }
        Destroy(gameObject, 10);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        bool shouldRunStandardHit = true;
        for (int i = 0; i < tearEffects.Count; i++)
        {
            shouldRunStandardHit = tearEffects[i].OnHit(this);
        }
        if (shouldRunStandardHit)
        {
            RobotHealth enemy = other.GetComponent<RobotHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1, transform.position, explodePower);
                hitIndices.Add(enemy.index);
            }
        }
    }
}
