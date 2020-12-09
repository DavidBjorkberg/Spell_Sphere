using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCombat : MonoBehaviour
{
    public FPSCamera playerCamera;
    public Transform tearSpawnPos;
    public Tear fireballPrefab;
    public float maxHealth;
    public Animator muzzleFlash;
    public Animator recoil;
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
        shootTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && !playerCamera.interacting)
        {
            if (shootTimer < 0)
            {
                recoil.Play("Recoil", -1, 0);
                Shoot();

            }
        }
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
                tearGO.Initialize(playerCamera.transform.forward, tearEffects);
                shootTimer = shootCooldown;
            }
        }
    }
}