# (304)Replace_Constructor_With_Factory_Method(已工廠代替建構)

## 希望在建立物件時不僅僅是對他做簡單的建構動作

``` cs
Employee (int type) {
    _type = type;
}
```

轉換成

``` cs
static Employee create (int type) {
    return new Employee(type);
}
```

## 動機

可能需要根據傳入的參數來建立相對應的物件

## 作法

1. 建立一個factory method，讓他呼叫現有的建構式
2. 將建構式的呼叫，改成factory method

## 範例1：根據整數 創建物件

```cs
class Employee {
    private int _type;
    static final int ENGINEER = 0;
    static final int SALESMAN = 1;
    static final int MANAGER = 2;

    Employee (int type) {
        _type = type;
    }
}

```

```cs
class Employee {
    private int _type;
    static final int ENGINEER = 0;
    static final int SALESMAN = 1;
    static final int MANAGER = 2;

    Employee (int type) {
        _type = type;
    }

    // 建立一個factory method
    static Employee create (int type){
        return new Employee(type);
    }

    // client code
    Employee eng = Employee.create(Employee.ENGINEER);
}

```

### 範例2：根據字串 創建subclass 物件

如果使用 [Replace_Type_Code_with_Subclasses (223)]((223)Replace_Type_Code_with_Subclasses.md) 把type code 轉換成 Employee的subclass

就可以運用 factory method，將這些subclass對使用者隱藏起來

```cs
static Employee create(int type){
    switch (type){
        case ENGINEER:
            return new Engineer();
            break;
        case SALESMAN:
            return new Salesman();
            break;
        case MANAGER:
            return new Manager();
            break;
    }
}

```

這邊有一個缺點，每新增一個subclass就要記得加入Switch

解法：ClaSS.forName() 這邊範例使用的是java 需要自己轉換C#

第一：修改參數型別，可參考 [(273)Raneme_Method]((273)Raneme_Method.md)

首先建立一個FUnction，讓他接收參數

```cs
static Employee create (string name){
    try{
        return (Employee) Class.forName(name).newInstance();
    }
    catch(Exception e){
        throw new .....
    }
}

// clint code
Employee.create(ENGINEER);

改成

employee.create("Engineer");

```

這樣就可以自動建立，但失去了編譯時期的檢查

### 範例3：已明確函式(Explicit Methods)創建 subclass

``` cs

class Person {

    static Person CreateMale(){
        return new Male();
    }

    static Person CreateFemale(){
        return new Female();
    }
}

// client
Person kent = new Male();
改成
Person kent = Person.crateMale();

```

### 後繼

範例一跟範例二是我常常使用的方法，但方法二的forname這段可以再補上加強寫法

我這邊最常使用的場景多半是用在工廠模式，需要根據不同狀況去建立物件

類似MOMO、YAHOO、PCHOME對帳，對帳的動作全部都是一樣的，只是裡面實作不同

所以通常會使用方法二來 return 我要的物件

1. 建立虛擬類別
2. SubClass 繼承虛擬類別實作其方法
3. 使用factory method 或 工廠模式 來創建我想要的物件
