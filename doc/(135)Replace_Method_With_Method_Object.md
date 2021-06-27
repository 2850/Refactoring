# (135)Replace_Method_With_Method_Object

## 將這個函式放近一個單獨物件中，如此一來區域變數就成了物件內的欄位。然後你可以在同一個物件中將這個大型函式分解為數個小型函式

``` mermaid
classDiagram

Order ..> PriceCalculator
Order : Price()
PriceCalculator --> Order
PriceCalculator : int primaryBasePrice
PriceCalculator : int SecondaryBasePrice
PriceCalculator : int tertiaryBasePrice
PriceCalculator : compute()
```

## 動機

書中不斷向讀者強調小型函式的優美。只要將相對獨立的程式碼從大型函式中提煉出來，就可以大大提升程式碼可讀性。

但區域變數的存在會增加函式分解難度。除了用[(120)](/doc/(120)Replace_Temp_With_Query.md)可以減輕負擔，，但有時候你會發現根本無法拆解。這種情況只能使用目前這大絕招。

此方法會將所有的區域變數都變成一個函式物件的欄位，然後你就可以針對新物件使用[(110)](/doc/((110)Extract_Method.md))，創造新函式，從原本大型拆解變短。

## 作法

1. 建立一個新的Class，根據【待被處理的函式】的用途命名
2. 在新Class建立欄位，用以保存原先大型函式所駐物件。同時針對舊函式每個暫時變數和每個參數都建立變數
3. 在新Class寫一個建構式，接收原物件及原函式的所有參數
4. 在新class建立新函式Compute()
5. 將原函式的程式碼copy到compute()中

## 範例

書中範例未必很完美，因為用這到這個方法的狀況通常都超級複雜，所以只能示意，但範例已經點出具體作法，不妨礙理解

### 範例1

``` cs

class Account
    int gamma (int inputVal, int quantity, int yearToDate){
        int importantValue1 = (inputVal * quantity) + delta();
        int importantValue2 = (inputVal * yearToDate) + 100;
        if ((yearToDate - importantValue1) > 100){
            importantValue2 _= 20;
            int importantValue3 = importantValue2 * 7;

            return importantValue3 -2 * importanValue1;
        }
    }
```

轉換第一步

``` cs
Class Gamma {
    private ReadOnly Account _account;
    private int inputVal;
    private int quantity;
    private int yearToDate;
    private int importantValue1;
    private int importantValue2;
    private int importantValue3;
}
```

步驟二：建立建構式

```cs
 Gamma (Account source, int inputValArg, int quantityArg, int yearToDateArg){
     _account = source,
     inputVal = inputValArg;
     quantity = quantitiyArg;
     yearToDate = yearToDateArg;
 }
```

步驟三：把原本還是搬到compute()

``` cs
 int compute(){
    int importantValue1 = (inputVal * quantity) + delta();
    int importantValue2 = (inputVal * yearToDate) + 100;
    if ((yearToDate - importantValue1) > 100){
        importantValue2 _= 20;
        int importantValue3 = importantValue2 * 7;

        return importantValue3 -2 * importanValue1;
    }
 }
```

步驟四：在原本的位置改成呼叫新的函式

```cs
int gamma(int inputVal,int quantity,int yearToDate){
    return new Gamma(this, inputVal, quantity, yearToDate).compute();
}
```

### 後繼

這就是這個方法最重要的原則。它帶來的好處是：現在我可以輕鬆地對compute()函式採取[(110)](/doc/(110)Extract_Method.md)，不必擔心引數傳遞。

再根據使用135方法後，重構一次(110)

```cs
int compute(){
    int importantValue1 = (inputVal * quantity) + delta();
    int importantValue2 = (inputVal * yearToDate) + 100;
    importantThing();
    int importantValue3 = importantValue2 * 7;
    return importantValue3 -2 * importanValue1;
 }

 void importantThing(){
    if ((yearToDate - importantValue1) > 100){
        importantValue2 _= 20;
    }
 }
```
