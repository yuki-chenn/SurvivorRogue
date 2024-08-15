using Survivor.Template;
using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TestLoadXml : MonoBehaviour
{

    public Text txt;
    public Image img;

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 50; // 设置文字大小

        // 创建一个按钮，如果按钮被点击，返回 true
        if (GUI.Button(new Rect(500, 300, 200, 100), "读取xml", buttonStyle))
        {
            loadxml();
        }
        if (GUI.Button(new Rect(500, 500, 200, 100), "读取图片", buttonStyle))
        {
            loadimg();
        }
        if (GUI.Button(new Rect(500, 700, 200, 100), "进游戏", buttonStyle))
        {
            SceneManager.LoadScene("MenuScene");
        }
        if (GUI.Button(new Rect(500, 900, 200, 100), "测试道具", buttonStyle))
        {
            var item = ItemFactory.GetItemByID(1);
            item.OnGet();
            item.OnDiscard();

            item = ItemFactory.GetItemByID(2);
            item.OnGet();
            item.OnDiscard();
        }
        if (GUI.Button(new Rect(500, 1100, 200, 100), "测试Buff", buttonStyle))
        {
            var buff = BuffFactory.GetBuffByID(1);
            buff.OnAdd();
            buff.OnKill();
            buff.OnTick();
            Debug.Log(buff.Info.RemoveType);
        }
    }

    private void loadxml()
    {
        var heros = TplUtil.GetTplMap<HeroTpl, HeroTplInfo>();
        string s = "";
        foreach (var hero in heros.Values)
        {
            s += hero.ToString() + "\n";
        }
        txt.text = s;
    }

    private void loadimg()
    {
        //var index = Random.Range(0, AssetManager.Instance.imgs.Count);
        //img.sprite = AssetManager.Instance.imgs[index];
    }
}
