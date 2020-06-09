// Learn more about F# at http://fsharp.org
module fsharp_sample.Main

// C#でいう所のusing
open System
open Hello
open Library1
open Hello2
open Tour1

// 1にadd2で2を加算
let num = add2 1

let baz = Baz.world

let tour1DispResult = dispResult

// F#ではletで関数定義する。
[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    hello()
    printfn "%d" (num)
    baz
    printfn "Tour1 ====>"
    tour1DispResult
    0 // return an integer exit code


