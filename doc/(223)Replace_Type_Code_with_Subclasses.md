# (223)Replace_Type_Code_with_Subclasses(已子類別取代型別代碼)

## 簡介說明

class之中有一個數值型別的代碼，但她會影響行為。

如果會影響請參考[(218)Replace_Type_Code_With_Class(類別取代型別代碼)]((218)Replace_Type_Code_With_Class.md)

## 動機

通常會影響行為的多半會用條件式來表現Switch 或 if-else-end。

通常是判斷type code 決定接下來的動作。

這種狀況你應該可以用[(255)Conditional_With_Polymorphism(多型取代條件式)]((255)Conditional_With_Polymorphism.md)來重構

但為了可以進行255，還需要先用多型，而且需要有繼承，而且多半這種關係都會根據Type Code 建立 SubClass，而要進行這種繼承體系的SubClass 本方法就是起手式

簡言之，要進行255之前223也是必須的。

有兩個狀況式不是用223的

1. Type Code植在物件建立之後，發生改變
2. Type Code宿主已經有SubClass

如果剛好面臨這種問題請參考[(227)Replace_Type_Code_With_State_Strategy(State/Strategy取代型別代碼)]((227)Replace_Type_Code_With_State_Strategy.md)

本方法的好處是他把【對不同行為的了解】從Class使用者那轉移到Class本身

如果需要加新的變化，只要加入SubClass本身。

## 範例

### 範例2

``` cs
class Employee{
    private int _type;
    static const int ENGINEER = 0;
    static const int SALESMAN = 1;
    static const int MANAGER = 2;

    Employee (int type){
        _type = type;
    }

    // ... 想像下面有超多程式碼在判斷ENGINEER、SALESMAN、MANAGER...
    // ... 大概6000多行程式碼...
}

```

修改步驟

```cs
class Employee{

    public int _type {get ; private set;}
    static const int ENGINEER = 0;
    static const int SALESMAN = 1;
    static const int MANAGER = 2;

    Employee (int type){
        _type = type;
    }

    // 步驟一：使用factory method，把建立的動作統一
    static Employee create(int type){
        return new Employee(type);
    }

    // 步驟二：建構式改為Priavte
    private Employee(int type){
        _type = type;
    }
}

// 步驟三：開始建立subclass
class Engineer : Employee{
    public new int Type{
        get {return Employee.ENGINER;}
    }
}

// 步驟三：開始建立subclass
class Salesman : Employee{
    public new int Type{
        get {return Employee.SALESMAN;}
    }
}

// 步驟三：開始建立subclass
class Manager : Employee{
    public new int Type{
        get {return Employee.Manager;}
    }
}

// 步驟四：回來修改factory method
static Employee create(int type){
    switch(type){
        case ENGINER:
            return new Engineer();
        case SALESMAN:
            return new Salesman();
        case MANAGER:
            return new EngManagerineer();
    }

    throw new Execption("UnKnow type");
}

// 步驟五 刪掉不必要的建構式
Employee (int type){
    _type = type;
}
```

到目前為止程式碼變成這樣子，其實到目前為止都還不錯，但還可以繼續優化

``` cs
class Employee{
    public int _type {get ; private set;}
    static const int ENGINEER = 0;
    static const int SALESMAN = 1;
    static const int MANAGER = 2;

    // 步驟四：回來修改factory method
    static Employee create(int type){
        switch(type){
            case ENGINER:
                return new Engineer();
            case SALESMAN:
                return new Salesman();
            case MANAGER:
                return new EngManagerineer();
        }

        throw new Execption("UnKnow type");
    }
}

class Engineer : Employee{
    public new int Type{
        get {return Employee.ENGINER;}
    }
}

class Salesman : Employee{
    public new int Type{
        get {return Employee.SALESMAN;}
    }
}

class Manager : Employee{
    public new int Type{
        get {return Employee.Manager;}
    }
}

```

最終優化後的結果

```cs
// 改為抽象類別
abstract class Employee{
    // public int _type {get ; private set;}
    public abstract int _type {get ;}   // 這邊只需要get
    static const int ENGINEER = 0;
    static const int SALESMAN = 1;
    static const int MANAGER = 2;

    // 步驟四：回來修改factory method
    static Employee create(int type){
        switch(type){
            case ENGINER:
                return new Engineer();
            case SALESMAN:
                return new Salesman();
            case MANAGER:
                return new EngManagerineer();
        }

        throw new Execption("UnKnow type");
    }
}

class Engineer : Employee{
    public override int Type{    // 改成override
        get {return Employee.ENGINER;}
    }
}

class Salesman : Employee{
    public override int Type{   // 改成override
        get {return Employee.SALESMAN;}
    }
}

class Manager : Employee{
    public override int Type{   // 改成override
        get {return Employee.Manager;}
    }
}

```

### 後繼

這個方法可以說是我最常使用的，常常用一個變數來控制一堆非常相像，但又有一點變化的地方

建立SubClass 來專心控制自己的動作，而控制可以用factory method來掌控我要的行為

原本核心邏輯都擠在一起而且都在Client端的手上，透過SubClass繼承之後，邏輯回到各自Class，而最終由Client端控制

這種依賴反轉的過程，可以大大提升程式可讀性，也可以將邏輯區分得更清楚。