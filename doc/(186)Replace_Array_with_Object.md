# (186)Replace_Array_with_Object

## 簡介說明

``` cs
string[] row = new string[3];
row[0] = "Liverpool";
row[1] = "15";
```

轉換成

``` cs
Performance row = new Performance();
row.setName("Liverpool");
row.setWins("15");
```

## 動機

有時候會發現陣列容納不同物件，這會給array帶來麻煩，很難記憶。

而且如果使用物件還可以透過[(142)Move_Method]((142)Move_Method.md)為她加上行為

## 範例

陣列有三個元素，分別保存一支球隊的名稱、勝利次數、輸的次數

### 範例1

```cs
string[] row = new string[3];

 // client code
row[0] = "Liverpool";
row[1] = "15";

string name = row[0];
int wins = int.Parse(row[1]);
```

步驟：

``` cs
class Performance {
    // 暫時宣告public 用來儲存原本的array
    public string[] _data = new string[3];

    row._data[0] = "Livepool";
    row._data[1] = "15";

    string name = row._data[0];
    int wins = intParse(row._data[1]);
}

// Client code 改成使用
Performance row = new Performance();

// 將所有元素改成get/set 方式
class Performance{
    public string getName(){
        return _data[0];
    }

    public void setName(string arg){
        _data[0] = arg;
    }

    public int getWins(){
        return _data[1];
    }

    public void setWins(string arg){
        _data[1] = arg;
    }
}


// client code 使用 get / set 取得
row.setName("Liverpool");
row.setWins("15");

string  name = row.getName();
int wins = row.getWins();


//之後將public string[] = new string[3]; 改成 private


```

``` cs
// 最終結果 把_data[0] 整個替換掉
class Performance{
    string _name = string.empty;
    int _wins = 0;
    public string getName(){
        return _name;
    }

    public void setName(string arg){
        _name = arg;
    }

    public int getWins(){
        return _wins;
    }

    public void setWins(string arg){
        _wins = arg;
    }
}
```


### 範例2

``` cs

```

### 有區域變數又再賦予值

``` cs

```

### 後繼

ABCDEF