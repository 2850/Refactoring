# Extract Class (提煉類別)

## 建立一個新Class，將相關的欄位和函式從就Class搬移到新Class

``` mermaid
classDiagram
class Person{
   string name
   string officeAreaCode
   string officeNumber
   getTelephoneNumber()
}

```

轉換成

``` cs
classDiagram

class Person{
    string name
    getTelephoneNumber()
}

class Telephone Number{
    string areaCode
    string number
    getTelephoneNumber()
}

Person --> "1" Telephone Number: office Telephone
```

## 動機

常常聽到要有抽象性，但實際工作中，Class會不斷成長擴展，給某個Class添加一項新責任時，你會覺得不值得為這項責任分離出來一個單獨的Class。隨著責任不斷增加，這個Class會變得過分複雜，很快就亂了。

這樣個Class往往有大量的函式與資料，太大不容易理解，此時你要考慮哪個部分可以分離出去，並分離到另一個單獨的Class。

## 作法

1. 決定如何分解Class所負責任
2. 建立一個新Class，用以表現從舊Class中分離出來的責任
3. 建立【從舊Class存取新Class】的連線關係。【白話：New 新Class】
4. 使用[Move Field(146)](/doc/(146)Move_Field.md)搬移
5. 使用[Move Method(142)](/doc/(142)Move_Method.md)將必要函式搬移到新Class。==先搬移低階函式([被其他呼函式呼叫] 多於 [呼叫其他函式])，再搬較高函式。==
6. 決定是否讓新Class曝光。

## 範例

``` cs
Class Person{
    public string getName(){
        return _name;
    }

    public string getTelephoneNumber(){
        return ("(" + _officeAreaCode + ") " + _officeNumber);
    }

    string getOfficeAreaCode(){
        return _officeAreaCode;
    }

    void setOfficeAreaCode(string arg){
        _officeAreaCode = arg;
    }

    string getOfficeNumber(){
        return _officeNumber;
    }

    void setOfficeNumber(string arg){
        _officeNumber = arg;
    }

    private string _name;
    private string _officeAreaCode;
    private string _officeNumber;

}
```

### 步驟一

決定要搬移的範圍，移動欄位[Move Field(146)](/doc/(146)Move_Field.md)

``` cs

``` cs
Class Person{

    private string _name;

    private string _officeNumber;
    pribvate TelephoneNumber _officeTelephone = new TelephoneNumber();

    public string getTelephoneNumber(){
        return ("(" + _officeTelephone.getOfficeAreaCode() + ") " + _officeNumber);
    }
    public string getName(){
        return _name;
    }

    string getOfficeNumber(){
        return _officeNumber;
    }

    void setOfficeNumber(string arg){
        _officeNumber = arg;
    }

}

Class TelephoneNumber{
    private string _officeAreaCode;

    string getOfficeAreaCode(){
        return _officeAreaCode;
    }

    void setOfficeAreaCode(string arg){
        _officeAreaCode = arg;
    }
}
```

### 第二步

移動其他欄位並且運用[Move Method(142)](/doc/(142)Move_Method.md)將相關函式移動到TelephoneNumber

``` cs
Class Person{

    private string _name;

    pribvate TelephoneNumber _officeTelephone = new TelephoneNumber();

    public string getTelephoneNumber(){
        return _officeTelephone.getTelephoneNumber();_
    }

    public string getTeOfficelephone(){
        return _officeTelephone;
    }

    public string getName(){
        return _name;
    }
}

Class TelephoneNumber{

    private string _areaCode;
    private string _number;

    public string getTelephoneNumber(){
        return ("(" + _areaCode + ") " + _number);
    }

    string getOfficeAreaCode(){
        return _areaCode;
    }

    void setOfficeAreaCode(string arg){
        _areaCode = arg;
    }

    string getOfficeNumber(){
        return _number;
    }

    void setOfficeNumber(string arg){
        _number = arg;
    }
}
```

### 決定是否對外公開

如果決定公開就要特別考慮別名帶來的危險。如果暴露TelephoneNumber，而有人修改了_areaCode我怎麼知道?又可能呼叫的是其他人

有以下這幾種狀況：

1. 完全公開TelephoneNumber。這就使得TelephoneNumber物件成為引用物件，使用[(179)Change_Value_to_Reference](/doc/(179)Change_Value_to_Reference.md)。這種情況下，person應該是TelephoneNumber的存取點

2. 不與任何人【不透過Person物件修改TelephoneNumber物件】。為了達到此目的，我可以將TelephoneNumber設為不可修改的，或為它提供一個不可修改的interface

### 後繼

[(149)Extract_Class](/doc/(149)Extract_Class.md)是改善程式的常用技術。因為它使你兩個Class分別加鎖。