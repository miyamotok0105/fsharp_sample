
# Env

```
mac os

Mono JIT compiler version 5.18.1.3
Microsoft (R) F# Compiler version 4.1
```

# Setup command


```
brew install mono
dotnet new console --language "F#"
dotnet run

# single file compile
fsharpc hello.fs -o hello
mono hello

fsharpc Tour1.fs -o Tour1 && mono Tour1

cd ProgrammingFsShakyo
dotnet run 1 2

参考になった
https://github.com/ExtinctionHD/FSharpProgram/blob/master/FSharpProgram/Program.fs

https://github.com/suzusime/fslexyacc-test
https://github.com/posaunehm/ProgrammingFsShakyo

```


# Feature of F#

```
https://qiita.com/cannorin/items/59d79cc9a3b64c761cd4

C# の速度・クロスプラットフォーム性・ライブラリの多さ・開発環境
Rust のツールチェイン
Go のデプロイしやすさ
Python のオフサイドルール
Haskell のモナド文化
静的ダックタイピング(事実上のトレイト/型クラス)
コンパイル時型生成(type providers)
```


# Reference

```
F のツアー#
https://docs.microsoft.com/ja-jp/dotnet/fsharp/tour
F# での関数型プログラミングの概要
https://docs.microsoft.com/ja-jp/dotnet/fsharp/introduction-to-functional-programming/
ファーストクラス関数
https://docs.microsoft.com/ja-jp/dotnet/fsharp/introduction-to-functional-programming/first-class-functions
F# を知ってほしい
https://qiita.com/cannorin/items/59d79cc9a3b64c761cd4
```

