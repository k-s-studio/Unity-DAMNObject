using UnityEngine;
using UnityEngine.UI;
using Assets.KsCode.DAMNObject.v1;
using Assets.KsCode.DAMNObject;

internal class Example : MonoBehaviour {
    //readonly GameObject obj = new(); //初始化過程是不能叫new GameObject()的唷
    public Transform canvas;
    public DamnModel damnModel = new() {
        name = "rowObject",
        components = {
            { typeof(RectTransform) },
            { typeof(HorizontalLayoutGroup), MyObj.HLayout}
        },
        children = {
            new(){
                name = "NameCell",
                components = {
                    { typeof(RectTransform) },
                    { typeof(Image) , MyObj.NameBg }
                },
                children = {
                    new(){
                        name = "NameText",
                        components = {
                            { typeof(RectTransform) },
                            { typeof(Text), MyObj.NameText }
                        }
                    },
                }
            },
            new("DataCell"){
                components = {
                    typeof(RectTransform),
                    { typeof(Image) , MyObj.DataBg },
                },
                children = {
                    new("DataText"){
                        components = {
                            type = typeof(Text),
                            id = MyObj.DataText
                        }
                    }
                }
            },
        }
    };
    public DamnConfig config = s => {
        var hLayout = s.GetComponent<HorizontalLayoutGroup>(MyObj.HLayout);
        hLayout.childAlignment = TextAnchor.MiddleCenter;
        (hLayout.childControlWidth, hLayout.childControlHeight) = (false, true);
        (hLayout.childScaleWidth, hLayout.childScaleHeight) = (false, false);
        (hLayout.childForceExpandWidth, hLayout.childForceExpandHeight) = (false, true);

        var nameText = s.GetComponent<Text>(MyObj.NameText);
        nameText.alignment = TextAnchor.MiddleCenter;

        var dataText = s.GetComponent<Text>(MyObj.DataText);
        dataText.alignment = TextAnchor.MiddleLeft;

        var nameCell = s.GetComponent<Image>(MyObj.NameBg);
        var dataCell = s.GetComponent<Image>(MyObj.DataBg);
        nameCell.type = dataCell.type = Image.Type.Sliced;
    };
    public DAMNPrefab prefab = new() {
        model = new() {
            name = "rowObject",
            components = {
                { typeof(RectTransform) },
                { typeof(HorizontalLayoutGroup), MyObj.HLayout}
            },
            children = {
                new(){
                    name = "NameCell",
                    components = {
                        { typeof(RectTransform) },
                        { typeof(Image) , MyObj.NameBg }
                    },
                    children = {
                        new(){
                            name = "NameText",
                            components = {
                                { typeof(RectTransform) },
                                { typeof(Text), MyObj.NameText }
                            }
                        },
                    }
                },
                new("DataCell"){
                    components = {
                        typeof(RectTransform),
                        { typeof(Image) , MyObj.DataBg },
                    },
                    children = {
                        new("DataText"){
                            components = {
                                type = typeof(Text),
                                id = MyObj.DataText
                            }
                        }
                    }
                }
            }
        },
        config = s => {
            HorizontalLayoutGroup hLayout = s.GetComponent<HorizontalLayoutGroup>(MyObj.HLayout);
            hLayout.childAlignment = TextAnchor.MiddleCenter;
            (hLayout.childControlWidth, hLayout.childControlHeight) = (false, true);
            (hLayout.childScaleWidth, hLayout.childScaleHeight) = (false, false);
            (hLayout.childForceExpandWidth, hLayout.childForceExpandHeight) = (false, true);

            var nameText = s.GetComponent<Text>(MyObj.NameText);
            nameText.alignment = TextAnchor.MiddleCenter;

            var dataText = s.GetComponent<Text>(MyObj.DataText);
            dataText.alignment = TextAnchor.MiddleLeft;

            var nameCell = s.GetComponent<Image>(MyObj.NameBg);
            var dataCell = s.GetComponent<Image>(MyObj.DataBg);
            nameCell.type = dataCell.type = Image.Type.Sliced;
        }
    };
    private DAMNObject obj1, obj2;
    void Start() {
        obj1 = damnModel.Instantiate(config);
        obj2 = prefab.Instantiate();

        obj1.transform.SetParent(canvas);
        obj2.transform.SetParent(canvas);
    }
    void OnDestroy() {
        DestroyImmediate(obj1);
        DestroyImmediate(obj2);
    }
}

internal enum MyObj {
    HLayout,
    NameBg, NameText,
    DataBg, DataText,
}

