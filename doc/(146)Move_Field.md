# 簡介

## Move Field (搬移欄位)

## 在Target Class 建立一個 new field，修改 source field 的所有用戶，令他們改用new field

``` cs
classDiagram
class Class1{
   int aField
}

class Class2{

}

```

轉換成

``` cs
classDiagram
class Class1{

}

class Class2{
   int aField
}


```

## 動機

在classes之間移動狀態和行為，這是重構避不可少的措施。這星期合理，下星期變成不再正確。這沒問題;如果你沒遇過這種狀況，才真的有問題。

對於一個Field，在所駐Class之外的另一個class中有更多函式使用了它，我就會考慮搬移Field。

==使用Extract Class(149)，我會先搬移Field在搬移函式。==

## 作法

1. 如果field是public，首先使用[Encapsulate Field(206)](/doc/(206)Encapsulate_Field.md)將它封裝起來。
   如果你可能移動那些頻繁存取該field的函式，或許多函式存取某個field，先使用[Self Encapsulate Field(171)](/doc/(206)Encapsulate_Field.md)也許有幫助
2. 在target class建立與source field相同的field，便同時建立get/set函式
3. 決定如何在source object 中引用target  object
4. 將所有的source field替換成 對target適當函式的呼叫
    * 如果讀取該變數，就替換成用get的函式，設定就替換用set的函式
    * 如果source field 不是private，就必須在source class的所有subclasses中搜尋source field的引用點，進行替換

## 範例

### 範例

``` cs
class Account{
    private AccountType _type;
    private double _interestRate;
    double interestForAmount_days (double amount, int days){
        return _interestRate * amount * dsys / 365;
    }
}
```

想把_interestRate搬到AccountType class去。首先我要建立_interestRate的存取函式

``` cs
 class AccountType{
     private doubl _interestRate;
     void setInterestRate(double arg){
         _interestRate = arg;
     }

     double getInterstRate(){
         return _interestRate;
     }
 }
```

然後修改Account用到_interestRate的地方

```cs
private double _interestRate;
double interestForAmount_days (double amount, int days){
    return _type.getInterstRate() * amount * dsys / 365;
}
```

### 範例 使用Self-Encapsulation(自我封裝)

如果很多函式已經使用了 _interestRate，那我們應該先用 [Self Encapsulate Field(171)](/doc/(171)Self_Encapsulate_Field.md)

```cs
class Account{
    private AccountType _type;
    private double _interestRate;
    double interestForAmount_days (double amount, int days){
        return getInterstRate() * amount * dsys / 365;
    }

     void setInterestRate(double arg){
         _interestRate = arg;
     }

     double getInterstRate(){
         return _interestRate;
     }
}
```

搬移後，只需要改存取函式就行了

```cs
class Account{
    private AccountType _type;
    private double _interestRate;
    double interestForAmount_days (double amount, int days){
        return getInterstRate() * amount * dsys / 365;
    }

     void setInterestRate(double arg){
         _type.setInterestRate(arg);
     }

     double getInterstRate(){
         return _type.getInterstRate();
     }
}
```

### 後繼

如果以後有必要，就直接修改存取函式(Set/get)就好，[Self Encapsulate Field(171)](/doc/(171)Self_Encapsulate_Field.md)讓我們保持一小步地前進。如果先使用[Self Encapsulate Field(171)](/doc/(171)Self_Encapsulate_Field.md)之後就可以輕鬆地使用[Move_Method(142)](/doc/(142)Move_Method.md)