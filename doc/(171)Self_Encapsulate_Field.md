# Self Encapsulate Field (自我封裝欄位)

## 為這個欄位建立 取值 / 設值 函式(getting/setting methods)，並且只透過這兩個函式取欄位

``` cs
private int _low; _high;
boolean includes(int arg) {
    return arg >= _low && arg <= _high;
}
```

轉換成

``` cs
private int _low; _high;
boolean includes(int arg) {
    return arg >- getLow() && arg <= getHigh();
}

int getLow() {return _low;}
int getHign() {return _high;}
```

## 動機

目前有兩派做法

- 直接存取變數:容易閱讀
- 間接存取變數:subclass 得經由【複寫一個函式】而改變獲取資料途徑;他還支援更靈活的資料管理方式，lazy initialization(意思是：只有再需一用到某值時，才對他初始化)

## 作法

1. 為封裝的值建立get/set methods
2. 找出該欄位所有引用點，全部替換
3. 將該欄位改為Private

## 範例

### 範例1

一般認為設定值會是在【物件創建後】才使用，但初始化過程中的行為有可能跟設值函式的行為不同。
這種狀況下，也許在建構式中直接存取欄位，要不就是建立另一個初始化函式

``` cs

class IntRange{
    private int _low;
    private int _high;

    IntRange (int low , int high) {
        initialize(low , hign);
    }

    private void initialize (int low , int high) {
        _low = low;
        _high = high;
    }
}

```

### 範例2

一旦擁有subclass，這動作的價值就會體現出來

``` cs
class CappedRange :IntRange {
    CappedRange (int low , int high , cap _cap ) {
        base(low , high);
        _cap = cap;
    }

    private int _cap;
    int getCap() {
        return _cap;
    }

    int getHigh() {
        return Math.min( base.getHigh(), base.getCap() );
    }

}
```

可以在cappedRange Override getHigh(),從而加入對Cap的考慮，而不必修改IntRange Class的行為

### 後繼

其實做法各有利弊，以我的開發經驗，我會優先選擇【直接存取變數】
等到之後有需要繼承或式Overwrite的時候，再重構即可，不需要一開始把程式弄得複雜