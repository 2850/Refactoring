# (131)Remove_Assignments_to_Parameters

## 以一個暫時變數取代該參數的位置

## 簡介說明

``` cs
in discount (int inputVal, int quantity, int yearToDate){
    if (inputVal > 50 ) inputVal -= 2;
}
```

轉換成

``` cs
in discount (int inputVal, int quantity, int yearToDate){
    int result = inputVal;
    if (inputVal > 50 ) result -= 2;
}
```

## 動機

當函式的參數用到某個物件的時候，如果是直接修改該參數，變化的就會是該參數物件的值，而Return後，繼續使用該物件的時候，內容已經變更，會讓你誤以為是pass by reference。 但狀況是java，而非C#

## 作法

做法很單純就不說明

## 範例

### 範例1

``` java
public static void main(string[] args){

    Date d1 =new Date ("1 Apr 98");
    nextDateUpdate(d1);
    Reponse.WriteLine("d1 after nextDay: " + d1);

    Date d2 = new Date ("1 Apr 98");
    nextDateReplace(d2);
    Response.WriteLine("d2 after nextDay: " + d2);
}

private static void nextDateUpdate (Date arg){
    arg.setDate(arg.getDate() + 1);
    Response.WriteLine("arg in nextDay: " + arg);
}

private static void nextDateReplace (Date arg){
    arg = new Date (arg.getYear(),arg.getMonth(),arg.getDate() + 1);
    Response.Write("arg in nextDay:" + arg);
}

```

結果會是

``` cs
arg in nextDay: Thu Apr 02 00:00:00 EST 1998
d1 after nextDay: Thu Apr 02 00:00:00 EST 1998
arg in nextDay: Thu Apr 02 00:00:00 EST 1998
d2 after nextDay: Wed 01 00:00:00 EST 1998
```
本質上是pass by value，但是


``` cs
// C# 結果
static void Main(string[] args)
{
    Console.WriteLine("Hello World!");

    DateTime d1 = new DateTime(2020,05,23);
    nextDateUpdate(d1);
    Console.WriteLine("d1 after nextDay: " + d1);

    DateTime d2 = new DateTime(2020,05,23);
    nextDateReplace(d2);
    Console.WriteLine("d2 after nextDay: " + d2);
}

private static void nextDateUpdate (DateTime arg){
    arg = arg.AddDays(1);
    Console.WriteLine("arg in nextDay: " + arg);
}

private static void nextDateReplace (DateTime arg){
    arg = new DateTime(arg.Year,arg.Month,arg.Day + 1);
    Console.WriteLine("arg in nextDay:" + arg);
}
```

``` cs
arg in nextDay: 2020/5/24 上午 12:00:00
d1 after nextDay: 2020/5/23 上午 12:00:00
arg in nextDay:2020/5/24 上午 12:00:00
d2 after nextDay: 2020/5/23 上午 12:00:00

```

### 後繼

JAVA 與 C# 不一樣，所以實戰上，未必要完全遵照它的說法，但的確多一個暫時變數來儲存，也可以讓程式碼更清晰，如果在函式很長的狀況下是這樣，但簡短幾行，就未必需要這樣做。
