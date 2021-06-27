# Rename_Method (重新命名函式)

273、275、277可以一起講

## 動機

將複雜的處理過程分解成小函式，如果做得不好會搞不清楚每個小函式的用途。

好的名稱就非常重要，首先先考慮怎樣寫註解，然後想辦法把註解變成一個函式名稱

好的名稱可以節省很多時間，壞的名稱只能憑你的經驗(往往就是浪費時間的開始)

起名稱非常重要，每個人應該要學會，如果修改參數的順序有幫助，就大膽的去做吧。

還有兩個方法可以去運用[(275)Add_Parameter]((275)Add_Parameter.md)和[(277)Remove_Parameter]((277)Remove_Parameter.md)這兩項武器

## 作法

1. 確認要修改的名稱是不是有被使用過
2. 宣告新的名稱，把就的程式碼搬過去
3. 測試

## 範例

### 範例1

```cs
public string getTelephoneNumber(){
    return("(" + _officeAreaCode + ")" + _officeNumber);
}

```

改成

```cs
public string getTelephoneNumber(){
    return getOfficeTelephoneNumber();
}

```cs
public string getOfficeTelephoneNumber(){
    return("(" + _officeAreaCode + ")" + _officeNumber);
}

把使用過getTelephoneNumber()的都改成getOfficeTelephoneNumber()

在經過編譯跟測試沒問題後，刪掉getTelephoneNumber()

```

### 後繼

商店最常遇到的問題就是一個函式雲做了非常多的動作

現在最迫切需要的技巧莫過於此，動作非常簡單，但需要大家下去執行