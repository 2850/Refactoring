# (206)Encapsulate_Field(封裝欄位)

## 簡介說明

Class 中存在一個public

將他宣告為private，並提供相應的存取函式(accessors)

``` cs
public string _name
```

轉換成

``` cs
private string _name;
public string getName(){return _name;}
public void setName(string arg){_name = arg;}
```

## 動機

物件導向首要原則就是封裝，或著叫做隱藏資料。

按照此原則，你絕對不應該把資料宣告為public，否則其他物件就有可能存取甚至修改這項資料，而擁有該資料的物件卻毫無察覺。

public 資料被看過是一種不好的做法，因為這樣會降低程式的模組化程度。

資料的使用或使用該資料的行為被集中在一起，一但情況發生變化，程式碼的修改就會簡單許多，而不是散落各地。

執行此方法之後也可以透過[(142) Move Method]((142)Move_Method.md)經快的將它移動到其他新物件去

### 後繼

在看這方法之前，我都覺得這方法很簡單很廢話，但其實經過這麼多年的撰寫才會發現

程式碼的雜亂往往就是在一開始的規劃就用嚴謹的態度去執行，只覺得變數寫得進去又可以取得就好

當下完全沒考慮到的就是【程式可讀性】，時至今日才發現，以前往往覺得很基礎不去做的事情

都是令我最頭痛的部分，現在我可以想像的好處有幾個

1. 統一管理：屬性散落各地，我真的不知道有多少個物件在使用他(即使現在的IDE已經做好提示的功能)，使用此法不僅集中管理還可以應對突發需求
2. 可讀性提升：回首過往的程式碼，一個物件宣告10~20屬性，真正使用到的頂多一半而已，而另外一半就成為我查看程式碼的髒東西
3. 封裝性：也因為第2點的好處，也盡可能減少不必要的修改，造成奇怪的Bug出現

再說一次，很簡單，但是非常重要。
再說一次，很簡單，但是非常重要。
再說一次，很簡單，但是非常重要。