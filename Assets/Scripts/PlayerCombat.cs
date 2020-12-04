using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCombat : MonoBehaviour
{
    public Camera playerCamera;
    public Transform tearSpawnPos;
    public Tear fireballPrefab;
    public float maxHealth;
    public RectTransform healthBar;
    internal List<TearEffect> tearEffects = new List<TearEffect>();
    private float curHealth;
    float shootCooldown = 1;
    float shootTimer = 0;

    private void Awake()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        Shoot();
        shootTimer -= Time.deltaTime;
    }
    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        healthBar.sizeDelta = new Vector2(curHealth / maxHealth * 500, healthBar.sizeDelta.y);
        if (curHealth < 0)
        {
            Died();
        }
    }
    void Died()
    {
        SceneManager.LoadScene(0);
    }
    void Shoot()
    {
        if (shootTimer < 0)
        {
            bool leftMouseDown = Input.GetKey(KeyCode.Mouse0);
            if (leftMouseDown)
            {
                Tear tearGO = Instantiate(fireballPrefab, tearSpawnPos.position, Quaternion.identity);
                tearGO.Initialize(transform.forward,tearEffects);
                shootTimer = shootCooldown;
            }
        }
    }
}