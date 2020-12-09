using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCombat : MonoBehaviour
{
    public float attackSpeed;
    public float damage;
    public float explodePower;
    public LayerMask enemyLayer;
    public Camera fpsCamera;
    public Animator muzzleFlash;
    public Animator recoil;
    private float attackTimer;

    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            muzzleFlash.gameObject.SetActive(true);
            muzzleFlash.enabled = true;
            muzzleFlash.Play("MuzzleFlash", -1, 0);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (attackTimer < 0)
            {
                recoil.Play("Recoil",-1,0);
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, 100, enemyLayer))
                {
                    Collider[] hits = Physics.OverlapSphere(hit.point, 0.5f, enemyLayer);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        hits[i].GetComponent<RobotHealth>().TakeDamage(damage, fpsCamera.transform.forward, explodePower);
                    }
                }
                attackTimer = attackSpeed;

            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            muzzleFlash.StopPlayback();
            muzzleFlash.enabled = false;
            muzzleFlash.gameObject.SetActive(false);
        }
    }

}
