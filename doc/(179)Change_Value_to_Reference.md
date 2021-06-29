# (179)Change_Value_to_Reference(將實質物件改為引用物件)

## 動機

reference object (引用物件) vs value object (實質物件)

一開始這個物件可能只是少量不可修改的資料，之後又需要加入可以修改的資料

並確保對任一個物件的修改都能影響到所有引用此物件的地方

這時候就會需要reference object

## 範例

``` cs

class Customer{
    public Customer(string name){
        _name = name;
    }
    public void getName(){
        return _name;
    }

    public const string _name;
}

// client code

``` cs
class Order{
    public order(string customerName){
        _customer = new Customer(customerName);
    }

    public void setCustomer(string customernName){
        _customer = new Customer(comstomerName);
    }

    public string getCustomerName(){
        return _customer.getName();
    }

    private Customer _customer;
}

// 還有其他地方會用到Custeomer
private static int numberOfOrdersFor(Collect orders,string customer){
    int result = 0;
    Iterator iter = orders.iterator();
    while (iter.hasNext()){
        order each = (Order)iter.next();
        if (eash.getCustomerName().equals(suctomer)) result++;
    }
    return result;
}

情境說明：

這邊所有的Order 都使用到Customer而且是value objest

意思是：每個訂單都各自擁有Customer物件，現在有一個bug或是需求，如果客戶相同，必須把所有的Customer都使用同一個物件

希望避免各自擁有Customer又資料不相同。

```

## 做法

1. 使用[Replace_Constructor_With_Factory_Method(304)]((304)Replace_Constructor_With_Factory_Method.md) 把創建Customer來控制建立的過程

``` cs
class Customer{
    public static Customer create(string name){
        returm new Customer(name);
    }
}

// 所以Order呼叫就可以改成

class Order{
    public order(string customer){
        _customer = Customer.create(customer);
    }
}

// 然後把Customer 的建構式改成Private
class Customer{
    private Customer(string name){
        _name = name;
    }
}


// 接著必須決定如何存取Customer物件
// 通常常見的做法是通過一個物件來存取，例如：Order內的欄位，或Customer的欄位
// 以下例子使用Customer

``` cs
private static Dictionary _instances = new Hashtable();
```

這時候可以決定，要預先或事後載入客戶資料，先用事前載入當範例，反正之後還可以用[(139)Substitute_Algorithm(替換你的演算法)]((139)Substitute_Algorithm.md)

``` cs
class Custmer{
    ...

    static void loadCustomers(){
        new Customer("lemon Car Hire").store();
        new Customer("Associated Coffee Machines").store();
        new Customer("Bilston Gasworkds").store();
    }
    private void store(){
        _instances.pub(this.getName(),this);
    }
}

```

因為使用預先載入，就需要修改factory method，讓他回傳育先建好的Customer物件

``` cs
public static Customer create(string name){
    return (Customer) _instance.get(name);
}
```

這邊還可以繼續優化，使用[Rename_Method (重新命名函式)]((273)Raneme_Method.md)表達更清楚的名稱

``` cs
public static Customer getNamed(string name){
    return (Customer) _instance.get(name);
}

```

### 後繼

這個例子練習完的核心有二

1. 使用factory method來統一建立Customer的過程
2. 將建立好的object，統一放在Order或是Customer(此為關鍵，直接使用此物件來當作reference)

以往為了確保Custeomer物件必須相同，我都是直接將Customer直接傳到Function運算之後，再回傳該物件

``` cs

public Customer somemethof(string name,Customer customer){
    // 一些計算

    return customer;
}

// 或是使用ref

public void somemethof(string name,ref Customer customer){
    // 一些計算
}

```

但這些方式都沒有這個方法來的直觀，而且如果要修改這些method，也將造成很多困擾

1. 使用ref就難以重構(其他引用的地方都要跟著ref，用多了連自己都會亂掉)
2. 將物件本身當作參數，表示這method承擔的工作量過大，應該回歸到Customer本身)
