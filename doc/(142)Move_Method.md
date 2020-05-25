# 簡介

## (142) Move Method 搬移函式

## 該函式最常引用的Class中建立一個有著==類似行為==的新函式。將舊函式變成一個單純的委託函式或將就函式移除。

``` mermaid
classDiagram
class Class1{
    getPoints()
}

class Class2{
    
}

```

轉換成

``` mermaid
classDiagram
class Class1{
    
}

class Class2{
    getPoints()
}


```

## 動機

*函式搬移* 是重構理論的支柱。 如果一個Class 有太多行為，獲如果一個Class與另一個Class有太多合作而形成高度耦合，舊需要搬移。

看所有class從中尋找：使用另一個物件的次數比自己所屬的物件還多，而通常在搬移一些函式或是變數的時候就會檢查，而且會判斷哪邊用得比較多，決定要搬到哪個Class，通常一個函式很難判斷，可以再查看相關的函式來判斷。

## 作法

1. 檢查Source class定義的Method或欄位。
   如果你搬移的函式內還有其他函式，而剛好只有這個函式用到而已，就順便搬過去吧。
2. 檢查Source Class的Subclass或SuperClass，是否也有使用到該函式
   如果有，你可能很難搬移，除非剛好Target Class也有一樣的多型
3. 在Target Class 中宣告這個函式
4. 將Source Class的程式碼Copy到Target Class
   * 如果tartget class中沒有相應的飲用機制，就把source object reference當作參數。
   * 如果source Method 包含異常處理式，你必須判斷邏輯上應該由哪個class來處理這個異常。
5. 決定從Source 正確引用Target Object
   可能有現成的函式或欄位可以用，如果沒有，自己建立一個，如果不行，可能就必須自己建立一個Object。可能永久或暫時，因為後繼的其他重購項目可能取代掉這變數

## 範例

### 範例1

``` cs
// 原始檔案
class Account
    double overdraftCharge(){
        if (_type.isPremium()) {
            double result = 10;
            if (_dayOverdrawn > 7)
                result += (_dayOverdrawn - 7) * 0.85;
            return result;
        }
        else return _dayOverdrawn * 1.75;
    }

    double bankCharge(){
        double result = 4.5;
        if (_dayOverdrawn > 0) result += overdraftCharge();
        return result;
    }

    private AccountType _type;
    private int _dayOverdrawn;
```

此範例想要將overdraftCahrge()搬到AccountType

* _type.isPremium() 變成 ispremium()
* _dayOverdrawn 直接當作參數

``` cs
 Class AccountType{
     double overdraftCharge(int _dayOverdrawn){
        if (isPremium()) {
            double result = 10;
            if (_dayOverdrawn > 7)
                result += (_dayOverdrawn - 7) * 0.85;
            return result;
        }
        else return _dayOverdrawn * 1.75;
    }
 }
```

當需要Source的特性的時候通常有四種狀況

1. 將這個特性也搬移到target Class
2. 建立或使用一個從target class到Source的引用關係
3. 將source oject當作參數傳給target
4. 如果特性是一個變數，直接當作參數傳給target method(上面例子就是用這方式)

``` cs
// Source class 變成
class Account{
    double overdraftCharge(){
        return _type.overdraftCharge(_dayOverdrawn);
    }

    double bankCharge(){
        double result = 4.5;
        if (_dayOverdrawn > 0)
        result += _type.overdraftCharge(_dayOverdrawn);
        return result;
    }
}
```

### 後繼

剛剛例子只有一個參數，如果BankCharge還有呼叫Account的其他函式，就必須把整個Account Oject當作參數傳遞

``` cs
 Class AccountType{
     double overdraftCharge(Account account){
        if (isPremium()) {
            double result = 10;
            if (account._dayOverdrawn() > 7)
                result += (account._dayOverdrawn() - 7) * 0.85;
            return result;
        }
        else return _dayOverdrawn * 1.75;
    }
 }
```