using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BaseItem
{
    public virtual int ID { get; set; }

    public ItemTplInfo Info { get { return TplUtil.GetItemMap()[ID]; } }

    protected PlayerAttribute attr { get { return GameManager.Instance.gameData.playerAttr; } }

    protected Player player { get { return GameManager.Instance.Player; } }

    public virtual void OnGet()
    {
        Debug.Log("得到道具:" + Info.Name);
    }

    public virtual void OnDiscard()
    {
        Debug.Log("丢弃道具:" + Info.Name);
    }




}
