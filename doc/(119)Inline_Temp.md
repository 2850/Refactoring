# 簡介

## Inline_Temp (119)

## 將所有對變數的引用動作，替換為對它賦予的那個運算式本身

``` cs
double basePrice = anOrder.basePrice();
return (basePrice > 1000)
```

改成

``` cs
return (anOrder.basePrice() > 1000)
```

## 動機

多半是作為Repace Temp with Query(120)的一部分來使用，所以真正動機是用120。單獨使用此方式的狀況是：你發現某個暫時變數被賦予某個函式呼嘯的返回值。
