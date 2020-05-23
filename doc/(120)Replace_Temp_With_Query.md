# 簡介

## 將運算式提煉到一個獨立函式，將這個暫時變數的所有【被引用點】替換為【對新函式的呼嘯】 (120)

## 簡介說明

``` cs
doube basePrice = quantity * itemPrice;
if (basePrice > 1000){
    return basePrice * 0.95;
}
else{
    return basePrice * 0.98;
}
```

轉換成

``` cs
if (basePrice() > 1000){
    return basePrice * 0.95;
}
else{
    return basePrice * 0.98;
}

double basePrice(){
    return quantity * itemPrice;
}
```

## 動機

暫時變數的問題在於，它是暫時的，而且只能在所屬的函式內使用。如果把它轉換成Query方式，表示同一個Class都可以獲得這份資訊。這會給你有很大的幫助，使你能夠為這個class寫出更清晰的程式碼。

在使用[(120)](/doc/(120)Replace_Temp_With_Query.md)之前，往往是因為你用[(110)](/doc/(110)Extract_Method.md)之前不可少的步驟，因為區域變數使你很難把程式碼提煉出來，所以你應該盡可能的替換成[(120)](/doc/(120)Replace_Temp_With_Query.md)的查詢式

重構這個手法的最直率的狀況：暫時變數只被賦予一次，或著復職給暫時變數的運算式不受其他條件影響。較複雜的狀況下，可能要先運用[(128)](/doc/(128)Split_Temporary_Variable.md)或(279)使狀況變得簡單一點，然後在替換暫時變數。

## 作法

1. 找出只被賦值一次的暫時變數
   * 如果暫時變數被賦予值超過一次，考慮使用 (128)將它分割成多個變數
2. 將該變數宣告為ReadOnly
3. 編譯，確保只給一次成果是沒問題的
4. 將【對該暫時變數賦予值】的述句等號右側部分提煉到獨立函式中。
   * 首先將函式宣告為Private。日後你可能發現有更多class需要使用它，到時可輕易修改
   * 確保提煉出來的函是無連帶影響，如果有影響，就使用(279)

## 範例

### 範例1

``` cs
double getPrice(){
    int basePrice = quantity * itemPrice;
    double discountFactor;
    if (basePrice > 1000) discountFactor = 0.95;
    else discountFactor = 0.98;
    return baseZPrice * discountFactor;
}
```

``` cs
double getPrice(){
    return basePrice() * discountFactor();
}

private double discountFactor(){
    if (basePrice() > 1000) return 0.95;
    else return 0.98;
}

private int basePrice(){
    return quantity * itemPrice;
}
```

### 後繼

重構後，會發現，basePrice()原本只有自己會用到的地方，變成discountFactor()也使用到了，而且主要商業邏輯getPrice()變成清楚明瞭。