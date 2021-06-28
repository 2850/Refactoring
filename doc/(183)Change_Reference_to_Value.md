# (183)Change Reference to Value(將引用物件改為實質物件)

如果Reference Object ，很小且不可變，也不易管理，就可以使用此技巧

## 簡介說明

如同[(179)Change_Value_to_Reference(將實質物件改為引用物件)]((179)Change_Value_to_Reference.md) 有Value Object to Reference Object

就有 Reference Object to Value Object，這兩個之間本來就很難決定。

如果Reference Object 用得多，可能會造成前面的[後繼]((179)Change_Value_to_Reference.md#)說明一樣，用得太多會亂，其實也未必是一件好事，此技巧也可以解決這類的問題

Value Object 有一個重要的特性，就是他們不可變(Immutable)，無論何時你呼叫這個物件的查詢函式，應該都得到一樣的結果

如果Value Object 是可變的，你就必須確保你對某一個物件的修改會自動更新他【代表相同的事物】，這太痛苦了，與其如此不如就把它改成Reference Object

## 可變(mutable) VS 不可變(immutable)

不可變(immutable)： 即物件一旦被建立初始化後，它們的值就不能被改變，之後的每次改變都會產生一個新物件。

例如：string 不可變

``` cs
var str="mushroomsir";
str.Substring(0, 6)

// c#中的string是不可變的，Substring(0, 6)返回的是一個新字串值，而原字串在共享域中是不變的。另外一個StringBuilder是可變的，這也是推薦使用StringBuilder的原因。

var age=18;
age=2;
// 此時會記憶體中新值2賦值給age變數，而不能改變18這個記憶體裡的值
```

物件的value object

``` cs
// 可變的範例
class Contact
{
    public string Name { get;  set; }
    public string Address { get;  set; }
    public Contact(string contactName, string contactAddress)
    {
        Name = contactName;
        Address = contactAddress;
    }
}

// client code
var mutable = new Contact("二毛", "清華");

mutable.Name = "大毛";
mutable.Address = "北大";

// new 了之後又可以修改他的值

```

在多執行緒就會出現，資料撕裂(A、B執行緒讀到的資料不一樣)

變成reference object 方法

``` cs
public class Contact2
{
    public string Name { get; private set; }
    public string Address { get; private set; }

    private Contact2(string contactName, string contactAddress)
    {
        Name = contactName;
        Address = contactAddress;
    }

    public static Contact2 CreateContact(string name, string address)
    {
        return new Contact2(name, address);
    }

}

```

只能通過建構式才可以修改裡面的值，所以每次使用該物件都必須重新New這個物件

可以完整資料完整性、保證安全性，也不會被執行緒修改到


## 範例

```cs

// 一個reference object
class Currency{
    private string _code;

    public string getCode(){
        return _code;
    }
    private Currency(string code){
        _code = code;
    }
}


// client code
Currency usd = Currency.get("USD");

// 建構式是Private無法建立
new Currency("USD").equals(new CUrrency("USD")) // return false
```

要把reference object 變成 value object 灌見動作是：檢查他是否為immutable(不可變)

如果不是，就不可能使用這方法重構，因為Mutable(可變的) value object 會造成令人苦惱的別名現象(aliasing)

``` cs
// 猜猜下面答案會是 True or Flase ?
bool sample(){
    return new Currency("USD").Equals(new Currency("USD"));
}

void Main()
{
    bool sample(){
        return new Currency("USD").Equals(new Currency("USD"));
    }

    Console.WriteLine(sample());
}

class Currency {
    public string Name {get; private set;}
    private string othervalue = ""; // 其他驗證的欄位

    public Currency(string name){
        Name = name;
    }

    // 調整二 把檢驗方式修正
    public override bool Equals(object obj){
        return Name.Equals(((Currency)obj).Name);
    }

    // 調整二 所有要驗證的欄位都要加上 ^
    public override int GetHashCode(){
        return Name.GetHashCode() ^ othervalue.GetHashCode();
    }
}
```

### 後繼

平常開發多半都使用的是可變 + Value Object，而使用Reference 的機會並不多

在加上這重構的手法，真的比較少見，感覺用到的機率也不算太大

但這是一個不錯的學習範例，可以有印象即可
