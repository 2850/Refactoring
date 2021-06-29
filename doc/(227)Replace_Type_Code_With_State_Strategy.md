# (227)Replace_Type_Code_With_State_Strategy(State/Strategy取代型別代碼)

## 簡介說明

class之中有一個數值型別的代碼，但她會影響行為，但你無法使用Subclassing 來處理

他跟[(223)Replace_Type_Code_with_Subclasses(已子類別取代型別代碼)]((223)Replace_Type_Code_with_Subclasses.md)非常像

## 範例

非常常見的作法，而且往往越加越多，非常痛苦

``` cs
public class Employee
{
    public const int ENGINEER = 0;
    public const int SALESMAN = 1;
    public const int MANAGER = 2;

    public int Type { get; set; }

    public int Salary { get; set; }
    public int Commission { get; set; }
    public int Bouns { get; set; }

    public Employee(int type)
    {
        Type = type;
    }

    // 根據不同身分取得不同運算邏輯
    public int GetPayAmount()
    {
        switch (Type)
        {
            case ENGINEER:
                return Salary;
            case SALESMAN:
                return Salary + Commission;
            case MANAGER:
                return Salary + Bouns;
        }
        throw new Exception("Unknow type");
    }
}
```

先建立虛擬類別、並且每個邏輯都繼承虛擬類別

並建立一個factory method 來讓client端切換執行不同的策略邏輯

``` cs
abstract class EmployeeType
{
    public abstract int Type { get; }

    // 使用factory method 來切換要建立的物件
    public static EmployeeType Create(int type)
    {
        switch (type)
        {
            case Employee.ENGINEER:
                return new Engineer();
            case Employee.SALESMAN:
                return new Salesman();
            case Employee.MANAGER:
                return new Manager();
        }

        throw new Exception("Unknow type");
    }
}

class Engineer : EmployeeType
{
    public override int Type
    {
        get {return Employee.ENGINEER;}
    }
}

class Salesman : EmployeeType
{
    public override int Type
    {
        get { return Employee.SALESMAN; }
    }
}

class Manager : EmployeeType
{
    public override int Type
    {
        get { return Employee.MANAGER; }
    }
}
```

開始把不需要存在於Client 端的程式碼都搬走了

``` cs

    abstract class EmployeeType
    {
        // 從Client搬到抽象類別
        public const int ENGINEER = 0;
        public const int SALESMAN = 1;
        public const int MANAGER = 2;

        public abstract int Type { get; }

        // 使用factory method 來切換要建立的物件
        public static EmployeeType Create(int type)
        {
            switch (type)
            {

                case ENGINEER:  // 直接引用
                    return new Engineer();
                case SALESMAN:  // 直接引用
                    return new Salesman();
                case MANAGER:  // 直接引用
                    return new Manager();
            }

            throw new Exception("Unknow type");
        }


        ....

    class Engineer : EmployeeType
    {
        public override int Type
        {
            // get {return Employee.ENGINEER;}
            get {return ENGINEER;}  // 因為繼承EmployeeType 所以也直接引用即可
        }
    }
```


開始改造Client程式碼

```cs
public class Employee
{

    // 步驟一：建立虛擬類別提供client 選擇策略
    private EmployeeType Type {get;set;};

    // 重構建構式，由Client決定之後的執行策略
    public Employee(EmployeeType type){
        Type = type;
    }

    // 這邊有怪怪的味道，已經拆分SubClass，把邏輯回歸到他們原本身邊吧
    public int GetPayAmount()
    {
        switch (Type)
        {
            case EmployeeType.ENGINEER:
                return Salary;
            case EmployeeType.SALESMAN:
                return Salary + Commission;
            case EmployeeType.MANAGER:
                return Salary + Bouns;
        }
        throw new Exception("Unknow type");
    }

    ...

```

最後一步驟：把所有商業邏輯都寫回給每個物件吧!!!

```cs
    abstract class EmployeeType
    {
        public const int ENGINEER = 0;
        public const int SALESMAN = 1;
        public const int MANAGER = 2;

        public abstract int Type { get; }

        // 讓每個員工自己實現自己的方法
        public abstract int GetPayAmount(Employee employee);

        ...

    class Engineer : EmployeeType
    {
        public override int Type
        {
            get {return ENGINEER;}
        }

        // 員工靠業績
        public override int GetPayAmount(Employee employee)
        {
            return employee.Salary;
        }
    }


    class Salesman : EmployeeType
    {
        public override int Type
        {
            get { return SALESMAN; }
        }

        // 銷售員就是賺抽成
        public override int GetPayAmount(Employee employee)
        {
            return employee.Salary + employee.Commission;
        }
    }

    class Manager : EmployeeType
    {
        public override int Type
        {
            get { return MANAGER; }
        }

        // 老闆靠紅利
        public override int GetPayAmount(Employee employee)
        {
            return employee.Salary + employee.Bouns;
        }
    }
```

### 後繼

這個方法跟223的差異除了把邏輯抽到各自SubClass以外

最大的差異在於，223把決定權在Class本身，227把決定權交給Client，其實這就是策略模式得一個特色

223跟227都是很棒的方法，而且也是最容易學習的設計模式，策略模式，簡單工廠模式

這兩個學會其實已經可以運用在非常多地方，邏輯也可以整理的非常清楚

請各位多看幾遍