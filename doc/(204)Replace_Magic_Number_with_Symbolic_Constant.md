# (204)Replace_Magic_Number_with_Symbolic_Constant(以符號常數(字面常數)取代魔術數字

## 簡介說明

``` cs
double potentialEnergy(double mass, bouble height){

    return mass * 9.81 * height;
}
```

轉換成

``` cs
double potentialEnergy(double mass, bouble height){

    return mass * GRAVITATIONAL_CONSTANT * height;
}

static const double GRAVITATIONAL_CONSTANT = 9.81;
```

## 動機

有些時候，科學定義或是公司定義的數字都是有經過計算或是考量的

但是這些數字工程師也很難解釋為什麼或是業務才清楚

而且這數字只要業務想修改，散落的數字或Debug往往造成很多困擾

直接使用常數宣告的方式，常數不會造成效能的開銷，大大提高可讀性

往往可以用[(218)Replace_Type_Code_With_Class]((218)Replace_Type_Code_With_Class.md)或是用Array來取代

### 後繼

其實重構手法非常多種，這方式看起來超級簡單，但越簡單的動作就越容易忘記

尤其使用常數來定義，例如圓周率3.14159、或是稅率1.05，甚至還有定義稅率不一樣的狀況(不同交易模式)

所以這類有意義的數字請把它常數化吧....