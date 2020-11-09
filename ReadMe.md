# 重構-改善既有程式的設計

## 初衷

此專案是小弟邊看書邊作的筆記

希望可以把所學的知識筆記下來並且提供我做查詢

文件化的目的也希望可以把這些實用的技巧分享給各位 by小柯

## 目錄


|方法  |簡述  |
|---------|---------|
|[(110) Extract Method](/doc/(110)Extract_Method.md)     |一段程式碼可以被組織在一起並且獨立出來         |
|[(119) Inline Temp Method](/doc/(119)Inline_Temp.md)     |將所有對肝變數的引用動作，替換為對它復職的那個運算式本身         |
|[(120) Replace_Temp_With_Query](/doc/((120)Replace_Temp_With_Query.md))     |將運算式提煉到一個獨立函式，將這個暫時變數的所有【被引用點】替換為【對新函式的呼嘯】         |
|[(124) Introduce_Explaining_Variable](/doc/(124)Introduce_Explaining_Variable.md)     |將複雜運算式(或其中一部分) 的結果放進一個暫時變數，以此變數名稱來解釋運算式用途         |
|[(128) Split Temporary Variable Method](/doc/(128)Split_Temporary_Variable.md)     |針對每次賦予值，創造一個獨立的、對應暫時變數         |
|[(131) Remove_Assignments_to_Parameters](/doc/(131)Remove_Assignments_to_Parameters)     |以一個暫時變數取代該參數的位置         |
|[(135) Replace_Method_With_Method_Object](/doc/(135)Replace_Method_With_Method_Object.md)     |將這個函式放近一個單獨物件中，如此一來區域變數就成了物件內的欄位。然後你可以在同一個物件中將這個大型函式分解為數個小型函式         |
|[(146) Move Method](/doc/(142)Move_Method.md)     |該函式最常引用的Class中建立一個有著==類似行為==的新函式。將舊函式變成一個單純的委託函式或將就函式移除。         |
|[(149) Extract_Class](/doc/(149)Extract_Class.md)     |建立一個新Class，將相關的欄位與函式從舊Class搬移到新Class         |
|[(157) Hide Delegate](/doc/(157)Hide_Delegate.md)     |隱藏 【委託關係】       |