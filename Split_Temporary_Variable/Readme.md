# 簡介

## Split Temporary Variable (128) 剖解暫時變數

## 針對每次賦予值，創造一個獨立的、對應暫時變數

``` cs
double temp = 2 * ( height + width);
Console.WriteLine(temp);
temp = height * width;
Console.WriteLine(temp);
return (basePrice > 1000)
```

改成

``` cs
double readonly perimeter = 2 * ( height + width);
Console.WriteLine(pe4imeter);
double readonly area = height * width;
Console.WriteLine(area);
```

## 動機

每個變數止承擔一個責任。同一個暫時變數承擔兩件不同的事情，會令程式碼閱讀讀者糊塗。

如果迴圈變數、遞迴型的運算不再此限制之中。