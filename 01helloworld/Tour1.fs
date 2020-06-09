// 関数とモジュール
module fsharp_sample.Tour1

//https://docs.microsoft.com/ja-jp/dotnet/fsharp/tour

// letバインドをするとイミュータブルで変更できない。
// let mutableを使うと変更可能になる。


// １つのinteger引数と１つのinteger戻り値を返す。
let sampleFunction1 x = x*x + 3

// sampleFunction1関数を実行
let result1 = sampleFunction1 4567


let sampleFunction2 (x:int) = 2*x*x - x/5 + 3
let result2 = sampleFunction2 (7 + 4)


let sampleFunction3 x =
  if x < 100.0 then
    // ちょっとしたスペースでもエラーになる。
    //FS0003: この値は関数ではないため、適用できません。
    // 2.0*x*x - x/5.0 + 3.0
    2.0*x*x - x/5.0 + 3.0
  else
    2.0*x*x + x/5.0 - 37.0

let result3 = sampleFunction3 (6.5 + 4.5)




let dispResult =
  printfn "result1 %d" result1
  printfn "result2 %d" result2
  printfn "result3 %f" result3
