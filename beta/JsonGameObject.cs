using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonGameObject : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject obj = new JJJ() {
        name = "rootObject",
        components = {
            typeof(RectTransform),
            typeof(Text)
        },
        children = {
            new(){
                name = "rootObject",
                components = {
                    typeof(RectTransform),
                    typeof(Text)
                },
                children= {
                    new(){ },
                    new(){ }
                }
            },
            new(){
                name = "rootObject",
                components = {
                    typeof(RectTransform),
                    typeof(Text)
                },
                children = {
                    new(),
                    new(){ }
                }
            }
        }
    }.ToGameObject();
    GameObject obj2 = JJJ.JJJJ(
        name: "NewGameObject",
        addons: new[]
        {
            typeof(RectTransform),
            typeof(Text)
        },
        children: new[] {
            JJJ.JJJJ(
                name: "NewGameObject",
                addons: new[] {
                    typeof(RectTransform),
                    typeof(Text)
                },
                children: new[]{
                    JJJ.JJJJ()
                }
            ),
            JJJ.JJJJ(
                name: "NewGameObject",
                addons: new[] {
                    typeof(RectTransform),
                    typeof(Text)
                },
                children: new[]{
                    JJJ.JJJJ()
                }
            )
        },
        child: new() {
            (
                name : "rootObject",
                components :  new[]{
                    typeof(RectTransform),
                    typeof(Text)
                },
                children : new[]{
                    new JJJ(){ },
                    new JJJ(){ }
                }
            )
        }
    ).ToGameObject();
    void Start() {
        GameObject obj3 = new JJJ(
            name: "Obj3",
                //components:
                (typeof(Text), "Text1"),
                (typeof(Text), "Text1")

        ).ToGameObject();
    }

    // Update is called once per frame
    void Update() {

    }
}

internal static class Extension {
    public static void Add(this Type[] t, Type tt) {
        Debug.Log(tt);
    }
    // public static void Add(this Type[] t, params Type[] tt) {
    //     Debug.Log(tt);
    // }
    public static void Add(this Type[] t, Type[] t1, Type[] t2) {
        Debug.Log(t1);
    }
    public static void Add(this JJJ[] t, JJJ tt) {
        Debug.Log(tt);
    }
    public static void Add(this Component[] t, Component tt) {
        Debug.Log(tt);
    }
    public static void Add(this Component t, Component tt) {
        Debug.Log(tt);
    }
    public static void Add(this (string s, int i) t, (string s, int i) tt) {
        Debug.Log(tt);
    }
}

internal class JJJ {
    public string name;
    public Type[] components;
    public Component[] addons;
    public JJJ[] children;
    public List<(string name, Type[] components, JJJ[] children)> child;
    public GameObject ToGameObject() => new();
    // public static GameObject MakeJJJObject() {
    //     GameObject obj = new GameObject();
    // }
    public int Test() => 1;
    public string Test(int i = 1) => "1";
    public static JJJ JJJJ(string name = "NewGameObject", Type[] addons = null, JJJ[] children = null, List<(string name, Type[] components, JJJ[] children)> child = null) {
        return default;
    }
    public JJJ(string name = "New GameObject", params (Type t, string s)[] components) {}
}

internal struct MyCollection : IEnumerable<(string s, int i)> {
    private List<(string s, int i)> items;

    public void Add(string s = "", int i = 0) {
        items.Add((s, i));
    }

    public IEnumerator<(string s, int i)> GetEnumerator() {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
    public MyCollection(int i) {
        items = new List<(string s, int i)>();
    }
}