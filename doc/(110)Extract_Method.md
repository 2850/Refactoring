# 簡介

## Extract_Method (110)

## 一段程式碼可以被組織在一起並且獨立出來

``` cs
void PrintOwing(double amount)
{
    PrintBasnner();

    // print details
    Reponse.Wirte("name: " + name);
    Reponse.Wirte("amount" + amount);
}
```

轉換成

``` cs
void PrintOwing(double amount){
    PrintBasnner();
    PrintDeteail(amount);
}


void PrintDetail(double amount){
    // print details
    Reponse.Wirte("name: " + name);
    Reponse.Wirte("amount" + amount);
}
```

## 動機

當看到過長的函式或一段需要備註才能讓人了解用途的程式碼，我都會將他獨力在函式中

- 不這樣做會讓Function看起來都是備註
- 每個函式的粒度都很小，函式彼此重複利用率就會大
- 粒度夠小，Override就越容易

## 作法

1. 創造新的Function，並且根據意圖來命名
2. 直接從原本(Source)的Function複製到新(Target)的Function
3. 仔細檢查提煉出來的程式碼，是否引用到作用域的變數
4. 檢查是否有【僅用於被提煉碼】暫時變數。如果有，在目標函式宣告這個變數
5. 檢查提煉碼，是否有任何區域變數的值，被他改變。如果一個暫時變數被修改了，看是不是可以將其提煉成一個(query)
   如果很難，或是變數不止有一個，就不能這樣做，可能需要[(128)Split_Temporary_Variable](/doc/(128)Split_Temporary_Variable.md)，在嘗試提煉。
   或是[(120)Replace_Temp_With_Query](/doc/(120)Replace_Temp_With_Query.md)將暫時變數消滅掉
6. 將被提煉碼中需要讀取的區域變數，當作參數傳給目標函式
7. 處理完所有區域變數之後，進行編譯
8. 如果沒問題把原本Source的程式碼刪掉

## 範例

### 無區域變數，同上

### 有需域變數：直接把參數傳進Function即可，物件也可，字串也行

``` cs
public class Extract_Method_DEMO1
{
    string name = "";
    public void PrintOwing()
    {
        double outstanding = 0.0;
        // 取得數量
        Order o = new Order();
        outstanding = o.getAmount();


        printBanner();
        printDetail(outstanding);
    }
    private void printBanner(){
        Console.WriteLine("Print Banner");
    }


    private void printDetail(double outstanding){
        Console.WriteLine($@"name:{name}");
        Console.WriteLine($@"amount:{outstanding}");
    }
}


public class Order{
    public double getAmount(){
        return 10.0;
    }
}
```

### 有區域變數又再賦予值

``` cs
    /// <summary>
    /// 有區域變數再賦予值
    /// </summary>
    public class Extract_Method_DEM2
    {
        string name = "";
        public void PrintOwing()
        {
            double outstanding = 0.0;
            printBanner();
            outstanding = getoutstanding();

            printDetail(outstanding);
        }
        private void printBanner(){
            Console.WriteLine("Print Banner");
        }
        private void printDetail(double outstanding){
            Console.WriteLine($@"name:{name}");
            Console.WriteLine($@"amount:{outstanding}");
        }
        private double getoutstanding(){
            double result = 0.0;
            // 取得數量
            Order o = new Order();
            result = o.getAmount();
          return result;
        }
    }
```

### 後繼

回傳如果有多個值怎麼辦? 使用Ref 或是 Out，讓他多回傳己幾個值
但我還是會盡量讓一個Function回傳一個值。

但是有時候變數非常多，讓提煉的過程很艱難，之後我們再介紹 [(120)Replace_Temp_With_Query](/doc/(120)Replace_Temp_With_Query.md) 減少暫時變數。
如果提煉還是困難，則可以使用 [(135)Replace_Method_With_Method_Object](/doc/(135)Replace_Method_With_Method_Object.md)，這個方法不在乎有多少變數、誰使用他。

之後再介紹囉.....  睡覺!!!