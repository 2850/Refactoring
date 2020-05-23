# 簡介

## Introduce Explanining Variable (124)

## 將複雜運算式(或其中一部分) 的結果放進一個暫時變數，以此變數名稱來解釋運算式用途

``` cs
if ( (platform.toUpperCase().indexOf("MAC") > -1 ) && 
    (browser.toUppserCase().indexOf("IE") > -1) &&
    wasInitialized() && resize > 0)
{

    // do something
}
```

轉換成

``` cs
boolean Readonly isMacOs = platform.toUpperCase().indexOf("MAC") > -1;
boolean Readonly isIEBrowser = browser.toUppserCase().indexOf("IE") > -1;
boolean Readonly wasResized = resize > 0;

if (isMacOs && isIEBrowser && wasInitialized() && wasResized){

    // do something
}
```

## 動機

運算式可能式非常複雜而難以閱讀的。這種情況下，暫時變數可以幫助將運算式分解為比較容易管理的形式。

在條件式的狀況下此方法特別有效，透過良好的命名暫時變數來解釋對應條件條毽子具的意義。使用這個方法的另一個狀況式，在較常演算法中，可以運用暫時邊數來解釋每一部運算的意義。

## 作法

1. 宣告ReadOnly暫時變數，將待分解的複雜運算式中呃一部分動作結果，賦予值給它
2. 將運算式中的【運算結果】，替算為上述的變數

## 範例

### 範例1

### 範例2

``` cs
double price(){
    return quantiy * itemPrice - 
    Math.max(0, quantity - 500) * itemPrice * 0.5 + 
    Math.min(quantity * itemPrice * 0.1, 100.0);
}
```

改成

```cs
double price(){
    double readonly basePrice = quantity * itemPrice;
    double readonly quantityDiscount = Math.max(0, quantity - 500) * itemPrice * 0.5;
    double readonly shipping = Math.min(quantity * itemPrice * 0.1, 100.0);

    return basePrice - quantityDiscount + shipping;
}
```

### 使用(110) Extract_Method 實作

``` cs
double price(){
    return quantiy * itemPrice - 
    Math.max(0, quantity - 500) * itemPrice * 0.5 + 
    Math.min(quantity * itemPrice * 0.1, 100.0);
}

```cs
double price(){
    return basePrice() - quantityDiscount() - shipping();
}

private double basePrice(){
    return quantity * itemPrice;
}

private double quantityDiscount(){
    return Math.max(0, quantity - 500) * itemPrice * 0.5;
}

private double shipping(){
    return Math.min(quantity * itemPrice * 0.1, 100.0);
}
```

### 後繼

(110)Extract_Method 與 (124)Introduce Explaining Variabl 之間得抉擇

我會比較喜歡用110的方式，而且會先用Private;如果其他地方要用在做修正即可，而且在多半的狀況下110的方式比較快。

所以(124)的方法多半在 (110)方式並沒有比較快的狀況下使用，有時候處理的範圍太大的時候，用110實在很累人，或許過程中可以用120的方式解決，在思考怎樣抽換。

