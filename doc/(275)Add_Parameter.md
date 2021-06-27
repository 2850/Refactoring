# (275)Add_Parameter

## 替函式新增一個參數，讓該物件帶進函式所需的資訊

## 動機

大家都用過，但要講的是怎樣不使用這種方式，你永遠有很多方式比這個方法好。

而且太常使用此方法只會讓程式碼更髒

## 作法

1. 看參數的時候請仔細看看，這些參數提供的資訊是需要的嗎?
2. 這些參數是不是用其他方式呈現就好
    - 共用抽出來
    - 其他地方沒用到直接自己函式內宣告
    - 這個參數是其他Class應該擁有的)
3. 使用方法 [(295)Introduce_Parameter_Object(引入參數物件)]((295)Introduce_Parameter_Object.md)

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
