module ProgrammingFsShakyo.Chapter3

open System
open System.IO
open System.Text.RegularExpressions

[<Literal>]
let Bill = "Bill Gates"

let ExecuteChapter3 = 

    let square x = x * x

    //宣言的な加算の書き方
    let imperaticeSum numbers = 
        //ミュータブル宣言。こういう書き方もあるのね。
        let mutable total = 0
        for i in numbers do
            let x = square i
            //代入はこう書くのか・・・
            total <- total + x
        total

    let ans = imperaticeSum [1 .. 10]

    //関数型な加算の書き方
    let functionalSum numbers = 
        numbers
        //パイプ演算だ！これは関数型プログラミングの範疇で書かれるのね。
        |> Seq.map square
        |> Seq.sum

    let ans = functionalSum [1 .. 100]

    //匿名関数
    let ret = (fun x -> x + 3) 5
    let ret = List.map (fun i -> i * i ) [1 .. 10]

    //部分関数適用
    //2つのstirngを引数に持つ関数
    let appendFile filename (text : string) = 
        use file = new StreamWriter(filename, true)
        file.WriteLine(text)
        file.Close()
    appendFile @"Log.txt" "Processing Event X..."

    //カリー化を行う。第一引数にのみ引数を割り当て
    let appendLogFile = appendFile @"Log.txt"
    appendLogFile "Processing Evenct Y..."

    //ラムダ式を使う
    List.iter (fun i -> printfn "%d" i) [1 .. 10]
    //部分関数適用で新たな式を作る
    List.iter (printfn "%d") [1 .. 10]

    //再帰関数
    //キーワードはrec。recキーワードにより関数定義が終わるまでにその関数を呼ぶことが許可される
    let rec factorial x = 
        if x <= 1 then 1
        else
            x * factorial (x - 1)

    let ans = factorial 5

    //再帰によるforループ
    let rec forLoop body times = 
        if times <= 0 then ()
        else
            body()
            forLoop body (times - 1)
    //再帰によるwhileループ
    let rec whileLoop predicate body = 
        if predicate() then
            body()
            whileLoop predicate body
        else
            ()
    forLoop (fun () -> printfn "Looping...") 3

    //相互再帰
    //andでつなぐことで、そちらで定義される関数も呼び出すことができる。
    //recの有効範囲を拡張しているようなイメージ
    let rec isOdd x = 
        if x = 0 then false
        elif x = 1 then true
        else isEven(x - 1)
    and isEven x = 
        if x = 0 then true
        elif x = 1 then false
        else isOdd(x - 1)

    let ans = isOdd 9
    let ans = isEven 100
        
    //演算子の定義
    //会場の演算子を定義する
    let (!) = factorial
    let ans = !5

    //正規表現の一致を定義する
    let (===) str (regex : string) = 
        Regex.Match(str, regex).Success

    let ans =  "The quick brown fox" === "The (.*) fox"

    //逆に定義済み演算子を関数として用いることも可能
    let ans = List.fold (+) 0 [1 .. 10]
    let ans = List.fold (*) 1 [1 .. 5]
    let minus = (-)

    let ans = minus 5 3

    //関数の組立（Function Composition）
    //フォルダのファイルサイズを取得
    //こう書くとほとんど型推論が働かず、冗長
    let sizeOfFolder folder =
        //フォルダ直下にある全てのファイルを取得 
        let filesInFolder : string [] = 
            Directory.GetFiles(
                folder, "*.*",
                SearchOption.AllDirectories)
        //直下にある全てのファイルからFileInfoクラスを作成
        let fileInfos : FileInfo[] = 
            Array.map
                (fun (file : string) -> new FileInfo(file))
                filesInFolder
        //直下にあるファイルの大きさを取得
        let fileSizes : Int64 [] = 
            Array.map
                (fun (info :  FileInfo) -> info.Length)
                fileInfos
        //全てのファイルサイズを合計する
        Array.sum fileSizes

    //とりあえず冗長なので、引数をインライン化
    //第二引数へは関数をひたすらネストさせていく
    //意味的には下から上へ登っていくのでわかりづらい！！
    let uglySizeOfFolder folder = 
        Array.sum
            (Array.map
                (fun (info : FileInfo) -> info.Length)
                (Array.map
                    (fun file -> new FileInfo(file))
                    (Directory.GetFiles(
                        folder, "*.*",
                        SearchOption.AllDirectories))))

    //そこでPipe-forward演算子を用いる
    //let (|>) x f = f x
    [1..3] |> List.iter (printfn "%d")
    //List.iter (printfn "%d" [1..3]と同値

    //Pipe-Forward演算子によって型推論が働くようになる
    //単に第二引数を先に評価するようにするから？
    let sizeOfFolderPiped folder = 
        Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
            |> Array.map (fun file -> new FileInfo(file))
            |> Array.map (fun info -> info.Length)
            |> Array.sum
    
    //Forward Composition演算子
    //前方合成演算子        
    //let (>>) f g x = g (f x)
    //部分関数適用を逆転するイメージ？
    let sizeOfFolderComposed = 
        let getFiles folder = 
            Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)

        getFiles
        >> Array.map (fun file -> new FileInfo(file))
        >> Array.map (fun info -> info.Length)
        >> Array.sum
    //適用結果：val sizeOfFolderComposed : (string -> int64)
    //関数が戻り値となっている

    //もう一例
    let square x = x * x
    let toString (x:int) = x.ToString()
    let strlen (x:string) = x.Length;
    let lenOfSquare = square >> toString >> strlen

    let ans = lenOfSquare 128

    //こう書くと自明の型推論が！
    //ただラムダ式書きまくりなので割と微妙
    let lenOfSquare =
        fun x -> x * x
        >> fun x -> x.ToString()
        >> fun x -> x.Length

    let ans = lenOfSquare 128

    //Pipe-Backward演算子
    //後方パイプ演算子
    //普通の関数適用順序で適用するだけ、、、
    List.iter (printfn "%d") [1 .. 3]
    List.iter (printfn "%d") <| [1 .. 3]
    
    //だが演算順序が微妙に変わる
    printfn "sprintf適用の結果は%sです" (sprintf "(%d,%d)" 1 3)
    printfn "sprintf適用の結果は%sです" <| sprintf "(%d,%d)" 1 3
    //↑括弧が消えてる！

    //Backward compososition parameter
    //後方合成演算子
    let square x = x * x
    let negate x = -x
    
    //前方合成すると
    let ans = (square >> negate) -10
    //10^2 * -1 = -100
    
    //明示的に逆順適用
    let ans = (square << negate) -10
    //(-1 * 10)^2 = 100
    //実はこれだよね
    let ans = square (negate -10)

    //空リストのフィルタリングにも使う
    let ans = [[1];[];[4;5;6;];[3;4;];[];[];[];[9]] |> List.filter (not << List.isEmpty)
    //こうすれば同じ順番でかけるけど、だいぶ冗長
    let ans = [[1];[];[4;5;6;];[3;4;];[];[];[];[9]] |> List.filter (fun x -> not (List.isEmpty x))

    //パターンマッチ
    let isOdd x = (x % 2 = 1)
    //単純なパターンマッチ
    let descriveNumber x = 
        match isOdd x with
        | true -> printfn "x is odd"
        | false -> printfn "x is Even"

    [1 .. 10]
    |> List.iter descriveNumber
    //and演算子
    let testAnd x y = 
        match x,y with
        |true, true -> true
    //  |true,false -> false
        |false,true -> false
        |false,false -> false
    //ワイルドカード"_"を使うとこんな感じに省略できる
    let testAnd x y = 
        match x, y with
        | true,true -> true
        | _, _      -> false

    //型マッチがうまくいかない場合、まず警告が発生
    let testAnd x y = 
        match x,y with
        |true, true -> true
    //  |true,false -> false
        |false,true -> false
        |false,false -> false
    //マッチするパターンがないと、例外発生
    //testAnd true,false

    //名前付きパターンマッチ
    let greet name = 
        match name with
        | "Robert" -> printfn "Hello, Bob"
        | "William" -> printfn "Hello, Bill"
        //変数として受け取ることができる！
        | x -> printfn "Hello, %s" x
    greet "Robert"
    greet "Hiroshi"

    //リテラル値のパターンマッチ
    let bill = "Bill Gates"
    let greet name = 
        match name with
        | bill -> "Hello Bill"
        | x    -> sprintf "Hello, %s" x
        //↑この規則には一致しない（変数billは新しいリテラルとして認識される)

    let ans = greet "Hiroshi"
    //Hello Bill
    
//    ↓なぜかLiteral属性が効かない・・・
//    Literal属性をつけるとOK
    let greet name = 
        match name with
        | Bill -> "Hello Bill"
        | x    -> sprintf "Hello, %s" x

    //whenガード節
    let highLowGame () =
        let rng = new Random()
        let secretNumber = rng.Next() % 100

        let rec highLowGameStep () =

            printfn "秘密の数字を考えてください:"
            let guessStr = Console.ReadLine()
            let guess = Int32.Parse(guessStr)

            match guess with
            | input when input > secretNumber
                -> printfn "秘密の数字はより小さいです"
                   highLowGameStep()
            | input when input = secretNumber
                -> printfn "正解！！"
                   ()
            //ワイルドカードも使える！
            | _
                -> printfn "秘密の数字はより大きいです"
                   highLowGameStep()

        highLowGameStep()

    //パターンマッチのグルーピング
    let vowelTest c = 
        match c with
        | 'a' | 'b' | 'i' | 'o' | 'u'
            -> true
        | _ -> false
    //他の記法。
    let describeNumbers x y = 
        match x, y with
        | 1, _
        | _, 1
            -> "数字のどちらかは1ですよ"
        //論理演算子が使える？
        | (2,_) & (_,2)
            -> "数字は両方とも2ですよ"
        | _ -> "それ以外ですね"
        
    //構造化データのパターンマッチ
    let testXor x y = 
       match x, y with
       //1変数で受け取るとtupleとして取れる
       | tpl when fst tpl <> snd tpl
           -> true
       | true, true -> false
       | false, false -> false
    //リスト型を受け取る例
    let rec listLength l = 
        match l with
        | []    -> 0
        | hd :: tail -> 1 + listLength tail

    //リスト型を受け取る場合のバリエーション
    let rec listLength =
    //functionキーワードは一引数しか受け取ることはできず、その中でパターンマッチを行うラムダ生成関数となる
        function
        | []    -> 0
        | hd :: tail -> 1 + listLength tail

    //Option型の場合
    let describeOption o = 
        match o with
        | Some(42)  -> "答えは42ですが、質問はなんでしょうか？"
        | Some(x)   -> sprintf "答えは%dです" x
        | None      -> "答えはありません"

    Some(42) |> describeOption |> Console.WriteLine
    Some(2) |> describeOption |> Console.WriteLine
    None |> describeOption |> Console.WriteLine

    //letバインディングは内部的にはmatch式と等価
    //たとえば以下のコードはコンパイル可能（実行時にパターンマッチ不能でエラーにはなる）
    //let 1 = 2
    //以下のように解釈される。変数で受けていないためパターンマッチが不完全になる。
    //match 2 with
    //| 1 -> ...

    //使用しない引数はワイルドカードで受けられる
    [1 .. 3] |> List.iter (fun _ -> printfn "Step...")

    //タプルの代入にもワイルドカードが使える
    let _ , second, _ = (1,2,3)
    ()

//判別共用体
//Enum的なもの
//トランプの判別共用体
type Suit =
    | Heart
    | Diamond
    | Spade
    | Club
//トランプのカード種別リスト
let suits = [Heart; Diamond; Spade; Club]

type PlayingCard = 
    | Ace of Suit
    | King of Suit
    | Queen of Suit
    | Jack of Suit
    | ValueCard of int * Suit

    //プロパティの追加
    member this.Value =
        match this with
        | Ace(_)
            -> 11 
        |King(_) | Queen(_) | Jack(_)
            -> 10
        | ValueCard(x, _)
            -> x
    //メソッドの追加
    member this.IsLarger x =
        this > x 

//トランプのデッキの定義
let deckOfCards = 
    [
        for suit in [Spade;Club;Diamond;Club] do
            yield Ace(suit)
            yield King(suit)
            yield Queen(suit)
            yield Jack(suit)
            for value in 2 .. 10 do
                yield ValueCard(value, suit)
    ]

//再帰的な判別共用体宣言
type Statement = 
    | Print of string
    | Sequence of Statement * Statement
    | IfStmt of Expression * Statement * Statement
and Expression = 
    | Integer of int
    | LessThan of Expression * Expression
    | GreaterThan of Expression * Expression

let program = 
    IfStmt(
        GreaterThan(
            Integer(3),
            Integer(1)),
        Print("3 is greater than 1"),
        Sequence(
            Print("3 is not"),
            Print("greater than 1")
        )
    )
//木構造に判別共用体を用いる
//二分木
type BinaryTree = 
    |Node of int * BinaryTree * BinaryTree
    |Empty

//二分木を左端から表示する
let rec printInOrder tree = 
    match tree with
    |Node(data,left,right)
        ->  printInOrder left
            printfn "Node %d" data
            printInOrder right
    |Empty
        -> ()
//二分木の例
let binTree = 
    Node(2,
        Node(1,Empty,Empty),
        Node(4,
            Node(3,Empty,Empty),
            Node(5,Empty,Empty)
            )
        )

printInOrder binTree

//二枚のカードの判定
let describeHoleCards cards = 
    match cards with
    | []
    | [_]
        -> failwith "Too few cards"
    | cards when List.length cards > 2
        -> failwith "Too many cards."

    | [Ace(_); Ace(_)] -> "Pocket Rockets"
    | [King(_);King(_)] -> "Cowboys"
    | [ValueCard(2, _); ValueCard(2,_)] -> "Docks"

    |[Queen(_);Queen(_)]
    |[Jack(_);Jack(_)]
        -> "Pair of Face Cards"

    | [ValueCard(x,_);ValueCard(y,_)] when x = y
        -> "A Pair"
    | [first;second]
        -> sprintf "Two cards: %A and %A" first second

//組織図の表示
type Employee = 
    | Manager of string * Employee list
    | Worker of string

let rec printOrganization worker = 
    match worker with
    //一人だけの場合
    | Worker(name) -> printfn "作業者 %s" name
    | Manager(name, [Worker(nameWorker)])
        ->  printfn "マネージャー%sには一人の部下%sがいる"  name nameWorker
    | Manager(name, [Worker(nameWorker1);Worker(nameWorker2)])
        ->  printfn "マネージャー%sには二人の部下%sと%sがいる" name nameWorker1 nameWorker2
    | Manager(name, workerList)
        ->  printfn "マネージャー%sには次に示す%d人の部下がいる" name workerList.Length
            workerList |> List.iter (
                fun(worker) -> 
                     printf "\t"
                     printOrganization worker
                     )

let tom = Manager ("Tom", 
                            [
                                Manager("Bob", [Worker("John"); Worker("Smith");Worker("Daniel")]);
                                Worker("George");
                                Worker("Jim");
                                Worker("Fred")
                            ]
                            )
printOrganization tom

let getCardValue card =
    match card with
    | King(_)
    | Queen(_)
    | Jack(_)
        -> 10
    | ValueCard(number,_)
        -> number
//↑warning FS0025: この式のパターン マッチが不完全です たとえば、値 'Ace (_)' はパターンに含まれないケースを示す可能性があります。
//ここで安易にワイルドカード(_)を使ってしまうと警告が出なくなってしまう。

//レコード
//データをグルーピングするための型。どことなく構造体っぽい？
//Firstネーム、Secondネーム、年齢を持つレコードPersonの定義
type Person = {First:string; Second:string; Age:int}

let Taro:Person = {First = "Taro"; Second = "Yamada"; Age = 20}
printfn "%s %s %d才" Taro.Second Taro.First Taro.Age

//withキーワードを使って一部を引き継ぎ別のものをクローンできる
let Hanako = {Taro with First = "Hanako"; Age=18}
printfn "%s %s %d才" Hanako.Second Hanako.First Hanako.Age

let Jiro = {Taro with First = "Jiro"; Age=15}
let Tohru = {Taro with First = "Tohru"; Age = 40}
let Keiko = {Taro with First = "Keiko";Age = 38}
let YamadaFamily = [Taro;Jiro;Hanako;Tohru;Keiko]

//パターンマッチ（中括弧に入れて使う）
let age20s = YamadaFamily
                 |> List.filter
                    (function
                    | {Age = 20} ->true
                    | _ ->false)
//whenガードはこれでいける
let pverage20s = YamadaFamily
                 |> List.filter
                    (function
                    | {Age = x} when x > 20 ->true
                    | _ ->false)
//型推論(使用方法からの型推論を行ってくれる)
let isFamily psn1 psn2 =
    psn1.Second = psn2.Second
//こっちはテキストの例
type Point = {X:double;Y:double}
let calcDistance pt1 pt2 =
    let square x = x * x
    sqrt <| square(pt1.X - pt2.X) + square(pt1.Y - pt2.Y)

calcDistance {X = 0.0; Y=0.0} {X=10.0;Y=10.0}
//型が持つ変数名かぶってしまうと厄介なことに（方推論がうまくいかなくなる）
type Vector = {X:double;Y:double;Z:double}
//これだと引数がVectorとみなされるので・・・
let calcDistance2 pt1 pt2 =
    let square x = x * x
    sqrt <| square(pt1.X - pt2.X) + square(pt1.Y - pt2.Y)
//引数の型を指定する
let calcDistance3 (pt1:Point) (pt2:Point) =
    let square x = x * x
    sqrt <| square(pt1.X - pt2.X) + square(pt1.Y - pt2.Y)
//宣言時も型指定をするか
let point:Point = {X = 0.0; Y=0.0}
//宣言時の要素で型を指定する
let point2 = {Point.X = 0.0; Point.Y=0.0}

//メンバ関数やプロパティを持つこともできる
type Vector2 = 
    {X:double;Y:double;Z:double}
    member this.Length = 
        sqrt <| this.X ** 2.0 + this.Y**2.0 + this.Z**2.0
    member this.sum (vec2:Vector2) =
        {X = this.X + vec2.X; Y=this.Y + vec2.Y;Z=this.Z + vec2.Z}

//Lazy
//使用されるまで実際には生成されないクラス。一度生成されるとそれを使いまわす。
//生成したい値を返す関数を引数として渡す
let x = Lazy<int>.Create(fun () -> 
    printfn "Evaluating x..."
    10)
//lazyキーワードだと方推論してくれて楽ちん？
let y = lazy (
    printfn "Evaluating y..."
    x.Value + x.Value)
//ここでx・yが生成される
let z = y.Value;
//二度目以降は使いまわされるので生成時に実行される関数はもう実行されない
z = y.Value;

//Sequence（Seq）
//遅延評価されるリスト。C#的にはIEnumerableですよね。
let numSeq = seq{1..5}
numSeq |> Seq.iter (printfn "%d")

//Seqなら問題なく定義できるが・・・
let allPositiveInteger = 
    seq{for i in 1 .. System.Int32.MaxValue -> i}
//ListだとOutofMemoryで落ちてしまう。
let allPositiveIntegerList = 
    [for i in 1 .. System.Int32.MaxValue -> i]

//リストの列挙記法と同じ記法が使える
let allAlphabet = seq{'a' .. 'z'}
allAlphabet |> Seq.take 4

//列挙のたびに出力を出すようにしたもの
let noisyAlphabet = 
    seq{ for i in 'a' .. 'z' do
            printfn "%c" i
            yield i}
//これは何度実行しても同じように出力が出る
noisyAlphabet |> Seq.take 5

open System.IO
//シークエンスを結合して返す構文がある。
//yield!（yield Bang!）を使う
let rec allFileUnder basepath = 
    seq{
        //まず今のフォルダ上のファイルを列挙
        yield! Directory.GetFiles(basepath)
        //次に今のフォルダの下のディレクトリ上のファイルを列挙していく
        for subdir in Directory.GetDirectories(basepath) do
            yield! allFileUnder subdir
        }

let allFiles = allFileUnder @"K:\"
allFiles |> Seq.iter (printfn "%s")

//seq.unfold
//関数（漸化式）からシーケンスを生成する（foldの逆みたいな感じですね、確かに）
let nextFibUnder100 (past,current) = 
    if((past + current) > 100) then
        None
    else
        let next = past + current
        printfn "next:%d, state:%d" next current
        Some(next, (current,next))
         
let fib100 = Seq.unfold nextFibUnder100 (0,1)
fib100 |> Seq.iter (printfn "%d" )

//Query:要はLinqのクエリ式相当？いや、それ以上
//普通に描くとこんな感じ
let allFamilyOver age = 
    YamadaFamily
    |> Seq.filter (fun mem -> mem.Age > age)
    |> Seq.map (fun mem -> mem.Second + " " + mem.First)
    |> Seq.distinct

allFamilyOver 10

//F#クエリ式だと・・・
let q_allFamilyOver age = 
    query{
        for mem in YamadaFamily do
        where (mem.Age > age)
        select (mem.Second + " " + mem.First)
        distinct
    }

q_allFamilyOver 18

//Processでクエリ式を遊ぶ
open System.Diagnostics

let activeProcCount = 
    query{
        for proc in Process.GetProcesses() do
        count
    }

let heaviestProc = 
    query{
        for proc in Process.GetProcesses() do
        sortByDescending proc.WorkingSet64
        head
    }
//select, where
let windowedProc = 
    query{
        for proc in Process.GetProcesses() do
        where (proc.MainWindowHandle <> nativeint 0)
        select proc.ProcessName
    }
windowedProc |> Seq.iter (fun(proc) -> printfn "%s" proc)

//contains
let isChromeRunning = 
    query{
        for proc in Process.GetProcesses() do
        select proc.ProcessName
        contains ("chrome")
    }
//count
let activeProcCount2 = 
    query{
        for proc in Process.GetProcesses() do
        where (proc.MainWindowHandle <> nativeint 0)
        count
    }
//distinct
let processNames = 
    query{
        for proc in Process.GetProcesses() do
        select proc.ProcessName
    }
processNames |> Seq.iter (printfn "%s")
let processNamesDistinct = 
    query{
        for proc in Process.GetProcesses() do
        select proc.ProcessName
        distinct
    }
processNamesDistinct |> Seq.iter (printfn "%s")

//maxBy
let mostHeavyProcessMem =
    query{
        for proc in Process.GetProcesses() do
        maxBy proc.WorkingSet64
    }

//sort系演算子
let sortedProc = 
    query{
        for proc in Process.GetProcesses() do
        let isWindowed = proc.MainWindowHandle <> nativeint 0
        sortBy isWindowed
        thenBy proc.ProcessName
        select proc.ProcessName
    }
sortedProc |> Seq.iter (printfn "%s")