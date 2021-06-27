# Hide Delegate (隱藏 【委託關係】)

## 簡介說明

``` cs
classDiagram
Person <.. ClientClass
Department <..ClientClass
class ClientClass{

}

class Person{
    getDepartment()
}

class Department{
    getManager()
}

```

轉換成

``` cs
classDiagram
Person <.. ClientClass
Department <..Person
class ClientClass{

}

class Person{
    getDepartment()
}

class Department{

}


```

## 動機

【封裝】意味著每個物件都應該盡可能少了解系統的其他部分。如此一來，一旦發生變化，需要了解變化的物件就越少。這樣需求改變也比較容易維護。


## 範例

``` cs
class Person{

    Department _department;

    public Department getDetatment(){
        return _department;
    }

    public void set Department(Department arg){
        _department = arg;
    }
}

class Department{
    private String _chargeCode;
    private Person _manager;

    public Department (Person manager){
        _manager = manager;
    }

    public Person getManager(){
        return _manager;
    }
}
```

#### 如果客戶希望知道某人的經理是誰，他必須先取得Department物件

``` cs
 manager = json.getDepartment().getManager();
```

這樣就對客戶揭漏了Department工作原理，客戶就知道Department用以追蹤【經理】這條資訊。如果對客戶隱藏Department，就可以減少耦合，修改建議為

```cs
public Person getManager(){
    return _deparment.getManager();
}
```

### 後繼

這個狀況也可能導致最後有很多的委託關係的Function，也會有反向的關係
重構本身就是根據當時的狀況去變化，不需要太擔心過多的Function。

``` cs
classDiagram
Person <.. ClientClass
Department <..Person
class ClientClass{

}

class Person{
    getDepartment()
}

class Department{

}

```

轉換成

``` cs
classDiagram
Person <.. ClientClass
Department <..ClientClass
class ClientClass{

}

class Person{
    getDepartment()
}

class Department{
    getManager()
}

```
