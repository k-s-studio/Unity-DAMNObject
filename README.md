DAMNObjet 
===
###### Delegate And Model Notation Object

- Array.Append() 使用記憶體不靈活 棄用
- ~~可以使用yield return自製列舉器了，有要更新ToGameObject()嗎?~~
- ~~以我的資料性質其實適合用HashSet試試?~~
- 如果把Blueprint序列化就能解決物件無法保存的問題嗎
- Inspector 宣告Blueprint參數會出現ToGameObject()按鈕?
- [x] struct的使用
- [ ] Config使用
- [ ] 編輯器模式運行的測試
- [ ] 不同blueprint生成的物件不會互相干擾的測試
- [x] 修正子物件不在字典裡的問題
- [x] 確認關閉編輯器之後字典鍵值去留
- [ ] 重複對同一個Blueprint使用ToGameObject()，緩存遊戲物件的優化
-----
1. 把indexer, element initializer, add等初始化的符號幾乎摸了個遍，最後決定用使用3.ver1並等待.NET語法升級
3. 做了PropertyDrawer發現複寫`VisualElement CreatePropertyGUI(SerializedProperty property) `的方式會在CustomEditor中變成`No GUI implemented`因為是不同Toolkit(多數時候不相容)
5. 做了可序列化的KeyCompPair要解決字典在編輯器的序列化，但SerializeCallback每幀調用，每幀更新List有效能擔憂，最後直接讓CustomEditor讀取target的資料，沒有序列化。
9. 可以使用yield return自製列舉器了
99. 如果先Cache gameobject.transform，添加RectTranform之後原先的cache會變成null。
10. GameObject clone之後字典值會消失，GameObject.Instantiate()過程牽涉序列化是真的，看來KeyCompPair還是有用處了。

大概是這樣。

最好是執行MonoBehaviour中的=new()，但又有Inspector的按鈕
* 目前將內部欄位全部NonSerialize就是一種方法，說到底讓=new()資料遺失的是內部List用了SerializeReference
* 萬一要用Inspector編輯就需要序列化，想序列化就只能用SerializeReference
* ...啊如果可以編輯之後，要保存編輯過的內容不就要覆蓋掉=new()。
* 也就是說=new()和Inspector編輯不是不能共存，但有Inspector編輯後=new()意義就不大了➡要考慮的是Inspector編輯後的資料能夠完全用序列化保存。

待處理:
- cache生成物件，但不會把改動一起複製
- 按按鈕時套用config
    1. ~將Blueprint&config包一起的類~
    2. ~將config外面包一個類~
    3. ~Reflection取得所有鄰近的Action\<Spine\>~
    4. 要Action\<Spine\>還是新定義一個專用delegate?
- 整理一下code
    - root和child分開class，但有繼承關係?
    - ComponentLst和單獨的可以分開吧，用同一個介面
    - 分檔案
- ~SerializeReference欄位不是Blueprint，但內部含有Blueprint會出錯嗎~ 沒錯，和SerializeReference完全不相容