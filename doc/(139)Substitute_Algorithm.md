# (139)Substitute_Algorithm(替換你的演算法)

## 將函式本體替換為另一個演算法

``` cs
string foundPerson(string[] people){
    for (int i = 0 ; i < people.length; i++){
        if (people[i].equals ("Don")){
            return "Don";
        }
        if (people[i].equals ("John")){
            return "John";
        }
    }

    return "";
}
```

轉換成

``` cs
string foundPerson(string[] people){

    List candidates = Arrays.asList(new string[]{"Don","John"});

    for (int i = 0 ; i < people.length; i++){
        if (candidates.contains(people[i])){
            return people[i];
        }
    }

    return "";
}
```

## 動機

- 有更好的表達方式就應該替換掉複雜的
- 有時你重構過程中你會做一些動作跟原先略有差異的事情，這時候你應該先把演算法改得清晰一點，再去調整差異，會輕鬆很多
- 使用此手法盡可能地分解複雜的Function，你才能有效的將原本的Function替換調

### 後繼

動機聽起來很基本，但是往往非常難做到，每次修改需求通常那些不是自己寫或是很龐大根本不想修改，但這惡魔的想法往往會在將來打到自己

我自己最常發生的狀況就是，第一次花了半小時去理解整的過程，大概了解就直接去根據需求去調整，打完收工

這異動可能造成Bug，而要除錯的過程中，我又要重新理解這段的意思(可能過了一個月我都忘的差不多了)

這時候又要重新理解，如果這需求又很緊急，我勢必又走一次回頭路，所以現在的修改習慣調整為

1. 難懂的計算方式我會先寫每一行註解
2. 將複雜的演算法拆成很多小Funciton
3. 將重複的程式碼盡也function
4. 往往做到第三步驟已經把原本的程式碼縮短的非常簡單
5. 重新調整流程
