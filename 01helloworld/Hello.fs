module fsharp_sample.Hello

open System

let hello() =
    printf "Who are you? "
    let name = Console.ReadLine()
    printfn "Oh, Hello %s!\nI'm F#." name

hello()
Console.ReadKey() |> ignore