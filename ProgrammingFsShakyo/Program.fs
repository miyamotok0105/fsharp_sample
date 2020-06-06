module ProgrammingFsShakyo.Main

open System
// open Chapter2
open Chapter3


//メイン関数
//プロジェクトの最後の一つである必要がある
[<EntryPoint>]
let main(args : string[]) = 
    //引数が2つでなければ
    if args.Length <> 2 then
        //例外を吐いて落ちる
        //インデントでifのスコープが決定される
        failwith "Error: Expected arguments <greeting> and <thing>"
    //コンマ区切りでの初期化。F#独特。
    let greeting, thing = args.[0], args.[1]
    //.NETクラスの呼び出し
    let timeOfDay = DateTime.Now.ToString("hh:mm tt")
    
    //コメントの別の書き方を試す
    (*
    コンソールへ出力
    Printf.TextWriterFormatのエイリアスっぽい
    *)
    printfn "%s, %s at %s" greeting thing timeOfDay

    //ExecuteChapter2
    ExecuteChapter3

    //ExitCodeが必要
    0