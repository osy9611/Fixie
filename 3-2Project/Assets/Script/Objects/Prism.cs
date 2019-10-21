﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MonoBehaviour
{
    public Player player;
    public bool RazerON;
    RaycastHit2D hit;
    public Transform raserPos;
    public Transform LayHit;
    public Prism Obj;
    public Prism HitObj;
    public LineRenderer laser;

    public Transform HitPoint;
    public Transform StoreTrans;

    public float Damage;
    public Transform TargetPos;
    float Range;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        Range = Vector2.Distance(laser.gameObject.transform.position, TargetPos.position);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (RazerON)
        {
            laser.enabled = true;
            hit = Physics2D.Raycast(raserPos.position, raserPos.up, Range);
            laser.SetPosition(0, raserPos.position);
            if (hit.collider)
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    laser.SetPosition(1, hit.point);
                }
                if (hit.collider.gameObject.tag == "LightIn")
                {
                    if (Obj==null)
                    {
                        HitPoint = hit.collider.transform;
                        Obj = hit.collider.gameObject.transform.parent.GetComponent<Prism>();
                        Obj.HitObj = GetComponent<Prism>();
                        Obj.RazerON = true;
                    }
                    else if (HitPoint.position != hit.collider.transform.position)
                    {
                        HitPoint = null;
                        Obj.RazerON = false;
                        Obj = null;
                    }
                }
                else
                {
                    if(Obj!=null)
                    {
                        Obj.RazerON = false;
                        Obj = null;
                    }
                }
                if (hit.collider.gameObject.tag == "LightOut")
                {
                    if(Obj ==null)
                    {
                        Obj = hit.collider.gameObject.transform.parent.GetComponent<Prism>();
                    }
                    else
                    {
                        if (Obj.transform.position != hit.collider.gameObject.transform.position)
                        {
                            Obj.laser.enabled = false;
                        }
                    }
                    Obj = null;
                }
                if(hit.collider.gameObject.tag == "Obstacle")
                {
                    if(!player.SetItem)
                    {
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if(Obj !=null)
                {
                    Obj.RazerON = false;
                    Obj = null;
                }
                if(raserPos.position.y <0)
                {
                    laser.SetPosition(1, TargetPos.position);
                }
                else
                {
                    laser.SetPosition(1, TargetPos.position);
                }
            }
        }
        else
        {
            if (Obj != null)
            {
                Obj.RazerON = false;
                Obj = null;
            }
            laser.enabled = false;
        }
    }
}
