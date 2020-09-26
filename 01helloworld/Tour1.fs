// 関数とモジュール
module fsharp_sample.Tour1

//fsharpc Tour1.fs -o Tour1 && mono Tour1

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


// イミュータブルにしてデータ書き換えをした
let mutable number1_1 = 1
let mutable number1_2 = 1
number1_1 <- number1_1 + 1
number1_2 <- number1_2 + 2

printfn "number1_1 %d" number1_1
printfn "number1_2 %d" number1_2

//型は.NETのプリミティブ型は使える

let sampleNumbers = [ 0 .. 3 ]
for num in sampleNumbers do
  printfn "sampleNumbers %d" num

let sampleTableOfSquares = [ for i in 0 .. 3 -> (i, i*i) ]
printfn "sampleTableOfSquares %A" sampleTableOfSquares

// ========================
// bool
let boolean1 = true
let boolean2 = false
let boolean3 = not boolean1 && (boolean2 || false)
printfn "boolean3 %b" boolean3

// ========================
// string
let string1 = "Hello"
let string2  = "world"
let helloWorld = string1 + " " + string2
printfn "%s" helloWorld

//0〜6までの文字を切り抜く
let substring = helloWorld.[0..6]
printfn "%s" substring

// ========================
// タプル
let tuple1 = (1, 2, 3)
printfn "tuple1: %A" tuple1
// 逆にして返信
let swapElems (a, b) = (b, a)
printfn "swapElems %A" (swapElems (1,2))

// ========================
// リスト
let list1 = [ ]
// 同じ行
let list2 = [ 1; 2; 3 ]
let list3 = [
        1
        2
        3
    ]
let numberList = [ 1 .. 3 ]
printfn "list2 %A" list2

let squares =
  numberList
  |> List.map (fun x -> x*x)
printfn "squares %A" squares

// ========================
// パイプライン
//   |>のパイプ演算子でデータの受け渡しをする
let sampleStructTuple = struct (1, 2)
let convertFromStructTuple (struct(a, b)) = (a, b)
// let convertToStructTuple (a, b) = struct(a, b)
printfn "Struct Tuple: %A\nReference tuple made from the Struct Tuple: %A" sampleStructTuple (sampleStructTuple |> convertFromStructTuple)


// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================
// ========================






